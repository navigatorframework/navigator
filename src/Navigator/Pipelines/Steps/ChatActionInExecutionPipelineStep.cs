using Microsoft.Extensions.Logging;
using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Client;
using Navigator.Abstractions.Pipelines.Context;
using Navigator.Abstractions.Pipelines.Steps;
using Navigator.Abstractions.Priorities;
using Navigator.Telegram;
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
    private readonly ILogger<ChatActionInExecutionPipelineStep> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ChatActionInExecutionPipelineStep" /> class.
    /// </summary>
    public ChatActionInExecutionPipelineStep(INavigatorClient client, ILogger<ChatActionInExecutionPipelineStep> logger)
    {
        _client = client;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task InvokeAsync(NavigatorActionExecutionContext context, PipelineStepHandlerDelegate next)
    {
        var chat = context.Update.GetChatOrDefault();
        if (chat is not null && context.Action.Information.ChatAction.HasValue)
        {
            _logger.LogDebug("Sending {ChatAction} notification to chat {ChatId}", context.Action.Information.ChatAction.Value, chat.Id);

            await _client.SendChatActionAsync(chat, context.Action.Information.ChatAction.Value);
        }
    }
}