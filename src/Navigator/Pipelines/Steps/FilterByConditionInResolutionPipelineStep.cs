using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Actions.Arguments;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;

namespace Navigator.Pipelines.Steps;

/// <summary>
///     Discards actions that do not meet the condition.
/// </summary>
internal class FilterByConditionInResolutionPipelineStep : IActionResolutionPipelineStepAfter
{
    private readonly IActionArgumentProvider _argumentProvider;
    private readonly ILogger<FilterByConditionInResolutionPipelineStep> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="FilterByConditionInResolutionPipelineStep" /> class.
    /// </summary>
    public FilterByConditionInResolutionPipelineStep(IActionArgumentProvider argumentProvider,
        ILogger<FilterByConditionInResolutionPipelineStep> logger)
    {
        _argumentProvider = argumentProvider;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task InvokeAsync(NavigatorActionResolutionContext context, PipelineStepHandlerDelegate next)
    {
        if (context.Actions.Count == 0) await next();

        for (var i = context.Actions.Count - 1; i >= 0; i--)
        {
            var arguments = await _argumentProvider.GetArguments(context.Update, context.Actions[i]);

            if (await context.Actions[i].ExecuteCondition(arguments)) continue;

            context.Actions.RemoveAt(i);

            _logger.LogDebug("Discarding action {ActionName} because condition is not met", context.Actions[i].Information.Name);
        }

        await next();
    }
}