using Incremental.Common.Configuration;
using Microsoft.AspNetCore.Builder;
using Navigator;
using Navigator.Catalog.Factory.Extensions;
using Navigator.Client;
using Navigator.Configuration;
using Navigator.Configuration.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chat = Navigator.Entities.Chat;

namespace Sample;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddCommonConfiguration();

        builder.Services.AddNavigator(options =>
        {
            options.SetWebHookBaseUrl(builder.Configuration["BASE_WEBHOOK_URL"]!);
            options.SetTelegramToken(builder.Configuration["TELEGRAM_TOKEN"]!);
            options.EnableTypingNotification();
            options.EnableMultipleActionsUsage();
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
        bot.OnMessage((Update _) => true,
            async (INavigatorClient client, Chat chat) => { await client.SendTextMessageAsync(chat, "text received!"); });

        app.MapNavigator();

        app.Run();
    }
}