using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Navigator.Entities;

/// <summary>
/// Represents a user.
/// </summary>
public abstract class User
{
    protected User()
    {
    }
    
    protected User(string input)
    {
        Id = new Guid(SHA256.HashData(Encoding.UTF8.GetBytes(input)).Take(16).ToArray());
    }
        
    /// <summary>
    /// Id of the user.
    /// <remarks>
    ///     Generally a deterministic Guid based on some kind of input.
    /// </remarks>
    /// </summary>
    public Guid Id { get; init; }
}