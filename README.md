# Navigator

A highly opinionated telegram bot framework, mainly based on [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot).

The only requirement is `Microsoft.AspNetCore.App (>= 8.0)`.

The usage is very simple yet powerful:

```csharp
...
using Navigator;
... 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Services.AddNavigator(options =>
{
    options.SetWebHookBaseUrl(builder.Configuration["BASE_WEBHOOK_URL"]!);
    options.SetTelegramToken(builder.Configuration["TELEGRAM_TOKEN"]!);
});

var app = builder.Build();

var bot = app.GetBot();

// This action will be triggered if the user sends a message in the style of `/join <text> <text>`.
bot.OnCommand("join", async (INavigatorClient client, Chat chat, string[] parameters) =>
{
    var result = string.Join(',', parameters);

    await client.SendTextMessageAsync(chat, result);
});

app.MapNavigator();

app.Run();
```

# Getting Started

After installing the [package](https://www.nuget.org/packages/Navigator/) you can start using it by first configuring Navigator:

```csharp
builder.Services.AddNavigator(options =>
{
    // Configuration here!
});
```

Inside the AddNavigator method you can define the following options:

- The WebHook base URL:`options.SetWebHookBaseUrl(string)`
- The Telegram token: `options.SetTelegramToken(string)`
- Optionally, the WebHook endpoint: `options.SetWebHookEndpoint(string)`
- Optionally, allow multiple actions for the same update: `options.EnableMultipleActionsUsage()`
- Optionally, enable the sending of chat actions (e.g. typing) notifications: `options.EnableChatActionNotification()`

Continue by defining the actions of your bot using the different helper methods (see source code for more details).

Finally add the following line to map the endpoint to the navigator framework:

```csharp
app.MapNavigator();
```

This will start the server and will listen for incoming requests from Telegram. Your bot is good to go!

# Examples

Some examples can be found in the [samples](https://github.com/navigatorframework/navigator/src/) repository.

Also checkout some bots made with `Navigator`:

- [@ThankiesBot](https://t.me/thankiesbot), check out it's [source code](https://github.com/elementh/thankies).
- [@FOSCBot](https://t.me/foscbot), check out it's [source code](https://github.com/elementh/foscbot).

# License

Navigator Framework
Copyright (C) 2019-2024 Lucas Maximiliano Marino

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program. If not, see <https://www.gnu.org/licenses/>.