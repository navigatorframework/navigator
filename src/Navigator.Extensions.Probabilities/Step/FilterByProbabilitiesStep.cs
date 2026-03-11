using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Introspection;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Abstractions.Priorities;
using Navigator.Extensions.Probabilities.Extensions;
using Navigator.Extensions.Probabilities.Introspection;

namespace Navigator.Extensions.Probabilities.Step;

/// <summary>
///     Discards actions by their configured probabilities.
/// </summary>
[Priority(EPriority.High)]
public class FilterByProbabilitiesStep : IActionResolutionPipelineStepAfter
{
    private readonly INavigatorTracerFactory<FilterByProbabilitiesStep> _tracerFactory;
    private readonly ILogger<FilterByProbabilitiesStep> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="FilterByProbabilitiesStep" /> class.
    /// </summary>
    public FilterByProbabilitiesStep(
        INavigatorTracerFactory<FilterByProbabilitiesStep> tracerFactory,
        ILogger<FilterByProbabilitiesStep> logger)
    {
        _tracerFactory = tracerFactory;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task InvokeAsync(NavigatorActionResolutionContext context, PipelineStepHandlerDelegate next)
    {
        await using var tracer = _tracerFactory.Get();
        tracer.AddTag(NavigatorTraceKeys.UpdateCategory, context.UpdateCategory.ToString());

        if (context.Actions.Count == 0) await next();

        for (var i = context.Actions.Count - 1; i >= 0; i--)
        {
            var probabilities = context.Actions[i].Information.GetProbabilities();

            if (probabilities is not null)
            {
                tracer.AddTag(ProbabilitiesTraceKeys.Threshold, $"{probabilities}");
            }

            if (probabilities is not null && Random.Shared.NextDouble() > probabilities)
            {
                tracer.AddTag(NavigatorTraceKeys.ActionDiscarded, context.Actions[i].Information.Name);
                _logger.LogDebug("Discarding action {ActionName} because of configured probabilities ({Probabilities})",
                    context.Actions[i].Information.Name,
                    probabilities);

                context.Actions.RemoveAt(i);
            }
        }

        await next();
    }
}