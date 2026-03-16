using Navigator.Abstractions.Introspection;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Pipelines;
using Navigator.Abstractions.Pipelines.Builder;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;

namespace Navigator.Pipelines.Builder;

/// <inheritdoc />
public class DefaultNavigatorResolutionPipelineBuilder : INavigatorResolutionPipelineBuilder
{
    private readonly ILogger<DefaultNavigatorResolutionPipelineBuilder> _logger;
    private readonly INavigatorTracerFactory<DefaultNavigatorResolutionPipelineBuilder> _tracerFactory;
    private readonly INavigatorPipelineStep[] _steps;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DefaultNavigatorResolutionPipelineBuilder" /> class.
    /// </summary>
    public DefaultNavigatorResolutionPipelineBuilder(
        ILogger<DefaultNavigatorResolutionPipelineBuilder> logger,
        INavigatorTracerFactory<DefaultNavigatorResolutionPipelineBuilder> tracerFactory,
        IEnumerable<INavigatorPipelineStep> steps)
    {
        _logger = logger;
        _tracerFactory = tracerFactory;
        _steps = steps.ToArray();
    }

    /// <inheritdoc />
    public async ValueTask<Pipeline> BuildResolutionPipeline(NavigatorActionResolutionContext context)
    {
        await using var tracer = _tracerFactory.Get();
        
        _logger.LogDebug("Building resolution pipeline for update {UpdateId}", context.UpdateContext.Update.Id);

        var steps = NavigatorPipelineBuilderHelpers.OrderSteps<
            IActionResolutionPipelineStep,
            IActionResolutionMainStep,
            IActionResolutionPipelineStepBefore,
            IActionResolutionPipelineStepAfter>(_steps);

        var pipeline = NavigatorPipelineBuilderHelpers.ComposePipeline(context, steps);

        _logger.LogDebug("Finished building resolution pipeline for update {UpdateId}", context.UpdateContext.Update.Id);

        return pipeline;
    }
}
