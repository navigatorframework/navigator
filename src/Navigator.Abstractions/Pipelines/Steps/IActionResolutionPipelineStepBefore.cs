namespace Navigator.Abstractions.Pipelines.Steps;

public interface IActionResolutionPipelineStepBefore<T> : IActionResolutionPipelineStep;

public interface IActionResolutionPipelineStepBefore : IActionResolutionPipelineStepBefore<IActionResolutionPipelineStep>;