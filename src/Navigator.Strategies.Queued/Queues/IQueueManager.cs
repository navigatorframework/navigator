using System.Threading.Channels;
using Telegram.Bot.Types;

namespace Navigator.Strategies.Queued.Queues;

/// <summary>
///     Manages per-key update queues and provides a control channel
///     that emits newly created queues for worker orchestration.
/// </summary>
public interface IQueueManager
{
    /// <summary>
    ///     Reader for the control channel that emits newly created queues.
    ///     The hosted service reads from this to start workers.
    /// </summary>
    ChannelReader<UpdateQueue> NewQueues { get; }

    /// <summary>
    ///     Gets or creates the queue for the given key, enqueues the update,
    ///     and notifies the hosted service if a new queue was just created.
    /// </summary>
    /// <param name="queueKey">The partition key identifying which queue to use.</param>
    /// <param name="update">The Telegram update to enqueue.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task EnqueueAsync(string queueKey, Update update, CancellationToken cancellationToken);

    /// <summary>
    ///     Removes a queue from the dictionary. Called by the worker when a queue's channel completes.
    /// </summary>
    /// <param name="queueKey">The partition key of the queue to remove.</param>
    void RemoveQueue(string queueKey);

    /// <summary>
    ///     Completes all queue channels and the control channel.
    ///     Called during shutdown.
    /// </summary>
    void CompleteAll();
}
