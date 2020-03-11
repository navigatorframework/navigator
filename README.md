# Navigator
A highly opinionated telegram bot framework, mainly based on [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot).

# Requirements
- ASP.NET Core 3 or higher
- MediatR

# Examples
Some examples can be found in the [samples](https://github.com/navigatorframework/samples) repository.

Also checkout some bots made with `Navigator`:
- TODO


# Usage
## Configuration
Including Navigator in your project is simple:

```csharp
public class Startup
{
    // ...

    public void ConfigureServices(IServiceCollection services)
    {
        // ...

        services.AddMediatR(typeof(Startup).Assembly);
        
        services.AddNavigator(options =>
        {
            options.BotToken = Configuration["BOT_TOKEN"];
            options.BaseWebHookUrl = Configuration["BASE_WEBHOOK_URL"];
        }, typeof(Startup).Assembly); // params reference all assemblies where actions are.

        /// ...
    }

    // ...
}
```

More options are available, check them out here. //TODO

## Bot Actions

# License
Navigator Framework
Copyright (C) 2019  Lucas Maximiliano Marino

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