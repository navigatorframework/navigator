namespace Navigator.Entities;

/// <summary>
/// Represents an interaction between a user and chat.
/// </summary>
public abstract record Conversation(User User, Chat Chat)
{
    /// <summary>
    /// User
    /// </summary>
    public User User { get; init; } = User;

    /// <summary>
    /// Chat
    /// </summary>
    public Chat Chat { get; init; } = Chat;
}