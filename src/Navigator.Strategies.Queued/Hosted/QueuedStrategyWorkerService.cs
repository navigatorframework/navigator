using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Navigator.Strategies.Queued.Queues;
using Navigator.Strategy;
using Telegram.Bot.Types;

namespace Navigator.Strategies.Queued.Hosted;

/// <summary>
///     Hosted service that listens for newly created queues on the
///     <see cref="IQueueManager" /> control channel and starts a
///     dedicated worker task for each one.
/// </summary>
public sealed class QueuedStrategyWorkerService : BackgroundService
{
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
        _queueManager = queueManager;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var workerTasks = new List<Task>();

        try
        {
            var reader = _queueManager.NewQueues;

            while (await reader.WaitToReadAsync(stoppingToken))
            {
                while (reader.TryRead(out var queue))
                {
                    _logger.LogDebug("Starting worker for queue {QueueKey}", queue.Key);
                    workerTasks.Add(RunWorkerAsync(queue, stoppingToken));
                }
            }
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            _logger.LogDebug("Queue worker service stopping");
        }
        finally
        {
            _queueManager.CompleteAll();

            if (workerTasks.Count > 0)
                await Task.WhenAll(workerTasks);
        }
    }

    private async Task RunWorkerAsync(UpdateQueue queue, CancellationToken cancellationToken)
    {
        var reader = queue.Channel.Reader;

        try
        {
            while (await reader.WaitToReadAsync(cancellationToken))
            {
                while (reader.TryRead(out var update))
                {
                    await ProcessUpdateAsync(queue.Key, update);
                }
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            _logger.LogDebug("Worker for queue {QueueKey} cancelled", queue.Key);
        }
        finally
        {
            _queueManager.RemoveQueue(queue.Key);
        }
    }

    private async Task ProcessUpdateAsync(string queueKey, Update update)
    {
        try
        {
            await using var scope = _scopeFactory.CreateAsyncScope();
            var strategy = scope.ServiceProvider.GetRequiredService<DefaultNavigationStrategy>();
            await strategy.Invoke(update);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error processing update {UpdateId} in queue {QueueKey}",
                update.Id, queueKey);
        }
    }
}
