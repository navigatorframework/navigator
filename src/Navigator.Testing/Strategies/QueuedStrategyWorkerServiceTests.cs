using System.Collections.Concurrent;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Navigator.Abstractions.Introspection;
using Navigator.Abstractions.Introspection.Sink;
using Navigator.Abstractions.Pipelines;
using Navigator.Abstractions.Pipelines.Builder;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Introspection;
using Navigator.Strategies.Queued;
using Navigator.Strategies.Queued.Hosted;
using Navigator.Strategies.Queued.Queues;
using Navigator.Strategies.Queued.Strategy;
using Navigator.Strategy;
using NSubstitute;
using Telegram.Bot.Types;
using Xunit;

namespace Navigator.Testing.Strategies;

/// <summary>
///     Exercises the REAL QueuedStrategyWorkerService against the real QueueManager and the real
///     DefaultNavigationStrategy (with no-op pipelines), so the identifier-forwarding line in
///     ProcessUpdateAsync is covered by an actual dequeue-and-process round trip.
/// </summary>
public class QueuedStrategyWorkerServiceTests
{
    private const int PoisonUpdateId = 666;

    [Fact]
    public async Task Worker_ShouldRootTracesAtTheCarriedIdentifier_AndSurviveProcessingFailures()
    {
        var sink = new CollectingSink();

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton(Options.Create(new QueuedStrategyOptions()));

        services.AddSingleton<INavigatorTracerSink>(sink);
        services.AddScoped<NavigatorTracerContext>();
        services.AddScoped(typeof(INavigatorTracerFactory<>), typeof(NavigatorTracerFactory<>));

        // Pipelines reduced to no-ops: the subject under test is the worker's dequeue → scope →
        // DefaultNavigationStrategy.Invoke(update, identifier) round trip, observed via the tracer.
        var contextBuilder = Substitute.For<INavigatorUpdateContextBuilder>();
        contextBuilder.Build(Arg.Any<Update>()).Returns(callInfo =>
        {
            var update = callInfo.Arg<Update>();

            if (update.Id == PoisonUpdateId)
                throw new InvalidOperationException("poisoned update");

            return ValueTask.FromResult(new NavigatorUpdateContext(update));
        });

        var resolutionPipelineBuilder = Substitute.For<INavigatorResolutionPipelineBuilder>();
        resolutionPipelineBuilder.BuildResolutionPipeline(Arg.Any<NavigatorActionResolutionContext>())
            .Returns(ValueTask.FromResult(new Pipeline(() => Task.CompletedTask, [])));

        services.AddSingleton(contextBuilder);
        services.AddSingleton(resolutionPipelineBuilder);
        services.AddSingleton(Substitute.For<INavigatorExecutionPipelineBuilder>());
        services.AddScoped<DefaultNavigationStrategy>();

        services.AddSingleton<IQueueManager, QueueManager>();

        await using var provider = services.BuildServiceProvider();

        var queueManager = provider.GetRequiredService<IQueueManager>();
        var strategy = new QueuedNavigationStrategy(queueManager);

        using var worker = new QueuedStrategyWorkerService(
            queueManager,
            provider.GetRequiredService<IServiceScopeFactory>(),
            provider.GetRequiredService<ILogger<QueuedStrategyWorkerService>>());

        await worker.StartAsync(CancellationToken.None);

        try
        {
            await strategy.Invoke(MakeUpdate(1, chatId: 42, messageId: 100), "0HNMBNNQTV3F8:00000001");
            await strategy.Invoke(MakeUpdate(PoisonUpdateId, chatId: 42, messageId: 101), "0HNMBNNQTV3F8:00000002");
            await strategy.Invoke(MakeUpdate(3, chatId: 42, messageId: 102), "0HNMBNNQTV3F8:00000003");

            await WaitForRootTraces(sink, expected: 3);
        }
        finally
        {
            await worker.StopAsync(CancellationToken.None);
        }

        var roots = sink.Traces.Where(trace => trace.ParentIdentifier is null).ToList();

        roots.Should().HaveCount(3);

        // Every trace is rooted at the identifier the update was enqueued with,
        // exactly as when the update was processed inline in the webhook request.
        var first = roots.Single(trace => trace.Identifier == "0HNMBNNQTV3F8:00000001");
        first.Tags[NavigatorTraceKeys.UpdateId].Should().ContainSingle().Which.Should().Be("1");
        first.Status.Should().Be(ENavigatorTraceStatus.Ok);

        // The poisoned update still produced a trace (with its own identifier and an error status),
        // and its failure did not kill the queue worker...
        var poisoned = roots.Single(trace => trace.Identifier == "0HNMBNNQTV3F8:00000002");
        poisoned.Status.Should().Be(ENavigatorTraceStatus.Error);

        // ...because the update enqueued after it was processed normally.
        var third = roots.Single(trace => trace.Identifier == "0HNMBNNQTV3F8:00000003");
        third.Tags[NavigatorTraceKeys.UpdateId].Should().ContainSingle().Which.Should().Be("3");
        third.Status.Should().Be(ENavigatorTraceStatus.Ok);
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

    private static async Task WaitForRootTraces(CollectingSink sink, int expected)
    {
        var deadline = DateTimeOffset.UtcNow + TimeSpan.FromSeconds(10);

        while (DateTimeOffset.UtcNow < deadline)
        {
            if (sink.Traces.Count(trace => trace.ParentIdentifier is null) >= expected)
                return;

            await Task.Delay(10);
        }
    }

    private sealed class CollectingSink : INavigatorTracerSink
    {
        public ConcurrentBag<NavigatorTrace> Traces { get; } = [];

        public Task Store(NavigatorTrace trace)
        {
            Traces.Add(trace);
            return Task.CompletedTask;
        }
    }
}
