namespace Navigator.Extensions.Store.Entities;

/// <summary>
/// Conversation.
/// </summary>
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
    public Conversation(User user, Chat chat)
    {
        UserId = user.Id;
        ChatId = chat.Id;
        FirstInteractionAt = DateTime.UtcNow;
    }
    
    /// <summary>
    /// Id of the user.
    /// </summary>
    public long UserId { get; set; }
    
    /// <summary>
    /// User.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Id of the chat.
    /// </summary>
    public long ChatId { get; set; }
    
    /// <summary>
    /// Chat.
    /// </summary>
    public Chat Chat { get; set; } = null!;

    /// <summary>
    /// Date of first interaction for this chat.
    /// </summary>
    public DateTime FirstInteractionAt { get; set; }
}