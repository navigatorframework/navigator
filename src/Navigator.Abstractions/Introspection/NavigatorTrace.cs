namespace Navigator.Abstractions.Introspection;

/// <summary>
///     Represents a single recorded Navigator trace.
/// </summary>
public class NavigatorTrace
{
    /// <summary>
    ///     Gets the unique identifier for the trace.
    /// </summary>
    public string Identifier { get; init; }

    /// <summary>
    ///     Gets the identifier of the parent trace when this trace is nested.
    /// </summary>
    public string? ParentIdentifier { get; init; }

    /// <summary>
    ///     Gets the logical source that created the trace.
    /// </summary>
    public string SourceContext { get; init; }

    /// <summary>
    ///     Gets the UTC timestamp when the trace started.
    /// </summary>
    public DateTimeOffset StartTime { get; init; }

    /// <summary>
    ///     Gets the tags collected for the trace.
    /// </summary>
    public Dictionary<string, HashSet<string>> Tags { get; init; }

    /// <summary>
    ///     Initializes a new trace instance.
    /// </summary>
    /// <param name="identifier">An optional identifier for the trace. A new one is generated when omitted.</param>
    /// <param name="parentIdentifier">The identifier of the parent trace, if any.</param>
    /// <param name="sourceContext">The logical source that created the trace.</param>
    public NavigatorTrace(string? identifier, string? parentIdentifier, string sourceContext)
    {
        Identifier = identifier ?? $"{Guid.CreateVersion7()}";
        ParentIdentifier = parentIdentifier;
        SourceContext = sourceContext;
        StartTime = TimeProvider.System.GetUtcNow();
        Tags = new Dictionary<string, HashSet<string>>();
    }

    /// <summary>
    ///     Gets or sets the UTC timestamp when the trace ended.
    /// </summary>
    public DateTimeOffset? EndTime { get; set; }

    /// <summary>
    ///     Gets or sets the overall status assigned to the trace.
    /// </summary>
    public ENavigatorTraceStatus Status { get; set; }

    /// <summary>
    ///     Gets or sets the optional status category.
    /// </summary>
    public string? StatusType { get; set; }

    /// <summary>
    ///     Gets or sets the optional status message.
    /// </summary>
    public string? StatusMessage { get; set; }
}

/// <summary>
///     Represents the final state recorded for a trace.
/// </summary>
public enum ENavigatorTraceStatus
{
    /// <summary>
    ///     The traced operation completed successfully.
    /// </summary>
    Ok,

    /// <summary>
    ///     The traced operation completed with a warning.
    /// </summary>
    Warning,

    /// <summary>
    ///     The traced operation failed.
    /// </summary>
    Error
}
