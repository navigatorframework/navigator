namespace Navigator.Abstractions.Pipelines.Steps;


/// <summary>
///     Interface for a step that is executed after an action execution pipeline step.
/// </summary>
public interface IActionExecutionPipelineStepAfter<T> : IActionExecutionPipelineStep;

/// <summary>
///     Interface for a step that is executed after an action execution pipeline step.
/// </summary>
public interface IActionExecutionPipelineStepAfter : IActionExecutionPipelineStepAfter<IActionExecutionPipelineStep>;