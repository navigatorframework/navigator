using Navigator.Abstractions.Pipelines.Context;

namespace Navigator.Abstractions.Pipelines.Builder;

/// <summary>
///     Builder for a navigator action execution pipeline.
/// </summary>
public interface INavigatorExecutionPipelineBuilder
{
    /// <summary>
    ///     Builds a navigator pipeline for executing actions.
    /// </summary>
    /// <param name="context">An instance of <see cref="NavigatorActionExecutionContext" />.</param>
    /// <returns>A built <see cref="Pipeline" />.</returns>
    ValueTask<Pipeline> BuildExecutionPipeline(NavigatorActionExecutionContext context);
}
