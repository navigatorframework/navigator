namespace Navigator.Abstractions.Introspection.Sink;

/// <summary>
///     Represents a trace and its nested child traces.
/// </summary>
public record NavigatorTraceEntry
{
    /// <summary>
    ///     Gets the trace for the current entry.
    /// </summary>
    public NavigatorTrace Trace { get; init; } = null!;

    /// <summary>
    ///     Gets the nested child trace entries.
    /// </summary>
    public IReadOnlyCollection<NavigatorTraceEntry> InnerTraces { get; init; } = [];
}
