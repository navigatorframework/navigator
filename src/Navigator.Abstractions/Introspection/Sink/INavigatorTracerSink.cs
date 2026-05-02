namespace Navigator.Abstractions.Introspection.Sink;

/// <summary>
///     Stores Navigator traces in a backing sink.
/// </summary>
public interface INavigatorTracerSink
{
    /// <summary>
    ///     Stores the specified trace.
    /// </summary>
    /// <param name="trace">The trace to store.</param>
    public Task Store(NavigatorTrace trace);
}
