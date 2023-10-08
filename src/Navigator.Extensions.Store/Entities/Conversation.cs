namespace Navigator.Extensions.Store.Entities;

public class Conversation
{
    /// <summary>
    /// Internal constructor.
    /// </summary>
    protected Conversation()
    {
    }
    
    /// <summary>
    /// Conversation constructor.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="chat"></param>
    public Conversation(User user, Chat? chat)
    {
        FirstInteractionAt = DateTime.UtcNow;
    }

    /// <summary>
    /// User.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Chat.
    /// </summary>
    public Chat? Chat { get; set; }

    /// <summary>
    /// Date of first interaction for this chat.
    /// </summary>
    public DateTime FirstInteractionAt { get; set; }
}