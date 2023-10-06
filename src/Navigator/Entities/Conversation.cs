using System.Security.Cryptography;
using System.Text;

namespace Navigator.Entities;

/// <summary>
/// Represents an interaction between a user and chat.
/// </summary>
public abstract class Conversation
{
    protected Conversation()
    {
    }
    
    protected Conversation(User user, Chat? chat)
    {
        Id = new Guid(SHA256.HashData(Encoding.UTF8.GetBytes($"{user.Id}+{chat?.Id}")).Take(16).ToArray());

        User = user;
        Chat = chat;
    }

    public Guid Id { get; set; }

    /// <summary>
    /// User
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Chat
    /// </summary>
    public Chat? Chat { get; set; }
}