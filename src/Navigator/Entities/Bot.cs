namespace Navigator.Entities;

/// <summary>
/// Bot.
/// </summary>
public record Bot(long Id, string FirstName) : User(Id, FirstName)
{
    /// <summary>
    /// Whether the bot can join groups or not.
    /// </summary>
    public bool? CanJoinGroups { get; init; }

    /// <summary>
    /// Whether the bot can read all group messages or not.
    /// </summary>
    public bool? CanReadAllGroupMessages { get; init; }

    /// <summary>
    /// Whether the bot supports inline queries or not.
    /// </summary>
    public bool? SupportsInlineQueries { get; init; }
}