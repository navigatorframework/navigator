#pragma warning disable SKEXP0001

using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Navigator.Extensions.AI.Models;

namespace Navigator.Extensions.AI.Services;

public class SemanticKernelChatContextParser : ISemanticKernelChatContextParser
{
    private const string ImageFallbackText = "User sent an image.";
    private const string AudioFallbackText = "User sent an audio message.";

    private readonly AIOptions _options;

    public SemanticKernelChatContextParser(AIOptions options)
    {
        _options = options;
    }

    public ChatHistory Parse(ChatContext context)
    {
        var messages = context.Select(ParseMessage).ToList();
        return new ChatHistory(messages);
    }

    private ChatMessageContent ParseMessage(ChatContextMessage message)
    {
        var items = new ChatMessageContentItemCollection();

        foreach (var item in message.Items)
        {
            switch (item.Type)
            {
                case ChatContextItemTypes.Text when item.Text is not null:
                    items.Add(new TextContent(item.Text));
                    break;
                case ChatContextItemTypes.Image:
                    items.Add(ParseImage(item));
                    break;
                case ChatContextItemTypes.Audio:
                    items.Add(ParseAudio(item));
                    break;
            }
        }

        return new ChatMessageContent(MapRole(message.Role), items, metadata: ToObjectMetadata(message.Metadata))
        {
            AuthorName = message.AuthorName
        };
    }

    private KernelContent ParseImage(ChatContextItem item)
    {
        if (_options.ChatCompletionProvider.IsMultimodal && item.Data is not null && item.MimeType is not null)
        {
            return new ImageContent(item.Data, item.MimeType);
        }

        return new TextContent(ImageFallbackText);
    }

    private KernelContent ParseAudio(ChatContextItem item)
    {
        if (_options.ChatCompletionProvider.IsMultimodal && item.Data is not null && item.MimeType is not null)
        {
            return new AudioContent(item.Data, item.MimeType);
        }

        return new TextContent(AudioFallbackText);
    }

    private static AuthorRole MapRole(string role)
    {
        return role switch
        {
            ChatContextRoles.User => AuthorRole.User,
            ChatContextRoles.Assistant => AuthorRole.Assistant,
            ChatContextRoles.System => AuthorRole.System,
            ChatContextRoles.Developer => AuthorRole.Developer,
            ChatContextRoles.Tool => AuthorRole.Tool,
            _ => new AuthorRole(role)
        };
    }

    private static IReadOnlyDictionary<string, object?> ToObjectMetadata(IReadOnlyDictionary<string, string?> metadata)
    {
        return metadata.ToDictionary(entry => entry.Key, entry => (object?)entry.Value);
    }
}
