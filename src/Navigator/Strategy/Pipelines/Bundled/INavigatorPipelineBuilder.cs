using Navigator.Strategy.Context;
using Navigator.Strategy.Pipelines.Abstractions;

namespace Navigator.Strategy.Pipelines.Bundled;

/// <summary>
///     Builder for a navigator pipeline.
/// </summary>
public interface INavigatorPipelineBuilder
{
    /// <summary>
    ///     Builds a navigator pipeline for resolving actions.
    /// </summary>
    /// <param name="context">An instance of <see cref="NavigatorStrategyContext" />.</param>
    /// <returns>A <see cref="PipelineStepHandlerDelegate" />.</returns>
    PipelineStepHandlerDelegate BuildResolutionPipeline(NavigatorStrategyContext context);

    /// <summary>
    ///     Builds a navigator pipeline for executing actions.
    /// </summary>
    /// <param name="context">An instance of <see cref="NavigatorStrategyContext" />.</param>
    /// <returns>A <see cref="PipelineStepHandlerDelegate" />.</returns>
    PipelineStepHandlerDelegate BuildExecutionPipeline(NavigatorActionExecutionContext context);
}