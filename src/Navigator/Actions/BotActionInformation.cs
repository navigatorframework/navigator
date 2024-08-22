using Telegram.Bot.Types.Enums;

namespace Navigator.Actions;

/// <summary>
///     Information about a <see cref="BotAction" />.
/// </summary>
public record BotActionInformation
{
    /// <summary>
    ///     The <see cref="UpdateCategory" /> of the <see cref="BotAction" />.
    /// </summary>
    public required UpdateCategory Category;

    /// <summary>
    ///     The chance of the <see cref="BotAction" /> being executed. Optional.
    /// </summary>
    public required double? Chances;

    /// <summary>
    ///     The <see cref="ChatAction" /> associtated with the <see cref="BotAction" />. Optional.
    /// </summary>
    public required ChatAction? ChatAction;

    /// <summary>
    ///     The input types of the condition delegate of the <see cref="BotAction" />.
    /// </summary>
    public required Type[] ConditionInputTypes;

    /// <summary>
    ///     The cooldown of the <see cref="BotAction" />. Optional.
    /// </summary>
    public required TimeSpan? Cooldown;

    /// <summary>
    ///     The input types of the handler delegate of the <see cref="BotAction" />.
    /// </summary>
    public required Type[] HandlerInputTypes;

    /// <summary>
    ///     The name of the <see cref="BotAction" />. If no name is set, the id is used.
    /// </summary>
    public required string Name;

    /// <summary>
    ///     The priority of the <see cref="BotAction" />. Optional.
    /// </summary>
    public required ushort Priority;
}

/// <summary>
///     The <see cref="UpdateCategory" /> of the <see cref="BotAction" /> to which the action belongs.
/// </summary>
/// <param name="Kind">A string that represents the kind of the <see cref="UpdateCategory" />.</param>
/// <param name="Subkind">A string that represents the subkind of the <see cref="UpdateCategory" />.</param>
public sealed record UpdateCategory(string Kind, string? Subkind = default)
{
    /// <inheritdoc />
    public bool Equals(UpdateCategory? other)
    {
        return Kind == other?.Kind && (Subkind == null || other.Subkind == null || Subkind == other.Subkind);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Kind, Subkind);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Subkind is null
            ? Kind
            : $"{Kind}.{Subkind}";
    }
}