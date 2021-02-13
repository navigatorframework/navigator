# Navigator
A highly opinionated telegram bot framework, mainly based on [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot).

This framework relies heavily in [MediatR](https://github.com/jbogard/MediatR) and it is shaped accordingly. 

The base package ([Navigator](https://www.nuget.org/packages/Navigator/)) it's usable on it's own but the [Actions](https://www.nuget.org/packages/Navigator.Extensions.Actions) extension it's highly encouraged as it brings default implementations for almost every type of incoming telegram update, in the future we may merge it into the base package.

For storage of users, chats and conversations the [Store](https://www.nuget.org/packages/Navigator.Extensions.Store) extension works really well with Navigator, it automatically recognizes users and groups, saves them to the database for future use and injects into the NavigatorContext all the data you may have about a user or a chat, you can also use your own models and it will still work, check out the examples for more information.

Finally the [Shipyard](https://www.nuget.org/packages/Navigator.Extensions.Shipyard) WIP package gives you an useful API to retrieve information about your bot, change some of it's configuration and launch some actions. A companion web-based UI is also planned.

## Packages

| Package | Last Stable | Last Prerelease |
|------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Navigator | [![package.nav](https://img.shields.io/nuget/v/Navigator?style=flat-square)](https://www.nuget.org/packages/Navigator/) | [![package.nav.pre](https://img.shields.io/nuget/vpre/Navigator?style=flat-square)](https://www.nuget.org/packages/Navigator/) |
| Navigator.Extensions.Actions | [![package.nav.ext.act](https://img.shields.io/nuget/v/Navigator.Extensions.Actions?style=flat-square)](https://www.nuget.org/packages/Navigator.Extensions.Actions) | [![package.nav.ext.act.pre](https://img.shields.io/nuget/vpre/Navigator.Extensions.Actions?style=flat-square)](https://www.nuget.org/packages/Navigator.Extensions.Actions) |
| Navigator.Extensions.Store | [![package.nav.ext.sto](https://img.shields.io/nuget/v/Navigator.Extensions.Actions?style=flat-square)](https://www.nuget.org/packages/Navigator.Extensions.Store) | [![package.nav.ext.sto.pre](https://img.shields.io/nuget/vpre/Navigator.Extensions.Store?style=flat-square)](https://www.nuget.org/packages/Navigator.Extensions.Store) |
| Navigator.Extensions.Shipyard | [![package.nav.ext.ship](https://img.shields.io/nuget/v/Navigator.Extensions.Shipyard?style=flat-square)](https://www.nuget.org/packages/Navigator.Extensions.Shipyard) | [![package.nav.ext.ship.pre](https://img.shields.io/nuget/vpre/Navigator.Extensions.Shipyard?style=flat-square)](https://www.nuget.org/packages/Navigator.Extensions.Shipyard) |

# Requirements
- ASP.NET (>= net5.0)
- MediatR (>= 9.0.0)

# Examples
Some examples can be found in the [samples](https://github.com/navigatorframework/navigator/src/) repository.

Also checkout some bots made with `Navigator`:
- [@ThankiesBot](https://t.me/thankiesbot), check out it's [source code](https://github.com/elementh/thankies).
- [@FOSCBot](https://t.me/foscbot), check out it's [source code](https://github.com/elementh/foscbot).

# Basic Usage
## Configuration
Including Navigator in your project is simple:

```csharp
public class Startup
{
    // ...

    public void ConfigureServices(IServiceCollection services)
    {
        // ...
        services.AddControllers().AddNewtonsoftJson();

        services.AddMediatR(typeof(Startup).Assembly); // And any other assembly that may be needed.
        
        services.AddNavigator(options =>
        {
            options.SetTelegramToken(Configuration["BOT_TOKEN"]); // Your telegram bot token.
            options.SetWebHookBaseUrl(Configuration["BASE_WEBHOOK_URL"]); // The base url where you are going to receive the updates from teelgram.
            options.RegisterActionsFromAssemblies(typeof(Startup).Assembly); // All your actions.
        });

        /// ...
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /// ...

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapNavigator(); // In order to receive updates from telegram.
            });

            /// ...
        }
}
```

More options are available, check out the wiki.

## Bot Actions
Example of a basic echo action using the base package + the actions extension.

```csharp
public class EchoAction : MessageAction
{
    public string MessageToEcho { get; set; } = string.Empty;
    
    public override IAction Init(INavigatorContext ctx)
    {
        if (string.IsNullOrWhiteSpace(ctx.Update.Message.Text))
        {
            MessageToEcho = ctx.Update.Message.Text;
        }
        return this;
    }

    public override bool CanHandle(INavigatorContext ctx)
    {
        return !string.IsNullOrWhiteSpace(MessageToEcho);
    }
}

/// ...

public class EchoActionHandler : ActionHandler<EchoAction>
{
    public EchoActionHandler(INavigatorContext ctx) : base(ctx)
    {
    }

    public override async Task<Unit> Handle(EchoAction request, CancellationToken cancellationToken)
    {
        await Ctx.Client.SendTextMessageAsync(Ctx.GetTelegramChat(), request.MessageToEcho,
            cancellationToken: cancellationToken);

        return Unit.Value;
    }
}
```


# License
Navigator Framework
Copyright (C) 2019-2021  Lucas Maximiliano Marino

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published
by the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.