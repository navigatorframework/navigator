namespace Navigator.Entities;

/// <summary>
/// Represents a user.
/// </summary>
public record User(long Id, string FirstName)
{
    /// <summary>
    /// Telegram identifier for the user.
    /// </summary>
    public long Id { get; init; } = Id;

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
    public string FirstName { get; init; } = FirstName;

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

    /// <summary>
    /// True if the user is premium.
    /// <remarks>
    ///     Optional.
    /// </remarks>
    /// </summary>
    public bool? IsPremium { get; init; }

    /// <summary>
    /// True if the user has added the bot to the attachment menu.
    /// <remarks>
    ///     Optional.
    /// </remarks>
    /// </summary>
    public bool? HasBotInAttachmentMenu { get; init; }
}