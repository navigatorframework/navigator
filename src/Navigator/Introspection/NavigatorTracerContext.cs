using Microsoft.Extensions.Logging;

namespace Navigator.Introspection;

public class NavigatorTracerContext
{
    private readonly ILogger<NavigatorTracerContext> _logger;
    private readonly Stack<string> _identifiers = new();

    public NavigatorTracerContext(ILogger<NavigatorTracerContext> logger)
    {
        _logger = logger;
    }

    public bool HasActiveTrace() => _identifiers.Count > 0;

    public string? PeekOrDefault() => _identifiers.TryPeek(out var identifier) ? identifier : null;

    public void Push(string identifier) => _identifiers.Push(identifier);

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
