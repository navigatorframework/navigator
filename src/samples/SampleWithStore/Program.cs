using Incremental.Common.Configuration;
using Microsoft.EntityFrameworkCore;
using Navigator;
using Navigator.Abstractions.Client;
using Navigator.Catalog.Factory.Extensions;
using Navigator.Configuration;
using Navigator.Configuration.Options;
using Navigator.Extensions.Store;
using Navigator.Extensions.Store.Persistence.Context;
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

    configuration.WithExtension<StoreExtension, StoreOptions>(options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("NavigatorStoreDb");

        options.ConfigureStore(contextOptionsBuilder =>
        {
            contextOptionsBuilder.UseNpgsql(connectionString, optionsBuilder =>
            {
                optionsBuilder.MigrationsAssembly(typeof(Program).Assembly);
            });
        });
    });
});

var app = builder.Build();

var bot = app.GetBot();

// This action will be triggered if the user sends a message in the style of `/join <text>`.
bot.OnCommand("join", async (INavigatorClient client, Chat chat, string[] parameters) =>
{
    var result = string.Join(',', parameters);

    await client.SendMessage(chat, result);
});

// This action will be triggered for every message sent to the chat. Additionally in this code example, this action will be triggered
// only if NavigatorOptions.MultipleActionsUSageIsEnabled is set to true.
bot.OnMessage((Update _) => true, async (INavigatorClient client, Chat chat, Message message) =>
{
    var text = $"message received: {message.MessageId}";

    await client.SendMessage(chat, text);
});

app.MapNavigator();

using var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
serviceScope?.ServiceProvider.GetRequiredService<NavigatorStoreDbContext>().Database.Migrate();

app.Run();