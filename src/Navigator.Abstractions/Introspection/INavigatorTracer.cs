namespace Navigator.Abstractions.Introspection;

/// <summary>
///     Represents an active trace scope for Navigator operations.
/// </summary>
public interface INavigatorTracer : IAsyncDisposable
{
    /// <summary>
    ///     Gets the unique identifier for the active trace.
    /// </summary>
    string Identifier { get; }

    /// <summary>
    ///     Adds a tag value under the specified key.
    /// </summary>
    /// <param name="key">The tag key.</param>
    /// <param name="value">The tag value.</param>
    void AddTag(string key, string value);

    /// <summary>
    ///     Sets the final status metadata for the active trace.
    /// </summary>
    /// <param name="status">The status to assign to the trace.</param>
    /// <param name="type">An optional status category.</param>
    /// <param name="message">An optional status message.</param>
    void SetStatus(ENavigatorTraceStatus status, string? type, string? message);
}
