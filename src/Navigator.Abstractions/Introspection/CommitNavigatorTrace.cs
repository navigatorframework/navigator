namespace Navigator.Abstractions.Introspection;

/// <summary>
///     Persists a completed <see cref="NavigatorTrace" />.
/// </summary>
/// <param name="trace">The trace to commit.</param>
public delegate ValueTask CommitNavigatorTrace(NavigatorTrace trace);
