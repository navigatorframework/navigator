namespace Navigator.Entities;

/// <summary>
/// Represents a chat.
/// </summary>
public record Chat(long Id, Chat.ChatType Type)
{
    /// <summary>
    /// Telegram identifier for the chat.
    /// </summary>
    public long Id { get; init; } = Id;

    /// <summary>
    /// Title of the chat.
    /// <remarks>
    ///     Optional.
    /// </remarks>
    /// </summary>
    public string? Title { get; init; }
    
    /// <summary>
    /// Type of the chat, can be any of <see cref="ChatType"/>.
    /// </summary>
    public ChatType Type { get; init; } = Type;

    /// <summary>
    /// If the supergroup chat is a forum (has topics enabled).
    /// <remarks>
    ///     Optional.
    /// </remarks>
    /// </summary>
    public bool? IsForum { get; init; }
    
    public enum ChatType
    {
        Private = 1,
        Group = 2,
        Channel = 3, 
        Supergroup = 4, 
        Sender = 5
    }
}
