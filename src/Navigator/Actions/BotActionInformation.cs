namespace Navigator.Actions;

public record BotActionInformation
{
    public required UpdateCategory Category;
    public required Type[] ConditionInputTypes;
    public required TimeSpan? Cooldown;
    public required Type[] HandlerInputTypes;
    public required ushort Priority;
}

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