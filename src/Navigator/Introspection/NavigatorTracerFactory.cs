using Navigator.Abstractions.Introspection;
using Navigator.Abstractions.Introspection.Sink;

namespace Navigator.Introspection;

/// <summary>
///     Creates runtime tracers for a specific category.
/// </summary>
/// <typeparam name="TCategoryName">The category marker type associated with created tracers.</typeparam>
public class NavigatorTracerFactory<TCategoryName> : INavigatorTracerFactory<TCategoryName>
{
    private readonly INavigatorTracerSink _sink;
    private readonly NavigatorTracerContext _context;

    /// <summary>
    ///     Initializes a new tracer factory.
    /// </summary>
    /// <param name="sink">The sink used to store completed traces.</param>
    /// <param name="context">The current tracer context.</param>
    public NavigatorTracerFactory(INavigatorTracerSink sink, NavigatorTracerContext context)
    {
        _sink = sink;
        _context = context;
    }

    /// <inheritdoc />
    public bool HasActiveTrace() => _context.HasActiveTrace();

    /// <inheritdoc />
    public INavigatorTracer Get(string? identifier = null)
    {
        var lastKnownIdentifier = _context.PeekOrDefault();
        
        async ValueTask CommitTrace(NavigatorTrace trace)
        {
            _context.Pop(trace.Identifier);
            await _sink.Store(trace);
        }
        
        var tracer = new NavigatorTracer(CommitTrace, identifier, lastKnownIdentifier, GetCategoryName());
        
        _context.Push(tracer.Identifier);
        
        return tracer;
    }
    
    private static string GetCategoryName() => typeof(TCategoryName).FullName ?? "_Unknown";
}
