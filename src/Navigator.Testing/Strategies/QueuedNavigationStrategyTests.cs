using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Navigator.Strategies.Queued;
using Navigator.Strategies.Queued.Queues;
using Navigator.Strategies.Queued.Strategy;
using Navigator.Strategies.Queued.Telegram;
using NSubstitute;
using Telegram.Bot.Types;
using Xunit;

namespace Navigator.Testing.Strategies;

public class QueuedNavigationStrategyTests
{
    private readonly QueueManager _queueManager;
    private readonly QueuedNavigationStrategy _strategy;

    public QueuedNavigationStrategyTests()
    {
        _queueManager = new QueueManager(
            Options.Create(new QueuedStrategyOptions()),
            Substitute.For<ILogger<QueueManager>>());
        _strategy = new QueuedNavigationStrategy(_queueManager);
    }

    private static Update MakeUpdate(int updateId, long chatId, int messageId)
    {
        return new Update
        {
            Id = updateId,
            Message = new Message
            {
                Id = messageId,
                Chat = new Chat { Id = chatId }
            }
        };
    }

    [Fact]
    public async Task Invoke_ShouldEnqueueUpdateTogetherWithItsIdentifier()
    {
        var update = MakeUpdate(424841378, -1001770877590, 114437);

        await _strategy.Invoke(update, "0HNMBNNQTV3F8:00000001");

        var reader = _queueManager.GetQueueReader(update.GetQueueKey());
        reader.Should().NotBeNull();

        var queued = await reader!.ReadAsync();
        queued.Update.Should().BeSameAs(update);
        queued.Identifier.Should().Be("0HNMBNNQTV3F8:00000001");
    }

    [Fact]
    public async Task Invoke_ShouldPreserveOrderAndIdentifiersWithinAChat()
    {
        var first = MakeUpdate(1, 42, 100);
        var second = MakeUpdate(2, 42, 101);

        await _strategy.Invoke(first, "request-1");
        await _strategy.Invoke(second, "request-2");

        var reader = _queueManager.GetQueueReader("chat:42")!;

        var queuedFirst = await reader.ReadAsync();
        var queuedSecond = await reader.ReadAsync();

        queuedFirst.Update.Id.Should().Be(1);
        queuedFirst.Identifier.Should().Be("request-1");
        queuedSecond.Update.Id.Should().Be(2);
        queuedSecond.Identifier.Should().Be("request-2");
    }

    [Fact]
    public async Task Invoke_ShouldPartitionDifferentChatsIntoDifferentQueues()
    {
        await _strategy.Invoke(MakeUpdate(1, 1, 10), "request-1");
        await _strategy.Invoke(MakeUpdate(2, 2, 20), "request-2");

        var firstQueue = _queueManager.GetQueueReader("chat:1");
        var secondQueue = _queueManager.GetQueueReader("chat:2");

        firstQueue.Should().NotBeNull();
        secondQueue.Should().NotBeNull();

        (await firstQueue!.ReadAsync()).Identifier.Should().Be("request-1");
        (await secondQueue!.ReadAsync()).Identifier.Should().Be("request-2");

        var newQueueKeys = new List<string>();
        while (_queueManager.NewQueuesReader.TryRead(out var key))
            newQueueKeys.Add(key);

        newQueueKeys.Should().BeEquivalentTo(["chat:1", "chat:2"]);
    }
}
