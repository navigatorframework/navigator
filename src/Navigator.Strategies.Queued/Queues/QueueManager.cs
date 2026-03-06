using System.Collections.Concurrent;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;

namespace Navigator.Strategies.Queued.Queues;

/// <summary>
///     Singleton that owns per-key update queues and a control channel
///     for notifying the hosted worker service about newly created queues.
/// </summary>
public sealed partial class QueueManager : IQueueManager
{
    private readonly ConcurrentDictionary<string, UpdateQueue> _queues = new();
    private readonly Channel<UpdateQueue> _newQueues = Channel.CreateUnbounded<UpdateQueue>(
        new UnboundedChannelOptions { SingleReader = true, SingleWriter = false });
    private readonly QueuedStrategyOptions _options;
    private readonly ILogger<QueueManager> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueueManager" /> class.
    /// </summary>
    /// <param name="options">Strategy options that control per-queue capacity.</param>
    /// <param name="logger">Logger instance.</param>
    public QueueManager(IOptions<QueuedStrategyOptions> options, ILogger<QueueManager> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    /// <inheritdoc />
    public ChannelReader<UpdateQueue> NewQueues => _newQueues.Reader;

    /// <inheritdoc />
    public async Task EnqueueAsync(string queueKey, Update update, CancellationToken cancellationToken)
    {
        var isNew = false;
        var queue = _queues.GetOrAdd(queueKey, _ =>
        {
            isNew = true;
            return new UpdateQueue(queueKey, CreateChannel());
        });

        if (isNew)
        {
            LogCreatedQueueForKeyQueueKey(queueKey);
            await _newQueues.Writer.WriteAsync(queue, cancellationToken);
        }

        while (await queue.Channel.Writer.WaitToWriteAsync(cancellationToken))
        {
            if (queue.Channel.Writer.TryWrite(update))
            {
                LogEnqueuedUpdateUpdateIdIntoQueue(update.Id, queueKey);
                return;
            }
        }
    }

    /// <inheritdoc />
    public void RemoveQueue(string queueKey)
    {
        if (_queues.TryRemove(queueKey, out _))
            LogRemovedQueue(queueKey);
        else
            GenerateReportSummary(queueKey);
    }

    /// <inheritdoc />
    public void CompleteAll()
    {
        _newQueues.Writer.TryComplete();

        foreach (var queue in _queues.Values)
            queue.Channel.Writer.TryComplete();
    }

    private Channel<Update> CreateChannel()
    {
        return _options.MaxMessagesPerQueue > 0
            ? Channel.CreateBounded<Update>(new BoundedChannelOptions(_options.MaxMessagesPerQueue)
            {
                FullMode = BoundedChannelFullMode.Wait,
                SingleReader = true,
                SingleWriter = false
            })
            : Channel.CreateUnbounded<Update>(new UnboundedChannelOptions
            {
                SingleReader = true,
                SingleWriter = false
            });
    }

    [LoggerMessage(LogLevel.Debug, "Created queue for key {queueKey}")]
    partial void LogCreatedQueueForKeyQueueKey(string queueKey);

    [LoggerMessage(LogLevel.Debug, "Enqueued update {updateId} into queue {queueKey}")]
    partial void LogEnqueuedUpdateUpdateIdIntoQueue(int updateId, string queueKey);

    [LoggerMessage(LogLevel.Debug, "Removed queue {queueKey}")]
    partial void LogRemovedQueue(string queueKey);

    [LoggerMessage(LogLevel.Warning, "Attempted to remove queue {queueKey} but it was not found")]
    partial void GenerateReportSummary(string queueKey);
}
