namespace Navigator.Entities;

/// <summary>
/// Represents an interaction between a user and chat.
/// </summary>
public abstract class Conversation
{
    protected Conversation()
    {
    }
    
    protected Conversation(User user, Chat chat)
    {
        User = user;
        Chat = chat;
    }

    /// <summary>
    /// User
    /// </summary>
    public User User { get; init; }

    /// <summary>
    /// Chat
    /// </summary>
    public Chat Chat { get; init; }
}