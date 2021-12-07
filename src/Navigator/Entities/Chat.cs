using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Navigator.Entities;

/// <summary>
/// Represents a chat.
/// </summary>
public abstract record Chat
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
    Guid Id { get; init; }
    
    /// <summary>
    /// Title of the chat, if any.
    /// <remarks>
    ///     Optional.
    /// </remarks>
    /// </summary>
    string? Title { get; init; }
}