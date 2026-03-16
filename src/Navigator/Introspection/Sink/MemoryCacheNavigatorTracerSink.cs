using Microsoft.Extensions.Caching.Memory;
using Navigator.Abstractions.Introspection;
using Navigator.Abstractions.Introspection.Reader;
using Navigator.Abstractions.Introspection.Sink;

namespace Navigator.Introspection.Sink;

public class MemoryCacheNavigatorTracerSink(IMemoryCache cache) : INavigatorTracerSink, INavigatorTraceReader
{
    private static readonly TimeSpan Ttl = TimeSpan.FromHours(1);

    public Task Store(NavigatorTrace trace)
    {
        cache.Set($"navigator:trace:{trace.Identifier}", trace, Ttl);

        if (trace.ParentIdentifier is not null)
        {
            var children = cache.GetOrCreate(
                $"navigator:children:{trace.ParentIdentifier}",
                entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = Ttl;
                    return new List<string>();
                })!;

            children.Add(trace.Identifier);
        }
        else
        {
            var roots = cache.GetOrCreate(
                "navigator:roots",
                entry =>
                {
                    entry.Priority = CacheItemPriority.NeverRemove;
                    return new List<string>();
                })!;

            roots.Add(trace.Identifier);
        }

        return Task.CompletedTask;
    }

    public Task<NavigatorTraceEntry?> Retrieve(string identifier)
    {
        return Task.FromResult(BuildTree(identifier));
    }

    public Task<IReadOnlyCollection<NavigatorTraceEntry>> RetrieveAll()
    {
        if (!cache.TryGetValue("navigator:roots", out List<string>? rootIds) || rootIds is null)
            return Task.FromResult<IReadOnlyCollection<NavigatorTraceEntry>>([]);

        var roots = rootIds
            .Select(BuildTree)
            .Where(e => e is not null)
            .Cast<NavigatorTraceEntry>()
            .ToList();

        return Task.FromResult<IReadOnlyCollection<NavigatorTraceEntry>>(roots);
    }

    private NavigatorTraceEntry? BuildTree(string identifier)
    {
        if (!cache.TryGetValue($"navigator:trace:{identifier}", out NavigatorTrace? trace)
            || trace is null)
            return null;

        var children = new List<NavigatorTraceEntry>();

        if (cache.TryGetValue($"navigator:children:{identifier}", out List<string>? childIds)
            && childIds is not null)
        {
            foreach (var childId in childIds)
            {
                var childEntry = BuildTree(childId);
                if (childEntry is not null)
                    children.Add(childEntry);
            }
        }

        return new NavigatorTraceEntry
        {
            Trace = trace,
            InnerTraces = children
        };
    }
}