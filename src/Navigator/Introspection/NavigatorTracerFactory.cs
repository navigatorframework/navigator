using Navigator.Abstractions.Introspection;
using Navigator.Abstractions.Introspection.Sink;

namespace Navigator.Introspection;

public class NavigatorTracerFactory<TCategoryName> : INavigatorTracerFactory<TCategoryName>
{
    private readonly INavigatorTracerSink _sink;
    private readonly NavigatorTracerContext _context;

    public NavigatorTracerFactory(INavigatorTracerSink sink, NavigatorTracerContext context)
    {
        _sink = sink;
        _context = context;
    }

    public bool HasActiveTrace() => _context.HasActiveTrace();

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