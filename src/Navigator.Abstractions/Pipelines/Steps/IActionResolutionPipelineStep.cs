using Navigator.Abstractions.Pipelines.Context;

namespace Navigator.Abstractions.Pipelines.Steps;

/// <summary>
///     An action resolution pipeline step.
/// </summary>
public interface IActionResolutionPipelineStep : INavigatorPipelineStep<NavigatorActionResolutionContext>;