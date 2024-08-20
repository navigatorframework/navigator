using Incremental.Common.Configuration;
using Microsoft.AspNetCore.Builder;
using Navigator;
using Navigator.Catalog.Factory.Extensions;
using Navigator.Client;
using Navigator.Configuration;
using Navigator.Configuration.Options;
using Navigator.Entities;
using Telegram.Bot;

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
        });

        var app = builder.Build();

        var bot = app.GetBot();

        bot.OnCommand("join", async (INavigatorClient client, Chat chat, string[] parameters) =>
        {
            var result = string.Join(',', parameters);

            await client.SendTextMessageAsync(chat, result);
        });

        app.MapNavigator();

        app.Run();
    }
}