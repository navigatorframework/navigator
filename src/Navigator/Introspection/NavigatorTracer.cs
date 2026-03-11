using Navigator.Abstractions.Introspection;
namespace Navigator.Introspection;

public class NavigatorTracer : INavigatorTracer
{
    private readonly CommitNavigatorTrace _commitTrace;
    private readonly NavigatorTrace _trace;

    public NavigatorTracer(CommitNavigatorTrace commitTrace, string? identifier, string? parentIdentifier, string sourceContext)
    {
        _commitTrace = commitTrace;
        _trace = new NavigatorTrace(identifier, parentIdentifier, sourceContext);
    }

    /// <inheritdoc />
    public string Identifier => _trace.Identifier;
    
    /// <inheritdoc />
    public void AddTag(string key, string value)
    {
        if (!_trace.Tags.TryGetValue(key, out var values))
        {
            values = [];
            _trace.Tags[key] = values;
        }

        values.Add(value);
    }
    
    /// <inheritdoc />
    public void SetStatus(ENavigatorTraceStatus status, string? type, string? message)
    {
        _trace.Status = status;
        _trace.StatusType = type;
        _trace.StatusMessage = message;
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        _trace.EndTime = TimeProvider.System.GetUtcNow();
        
        await _commitTrace(_trace);
    }
    
    

}