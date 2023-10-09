using System;
using Incremental.Common.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Navigator;
using Navigator.Configuration;
using Navigator.Extensions.Store;
using Navigator.Extensions.Store.Context;

namespace Sample.Store;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddCommonConfiguration();

        builder.Services.AddMemoryCache();
        
        builder.Services.AddNavigator(options =>
        {
            options.SetWebHookBaseUrl(builder.Configuration["BASE_WEBHOOK_URL"]!);
            options.RegisterActionsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            options.SetTelegramToken(builder.Configuration["TELEGRAM_TOKEN"]!);
        }).WithExtension.Store(optionsBuilder =>
        {
            optionsBuilder.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Sample.Store"));
        });

        var app = builder.Build();
        
        using var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
        serviceScope?.ServiceProvider.GetRequiredService<NavigatorDbContext>().Database.Migrate();
        
        app.MapNavigator();

        app.Run();
    }
}