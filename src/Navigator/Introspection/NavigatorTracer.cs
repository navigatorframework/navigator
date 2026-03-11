using Navigator.Abstractions.Introspection;
namespace Navigator.Introspection;

public class NavigatorTracer : INavigatorTracer
{
    private readonly CommitNavigatorTrace _commitTrace;
    private readonly NavigatorTrace _trace;

    public NavigatorTracer(CommitNavigatorTrace commitTrace, string? identifier, string? parentIdentifier, string sourceContext)
    {
        _commitTrace = commitTrace;
        _trace = new NavigatorTrace(identifier, parentIdentifier)
        {
            SourceContext = sourceContext
        };
        
        Identifier = _trace.Identifier;
    }

    public string Identifier { get; }

    public async ValueTask DisposeAsync()
    {
        _trace.EndTime = TimeProvider.System.GetUtcNow();
        
        await _commitTrace(_trace);
    }

}