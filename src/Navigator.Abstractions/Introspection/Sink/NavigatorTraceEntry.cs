namespace Navigator.Abstractions.Introspection.Sink;

public record NavigatorTraceEntry
{
    public NavigatorTrace Trace { get; init; }
    public IReadOnlyCollection<NavigatorTraceEntry> InnerTraces { get; init; }
}