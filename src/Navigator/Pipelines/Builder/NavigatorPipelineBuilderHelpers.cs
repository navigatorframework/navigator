using System.Reflection;
using Navigator.Abstractions.Pipelines;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Abstractions.Priorities;

namespace Navigator.Pipelines.Builder;

internal static class NavigatorPipelineBuilderHelpers
{
    public static Pipeline ComposePipeline<TContext, TStep>(TContext context, IReadOnlyList<TStep> orderedSteps)
        where TStep : class, INavigatorPipelineStep<TContext>
    {
        PipelineStepHandlerDelegate nextStep = async () => await Task.CompletedTask;

        for (var i = orderedSteps.Count - 1; i >= 0; i--)
        {
            var currentStep = orderedSteps[i];
            var previousNextStep = nextStep;

            PipelineStepHandlerDelegate next = async () => await currentStep.InvokeAsync(context, previousNextStep);
            nextStep = next;
        }

        return new Pipeline(nextStep, orderedSteps);
    }

    public static List<TStep> OrderSteps<TStep, TMainStep, TBeforeStep, TAfterStep>(IEnumerable<INavigatorPipelineStep> steps)
        where TStep : class, INavigatorPipelineStep
        where TMainStep : class, TStep
        where TBeforeStep : class, TStep
        where TAfterStep : class, TStep
    {
        var list = new List<TStep>();

        list.AddRange(steps
            .OfType<TBeforeStep>()
            .OrderBy(GetPriority));

        list.Add(steps.OfType<TMainStep>().First());

        list.AddRange(steps
            .OfType<TAfterStep>()
            .OrderBy(GetPriority));

        return list;
    }

    private static EPriority GetPriority(object step) =>
        step.GetType().GetCustomAttribute<PriorityAttribute>()?.Level ?? EPriority.Normal;
}
