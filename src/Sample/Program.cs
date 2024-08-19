using System;
using System.Linq;
using System.Threading.Tasks;
using Incremental.Common.Configuration;
using Microsoft.AspNetCore.Builder;
using Navigator;
using Navigator.Configuration;
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
            options.RegisterActionsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            options.SetTelegramToken(builder.Configuration["TELEGRAM_TOKEN"]!);
            options.EnableTypingNotification();
        });
        
        var app = builder.Build();

        var bot = app.GetBot();

        // bot.OnCommand("join", async (ctx, parameters) =>
        // {
        //     var result = string.Join(' ', parameters);
        //
        //     await ctx.Client.SendTextMessageAsync(ctx.Conversation.Chat!.Id, result);
        // } );

        bot.OnUpdate((int a) =>
        {
            return a * 2;
        }, async (int a) =>
        {
            await Task.Delay(2);
            return a * 3;
        });
        
        app.MapNavigator();
        
        app.Run();
    }
}

