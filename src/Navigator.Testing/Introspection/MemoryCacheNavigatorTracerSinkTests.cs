using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Navigator.Abstractions.Introspection;
using Navigator.Introspection.Sink;
using Xunit;

namespace Navigator.Testing.Introspection;

public class MemoryCacheNavigatorTracerSinkTests
{
    private readonly MemoryCache _cache;
    private readonly MemoryCacheNavigatorTracerSink _sink;

    public MemoryCacheNavigatorTracerSinkTests()
    {
        _cache = new MemoryCache(new MemoryCacheOptions());
        _sink = new MemoryCacheNavigatorTracerSink(_cache);
    }

    private static NavigatorTrace MakeTrace(string identifier, string? parentIdentifier,
        string sourceContext = "Test.Source", long? chatId = null, int? messageId = null)
    {
        var trace = new NavigatorTrace(identifier, parentIdentifier, sourceContext);

        if (chatId is not null)
            trace.Tags[NavigatorTraceKeys.UpdateChatId] = [$"{chatId}"];

        if (messageId is not null)
            trace.Tags[NavigatorTraceKeys.UpdateMessageId] = [$"{messageId}"];

        return trace;
    }

    [Fact]
    public async Task ConcurrentStoresAndReads_ShouldNotCorruptIndexes()
    {
        const int rounds = 3;
        const int roots = 50;
        const int childrenPerRoot = 5;

        // Races are probabilistic; independent rounds plus the leading Task.Yield fan-out
        // make a single test run catch a missing sink lock deterministically in practice
        // (measured: 6/6 failures against a mutant sink with the lock statements removed).
        for (var round = 0; round < rounds; round++)
        {
            var sink = new MemoryCacheNavigatorTracerSink(new MemoryCache(new MemoryCacheOptions()));

            var writers = Enumerable.Range(0, roots).Select(async root =>
            {
                // Yield first so every writer and reader is scheduled before any of them
                // starts working — maximum overlap on the sink's synchronous internals.
                await Task.Yield();

                // Children commit before their parent, mirroring tracer disposal order.
                for (var child = 0; child < childrenPerRoot; child++)
                {
                    await sink.Store(MakeTrace($"root-{root}:child-{child}", $"root-{root}"));
                }

                await sink.Store(MakeTrace($"root-{root}", null, chatId: 1000 + root, messageId: root));
            });

            var readers = Enumerable.Range(0, 20).Select(async reader =>
            {
                await Task.Yield();

                for (var i = 0; i < 25; i++)
                {
                    // Children are committed before their root and every index mutation is
                    // serialized, so any root a reader can see must already be complete.
                    var snapshot = await sink.RetrieveAll();
                    snapshot.Should().AllSatisfy(entry => entry.InnerTraces.Should().HaveCount(childrenPerRoot));

                    var byMessage = await sink.RetrieveByChatAndMessage(1000 + reader, reader, findRoot: true);
                    byMessage.Should().AllSatisfy(entry => entry.InnerTraces.Should().HaveCount(childrenPerRoot));
                }
            });

            await Task.WhenAll(writers.Concat(readers));

            var all = await sink.RetrieveAll();

            all.Should().HaveCount(roots, "round {0} must index every root", round);
            all.Should().AllSatisfy(entry => entry.InnerTraces.Should().HaveCount(childrenPerRoot));
        }
    }

    [Fact]
    public async Task RetrieveByChatAndMessage_ShouldFindRootFromAnIndexedChild()
    {
        // Mirrors a real update's trace: the root strategy trace is tagged with the incoming
        // message id, while the nested sendMessage client trace is tagged with the id of the
        // message the bot sent. /debug replies to the bot's message, so the lookup starts at
        // the client trace and must walk up to the root.
        const long chatId = -1001770877590;
        const int incomingMessageId = 114437;
        const int sentMessageId = 114440;

        var root = MakeTrace("0HNMBNNQTV3F8:00000001", null,
            "Navigator.Strategy.DefaultNavigationStrategy", chatId, incomingMessageId);
        var handler = MakeTrace("handler-trace", "0HNMBNNQTV3F8:00000001",
            "FOSCBot.Core.Application.Actions.CatchAllFallbackHandler");
        var clientCall = MakeTrace("client-trace", "handler-trace",
            "Navigator.Client.NavigatorClient", chatId, sentMessageId);

        await _sink.Store(clientCall);
        await _sink.Store(handler);
        await _sink.Store(root);

        var byBotMessage = await _sink.RetrieveByChatAndMessage(chatId, sentMessageId, findRoot: true);

        byBotMessage.Should().HaveCount(1);
        var entry = byBotMessage.Single();
        entry.Trace.Identifier.Should().Be("0HNMBNNQTV3F8:00000001");
        entry.Trace.ParentIdentifier.Should().BeNull();
        entry.InnerTraces.Should().ContainSingle(inner => inner.Trace.Identifier == "handler-trace")
            .Which.InnerTraces.Should().ContainSingle(inner => inner.Trace.Identifier == "client-trace");

        var byIncomingMessage = await _sink.RetrieveByChatAndMessage(chatId, incomingMessageId, findRoot: true);

        byIncomingMessage.Should().HaveCount(1);
        byIncomingMessage.Single().Trace.Identifier.Should().Be("0HNMBNNQTV3F8:00000001");
    }

    [Fact]
    public async Task StoringARoot_ShouldPruneRootsWhoseTraceExpired()
    {
        await _sink.Store(MakeTrace("expired-root", null));
        await _sink.Store(MakeTrace("live-root", null));

        // Simulate the expired root's trace entry falling out of the cache while
        // the roots index (which never expires) still references it.
        _cache.Remove("navigator:trace:expired-root");

        await _sink.Store(MakeTrace("new-root", null));

        _cache.TryGetValue("navigator:roots", out List<string>? rootIds).Should().BeTrue();
        rootIds.Should().BeEquivalentTo(["live-root", "new-root"]);
    }
}
