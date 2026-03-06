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
    private readonly ConcurrentDictionary<string, UpdateQueue> _queues;
    private readonly Channel<string> _newQueues;
    private readonly QueuedStrategyOptions _options;
    private readonly ILogger<QueueManager> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueueManager" /> class.
    /// </summary>
    /// <param name="options">Strategy options that control per-queue capacity.</param>
    /// <param name="logger">Logger instance.</param>
    public QueueManager(IOptions<QueuedStrategyOptions> options, ILogger<QueueManager> logger)
    {
        _queues = new ConcurrentDictionary<string, UpdateQueue>();
        _newQueues = Channel.CreateUnbounded<string>(
            new UnboundedChannelOptions
            {
                SingleReader = true, 
                SingleWriter = false
            });
        _options = options.Value;
        _logger = logger;
    }

    /// <inheritdoc />
    public ChannelWriter<Update> GetQueueWriter(string queueKey)
    {
        var queue = _queues.GetOrAdd(queueKey, QueueValueFactory);
        
        return queue.Channel.Writer;

        UpdateQueue QueueValueFactory(string _)
        {
            LogCreatingQueueForKey(queueKey);
            return new UpdateQueue(queueKey, CreateChannel(queueKey));
        }
    }

    /// <inheritdoc />
    public ChannelReader<Update>? GetQueueReader(string queueKey)
    {
        var queue = _queues.GetValueOrDefault(queueKey);
        
        return queue?.Channel.Reader;
    }

    /// <inheritdoc />
    public ChannelReader<string> NewQueuesReader => _newQueues.Reader;

    private Channel<Update> CreateChannel(string queueKey)
    {
        var channel = _options.MaxMessagesPerQueue > 0
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

        var result = _newQueues.Writer.TryWrite(queueKey);

        if (!result)
        {
            LogFailedToWriteToNewQueuesChannelForKey(queueKey);
        }
        
        return channel;
    }

    [LoggerMessage(LogLevel.Debug, "Creating queue for key {key}")]
    partial void LogCreatingQueueForKey(string key);

    [LoggerMessage(LogLevel.Warning, "Failed to write to new queues channel for key {QueueKey}")]
    partial void LogFailedToWriteToNewQueuesChannelForKey(string QueueKey);
}
