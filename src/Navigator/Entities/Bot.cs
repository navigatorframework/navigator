using System.Security.Cryptography;
using System.Text;

namespace Navigator.Entities;

public abstract class Bot
{
    protected Bot(string input)
    {
        Id = new Guid(SHA256.HashData(Encoding.UTF8.GetBytes(input)).Take(16).ToArray());
    }
    
    /// <summary>
    /// Id of the bot.
    /// <remarks>
    ///     Generally a deterministic Guid based on some kind of input.
    /// </remarks>
    /// </summary>
    public Guid Id { get; init; }
}