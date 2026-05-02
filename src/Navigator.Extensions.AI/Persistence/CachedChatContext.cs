namespace Navigator.Extensions.AI.Persistence;

internal sealed class CachedChatContext
{
    public int MaxLength { get; init; }
    public List<CachedChatContextMessage> Messages { get; init; } = [];
}
