using Navigator.Abstractions.Introspection.Sink;

namespace Navigator.Extensions.Management.Services;

/// <summary>
///     Interface for formatting trace entries into human-readable text.
/// </summary>
public interface ITraceFormatter
{
    /// <summary>
    ///     Formats multiple trace entries into human-readable text.
    /// </summary>
    /// <param name="traces">The trace entries to format.</param>
    /// <returns>Human-readable formatted traces string.</returns>
    string FormatTraces(IReadOnlyCollection<NavigatorTraceEntry> traces);
}
