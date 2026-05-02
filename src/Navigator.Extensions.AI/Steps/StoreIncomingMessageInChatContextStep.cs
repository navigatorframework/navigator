using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Abstractions.Priorities;
using Navigator.Extensions.AI.Services;
using Telegram.Bot.Types.Enums;

namespace Navigator.Extensions.AI.Steps;

/// <summary>
///     Stores incoming Telegram messages in chat context before action resolution continues.
/// </summary>
[Priority(EPriority.High)]
public class StoreIncomingMessageInChatContextStep : IActionResolutionPipelineStepBefore
{
    private readonly IChatContextStore _chatContextStore;

    /// <summary>
    ///     Initializes a new pipeline step instance.
    /// </summary>
    /// <param name="chatContextStore">The chat context store used to persist incoming messages.</param>
    public StoreIncomingMessageInChatContextStep(IChatContextStore chatContextStore)
    {
        _chatContextStore = chatContextStore;
    }

    /// <summary>
    ///     Stores the incoming message in chat context and then invokes the next pipeline step.
    /// </summary>
    /// <param name="context">The current action resolution context.</param>
    /// <param name="next">The next pipeline step delegate.</param>
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
