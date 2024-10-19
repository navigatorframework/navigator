using Navigator.Abstractions.Priorities;

namespace Navigator.Abstractions.Pipelines.Steps;

/// <summary>
///     Interface for a pipeline step.
/// </summary>
[Priority(EPriority.Normal)]
public interface INavigatorPipelineStep;

/// <summary>
///     Interface for a pipeline step with a context and an invoke method.
/// </summary>
/// <typeparam name="TContext"></typeparam>
public interface INavigatorPipelineStep<in TContext> : INavigatorPipelineStep
{
    /// <summary>
    ///     Invokes the pipeline step.
    /// </summary>
    /// <param name="context">The <see cref="NavigatorStrategyContext" /> object.</param>
    /// <param name="next">The next step in the process.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    Task InvokeAsync(TContext context, PipelineStepHandlerDelegate next);
}