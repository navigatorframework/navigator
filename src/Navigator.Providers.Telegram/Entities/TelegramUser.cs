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
}