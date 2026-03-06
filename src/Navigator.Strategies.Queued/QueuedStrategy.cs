using Microsoft.Extensions.DependencyInjection;
using Navigator.Abstractions.Strategies;
using Navigator.Configuration.Options;
using Navigator.Strategies.Queued.Hosted;
using Navigator.Strategies.Queued.Queues;
using Navigator.Strategies.Queued.Strategy;

namespace Navigator.Strategies.Queued;

/// <summary>
///     Strategy definition for the queued navigation strategy.
///     Registers the queue manager, hosted worker service, and the enqueue-only strategy.
/// </summary>
public class QueuedStrategy : INavigatorStrategyDefinition, INavigatorStrategyDefinition<QueuedStrategyOptions>
{
    /// <inheritdoc />
    public void Configure(IServiceCollection services, NavigatorOptions options)
    {
        Configure(services, options, new QueuedStrategyOptions());
    }

    /// <inheritdoc />
    public void Configure(IServiceCollection services, NavigatorOptions navigatorOptions, QueuedStrategyOptions strategyOptions)
    {
        services.Configure<QueuedStrategyOptions>(opts =>
        {
            opts.MaxMessagesPerQueue = strategyOptions.MaxMessagesPerQueue;
        });

        services.AddSingleton<IQueueManager, QueueManager>();
        services.AddSingleton<INavigatorStrategy, QueuedNavigationStrategy>();
        services.AddHostedService<QueuedStrategyWorkerService>();
    }
}
