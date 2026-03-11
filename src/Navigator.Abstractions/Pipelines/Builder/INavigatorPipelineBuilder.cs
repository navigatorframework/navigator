using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;

namespace Navigator.Abstractions.Pipelines.Builder;

/// <summary>
///     Builder for a navigator pipeline.
/// </summary>
public interface INavigatorPipelineBuilder
{
    /// <summary>
    ///     Builds a navigator pipeline for resolving actions.
    /// </summary>
    /// <param name="context">An instance of <see cref="NavigatorActionResolutionContext" />.</param>
    /// <returns>A built <see cref="Pipeline" />.</returns>
    ValueTask<Pipeline> BuildResolutionPipeline(NavigatorActionResolutionContext context);

    /// <summary>
    ///     Builds a navigator pipeline for executing actions.
    /// </summary>
    /// <param name="context">An instance of <see cref="NavigatorActionExecutionContext" />.</param>
    /// <returns>A built <see cref="Pipeline" />.</returns>
    ValueTask<Pipeline> BuildExecutionPipeline(NavigatorActionExecutionContext context);
}