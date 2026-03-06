using Navigator.Abstractions.Strategies;
using Navigator.Strategies.Queued.Queues;
using Navigator.Strategies.Queued.Telegram;
using Telegram.Bot.Types;

namespace Navigator.Strategies.Queued.Strategy;

/// <summary>
///     An <see cref="INavigatorStrategy" /> that only enqueues updates into per-key queues.
///     Actual processing is handled by the hosted worker service.
/// </summary>
public sealed class QueuedNavigationStrategy : INavigatorStrategy
{
    private readonly IQueueManager _queueManager;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueuedNavigationStrategy" /> class.
    /// </summary>
    /// <param name="queueManager">The queue manager that owns per-key queues.</param>
    public QueuedNavigationStrategy(IQueueManager queueManager)
    {
        _queueManager = queueManager;
    }

    /// <inheritdoc />
    public async Task Invoke(Update update)
    {
        var queueWriter = _queueManager.GetQueueWriter(update.GetQueueKey());

        await queueWriter.WriteAsync(update);
    }
}
