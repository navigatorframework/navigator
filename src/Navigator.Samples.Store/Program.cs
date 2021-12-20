using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Navigator;
using Navigator.Configuration;
using Navigator.Extensions.Cooldown;
using Navigator.Extensions.Store;
using Navigator.Providers.Telegram;
using Navigator.Samples.Echo.Actions;

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
    .WithExtension.Cooldown()
    .WithExtension.Store(dbBuilder =>
    {
        dbBuilder.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("Navigator.Samples.Echo"));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.MapNavigator()
    .ForProvider.Telegram();

app.Run();