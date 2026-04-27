using FluentAssertions;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Introspection;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Pipelines.Builder;
using NSubstitute;
using Telegram.Bot.Types;
using Xunit;

namespace Navigator.Testing.Pipelines.Builder;

public class NavigatorPipelineBuilderTests
{
    private readonly INavigatorTracerFactory<DefaultNavigatorResolutionPipelineBuilder> _resolutionTracerFactory;
    private readonly INavigatorTracerFactory<DefaultNavigatorExecutionPipelineBuilder> _executionTracerFactory;

    public readonly INavigatorPipelineStep[] Steps =
    [
        new ShouldGoFifth(),
        new ShouldGoFirst(),
        new ShouldGoFourth(),
        new ShouldGoSixth(),
        new ShouldGoSecond(),
        new ShouldGoThird()
    ];

    public NavigatorPipelineBuilderTests()
    {
        var tracer = Substitute.For<INavigatorTracer>();

        _resolutionTracerFactory = Substitute.For<INavigatorTracerFactory<DefaultNavigatorResolutionPipelineBuilder>>();
        _resolutionTracerFactory.Get(Arg.Any<string?>()).Returns(tracer);
        _resolutionTracerFactory.HasActiveTrace().Returns(false);

        _executionTracerFactory = Substitute.For<INavigatorTracerFactory<DefaultNavigatorExecutionPipelineBuilder>>();
        _executionTracerFactory.Get(Arg.Any<string?>()).Returns(tracer);
        _executionTracerFactory.HasActiveTrace().Returns(false);
    }

    [Fact]
    public async Task ShouldBuildResolutionPipelineInOrder()
    {
        var builder = new DefaultNavigatorResolutionPipelineBuilder(
            Substitute.For<ILogger<DefaultNavigatorResolutionPipelineBuilder>>(),
            _resolutionTracerFactory,
            Steps);

        var context = new NavigatorActionResolutionContext(new NavigatorUpdateContext(new Update()));

        var pipeline = await builder.BuildResolutionPipeline(context);

        pipeline.OrderedSteps.Should().HaveCount(3);

        pipeline.OrderedSteps[0].Should().BeOfType<ShouldGoFirst>();
        pipeline.OrderedSteps[1].Should().BeOfType<ShouldGoSecond>();
        pipeline.OrderedSteps[2].Should().BeOfType<ShouldGoThird>();
    }

    [Fact]
    public async Task ShouldExecuteAllStepsInResolutionPipeline()
    {
        var builder = new DefaultNavigatorResolutionPipelineBuilder(
            Substitute.For<ILogger<DefaultNavigatorResolutionPipelineBuilder>>(),
            _resolutionTracerFactory,
            Steps);

        var context = new NavigatorActionResolutionContext(new NavigatorUpdateContext(new Update()));

        var pipeline = await builder.BuildResolutionPipeline(context);

        await pipeline.InvokeAsync();

        context.Items.Should().HaveCount(3);
    }

    [Fact]
    public async Task ShouldBuildExecutionPipelineInOrder()
    {
        var builder = new DefaultNavigatorExecutionPipelineBuilder(
            Substitute.For<ILogger<DefaultNavigatorExecutionPipelineBuilder>>(),
            _executionTracerFactory,
            Steps);

        var context = new NavigatorActionResolutionContext(new NavigatorUpdateContext(new Update()));
        context.Actions.Add(new BotAction(Guid.NewGuid(), Substitute.For<BotActionInformation>(), () => true, () => Task.CompletedTask));

        var executionContext = context.GetExecutionContexts().First();

        var pipeline = await builder.BuildExecutionPipeline(executionContext);

        pipeline.OrderedSteps.Should().HaveCount(3);

        pipeline.OrderedSteps[0].Should().BeOfType<ShouldGoFourth>();
        pipeline.OrderedSteps[1].Should().BeOfType<ShouldGoFifth>();
        pipeline.OrderedSteps[2].Should().BeOfType<ShouldGoSixth>();
    }

    [Fact]
    public async Task ShouldExecuteAllStepsInExecutionPipeline()
    {
        var builder = new DefaultNavigatorExecutionPipelineBuilder(
            Substitute.For<ILogger<DefaultNavigatorExecutionPipelineBuilder>>(),
            _executionTracerFactory,
            Steps);

        var context = new NavigatorActionResolutionContext(new NavigatorUpdateContext(new Update()));
        context.Actions.Add(new BotAction(Guid.NewGuid(), Substitute.For<BotActionInformation>(), () => true, () => Task.CompletedTask));

        var executionContext = context.GetExecutionContexts().First();

        var pipeline = await builder.BuildExecutionPipeline(executionContext);

        await pipeline.InvokeAsync();

        executionContext.Items.Should().HaveCount(3);
    }
}

internal record ShouldGoFirst : IActionResolutionPipelineStepBefore
{
    public async Task InvokeAsync(NavigatorActionResolutionContext context, PipelineStepHandlerDelegate next)
    {
        context.Items.Add(nameof(ShouldGoFirst), nameof(ShouldGoFirst));
        await next();
    }
}

internal record ShouldGoSecond : IActionResolutionMainStep
{
    public async Task InvokeAsync(NavigatorActionResolutionContext context, PipelineStepHandlerDelegate next)
    {
        context.Items.Add(nameof(ShouldGoSecond), nameof(ShouldGoSecond));
        await next();
    }
}

internal record ShouldGoThird : IActionResolutionPipelineStepAfter
{
    public async Task InvokeAsync(NavigatorActionResolutionContext context, PipelineStepHandlerDelegate next)
    {
        context.Items.Add(nameof(ShouldGoThird), nameof(ShouldGoThird));
        await next();
    }
}

internal record ShouldGoFourth : IActionExecutionPipelineStepBefore
{
    public async Task InvokeAsync(NavigatorActionExecutionContext context, PipelineStepHandlerDelegate next)
    {
        context.Items.Add(nameof(ShouldGoFourth), nameof(ShouldGoFourth));
        await next();
    }
}

internal record ShouldGoFifth : IActionExecutionMainStep
{
    public async Task InvokeAsync(NavigatorActionExecutionContext context, PipelineStepHandlerDelegate next)
    {
        context.Items.Add(nameof(ShouldGoFifth), nameof(ShouldGoFifth));
        await next();
    }
}

internal record ShouldGoSixth : IActionExecutionPipelineStepAfter
{
    public async Task InvokeAsync(NavigatorActionExecutionContext context, PipelineStepHandlerDelegate next)
    {
        context.Items.Add(nameof(ShouldGoSixth), nameof(ShouldGoSixth));
        await next();
    }
}
