using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Abstractions.Telegram;
using Navigator.Extensions.Cooldown.Extensions;
using Telegram.Bot.Types;

namespace Navigator.Extensions.Cooldown.Step;

internal class SetCooldownForActionPipelineStep : IActionExecutionPipelineStepAfter
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<SetCooldownForActionPipelineStep> _logger;

    public SetCooldownForActionPipelineStep(IMemoryCache cache, ILogger<SetCooldownForActionPipelineStep> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task InvokeAsync(NavigatorActionExecutionContext context, PipelineStepHandlerDelegate next)
    {
        if (context.Action.Information.GetCooldown() != TimeSpan.Zero)
        {
            _logger.LogDebug("Setting action {ActionName} to cooldown for {Cooldown} minutes", context.Action.Information.Name,
                context.Action.Information.GetCooldown().TotalMinutes);

            _cache.Set(GenerateCacheKey(context.Action, context.Update), true, context.Action.Information.GetCooldown());
        }

        await next();
    }

    public static string GenerateCacheKey(BotAction action, Update update)
    {
        return $"{action.Id}:cooldown:{update.GetChatOrDefault()?.Id}";
    }
}