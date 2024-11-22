using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Abstractions.Priorities;
using Telegram.Bot.Types;

namespace Navigator.Extensions.Cooldown.Step;

[Priority(EPriority.High)]
internal class FilterByActionsInCooldownPipelineStep : IActionResolutionPipelineStepAfter
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<FilterByActionsInCooldownPipelineStep> _logger;

    public FilterByActionsInCooldownPipelineStep(IMemoryCache cache, ILogger<FilterByActionsInCooldownPipelineStep> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task InvokeAsync(NavigatorActionResolutionContext context, PipelineStepHandlerDelegate next)
    {
        for (var i = context.Actions.Count - 1; i >= 0; i--)
            if (IsInCooldown(context.Actions[i], context.Update))
            {
                _logger.LogDebug("Discarding action {ActionName} because is in cooldown", context.Actions[i].Information.Name);

                context.Actions.RemoveAt(i);
            }

        await next();
    }

    private bool IsInCooldown(BotAction botAction, Update update)
    {
        return _cache.TryGetValue(SetCooldownForActionPipelineStep.GenerateCacheKey(botAction, update), out _);
    }
}