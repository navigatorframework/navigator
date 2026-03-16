using Navigator.Abstractions.Pipelines.Context;

namespace Navigator.Abstractions.Pipelines.Builder;

/// <summary>
///     Builder for a navigator action resolution pipeline.
/// </summary>
public interface INavigatorResolutionPipelineBuilder
{
    /// <summary>
    ///     Builds a navigator pipeline for resolving actions.
    /// </summary>
    /// <param name="context">An instance of <see cref="NavigatorActionResolutionContext" />.</param>
    /// <returns>A built <see cref="Pipeline" />.</returns>
    ValueTask<Pipeline> BuildResolutionPipeline(NavigatorActionResolutionContext context);
}
