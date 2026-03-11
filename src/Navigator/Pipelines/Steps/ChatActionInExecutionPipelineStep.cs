using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Client;
using Navigator.Abstractions.Introspection;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Abstractions.Priorities;
using Navigator.Abstractions.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Navigator.Pipelines.Steps;

/// <summary>
///     Pipeline step that sends a <see cref="ChatAction" /> to chat just before executing an <see cref="BotAction" />.
/// </summary>
[Priority(EPriority.VeryHigh)]
public class ChatActionInExecutionPipelineStep : IActionExecutionPipelineStepBefore
{
    private readonly INavigatorClient _client;
    private readonly INavigatorTracerFactory<ChatActionInExecutionPipelineStep> _tracerFactory;
    private readonly ILogger<ChatActionInExecutionPipelineStep> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ChatActionInExecutionPipelineStep" /> class.
    /// </summary>
    public ChatActionInExecutionPipelineStep(INavigatorClient client,
        INavigatorTracerFactory<ChatActionInExecutionPipelineStep> tracerFactory,
        ILogger<ChatActionInExecutionPipelineStep> logger)
    {
        _client = client;
        _tracerFactory = tracerFactory;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task InvokeAsync(NavigatorActionExecutionContext context, PipelineStepHandlerDelegate next)
    {
        await using var tracer = _tracerFactory.Get();

        var chat = context.UpdateContext.Update.GetChatOrDefault();
        if (chat is not null && context.Action.Information.ChatAction.HasValue)
        {
            _logger.LogDebug("Sending {ChatAction} notification to chat {ChatId}", context.Action.Information.ChatAction.Value, chat.Id);
            tracer.AddTag(NavigatorTraceKeys.ExecutionChatAction, context.Action.Information.ChatAction.Value.ToString());

            await _client.SendChatAction(chat, context.Action.Information.ChatAction.Value);
        }

        await next();
    }
}