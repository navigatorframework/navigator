using Navigator.Extensions.AI.Models;

namespace Navigator.Extensions.AI.Persistence;

internal sealed class CachedChatContextItem
{
    public string Type { get; init; } = ChatContextItemTypes.Text;
    public string? Text { get; init; }
    public byte[]? Data { get; init; }
    public string? MimeType { get; init; }
    public Dictionary<string, string?> Metadata { get; init; } = [];
}
