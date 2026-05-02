using Navigator.Extensions.AI.Models;

namespace Navigator.Extensions.AI.Persistence;

internal sealed class CachedChatContextMessage
{
    public string Role { get; init; } = ChatContextRoles.User;
    public string? AuthorName { get; init; }
    public DateTime? CreatedAt { get; init; }
    public Dictionary<string, string?> Metadata { get; init; } = [];
    public List<CachedChatContextItem> Items { get; init; } = [];
}
