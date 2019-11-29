# Navigator
A highly opinionated telegram bot framework, mainly based on Telegram.Bot

This is the repository of the metapackage for Navigator framework which includes:

- [Navigator.Core](https://github.com/navigatorframework/navigator.core): actual core of Navigator.
- [Navigator.Extensions.Store](https://github.com/navigatorframework/navigator.extensions.store): Support for storing and mananing chats and users with EF Core and Navigator.
- [Navigator.Extensions.Actions](https://github.com/navigatorframework/navigator.extensions.actions): Default abstract actions to to make easy to develop new funcionalities for a bot.

# Examples
Some examples can be found in the [samples](https://github.com/navigatorframework/samples) repository.

Also checkout some bots made with `Navigator`:
- TODO


# Usage
Including Navigator in your project is simple:

```csharp
public class Startup
{
    // ...

    public void ConfigureServices(IServiceCollection services)
    {
        // ...

        services.AddMediatR(typeof(Startup).Assembly);
        
        services.AddNavigator()
            .AddBotToken(Configuration["TELEGRAM_BOT_TOKEN"])
            .AddActionsFromAssemblyOf<Startup>();

        /// ...
    }

    // ...
}
```

`.AddNavigator()` initializes the navigator builder and the basic services for Navigator to work.

`.AddBotToken(...)` sets the token for your bot.

`.AddActionsFromAssemblyOf<T>()` registers all your bot actions.

More options are available, check them out here. //TODO

Implemen

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