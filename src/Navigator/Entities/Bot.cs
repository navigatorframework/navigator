using Navigator.Context;

namespace Navigator.Entities;

/// <summary>
/// Bot.
/// </summary>
public record Bot(long Id, string Username, string FirstName) : User(Id, FirstName)
{
    /// <summary>
    /// Whether the bot can join groups or not.
    /// <remarks>
    ///     Optional. Only available on <see cref="NavigatorContext.BotProfile"/>
    /// </remarks>
    /// </summary>
    public bool? CanJoinGroups { get; init; }

    /// <summary>
    /// Whether the bot can read all group messages or not.
    /// <remarks>
    ///     Optional. Only available on <see cref="NavigatorContext.BotProfile"/>
    /// </remarks>
    /// </summary>
    public bool? CanReadAllGroupMessages { get; init; }

    /// <summary>
    /// Whether the bot supports inline queries or not.
    /// <remarks>
    ///     Optional. Only available on <see cref="NavigatorContext.BotProfile"/>
    /// </remarks>
    /// </summary>
    public bool? SupportsInlineQueries { get; init; }
}