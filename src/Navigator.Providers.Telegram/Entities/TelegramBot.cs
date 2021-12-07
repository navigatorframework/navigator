using Navigator.Context;
using Navigator.Entities;

namespace Navigator.Providers.Telegram.Entities;

public record TelegramBot(long ExternalIdentifier) : Bot(ExternalIdentifier.ToString())
{
    /// <summary>
    /// Telegram identifier for the bot.
    /// </summary>
    public long ExternalIdentifier { get; init; } = ExternalIdentifier;
    
    /// <summary>
    /// Username of the bot.
    /// </summary>
    public string Username { get; init; }

    /// <summary>
    /// First name of the bot.
    /// </summary>
    public string FirstName { get; init; } = default!;
    
    /// <summary>
    /// Last name of the bot.
    /// <remarks>
    ///     Optional.
    /// </remarks>
    /// </summary>
    public string? LastName { get; init; }
    
    /// <summary>
    /// Whether the bot can join groups or not.
    /// <remarks>
    ///     Optional. Only available on <see cref="NavigatorContext.BotProfile"/>
    /// </remarks>
    /// </summary>
    public bool? CanJoinGroups { get; set; }

    /// <summary>
    /// Whether the bot can read all group messages or not.
    /// <remarks>
    ///     Optional. Only available on <see cref="NavigatorContext.BotProfile"/>
    /// </remarks>
    /// </summary>
    public bool? CanReadAllGroupMessages { get; set; }

    /// <summary>
    /// Whether the bot supports inline queries or not.
    /// <remarks>
    ///     Optional. Only available on <see cref="NavigatorContext.BotProfile"/>
    /// </remarks>
    /// </summary>
    public bool? SupportsInlineQueries { get; set; }

}