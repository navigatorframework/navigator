using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Abstractions.Priorities;

namespace Navigator.Pipelines.Steps;

/// <summary>
///     Filters matched actions based on their <see cref="EExclusivityLevel" />.
///     Global-exclusive actions discard everything else when they are the highest priority.
///     Category-exclusive actions discard other exclusive actions in the same exact category.
/// </summary>
[Priority(EPriority.BelowNormal)]
public class FilterExclusiveActionsPipelineStep : IActionResolutionPipelineStepAfter
{
    private readonly ILogger<FilterExclusiveActionsPipelineStep> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="FilterExclusiveActionsPipelineStep" /> class.
    /// </summary>
    public FilterExclusiveActionsPipelineStep(ILogger<FilterExclusiveActionsPipelineStep> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task InvokeAsync(NavigatorActionResolutionContext context, PipelineStepHandlerDelegate next)
    {
        if (context.Actions.Count > 1)
        {
            if (context.Actions[0].Information.ExclusivityLevel == EExclusivityLevel.Global)
            {
                context.Actions.RemoveRange(1, context.Actions.Count - 1);
            }
            else
            {
                FilterByCategory(context);
            }
        }

        await next();
    }

    private void FilterByCategory(NavigatorActionResolutionContext context)
    {
        for (var i = context.Actions.Count - 1; i >= 0; i--)
        {
            if (context.Actions[i].Information.ExclusivityLevel != EExclusivityLevel.Global)
                continue;

            _logger.LogDebug(
                "Discarding global-exclusive action {ActionName} because it is not the highest-priority action",
                context.Actions[i].Information.Name);

            context.Actions.RemoveAt(i);
        }

        var firstExclusivePerCategory = new Dictionary<(string, string?), int>();

        for (var i = 0; i < context.Actions.Count; i++)
        {
            if (context.Actions[i].Information.ExclusivityLevel < EExclusivityLevel.Category)
                continue;

            var key = (context.Actions[i].Information.Category.Kind, context.Actions[i].Information.Category.Subkind);
            firstExclusivePerCategory.TryAdd(key, i);
        }

        for (var i = context.Actions.Count - 1; i >= 0; i--)
        {
            if (context.Actions[i].Information.ExclusivityLevel < EExclusivityLevel.Category)
                continue;

            var key = (context.Actions[i].Information.Category.Kind, context.Actions[i].Information.Category.Subkind);

            if (firstExclusivePerCategory[key] == i)
                continue;

            _logger.LogDebug(
                "Discarding exclusive action {ActionName} because a higher-priority exclusive action matched in category {Category}",
                context.Actions[i].Information.Name, context.Actions[i].Information.Category);

            context.Actions.RemoveAt(i);
        }
    }
}
