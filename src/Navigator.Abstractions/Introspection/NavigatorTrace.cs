namespace Navigator.Abstractions.Introspection;

public class NavigatorTrace
{
    public readonly string Identifier;
    public readonly string? ParentIdentifier;
    public readonly DateTimeOffset StartTime;
    public required string SourceContext { get; init; }
    
    public DateTimeOffset? EndTime { get; set; }
    public NavigatorTrace(string? identifier, string? parentIdentifier)
    {
        Identifier = identifier ?? $"{Guid.CreateVersion7()}";
        ParentIdentifier = parentIdentifier;
        StartTime = TimeProvider.System.GetUtcNow();
    }
    
    
}