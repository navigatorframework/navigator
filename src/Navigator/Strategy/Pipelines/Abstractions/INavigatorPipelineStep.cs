using Navigator.Strategy.Context;

namespace Navigator.Strategy.Pipelines.Abstractions;

/// <summary>
///     Interface for a pipeline step.
/// </summary>
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

public interface IActionResolutionPipelineStep : INavigatorPipelineStep<NavigatorStrategyContext>;

public interface IActionResolutionPipelineStepBefore<T> : IActionResolutionPipelineStep;

public interface IActionResolutionPipelineStepAfter<T> : IActionResolutionPipelineStep;

public interface IActionExecutionPipelineStep : INavigatorPipelineStep<NavigatorActionExecutionContext>;

public interface IActionExecutionPipelineStepBefore<T> : IActionExecutionPipelineStep;

public interface IActionExecutionPipelineStepAfter<T> : IActionExecutionPipelineStep;