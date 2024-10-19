using Navigator.Abstractions.Pipelines.Context;

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
    /// <returns>A <see cref="PipelineStepHandlerDelegate" />.</returns>
    PipelineStepHandlerDelegate BuildResolutionPipeline(NavigatorActionResolutionContext context);

    /// <summary>
    ///     Builds a navigator pipeline for executing actions.
    /// </summary>
    /// <param name="context">An instance of <see cref="NavigatorActionResolutionContext" />.</param>
    /// <returns>A <see cref="PipelineStepHandlerDelegate" />.</returns>
    PipelineStepHandlerDelegate BuildExecutionPipeline(NavigatorActionExecutionContext context);
}