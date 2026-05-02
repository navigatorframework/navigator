using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Navigator.Abstractions.Telegram;
using Navigator.Extensions.AI.Models;
using Navigator.Extensions.AI.Persistence;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Extensions.AI.Services;

public class ChatContextStore : IChatContextStore
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);
    private const string ChatContextCacheKeyPrefix = "navigator:ai:chat-context:";

    private readonly IDistributedCache _cache;
    private readonly AIOptions _options;
    private readonly IChatContextMediaDownloader _mediaDownloader;

    public ChatContextStore(IDistributedCache cache, AIOptions options, IChatContextMediaDownloader mediaDownloader)
    {
        _cache = cache;
        _options = options;
        _mediaDownloader = mediaDownloader;
    }

    public async Task AppendIncomingMessageAsync(Message message, CancellationToken cancellationToken = default)
    {
        var chatId = message.Chat?.Id;

        if (chatId is null)
        {
            return;
        }

        var contextMessage = await BuildChatContextMessageAsync(message, cancellationToken);

        if (contextMessage is null)
        {
            return;
        }

        var context = await GetAsync(chatId.Value, cancellationToken);
        context.Add(contextMessage);

        var cachedContext = new CachedChatContext
        {
            MaxLength = context.MaxLength,
            Messages = context.Select(Map).ToList()
        };

        var payload = JsonSerializer.SerializeToUtf8Bytes(cachedContext, SerializerOptions);
        await _cache.SetAsync(GetCacheKey(chatId.Value), payload, cancellationToken);
    }

    public async Task<ChatContext> GetForUpdateAsync(Update update, CancellationToken cancellationToken = default)
    {
        var chatId = update.GetChatOrDefault()?.Id;
        return chatId is null
            ? CreateEmptyContext()
            : await GetAsync(chatId.Value, cancellationToken);
    }

    public ChatContext CreateEmptyContext() => new(_options.ChatContextLength);

    private async Task<ChatContext> GetAsync(long chatId, CancellationToken cancellationToken)
    {
        var payload = await _cache.GetAsync(GetCacheKey(chatId), cancellationToken);

        if (payload is null || payload.Length == 0)
        {
            return CreateEmptyContext();
        }

        var cachedContext = JsonSerializer.Deserialize<CachedChatContext>(payload, SerializerOptions);

        if (cachedContext is null)
        {
            return CreateEmptyContext();
        }

        return new ChatContext(_options.ChatContextLength, cachedContext.Messages.Select(Map));
    }

    private async Task<ChatContextMessage?> BuildChatContextMessageAsync(Message message, CancellationToken cancellationToken)
    {
        return message.Type switch
        {
            MessageType.Text when !string.IsNullOrWhiteSpace(message.Text) => BuildTextMessage(message),
            MessageType.Photo when message.Photo is { Length: > 0 } => await BuildPhotoMessageAsync(message, cancellationToken),
            MessageType.Audio when message.Audio is not null => await BuildAudioMessageAsync(message, message.Audio, cancellationToken),
            MessageType.Voice when message.Voice is not null => await BuildVoiceMessageAsync(message, message.Voice, cancellationToken),
            _ => null
        };
    }

    private ChatContextMessage BuildTextMessage(Message message)
    {
        return CreateBaseMessage(message,
        [
            new ChatContextItem
            {
                Type = ChatContextItemTypes.Text,
                Text = message.Text
            }
        ]);
    }

    private async Task<ChatContextMessage> BuildPhotoMessageAsync(Message message, CancellationToken cancellationToken)
    {
        var photo = message.Photo!
            .OrderByDescending(item => item.FileSize ?? 0)
            .ThenByDescending(item => item.Width * item.Height)
            .First();

        var items = new List<ChatContextItem>
        {
            new()
            {
                Type = ChatContextItemTypes.Image,
                Data = await _mediaDownloader.DownloadAsync(photo.FileId, cancellationToken),
                MimeType = "image/jpeg",
                Metadata = new Dictionary<string, string?>
                {
                    ["file_id"] = photo.FileId,
                    ["file_unique_id"] = photo.FileUniqueId,
                    ["file_size"] = ToMetadataValue(photo.FileSize),
                    ["width"] = ToMetadataValue(photo.Width),
                    ["height"] = ToMetadataValue(photo.Height)
                }
            }
        };

        AppendCaption(message, items);

        return CreateBaseMessage(message, items);
    }

    private async Task<ChatContextMessage> BuildAudioMessageAsync(Message message, Audio audio, CancellationToken cancellationToken)
    {
        var items = new List<ChatContextItem>
        {
            new()
            {
                Type = ChatContextItemTypes.Audio,
                Data = await _mediaDownloader.DownloadAsync(audio.FileId, cancellationToken),
                MimeType = audio.MimeType ?? "audio/mpeg",
                Metadata = new Dictionary<string, string?>
                {
                    ["file_id"] = audio.FileId,
                    ["file_unique_id"] = audio.FileUniqueId,
                    ["file_size"] = ToMetadataValue(audio.FileSize),
                    ["mime_type"] = audio.MimeType,
                    ["duration"] = ToMetadataValue(audio.Duration),
                    ["file_name"] = audio.FileName,
                    ["performer"] = audio.Performer,
                    ["title"] = audio.Title
                }
            }
        };

        AppendCaption(message, items);

        return CreateBaseMessage(message, items);
    }

    private async Task<ChatContextMessage> BuildVoiceMessageAsync(Message message, Voice voice, CancellationToken cancellationToken)
    {
        var items = new List<ChatContextItem>
        {
            new()
            {
                Type = ChatContextItemTypes.Audio,
                Data = await _mediaDownloader.DownloadAsync(voice.FileId, cancellationToken),
                MimeType = voice.MimeType ?? "audio/ogg",
                Metadata = new Dictionary<string, string?>
                {
                    ["file_id"] = voice.FileId,
                    ["file_unique_id"] = voice.FileUniqueId,
                    ["file_size"] = ToMetadataValue(voice.FileSize),
                    ["mime_type"] = voice.MimeType,
                    ["duration"] = ToMetadataValue(voice.Duration)
                }
            }
        };

        AppendCaption(message, items);

        return CreateBaseMessage(message, items);
    }

    private ChatContextMessage CreateBaseMessage(Message message, List<ChatContextItem> items)
    {
        return new ChatContextMessage
        {
            Role = ChatContextRoles.User,
            AuthorName = GetAuthorName(message),
            CreatedAt = message.Date,
            Metadata = new Dictionary<string, string?>
            {
                ["message_id"] = ToMetadataValue(message.MessageId),
                ["message_type"] = message.Type.ToString()
            },
            Items = items
        };
    }

    private static string GetCacheKey(long chatId) => $"{ChatContextCacheKeyPrefix}{chatId}";

    private static string? GetAuthorName(Message message)
    {
        if (message.From is { } user)
        {
            var fullName = string.Join(" ", new[] { user.FirstName, user.LastName }.Where(value => !string.IsNullOrWhiteSpace(value)));
            return !string.IsNullOrWhiteSpace(fullName)
                ? fullName
                : user.Username;
        }

        return message.SenderChat?.Title;
    }

    private static void AppendCaption(Message message, ICollection<ChatContextItem> items)
    {
        if (string.IsNullOrWhiteSpace(message.Caption))
        {
            return;
        }

        items.Add(new ChatContextItem
        {
            Type = ChatContextItemTypes.Text,
            Text = message.Caption
        });
    }

    private static string? ToMetadataValue(object? value)
    {
        return value?.ToString();
    }

    private static CachedChatContextMessage Map(ChatContextMessage message)
    {
        return new CachedChatContextMessage
        {
            Role = message.Role,
            AuthorName = message.AuthorName,
            CreatedAt = message.CreatedAt,
            Metadata = new Dictionary<string, string?>(message.Metadata),
            Items = message.Items.Select(Map).ToList()
        };
    }

    private static CachedChatContextItem Map(ChatContextItem item)
    {
        return new CachedChatContextItem
        {
            Type = item.Type,
            Text = item.Text,
            Data = item.Data,
            MimeType = item.MimeType,
            Metadata = new Dictionary<string, string?>(item.Metadata)
        };
    }

    private static ChatContextMessage Map(CachedChatContextMessage message)
    {
        return new ChatContextMessage
        {
            Role = message.Role,
            AuthorName = message.AuthorName,
            CreatedAt = message.CreatedAt,
            Metadata = new Dictionary<string, string?>(message.Metadata),
            Items = message.Items.Select(Map).ToList()
        };
    }

    private static ChatContextItem Map(CachedChatContextItem item)
    {
        return new ChatContextItem
        {
            Type = item.Type,
            Text = item.Text,
            Data = item.Data,
            MimeType = item.MimeType,
            Metadata = new Dictionary<string, string?>(item.Metadata)
        };
    }
}
