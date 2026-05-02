namespace Navigator.Abstractions.Introspection;

/// <summary>
///     Creates and retrieves <see cref="INavigatorTracer" /> instances for a category.
/// </summary>
/// <typeparam name="TCategoryName">The category marker type associated with created tracers.</typeparam>
public interface INavigatorTracerFactory<out TCategoryName>
{
    /// <summary>
    ///     Determines whether a trace is currently active for the factory scope.
    /// </summary>
    /// <returns><see langword="true" /> when an active trace exists; otherwise, <see langword="false" />.</returns>
    public bool HasActiveTrace();

    /// <summary>
    ///     Gets a tracer for the current scope.
    /// </summary>
    /// <param name="identifier">An optional identifier to reuse for the trace.</param>
    /// <returns>The active or newly created tracer.</returns>
    public INavigatorTracer Get(string? identifier = null);
}
