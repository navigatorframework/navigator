using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Navigator.Abstractions.Client;
using Navigator.Abstractions.Introspection.Reader;
using Navigator.Catalog.Factory;
using Navigator.Catalog.Factory.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Sample;

public static class BotTraceExamples
{
    public static void RegisterBotTraceExamples(this BotActionCatalogFactory bot)
    {
        bot.OnCommand("trace", async (INavigatorClient client, Chat chat, INavigatorTraceReader traceReader) =>
        {
            var traces = await traceReader.RetrieveAll();
            var fileName = $"navigator-traces-{DateTimeOffset.UtcNow:yyyyMMdd-HHmmss}.json";
            var json = JsonSerializer.Serialize(traces, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            await using var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

            await client.SendDocument(
                chat.Id,
                document: new InputFileStream(stream, fileName),
                caption: $"Exported {traces.Count} root trace(s).");
        });
    }
}
