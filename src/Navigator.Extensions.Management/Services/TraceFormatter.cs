using Navigator.Abstractions.Introspection;
using Navigator.Abstractions.Introspection.Sink;

namespace Navigator.Extensions.Management.Services;

/// <summary>
///     Simple trace formatter for human-readable output.
/// </summary>
public class TraceFormatter : ITraceFormatter
{
    public string FormatTraces(IReadOnlyCollection<NavigatorTraceEntry> traces)
    {
        if (!traces.Any())
            return "🔍 No traces found for this message.";

        var result = new System.Text.StringBuilder();
        
        // Simple summary
        var allTraces = FlattenTraces(traces).ToList();
        var totalDuration = CalculateTotalDuration(allTraces);
        var longestTrace = FindLongestTrace(traces.First().InnerTraces.Select(t => t.Trace).ToList());
        var hasErrors = allTraces.Any(t => t.Status == ENavigatorTraceStatus.Error);
        var hasWarnings = allTraces.Any(t => t.Status == ENavigatorTraceStatus.Warning);
        
        result.AppendLine("```");
        result.AppendLine("🔍 **Debug Trace Summary**");
        result.AppendLine($"⏱️ Total Duration: {totalDuration:F0}ms");
        
        if (longestTrace != null)
        {
            var duration = longestTrace.EndTime.HasValue 
                ? (longestTrace.EndTime.Value - longestTrace.StartTime).TotalMilliseconds 
                : 0;
            result.AppendLine($"📊 Longest: {longestTrace.SourceContext} ({duration:F0}ms)");
        }
        
        if (hasErrors)
            result.AppendLine("❌ Contains errors");
        if (hasWarnings)
            result.AppendLine("⚠️ Contains warnings");

        result.AppendLine("```");
        return result.ToString();
    }

    private static IEnumerable<NavigatorTrace> FlattenTraces(IEnumerable<NavigatorTraceEntry> entries)
    {
        foreach (var entry in entries)
        {
            yield return entry.Trace;
            
            foreach (var innerTrace in FlattenTraces(entry.InnerTraces))
            {
                yield return innerTrace;
            }
        }
    }

    private static double CalculateTotalDuration(List<NavigatorTrace> traces)
    {
        var rootTraces = traces.Where(t => t.ParentIdentifier == null);
        return rootTraces
            .Where(t => t.EndTime.HasValue)
            .Select(t => (t.EndTime!.Value - t.StartTime).TotalMilliseconds)
            .DefaultIfEmpty(0)
            .Max();
    }

    private static NavigatorTrace? FindLongestTrace(List<NavigatorTrace> traces)
    {
        return traces
            .Where(t => t.EndTime.HasValue)
            .Select(t => new { Trace = t, Duration = (t.EndTime!.Value - t.StartTime).TotalMilliseconds })
            .OrderByDescending(x => x.Duration)
            .FirstOrDefault()?.Trace;
    }
}
