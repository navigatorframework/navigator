using System.Threading.Channels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Navigator.Strategies.Queued.Queues;
using Navigator.Strategy;

namespace Navigator.Strategies.Queued.Hosted;

/// <summary>
///     Hosted service that listens for newly created queues on the
///     <see cref="IQueueManager" /> control channel and starts a
///     dedicated worker task for each one.
/// </summary>
public sealed class QueuedStrategyWorkerService : BackgroundService
{
    private readonly Dictionary<string, Task> _activeWorkers;
    private readonly IQueueManager _queueManager;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<QueuedStrategyWorkerService> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueuedStrategyWorkerService" /> class.
    /// </summary>
    /// <param name="queueManager">The queue manager that provides new-queue notifications.</param>
    /// <param name="scopeFactory">Factory used to create DI scopes for each dequeued update.</param>
    /// <param name="logger">Logger instance.</param>
    public QueuedStrategyWorkerService(
        IQueueManager queueManager,
        IServiceScopeFactory scopeFactory,
        ILogger<QueuedStrategyWorkerService> logger)
    {
        _activeWorkers = new Dictionary<string, Task>();
        _queueManager = queueManager;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var reader = _queueManager.NewQueuesReader;

            await foreach (var queueKey in reader.ReadAllAsync(stoppingToken))
            {
                if (_activeWorkers.ContainsKey(queueKey))
                {
                    continue;
                }

                _logger.LogDebug("Starting worker for queue {QueueKey}", queueKey);

                var queue = _queueManager.GetQueueReader(queueKey);
                if (queue is not null)
                {
                    _activeWorkers[queueKey] = RunWorkerAsync(queueKey, queue, stoppingToken);
                }
            }
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Queue worker service stopping");
        }
    }

    private async Task RunWorkerAsync(string queueKey, ChannelReader<QueuedUpdate> queueReader, CancellationToken cancellationToken)
    {
        try
        {
            await foreach (var queuedUpdate in queueReader.ReadAllAsync(cancellationToken))
            {
                await ProcessUpdateAsync(queuedUpdate);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker for queue {QueueKey} cancelled", queueKey);
        }
    }

    private async Task ProcessUpdateAsync(QueuedUpdate queuedUpdate)
    {
        try
        {
            await using var scope = _scopeFactory.CreateAsyncScope();
            var strategy = scope.ServiceProvider.GetRequiredService<DefaultNavigationStrategy>();
            await strategy.Invoke(queuedUpdate.Update, queuedUpdate.Identifier);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error processing update {UpdateId}", queuedUpdate.Update.Id);
        }
    }
}
