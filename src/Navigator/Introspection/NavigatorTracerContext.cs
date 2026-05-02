using Microsoft.Extensions.Logging;

namespace Navigator.Introspection;

/// <summary>
///     Tracks the nesting of active trace identifiers for the current execution flow.
/// </summary>
public class NavigatorTracerContext
{
    private readonly ILogger<NavigatorTracerContext> _logger;
    private readonly Stack<string> _identifiers = new();

    /// <summary>
    ///     Initializes a new tracer context.
    /// </summary>
    /// <param name="logger">The logger used to report invalid stack operations.</param>
    public NavigatorTracerContext(ILogger<NavigatorTracerContext> logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///     Determines whether at least one trace is currently active.
    /// </summary>
    /// <returns><see langword="true" /> when a trace is active; otherwise, <see langword="false" />.</returns>
    public bool HasActiveTrace() => _identifiers.Count > 0;

    /// <summary>
    ///     Returns the current trace identifier without removing it.
    /// </summary>
    /// <returns>The current trace identifier, or <see langword="null" /> when the stack is empty.</returns>
    public string? PeekOrDefault() => _identifiers.TryPeek(out var identifier) ? identifier : null;

    /// <summary>
    ///     Pushes a new active trace identifier onto the context stack.
    /// </summary>
    /// <param name="identifier">The identifier to push.</param>
    public void Push(string identifier) => _identifiers.Push(identifier);

    /// <summary>
    ///     Removes the expected trace identifier from the context stack.
    /// </summary>
    /// <param name="expectedIdentifier">The identifier expected at the top of the stack.</param>
    public void Pop(string expectedIdentifier)
    {
        if (!_identifiers.TryPeek(out var identifier))
        {
            _logger.LogError("Cannot pop trace {TraceIdentifier} because the tracer context stack is empty", expectedIdentifier);
            return;
        }

        if (identifier != expectedIdentifier)
        {
            _logger.LogError(
                "Cannot pop trace {TraceIdentifier} because the current tracer context identifier is {CurrentTraceIdentifier}",
                expectedIdentifier,
                identifier);
            return;
        }

        _identifiers.Pop();
    }
}
