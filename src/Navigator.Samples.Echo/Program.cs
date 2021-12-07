using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Navigator;
using Navigator.Configuration;
using Navigator.Extensions.Cooldown;
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
    .WithExtension.Cooldown()
    .WithProvider.Telegram(options => { options.SetTelegramToken(builder.Configuration["BOT_TOKEN"]); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.MapNavigator()
    .ForProvider.Telegram();

app.Run();