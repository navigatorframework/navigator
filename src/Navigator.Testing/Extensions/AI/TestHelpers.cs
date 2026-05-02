using Microsoft.Extensions.Caching.Distributed;
using Navigator.Abstractions.Actions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Testing.Extensions.AI;

internal static class TestHelpers
{
    public static Navigator.Extensions.AI.AIOptions CreateOptions(bool isMultimodal = false, int chatContextLength = 15)
    {
        return new Navigator.Extensions.AI.AIOptions
        {
            ChatContextLength = chatContextLength,
            ChatCompletionProvider = new Navigator.Extensions.AI.AIOptions.AiProvider
            {
                IsMultimodal = isMultimodal
            }
        };
    }

    public static BotAction CreateAction()
    {
        return new BotAction(
            Guid.NewGuid(),
            new BotActionInformation
            {
                Category = UpdateCategory.Unknown,
                Priority = Navigator.Abstractions.Priorities.EPriority.Normal,
                ExclusivityLevel = EExclusivityLevel.None,
                ChatAction = null,
                ConditionInputTypes = [],
                HandlerInputTypes = [],
                Name = "test-action",
                Options = []
            },
            () => true,
            () => Task.CompletedTask);
    }

    public static Update CreateTextUpdate(
        long chatId = 123,
        int messageId = 1,
        string text = "hello",
        string firstName = "Lucas",
        string? lastName = "Lopez")
    {
        return new Update
        {
            Message = new Message
            {
                Id = messageId,
                Text = text,
                Date = DateTime.UtcNow,
                Chat = CreateChat(chatId),
                From = CreateUser(firstName, lastName)
            }
        };
    }

    public static Message CreatePhotoMessage(long chatId = 123, int messageId = 2, string? caption = "photo caption")
    {
        return new Message
        {
            Id = messageId,
            Date = DateTime.UtcNow,
            Chat = CreateChat(chatId),
            From = CreateUser("Lucas", "Lopez"),
            Caption = caption,
            Photo =
            [
                new PhotoSize
                {
                    FileId = "photo-small",
                    FileUniqueId = "photo-small-unique",
                    Width = 64,
                    Height = 64,
                    FileSize = 16
                },
                new PhotoSize
                {
                    FileId = "photo-large",
                    FileUniqueId = "photo-large-unique",
                    Width = 256,
                    Height = 256,
                    FileSize = 256
                }
            ]
        };
    }

    public static Chat CreateChat(long chatId)
    {
        return new Chat
        {
            Id = chatId,
            Type = ChatType.Private
        };
    }

    public static User CreateUser(string firstName, string? lastName)
    {
        return new User
        {
            Id = 99,
            FirstName = firstName,
            LastName = lastName
        };
    }
}

internal sealed class InMemoryDistributedCache : IDistributedCache
{
    private readonly Dictionary<string, byte[]> _entries = [];

    public byte[]? Get(string key) => _entries.TryGetValue(key, out var value) ? value : null;

    public Task<byte[]?> GetAsync(string key, CancellationToken token = default) => Task.FromResult(Get(key));

    public void Set(string key, byte[] value, DistributedCacheEntryOptions options) => _entries[key] = value;

    public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
    {
        Set(key, value, options);
        return Task.CompletedTask;
    }

    public void Refresh(string key)
    {
    }

    public Task RefreshAsync(string key, CancellationToken token = default) => Task.CompletedTask;

    public void Remove(string key) => _entries.Remove(key);

    public Task RemoveAsync(string key, CancellationToken token = default)
    {
        Remove(key);
        return Task.CompletedTask;
    }
}
