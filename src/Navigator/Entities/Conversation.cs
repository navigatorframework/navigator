namespace Navigator.Entities;

/// <summary>
/// Represents an interaction between a user and chat.
/// </summary>
public abstract record Conversation
{
    /// <summary>
    /// User
    /// </summary>
    public User User { get; init; }

    /// <summary>
    /// Chat
    /// </summary>
    public Chat Chat { get; init; }
}