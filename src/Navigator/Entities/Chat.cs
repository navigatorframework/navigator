namespace Navigator.Entities;

/// <summary>
/// Represents a chat.
/// </summary>
public abstract class Chat
{
    /// <summary>
    /// Telegram identifier for the chat.
    /// </summary>
    public long Id { get; init; }

    /// <summary>
    /// Title of the chat, if any.
    /// </summary>
    public string? Title { get; init; }
    
    /// <summary>
    /// Type of the chat, can be any of <see cref="ChatType"/>.
    /// </summary>
    public ChatType Type { get; init; }
    
    /// <summary>
    /// If the supergroup chat is a forum (has topics enabled).
    /// </summary>
    public bool? IsForum { get; set; }

}

public enum ChatType
{
    Private,
    Group,
    Channel, 
    Supergroup, 
    Sender
}