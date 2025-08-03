namespace Navigator.Abstractions.Pipelines.Steps;

/// <summary>
///     Interface for a step that is executed after an action resolution pipeline step.
/// </summary>
public interface IActionResolutionPipelineStepAfter<T> : IActionResolutionPipelineStep;

/// <summary>
///     Interface for a step that is executed after an action resolution pipeline step.
/// </summary>
public interface IActionResolutionPipelineStepAfter : IActionResolutionPipelineStepAfter<IActionResolutionPipelineStep>;