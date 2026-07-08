using System.Threading.Channels;

namespace Navigator.Strategies.Queued.Queues;

/// <summary>
///     Manages per-key update queues and provides a control channel
///     that emits newly created queues for worker orchestration.
/// </summary>
public interface IQueueManager
{
    /// <summary>
    ///     Returns the writer for the queue with the given key.
    /// </summary>
    /// <param name="queueKey">The partition key identifying which queue to use.</param>
    /// <returns>A <see cref="ChannelWriter{QueuedUpdate}" /> for the queue.</returns>
    ChannelWriter<QueuedUpdate> GetQueueWriter(string queueKey);

    /// <summary>
    ///     Returns the reader for the queue with the given key.
    /// </summary>
    /// <param name="queueKey">The partition key identifying which queue to use.</param>
    /// <returns>A <see cref="ChannelReader{QueuedUpdate}" /> for the queue, or <c>null</c> if the queue does not exist.</returns>
    ChannelReader<QueuedUpdate>? GetQueueReader(string queueKey);

    /// <summary>
    ///     Returns a reader for the control channel that emits newly created queues for worker orchestration.
    /// </summary>
    ChannelReader<string> NewQueuesReader { get; }
}
