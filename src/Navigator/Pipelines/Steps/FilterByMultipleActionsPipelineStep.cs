using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Abstractions.Priorities;
using Navigator.Configuration.Options;

namespace Navigator.Pipelines.Steps;

/// <summary>
///     Discards all actions except the first one if multiple actions are not allowed.
/// </summary>
[Priority(EPriority.Low)]
public class FilterByMultipleActionsPipelineStep : IActionResolutionPipelineStepAfter
{
    private readonly ILogger<FilterByMultipleActionsPipelineStep> _logger;
    private readonly NavigatorOptions _navigatorOptions;

    /// <summary>
    ///     Initializes a new instance of the <see cref="FilterByMultipleActionsPipelineStep" /> class.
    /// </summary>
    public FilterByMultipleActionsPipelineStep(ILogger<FilterByMultipleActionsPipelineStep> logger,
        IOptions<NavigatorOptions> navigatorOptions)
    {
        _logger = logger;
        _navigatorOptions = navigatorOptions.Value;
    }

    /// <inheritdoc />
    public async Task InvokeAsync(NavigatorActionResolutionContext context, PipelineStepHandlerDelegate next)
    {
        if (context.Actions.Count > 1)
        {
            context.Actions.RemoveRange(1, context.Actions.Count - 1);

            _logger.LogDebug("Discarding all actions except {ActionName} because multiple actions are not allowed",
                context.Actions[0].Information.Name);
        }

        await next();
    }
}