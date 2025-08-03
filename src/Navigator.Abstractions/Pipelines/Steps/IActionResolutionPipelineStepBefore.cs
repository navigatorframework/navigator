namespace Navigator.Abstractions.Pipelines.Steps;

/// <summary>
///     Interface for a step that is executed before an action resolution pipeline step.
/// </summary>
public interface IActionResolutionPipelineStepBefore<T> : IActionResolutionPipelineStep;

/// <summary>
///     Interface for a step that is executed before an action resolution pipeline step.
/// </summary>
public interface IActionResolutionPipelineStepBefore : IActionResolutionPipelineStepBefore<IActionResolutionPipelineStep>;