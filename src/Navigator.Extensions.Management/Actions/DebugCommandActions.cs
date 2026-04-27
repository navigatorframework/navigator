using Navigator.Abstractions.Client;
using Navigator.Abstractions.Introspection.Reader;
using Navigator.Extensions.Management.Helpers;
using Navigator.Extensions.Management.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Chat = Navigator.Abstractions.Entities.Chat;

namespace Navigator.Extensions.Management.Actions;

/// <summary>
///     Handles the /debug command for retrieving trace information.
/// </summary>
public static class DebugCommandActions
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

        var chatId = chat.Id;
        var messageId = message.ReplyToMessage.MessageId;

        var traces = await traceReader.RetrieveByChatAndMessage(chatId, messageId);

        var formattedOutput = traceFormatter.FormatTraces(traces);

        var replyMarkup = traces.Count != 0 ? new InlineKeyboardMarkup([
            [
                InlineKeyboardButton.WithCallbackData("📄 Get Full Trace", $"debug_full_trace_{chatId}_{messageId}")
            ]
        ]) : null;

        await client.SendMessage(chat, formattedOutput, parseMode: ParseMode.Html, replyMarkup: replyMarkup);
    }

    /// <summary>
    ///     Handles the "Get Full Trace" callback query by sending the full trace data as a JSON document.
    /// </summary>
    public static async Task HandleFullTraceCallback(INavigatorClient client, Chat chat, CallbackQuery callbackQuery,
        INavigatorTraceReader traceReader, ITraceFormatter traceFormatter)
    {
        // Parse callback data to extract chatId and messageId
        var callbackData = callbackQuery.Data;
        
        if (callbackData == null || !callbackData.StartsWith("debug_full_trace_"))
        {
            return;
        }

        var parts = callbackData["debug_full_trace_".Length..].Split('_');
        if (parts.Length != 2 || !long.TryParse(parts[0], out var chatId) || !int.TryParse(parts[1], out var messageId))
        {
            return;
        }

        // Acknowledge the callback
        await client.AnswerCallbackQuery(callbackQuery.Id, "Generating full trace...");

        // Retrieve traces
        var traces = await traceReader.RetrieveByChatAndMessage(chatId, messageId);
        
        if (!traces.Any())
        {
            await client.SendMessage(chat, "No traces found for this message.", replyParameters: callbackQuery.Message);
            return;
        }

        // Format traces as JSON
        var jsonTrace = traceFormatter.FormatTracesAsJson(traces);
        var fileName = $"trace-{chatId}-{messageId}-{DateTimeOffset.UtcNow:yyyyMMdd-HHmmss}.json";

        // Send as document
        using var stream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonTrace));
        var inputFile = new InputFileStream(stream, fileName);
        
        await client.SendDocument(
            chat,
            inputFile,
            caption: $"Full trace data for message {messageId} ({traces.Count} trace entries).",
            replyParameters: callbackQuery.Message
        );
    }

    private static readonly string DebugCommandUsage = ManagementMessageHelper.Info(
        "Debug Command Usage",
        "The /debug command must be used as a reply to a message.",
        "Please reply to the message you want to debug and use /debug.");
}