namespace Navigator.Abstractions.Entities;

/// <summary>
/// Represents an interaction between a user and chat.
/// </summary>
public record Conversation(User User)
{
    /// <summary>
    /// User.
    /// </summary>
    public User User { get; init; } = User;

    /// <summary>
    /// Chat.
    /// </summary>
    public Chat? Chat { get; init; }
}