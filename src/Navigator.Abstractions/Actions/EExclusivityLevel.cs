namespace Navigator.Abstractions.Actions;

/// <summary>
///     Defines the exclusivity level of a <see cref="BotAction" />.
/// </summary>
public enum EExclusivityLevel
{
    /// <summary>
    ///     No exclusivity. The action always runs if matched.
    /// </summary>
    None = 0,

    /// <summary>
    ///     Category-level exclusivity. Among matched actions with exclusivity >= Category in the same exact category,
    ///     only the highest-priority one runs. Actions in other categories are unaffected.
    /// </summary>
    Category = 1,

    /// <summary>
    ///     Global exclusivity. If this action is the highest-priority matched action overall,
    ///     all other actions are discarded.
    /// </summary>
    Global = 2
}
