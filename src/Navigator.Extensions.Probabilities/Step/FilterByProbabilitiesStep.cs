using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Abstractions.Priorities;

namespace Navigator.Extensions.Probabilities.Step;

/// <summary>
///     Discards actions by their configured probabilities.
/// </summary>
[Priority(EPriority.High)]
public class FilterByProbabilitiesStep : IActionResolutionPipelineStepAfter
{
    private readonly ILogger<FilterByProbabilitiesStep> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="FilterByProbabilitiesStep" /> class.
    /// </summary>
    public FilterByProbabilitiesStep(ILogger<FilterByProbabilitiesStep> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task InvokeAsync(NavigatorActionResolutionContext context, PipelineStepHandlerDelegate next)
    {
        if (context.Actions.Count == 0) await next();

        for (var i = context.Actions.Count - 1; i >= 0; i--)
            if (context.Actions[i].Information.GetProbabilities() is not null &&
                Random.Shared.NextDouble() > context.Actions[i].Information.GetProbabilities())
            {
                _logger.LogDebug("Discarding action {ActionName} because of configured probabilities ({Probabilities})",
                    context.Actions[i].Information.Name,
                    context.Actions[i].Information.GetProbabilities());

                context.Actions.RemoveAt(i);
            }

        await next();
    }
}