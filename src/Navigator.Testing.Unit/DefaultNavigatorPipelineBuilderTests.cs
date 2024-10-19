using Microsoft.Extensions.Logging;
using Navigator.Actions;
using Navigator.Strategy.Context;
using Navigator.Strategy.Pipelines.Abstractions;
using Navigator.Strategy.Pipelines.Bundled;
using NSubstitute;
using Telegram.Bot.Types;
using Xunit;

namespace Navigator.Testing.Unit;

public class DefaultNavigatorPipelineBuilderTests
{
    public readonly INavigatorPipelineStep[] Steps =
    [
        new ActionResolutionStep(),
        new ActionExecutionStep()
    ];

    [Fact]
    public async Task ShouldBuildResolutionPipeline()
    {
        var builder = new DefaultNavigatorPipelineBuilder(Steps, Substitute.For<ILogger<DefaultNavigatorPipelineBuilder>>());

        var context = new NavigatorStrategyContext(new Update());
        
        var pipeline = builder.BuildResolutionPipeline(context);

        await pipeline();
        
        Assert.Single(context.Items);
    }
    [Fact]
    public async Task ShouldBuildExecutionPipeline()
    {
        var builder = new DefaultNavigatorPipelineBuilder(Steps, Substitute.For<ILogger<DefaultNavigatorPipelineBuilder>>());

        var context = new NavigatorStrategyContext(new Update());
        context.Actions.Add(new BotAction(Guid.NewGuid(), Substitute.For<BotActionInformation>(), () => true, () => Task.CompletedTask));

        var executionContext = context.GetExecutionContexts().First();
        
        var pipeline = builder.BuildExecutionPipeline(executionContext);

        await pipeline();
        
        Assert.Equal(2, executionContext.Items.First().Value);
    }
}


internal class ActionResolutionStep : IActionResolutionPipelineStep
{
    public async Task InvokeAsync(NavigatorStrategyContext context, PipelineStepHandlerDelegate next)
    {
        context.Items.Add(1, 1);
        await next();
    }
}

internal class ActionExecutionStep : IActionExecutionPipelineStep
{
    public async Task InvokeAsync(NavigatorActionExecutionContext context, PipelineStepHandlerDelegate next)
    {
        context.Items.Add(2, 2);
        await next();
    }
}