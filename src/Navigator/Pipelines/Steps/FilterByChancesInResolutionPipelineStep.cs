using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Abstractions.Priorities;

namespace Navigator.Pipelines.Steps;

/// <summary>
///     Discards actions with configured chances.
/// </summary>
[Priority(EPriority.High)]
internal class FilterByChancesInResolutionPipelineStep : IActionResolutionPipelineStepAfter
{
    private readonly ILogger<FilterByChancesInResolutionPipelineStep> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="FilterByChancesInResolutionPipelineStep" /> class.
    /// </summary>
    public FilterByChancesInResolutionPipelineStep(ILogger<FilterByChancesInResolutionPipelineStep> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task InvokeAsync(NavigatorActionResolutionContext context, PipelineStepHandlerDelegate next)
    {
        if (context.Actions.Count == 0) await next();

        for (var i = context.Actions.Count - 1; i >= 0; i--)
            if (context.Actions[i].Information.Chances is not null && Random.Shared.NextDouble() > context.Actions[i].Information.Chances)
            {
                context.Actions.RemoveAt(i);

                _logger.LogDebug("Discarding action {ActionName} because of configured chances ({Chances})",
                    context.Actions[i].Information.Name,
                    context.Actions[i].Information.Chances);
            }

        await next();
    }
}