using Microsoft.Extensions.Caching.Memory;
using Navigator.Abstractions.Introspection;
using Navigator.Abstractions.Introspection.Reader;
using Navigator.Abstractions.Introspection.Sink;
using static Navigator.Abstractions.Introspection.NavigatorTraceKeys;

namespace Navigator.Introspection.Sink;

/// <summary>
///     Stores traces in <see cref="IMemoryCache" /> and exposes lookup operations over the cached tree.
/// </summary>
public class MemoryCacheNavigatorTracerSink : INavigatorTracerSink, INavigatorTraceReader
{
    private static readonly TimeSpan Ttl = TimeSpan.FromHours(1);
    private readonly IMemoryCache _cache;

    /// <summary>
    ///     Initializes a new memory-backed trace sink.
    /// </summary>
    /// <param name="cache">The cache used to persist trace entries and indexes.</param>
    public MemoryCacheNavigatorTracerSink(IMemoryCache cache)
    {
        _cache = cache;
    }

    /// <inheritdoc />
    public Task Store(NavigatorTrace trace)
    {
        _cache.Set($"navigator:trace:{trace.Identifier}", trace, Ttl);

        // Index by chat ID and message ID if the trace has both tags
        if (HasValidChatAndMessageTags(trace, out var chatId, out var messageId))
        {
            var messageIndex = _cache.GetOrCreate(
                $"navigator:message:{chatId}:{messageId}",
                entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = Ttl;
                    return new List<string>();
                })!;

            messageIndex.Add(trace.Identifier);
        }

        if (trace.ParentIdentifier is not null)
        {
            var children = _cache.GetOrCreate(
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
            var roots = _cache.GetOrCreate(
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

    private bool HasValidChatAndMessageTags(NavigatorTrace trace, out string? chatId, out string? messageId)
    {
        if (trace.Tags.TryGetValue(UpdateChatId, out var chatIds) && chatIds.Count != 0 &&
            trace.Tags.TryGetValue(UpdateMessageId, out var messageIds) && messageIds.Count != 0)
        {
            chatId = chatIds.First();
            messageId = messageIds.First();
            return true;
        }

        chatId = null;
        messageId = null;
        return false;
    }

    /// <inheritdoc />
    public Task<NavigatorTraceEntry?> Retrieve(string identifier)
    {
        return Task.FromResult(BuildTree(identifier));
    }

    /// <inheritdoc />
    public Task<IReadOnlyCollection<NavigatorTraceEntry>> RetrieveAll()
    {
        if (!_cache.TryGetValue("navigator:roots", out List<string>? rootIds) || rootIds is null)
            return Task.FromResult<IReadOnlyCollection<NavigatorTraceEntry>>([]);

        var roots = rootIds
            .Select(BuildTree)
            .Where(e => e is not null)
            .Cast<NavigatorTraceEntry>()
            .ToList();

        return Task.FromResult<IReadOnlyCollection<NavigatorTraceEntry>>(roots);
    }

    /// <inheritdoc />
    public Task<IReadOnlyCollection<NavigatorTraceEntry>> RetrieveByChatAndMessage(long chatId, int messageId, bool findRoot = false)
    {
        if (!_cache.TryGetValue($"navigator:message:{chatId}:{messageId}", out List<string>? messageTraceIds) || messageTraceIds is null)
            return Task.FromResult<IReadOnlyCollection<NavigatorTraceEntry>>([]);

        var matchingTraces = new List<NavigatorTraceEntry>();

        foreach (var traceId in messageTraceIds)
        {
            var currentTraceId = traceId;

            if (findRoot)
            {
                while (_cache.TryGetValue($"navigator:trace:{currentTraceId}", out NavigatorTrace? trace) && trace?.ParentIdentifier != null)
                {
                    currentTraceId = trace.ParentIdentifier;
                }
            }

            var traceEntry = BuildTree(currentTraceId);
            if (traceEntry is not null)
            {
                matchingTraces.Add(traceEntry);
            }
        }

        return Task.FromResult<IReadOnlyCollection<NavigatorTraceEntry>>(matchingTraces);
    }

    private NavigatorTraceEntry? BuildTree(string identifier)
    {
        if (!_cache.TryGetValue($"navigator:trace:{identifier}", out NavigatorTrace? trace)
            || trace is null)
            return null;

        var children = new List<NavigatorTraceEntry>();

        if (_cache.TryGetValue($"navigator:children:{identifier}", out List<string>? childIds)
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
