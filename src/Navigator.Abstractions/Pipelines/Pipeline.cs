using Navigator.Abstractions.Pipelines.Steps;

namespace Navigator.Abstractions.Pipelines;

/// <summary>
///     Represents a built navigator pipeline.
/// </summary>
public record Pipeline
{
    private readonly PipelineStepHandlerDelegate _pipelineDelegate;

    /// <summary>
    ///     The steps of the pipeline, in order of execution.
    /// </summary>
    public readonly INavigatorPipelineStep[] OrderedSteps;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Pipeline" /> class.
    /// </summary>
    public Pipeline(PipelineStepHandlerDelegate pipelineDelegate, IEnumerable<INavigatorPipelineStep> orderedSteps)
    {
        _pipelineDelegate = pipelineDelegate;
        OrderedSteps = orderedSteps.ToArray();
    }

    /// <summary>
    ///     Invokes the pipeline.
    /// </summary>
    /// <returns>An awaitable task.</returns>
    public Task InvokeAsync() => _pipelineDelegate();
}