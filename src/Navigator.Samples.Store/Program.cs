using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Navigator;
using Navigator.Configuration;
using Navigator.Extensions.Store;
using Navigator.Extensions.Store.Context;
using Navigator.Extensions.Store.Telegram;
using Navigator.Providers.Telegram;
using Navigator.Samples.Store.Actions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// For Extensions.Cooldown
builder.Services.AddDistributedMemoryCache();

builder.Services
    .AddNavigator(options =>
    {
        options.SetWebHookBaseUrl(builder.Configuration["BASE_WEBHOOK_URL"]);
        options.RegisterActionsFromAssemblies(typeof(EchoAction).Assembly);
    })
    .WithProvider.Telegram(options => { options.SetTelegramToken(builder.Configuration["BOT_TOKEN"]); })
    .WithExtension.Store(dbBuilder =>
    {
        dbBuilder.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("Navigator.Samples.Store"));
    }).WithExtension.StoreForTelegram();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}


using var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
serviceScope?.ServiceProvider.GetRequiredService<NavigatorDbContext>().Database.Migrate();

app.UseHttpsRedirection();

app.MapNavigator()
    .ForProvider.Telegram();

app.Run();