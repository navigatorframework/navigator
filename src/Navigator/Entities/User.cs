using System.Security.Cryptography;
using System.Text;

namespace Navigator.Entities;

/// <summary>
/// Represents a user.
/// </summary>
public class User
{
    /// <summary>
    /// Telegram identifier for the user.
    /// </summary>
    public long Id { get; init; }

    /// <summary>
    /// Username of the user, if any.
    /// <remarks>
    ///     Optional.
    /// </remarks>
    /// </summary>
    public string? Username { get; init; }

    /// <summary>
    /// First name of the user.
    /// </summary>
    public string FirstName { get; init; }
    
    /// <summary>
    /// Last name of the user.
    /// <remarks>
    ///     Optional.
    /// </remarks>
    /// </summary>
    public string? LastName { get; init; }

    /// <summary>
    /// Language code of the user.
    /// <remarks>
    ///     Optional.
    /// </remarks>
    /// </summary>
    public string? LanguageCode { get; init; }
}