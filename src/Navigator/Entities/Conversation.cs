namespace Navigator.Entities;

/// <summary>
/// Represents an interaction between a user and chat.
/// </summary>
public abstract record Conversation
{
    User User { get; init; }
    
    Chat Chat { get; init; }
}