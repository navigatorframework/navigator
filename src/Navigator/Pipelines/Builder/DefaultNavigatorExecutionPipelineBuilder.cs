using Navigator.Abstractions.Introspection;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Pipelines;
using Navigator.Abstractions.Pipelines.Builder;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;

namespace Navigator.Pipelines.Builder;

/// <inheritdoc />
public class DefaultNavigatorExecutionPipelineBuilder : INavigatorExecutionPipelineBuilder
{
    private readonly ILogger<DefaultNavigatorExecutionPipelineBuilder> _logger;
    private readonly INavigatorTracerFactory<DefaultNavigatorExecutionPipelineBuilder> _tracerFactory;
    private readonly INavigatorPipelineStep[] _steps;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DefaultNavigatorExecutionPipelineBuilder" /> class.
    /// </summary>
    public DefaultNavigatorExecutionPipelineBuilder(
        ILogger<DefaultNavigatorExecutionPipelineBuilder> logger,
        INavigatorTracerFactory<DefaultNavigatorExecutionPipelineBuilder> tracerFactory,
        IEnumerable<INavigatorPipelineStep> steps)
    {
        _logger = logger;
        _tracerFactory = tracerFactory;
        _steps = steps.ToArray();
    }

    /// <inheritdoc />
    public async ValueTask<Pipeline> BuildExecutionPipeline(NavigatorActionExecutionContext context)
    {
        await using var tracer = _tracerFactory.Get();
        
        _logger.LogDebug("Building execution pipeline for update {UpdateId} and action {ActionName}", 
            context.UpdateContext.Update.Id, context.Action.Information.Name);

        tracer.AddTag(NavigatorTraceKeys.ActionName, context.Action.Information.Name);
        
        var steps = NavigatorPipelineBuilderHelpers.OrderSteps<
            IActionExecutionPipelineStep,
            IActionExecutionMainStep,
            IActionExecutionPipelineStepBefore,
            IActionExecutionPipelineStepAfter>(_steps);

        var pipeline = NavigatorPipelineBuilderHelpers.ComposePipeline(context, steps);

        _logger.LogDebug("Finished building execution pipeline for update {UpdateId} and action {ActionName}", 
            context.UpdateContext.Update.Id, context.Action.Information.Name);

        return pipeline;
    }
}
