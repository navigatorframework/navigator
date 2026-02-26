namespace Navigator.Extensions.Store.Entities;

/// <summary>
///     Represents a conversation between a <see cref="User"/> and optionally within a <see cref="Chat"/>.
/// </summary>
public class Conversation
{
    /// <summary>
    ///     EF Core constructor.
    /// </summary>
    protected Conversation() { }
    
    /// <summary>
    ///     Creates a new <see cref="Conversation"/> for the specified user.
    /// </summary>
    /// <param name="user">The user participating in this conversation.</param>
    public Conversation(User user)
    {
        User = user;
    }
    
    /// <summary>
    ///     Internal unique identifier.
    /// </summary>
    public Guid Id { get; init; } = Guid.CreateVersion7();

    /// <summary>
    ///     The user participating in this conversation.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    ///     The chat this conversation belongs to, if any.
    /// </summary>
    public Chat? Chat { get; set; }
    
    /// <summary>
    ///     Timestamp of the first recorded activity for this conversation.
    /// </summary>
    public DateTimeOffset FirstActiveAt { get; set; } = TimeProvider.System.GetUtcNow();
}
