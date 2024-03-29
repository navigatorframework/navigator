using System;
using Incremental.Common.Configuration;
using Microsoft.AspNetCore.Builder;
using Navigator;
using Navigator.Configuration;

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

        app.MapNavigator();
        
        app.Run();
    }
}

