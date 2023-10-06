using System.Security.Cryptography;
using System.Text;

namespace Navigator.Entities;

/// <summary>
/// Represents an interaction between a user and chat.
/// </summary>
public class Conversation
{
    public Conversation(User user, Chat? chat = default)
    {
        Id = Guid.NewGuid();
        User = user;
        Chat = chat;
    }
    
    public Guid Id { get; } 
    public User User { get; }
    public Chat? Chat { get; }
}