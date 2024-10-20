using System.Reflection;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Pipelines;
using Navigator.Abstractions.Pipelines.Builder;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Abstractions.Priorities;

namespace Navigator.Pipelines.Builder;

/// <inheritdoc />
public class DefaultNavigatorPipelineBuilder : INavigatorPipelineBuilder
{
    private readonly ILogger<DefaultNavigatorPipelineBuilder> _logger;
    private readonly INavigatorPipelineStep[] _steps;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DefaultNavigatorPipelineBuilder" /> class.
    /// </summary>
    public DefaultNavigatorPipelineBuilder( ILogger<DefaultNavigatorPipelineBuilder> logger, INavigatorPipelineStep[] steps)
    {
        _logger = logger;
        _steps = steps;
    }

    /// <inheritdoc />
    public Pipeline BuildResolutionPipeline(NavigatorActionResolutionContext context)
    {
        _logger.LogInformation("Building resolution pipeline for update {UpdateId}", context.Update.Id);

        var steps = OrderResolutionSteps(_steps);

        PipelineStepHandlerDelegate nextStep = async () => await Task.CompletedTask;

        for (var i = steps.Count - 1; i >= 0; i--)
        {
            var currentStep = steps[i];

            var previousNextstep = nextStep;

            PipelineStepHandlerDelegate next = async () => await currentStep.InvokeAsync(context, previousNextstep);

            nextStep = next;
        }

        _logger.LogInformation("Finished building resolution pipeline for update {UpdateId}", context.Update.Id);

        return new Pipeline(nextStep, steps);
    }

    /// <inheritdoc />
    public Pipeline BuildExecutionPipeline(NavigatorActionExecutionContext context)
    {
        _logger.LogInformation("Building execution pipeline for update {UpdateId} and action {ActionName}", context.Update.Id,
            context.Action.Information.Name);

        var steps = OrderExecutionSteps(_steps);

        PipelineStepHandlerDelegate nextStep = async () => await Task.CompletedTask;

        for (var i = steps.Count - 1; i >= 0; i--)
        {
            var currentStep = steps[i];

            var previousNextstep = nextStep;

            PipelineStepHandlerDelegate next = async () => await currentStep.InvokeAsync(context, previousNextstep);

            nextStep = next;
        }

        _logger.LogInformation("Finished building resolution pipeline for update {UpdateId} and action {ActionName}", context.Update.Id,
            context.Action.Information.Name);

        return new Pipeline(nextStep, steps);
    }

    private static List<IActionResolutionPipelineStep> OrderResolutionSteps(INavigatorPipelineStep[] steps)
    {
        var list = new List<IActionResolutionPipelineStep>();

        list.AddRange(steps
            .OfType<IActionResolutionPipelineStepBefore<IActionResolutionPipelineStep>>()
            .OrderBy(step => step.GetType().GetCustomAttribute<PriorityAttribute>()?.Level ?? EPriority.Normal));

        list.Add(steps.OfType<IActionResolutionPipelineStep>().First());

        list.AddRange(steps
            .OfType<IActionResolutionPipelineStepAfter<IActionResolutionPipelineStep>>()
            .OrderBy(step => step.GetType().GetCustomAttribute<PriorityAttribute>()?.Level ?? EPriority.Normal));

        return list;
    }

    private static List<IActionExecutionPipelineStep> OrderExecutionSteps(INavigatorPipelineStep[] steps)
    {
        var list = new List<IActionExecutionPipelineStep>();

        list.AddRange(steps
            .OfType<IActionExecutionPipelineStepBefore<IActionExecutionPipelineStep>>()
            .OrderBy(step => step.GetType().GetCustomAttribute<PriorityAttribute>()?.Level ?? EPriority.Normal));

        list.Add(steps.OfType<IActionExecutionPipelineStep>().First());

        list.AddRange(steps
            .OfType<IActionExecutionPipelineStepAfter<IActionExecutionPipelineStep>>()
            .OrderBy(step => step.GetType().GetCustomAttribute<PriorityAttribute>()?.Level ?? EPriority.Normal));

        return list;
    }
}