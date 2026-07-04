using System.Collections.Immutable;
using Microsoft.Extensions.Logging;

namespace Navigator.Introspection;

/// <summary>
///     Tracks the stack of active trace identifiers for the current logical async flow.
///     The stack is stored in an <see cref="AsyncLocal{T}" /> so that concurrent updates
///     (e.g. parallel queue workers) and parallel branches inside a single update each see
///     a consistent view of their own ancestry: pushes and pops made by a child async flow
///     never leak into sibling flows or back into the parent flow.
/// </summary>
public class NavigatorTracerContext
{
    private readonly ILogger<NavigatorTracerContext> _logger;
    private readonly AsyncLocal<ImmutableStack<string>?> _identifiers = new();

    public NavigatorTracerContext(ILogger<NavigatorTracerContext> logger)
    {
        _logger = logger;
    }

    private ImmutableStack<string> Identifiers => _identifiers.Value ?? ImmutableStack<string>.Empty;

    /// <summary>
    ///     Whether the current async flow has at least one active trace.
    /// </summary>
    public bool HasActiveTrace() => !Identifiers.IsEmpty;

    /// <summary>
    ///     Returns the identifier of the innermost active trace of the current async flow, or <c>null</c>.
    /// </summary>
    public string? PeekOrDefault() => Identifiers.IsEmpty ? null : Identifiers.Peek();

    /// <summary>
    ///     Pushes a trace identifier onto the current async flow's stack.
    /// </summary>
    public void Push(string identifier) => _identifiers.Value = Identifiers.Push(identifier);

    /// <summary>
    ///     Pops the innermost trace identifier of the current async flow, if it matches the expected one.
    /// </summary>
    public void Pop(string expectedIdentifier)
    {
        var identifiers = Identifiers;

        if (identifiers.IsEmpty)
        {
            _logger.LogError("Cannot pop trace {TraceIdentifier} because the tracer context stack is empty", expectedIdentifier);
            return;
        }

        if (identifiers.Peek() != expectedIdentifier)
        {
            _logger.LogError(
                "Cannot pop trace {TraceIdentifier} because the current tracer context identifier is {CurrentTraceIdentifier}",
                expectedIdentifier,
                identifiers.Peek());
            return;
        }

        _identifiers.Value = identifiers.Pop();
    }
}
