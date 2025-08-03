namespace Navigator.Abstractions.Pipelines.Steps;

/// <summary>
///     Interface for a step that is executed before an action execution pipeline step.
/// </summary>
public interface IActionExecutionPipelineStepBefore<T> : IActionExecutionPipelineStep;

/// <summary>
///     Interface for a step that is executed before an action execution pipeline step.
/// </summary>
public interface IActionExecutionPipelineStepBefore : IActionExecutionPipelineStepBefore<IActionExecutionPipelineStep>;