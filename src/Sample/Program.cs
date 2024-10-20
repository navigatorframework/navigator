using System;
using Incremental.Common.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Navigator;
using Navigator.Abstractions.Client;
using Navigator.Catalog.Factory.Extensions;
using Navigator.Client;
using Navigator.Configuration;
using Navigator.Configuration.Options;
using Telegram.Bot;
using Telegram.Bot.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddCommonConfiguration();

builder.Services.AddMemoryCache();

builder.Services.AddNavigator(configuration =>
{
    configuration.Options.SetWebHookBaseUrl(builder.Configuration["BASE_WEBHOOK_URL"]!);
    configuration.Options.SetTelegramToken(builder.Configuration["TELEGRAM_TOKEN"]!);
    configuration.Options.EnableChatActionNotification();
    configuration.Options.EnableMultipleActionsUsage();
});

var app = builder.Build();

var bot = app.GetBot();

// This action will be triggered if the user sends a message in the style of `/join <text>`.
bot.OnCommand("join", async (INavigatorClient client, Chat chat, string[] parameters) =>
{
    var result = string.Join(',', parameters);

    await client.SendTextMessageAsync(chat, result);
});

// This action will be triggered for every message sent to the chat. Additionally in this code example, this action will be triggered
// only if NavigatorOptions.MultipleActionsUSageIsEnabled is set to true.
bot.OnMessage((Update _) => true, async (INavigatorClient client, Chat chat, Message message) =>
{
    var text = $"message received: {message.MessageId}";

    await client.SendTextMessageAsync(chat, text);
}).WithCooldown(TimeSpan.FromSeconds(30));

app.MapNavigator();

app.Run();