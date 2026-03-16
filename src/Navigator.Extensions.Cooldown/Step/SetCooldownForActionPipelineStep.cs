using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Introspection;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Abstractions.Telegram;
using Navigator.Extensions.Cooldown.Extensions;
using Navigator.Extensions.Cooldown.Introspection;
using Telegram.Bot.Types;

namespace Navigator.Extensions.Cooldown.Step;

internal class SetCooldownForActionPipelineStep : IActionExecutionPipelineStepAfter
{
    private readonly IMemoryCache _cache;
    private readonly INavigatorTracerFactory<SetCooldownForActionPipelineStep> _tracerFactory;
    private readonly ILogger<SetCooldownForActionPipelineStep> _logger;

    public SetCooldownForActionPipelineStep(
        IMemoryCache cache,
        INavigatorTracerFactory<SetCooldownForActionPipelineStep> tracerFactory,
        ILogger<SetCooldownForActionPipelineStep> logger)
    {
        _cache = cache;
        _tracerFactory = tracerFactory;
        _logger = logger;
    }

    public async Task InvokeAsync(NavigatorActionExecutionContext context, PipelineStepHandlerDelegate next)
    {
        await using var tracer = _tracerFactory.Get();
        tracer.AddTag(NavigatorTraceKeys.ActionName, context.Action.Information.Name);

        var cooldown = context.Action.Information.GetCooldown();

        if (cooldown != TimeSpan.Zero)
        {
            tracer.AddTag(CooldownTraceKeys.CooldownDuration, $"{cooldown.TotalMinutes}");
            _logger.LogDebug("Setting action {ActionName} to cooldown for {Cooldown} minutes", context.Action.Information.Name,
                cooldown.TotalMinutes);

            _cache.Set(GenerateCacheKey(context.Action, context.UpdateContext.Update), true, cooldown);
        }

        await next();
    }

    public static string GenerateCacheKey(BotAction action, Update update)
    {
        return $"{action.Id}:cooldown:{update.GetChatOrDefault()?.Id}";
    }
}