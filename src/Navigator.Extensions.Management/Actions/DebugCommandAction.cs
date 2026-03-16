using Navigator.Abstractions.Client;
using Navigator.Abstractions.Introspection.Reader;
using Navigator.Extensions.Management.Helpers;
using Navigator.Extensions.Management.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Chat = Navigator.Abstractions.Entities.Chat;

namespace Navigator.Extensions.Management.Actions;

/// <summary>
///     Handles the /debug command for retrieving trace information.
/// </summary>
public static class DebugCommandAction
{
    /// <summary>
    ///     Handles the debug command by extracting message ID from reply context and retrieving traces.
    /// </summary>
    public static async Task HandleDebugCommand(INavigatorClient client, Chat chat, Message message,
        INavigatorTraceReader traceReader, ITraceFormatter traceFormatter)
    {
        if (message.ReplyToMessage == null)
        {
            await client.SendMessage(chat, DebugCommandUsage, ParseMode.Html);
            return;
        }

        var repliedMessage = message.ReplyToMessage;
        var chatId = chat.Id;
        var messageId = repliedMessage.MessageId;

        var traces = await traceReader.RetrieveByChatAndMessage(chatId, messageId);

        var formattedOutput = traceFormatter.FormatTraces(traces);

        await client.SendMessage(chat, formattedOutput, parseMode: ParseMode.Html);
    }

    private static readonly string DebugCommandUsage = ManagementMessageHelper.Info(
        "Debug Command Usage",
        "The /debug command must be used as a reply to a message.",
        "Please reply to the message you want to debug and use /debug.");
}