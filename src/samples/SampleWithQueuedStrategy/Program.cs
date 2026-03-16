using System;
using System.Threading.Tasks;
using Incremental.Common.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Navigator;
using Navigator.Abstractions.Client;
using Navigator.Abstractions.Catalog.Extensions;
using Navigator.Configuration;
using Navigator.Configuration.Options;
using Navigator.Strategies.Queued;
using Telegram.Bot;
using Telegram.Bot.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddCommonConfiguration();

builder.Services.AddMemoryCache();

builder.Services.AddNavigator(configuration =>
{
    configuration.Options.SetWebHookBaseUrl(builder.Configuration["BASE_WEBHOOK_URL"]!);
    configuration.Options.SetTelegramToken(builder.Configuration["TELEGRAM_TOKEN"]!);
    configuration.Options.EnableMultipleActionsUsage();

    configuration.WithStrategy<QueuedStrategy, QueuedStrategyOptions>(options =>
    {
        options.MaxMessagesPerQueue = 100;
    });
});

var app = builder.Build();

var bot = app.GetBot();

bot.OnCommand("join", async (INavigatorClient client, Chat chat, string[] parameters) =>
{
    var result = string.Join(',', parameters);

    await client.SendMessage(chat, result);
});

bot.OnMessage((Update _) => true, async (INavigatorClient client, Chat chat, Message message) =>
{
    var text = $"message received: {message.MessageId}";

    await client.SendMessage(chat, text);
});

bot.OnCommand("test", async (INavigatorClient client, Chat chat, Update update) =>
{
    var receivedAt = update.Message?.Date;
    var processedAt = DateTime.UtcNow;
    await Task.Delay(5000);
    var currentTime = DateTime.UtcNow;
    await client.SendMessage(chat, $"message received: {receivedAt}, processed: {processedAt}, now: {currentTime}");
});

app.MapNavigator();

app.Run();
