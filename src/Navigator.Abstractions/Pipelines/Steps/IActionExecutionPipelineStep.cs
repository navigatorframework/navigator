using Navigator.Abstractions.Pipelines.Context;

namespace Navigator.Abstractions.Pipelines.Steps;

/// <summary>
///     An action execution pipeline step.
/// </summary>
public interface IActionExecutionPipelineStep : INavigatorPipelineStep<NavigatorActionExecutionContext>;