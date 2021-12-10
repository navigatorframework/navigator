using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Navigator.Entities;

/// <summary>
/// Represents a chat.
/// </summary>
public abstract class Chat
{
    protected Chat(string input)
    {
        Id = new Guid(SHA256.HashData(Encoding.UTF8.GetBytes(input)).Take(16).ToArray());
    }
    
    /// <summary>
    /// Id of the chat.
    /// <remarks>
    ///     Generally a deterministic Guid based on some kind of input.
    /// </remarks>
    /// </summary>
    public Guid Id { get; init; }
}