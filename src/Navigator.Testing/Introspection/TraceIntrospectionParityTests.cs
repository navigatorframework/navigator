using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Navigator.Abstractions.Introspection;
using Navigator.Introspection;
using Navigator.Introspection.Sink;
using Navigator.Strategies.Queued;
using Navigator.Strategies.Queued.Queues;
using Navigator.Strategies.Queued.Strategy;
using Navigator.Strategies.Queued.Telegram;
using NSubstitute;
using Telegram.Bot.Types;
using Xunit;

namespace Navigator.Testing.Introspection;

/// <summary>
///     End-to-end parity test for /debug traces under the queued strategy: an update enqueued
///     with the webhook's request identifier must, after background processing, yield the same
///     trace tree that inline processing produced (root identified by the request identifier,
///     nested spans linked through parent identifiers, retrievable by chat and message id).
/// </summary>
public class TraceIntrospectionParityTests
{
    private const string RequestIdentifier = "0HNMBNNQTV3F8:00000001";
    private const long ChatId = -1001770877590;
    private const int IncomingMessageId = 114437;
    private const int SentMessageId = 114440;

    [Fact]
    public async Task QueuedUpdate_ShouldProduceTheSameTraceTreeAsInlineProcessing()
    {
        // Shared infrastructure, as in production: singleton sink over a singleton cache.
        var sink = new MemoryCacheNavigatorTracerSink(new MemoryCache(new MemoryCacheOptions()));

        // 1. The webhook enqueues the update with the request's trace identifier.
        var queueManager = new QueueManager(
            Options.Create(new QueuedStrategyOptions()),
            Substitute.For<ILogger<QueueManager>>());
        var strategy = new QueuedNavigationStrategy(queueManager);

        var update = new Update
        {
            Id = 424841378,
            Message = new Message
            {
                Id = IncomingMessageId,
                Chat = new Chat { Id = ChatId }
            }
        };

        await strategy.Invoke(update, RequestIdentifier);

        // 2. The worker dequeues it and processes it in its own scope, on a worker thread.
        var queued = await queueManager.GetQueueReader(update.GetQueueKey())!.ReadAsync();

        await Task.Run(() => SimulateDefaultStrategyProcessing(sink, queued));

        // 3. /debug replies to the bot's message and rebuilds the full trace tree.
        var traces = await sink.RetrieveByChatAndMessage(ChatId, SentMessageId, findRoot: true);

        traces.Should().HaveCount(1);

        var root = traces.Single();
        root.Trace.Identifier.Should().Be(RequestIdentifier, "the trace must stay rooted at the webhook request identifier");
        root.Trace.ParentIdentifier.Should().BeNull();
        root.Trace.Tags[NavigatorTraceKeys.UpdateChatId].Should().ContainSingle().Which.Should().Be($"{ChatId}");
        root.Trace.Tags[NavigatorTraceKeys.UpdateMessageId].Should().ContainSingle().Which.Should().Be($"{IncomingMessageId}");

        // Resolution and execution spans hang directly off the root, like in the inline traces.
        root.InnerTraces.Should().HaveCount(2);

        var resolution = root.InnerTraces.Single(entry =>
            entry.Trace.SourceContext.Contains("ResolutionPipeline", StringComparison.Ordinal));
        resolution.InnerTraces.Should().BeEmpty();

        var execution = root.InnerTraces.Single(entry =>
            entry.Trace.SourceContext.Contains("ExecutionMainStep", StringComparison.Ordinal));

        var handler = execution.InnerTraces.Should().ContainSingle().Subject;
        handler.Trace.SourceContext.Should().Contain("Handler");

        // The handler's two client calls are siblings under the handler, even though they ran in parallel.
        handler.InnerTraces.Should().HaveCount(2);
        handler.InnerTraces.Should().AllSatisfy(entry =>
        {
            entry.Trace.SourceContext.Should().Contain("ClientCategory");
            entry.Trace.ParentIdentifier.Should().Be(handler.Trace.Identifier);
        });
    }

    /// <summary>
    ///     Reproduces the tracer usage pattern of <c>DefaultNavigationStrategy</c> and its pipeline:
    ///     a root tracer opened with the carried identifier, nested spans opened by awaited child
    ///     calls, and two client calls running in parallel inside the handler.
    /// </summary>
    private static async Task SimulateDefaultStrategyProcessing(MemoryCacheNavigatorTracerSink sink, QueuedUpdate queued)
    {
        var context = new NavigatorTracerContext(Substitute.For<ILogger<NavigatorTracerContext>>());
        var strategyTracerFactory = new NavigatorTracerFactory<StrategyCategory>(sink, context);
        var resolutionTracerFactory = new NavigatorTracerFactory<ResolutionPipelineCategory>(sink, context);
        var executionTracerFactory = new NavigatorTracerFactory<ExecutionMainStepCategory>(sink, context);
        var handlerTracerFactory = new NavigatorTracerFactory<FallbackHandlerCategory>(sink, context);
        var clientTracerFactory = new NavigatorTracerFactory<ClientCategory>(sink, context);

        await using var rootTracer = strategyTracerFactory.Get(queued.Identifier);
        rootTracer.AddTag(NavigatorTraceKeys.UpdateId, $"{queued.Update.Id}");
        rootTracer.AddTag(NavigatorTraceKeys.UpdateChatId, $"{queued.Update.Message!.Chat.Id}");
        rootTracer.AddTag(NavigatorTraceKeys.UpdateMessageId, $"{queued.Update.Message.Id}");

        await RunResolutionPipeline(resolutionTracerFactory);
        await RunExecutionPipeline(executionTracerFactory, handlerTracerFactory, clientTracerFactory);
    }

    private static async Task RunResolutionPipeline(NavigatorTracerFactory<ResolutionPipelineCategory> tracerFactory)
    {
        await using var tracer = tracerFactory.Get();
        await Task.Yield();
    }

    private static async Task RunExecutionPipeline(
        NavigatorTracerFactory<ExecutionMainStepCategory> executionTracerFactory,
        NavigatorTracerFactory<FallbackHandlerCategory> handlerTracerFactory,
        NavigatorTracerFactory<ClientCategory> clientTracerFactory)
    {
        await using var executionTracer = executionTracerFactory.Get();
        await using var handlerTracer = handlerTracerFactory.Get();

        await Task.WhenAll(
            SimulateClientCall(clientTracerFactory, "POST: sendChatAction"),
            SimulateClientCall(clientTracerFactory, "POST: sendMessage", ChatId, SentMessageId));
    }

    private static async Task SimulateClientCall(NavigatorTracerFactory<ClientCategory> tracerFactory,
        string apiCall, long? chatId = null, int? messageId = null)
    {
        await using var tracer = tracerFactory.Get();
        tracer.AddTag(NavigatorTraceKeys.TelegramApiCall, apiCall);

        if (chatId is not null)
            tracer.AddTag(NavigatorTraceKeys.UpdateChatId, $"{chatId}");

        if (messageId is not null)
            tracer.AddTag(NavigatorTraceKeys.UpdateMessageId, $"{messageId}");

        await Task.Yield();
    }

    private sealed class StrategyCategory;

    private sealed class ResolutionPipelineCategory;

    private sealed class ExecutionMainStepCategory;

    private sealed class FallbackHandlerCategory;

    private sealed class ClientCategory;
}
