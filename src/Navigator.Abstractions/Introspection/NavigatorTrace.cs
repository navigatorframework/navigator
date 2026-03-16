namespace Navigator.Abstractions.Introspection;

public class NavigatorTrace
{
    
    public string Identifier { get; init; }
    public string? ParentIdentifier { get; init; }
    public string SourceContext { get; init; }
    public DateTimeOffset StartTime { get; init; }
    public Dictionary<string, HashSet<string>> Tags { get; init; }
    
    public NavigatorTrace(string? identifier, string? parentIdentifier, string sourceContext)
    {
        Identifier = identifier ?? $"{Guid.CreateVersion7()}";
        ParentIdentifier = parentIdentifier;
        SourceContext = sourceContext;
        StartTime = TimeProvider.System.GetUtcNow();
        Tags = new Dictionary<string, HashSet<string>>();
    }
    
    public DateTimeOffset? EndTime { get; set; }
    public ENavigatorTraceStatus Status { get; set; }
    public string? StatusType { get; set; }
    public string? StatusMessage { get; set; }
}

public enum ENavigatorTraceStatus
{
    Ok,
    Warning,
    Error
}