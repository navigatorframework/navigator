using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Abstractions.Priorities;
using Navigator.Extensions.AI.Services;
using Telegram.Bot.Types.Enums;

namespace Navigator.Extensions.AI.Steps;

[Priority(EPriority.High)]
public class StoreIncomingMessageInChatContextStep : IActionResolutionPipelineStepBefore
{
    private readonly IChatContextStore _chatContextStore;

    public StoreIncomingMessageInChatContextStep(IChatContextStore chatContextStore)
    {
        _chatContextStore = chatContextStore;
    }

    public async Task InvokeAsync(NavigatorActionResolutionContext context, PipelineStepHandlerDelegate next)
    {
        var update = context.UpdateContext.Update;

        if (update is { Type: UpdateType.Message, Message: not null })
        {
            await _chatContextStore.AppendIncomingMessageAsync(update.Message);
        }

        await next();
    }
}
