using Navigator.Entities;

namespace Navigator.Providers.Telegram.Entities;

public record TelegramUser(long ExternalIdentifier) : User(ExternalIdentifier.ToString())
{
    /// <summary>
    /// Telegram identifier for the user.
    /// </summary>
    public long ExternalIdentifier { get; init; } = ExternalIdentifier;

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
    public string FirstName { get; init; } = default!;
    
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