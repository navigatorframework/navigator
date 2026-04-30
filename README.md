# Navigator

A highly opinionated Telegram bot framework, mainly based on [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot).

The only requirement is `Microsoft.AspNetCore.App (>= 10.0)`.

## Quickstart

Install the [Navigator](https://www.nuget.org/packages/Navigator/) NuGet package, then create a minimal bot:

```csharp
using Navigator;
using Navigator.Abstractions.Client;
using Navigator.Configuration;
using Navigator.Configuration.Options;
using Telegram.Bot.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Services.AddNavigator(configuration =>
{
    configuration.Options.SetWebHookBaseUrl(builder.Configuration["BASE_WEBHOOK_URL"]!);
    configuration.Options.SetTelegramToken(builder.Configuration["TELEGRAM_TOKEN"]!);
});

var app = builder.Build();

var bot = app.GetBot();

bot.OnCommand("join", async (INavigatorClient client, Chat chat, string[] parameters) =>
{
    var result = string.Join(',', parameters);

    await client.SendMessage(chat, result);
});

app.MapNavigator();

app.Run();
```

`MapNavigator()` maps a POST endpoint that receives Telegram webhook updates. At startup, Navigator automatically registers the webhook with Telegram using the base URL (and an optional custom endpoint set via `SetWebHookEndpoint`).

## How It Works

1. **Configure** – call `AddNavigator(configuration => ...)` to register Navigator services and set options.
2. **Register actions** – use `app.GetBot()` to get the action catalog, then register handlers with `OnCommand`, `OnMessage`, `OnCallbackQuery`, and other helpers.
3. **Map the endpoint** – call `app.MapNavigator()` to expose the webhook endpoint.
4. **Receive updates** – Telegram sends updates to the endpoint; Navigator classifies each update, resolves matching actions, and executes them through its pipeline.

## Configuration

Configuration is done inside the `AddNavigator` callback through `NavigatorConfiguration`:

```csharp
builder.Services.AddNavigator(configuration =>
{
    // Required
    configuration.Options.SetWebHookBaseUrl("https://example.com");
    configuration.Options.SetTelegramToken("your-token");

    // Optional
    configuration.Options.SetWebHookEndpoint("telegram/bot/custom-endpoint");
    configuration.Options.EnableMultipleActionsUsage();
    configuration.Options.EnableChatActionNotification();

    // Register extensions
    configuration.WithExtension<CooldownExtension>();

    // Register a strategy
    configuration.WithStrategy<QueuedStrategy>();
});
```

### Core Options

| Method                           | Description                                                                    |
|----------------------------------|--------------------------------------------------------------------------------|
| `SetWebHookBaseUrl(string)`      | Base URL for the Telegram webhook (required).                                  |
| `SetTelegramToken(string)`       | Bot token from [@BotFather](https://t.me/botfather) (required).                |
| `SetWebHookEndpoint(string)`     | Custom webhook path. If omitted, a unique endpoint is generated automatically. |
| `EnableMultipleActionsUsage()`   | Allow more than one action to match and execute for a single update.           |
| `EnableChatActionNotification()` | Send chat action indicators (e.g. "typing") while executing actions.           |

### Extensions and Strategies

| Method                                         | Description                                |
|------------------------------------------------|--------------------------------------------|
| `WithExtension<T>()`                           | Register an extension package.             |
| `WithExtension<T, TOptions>(Action<TOptions>)` | Register an extension with options.        |
| `WithStrategy<T>()`                            | Replace the default navigation strategy.   |
| `WithStrategy<T, TOptions>(Action<TOptions>)`  | Replace the default strategy with options. |

## Registering Actions

Actions are registered on the bot catalog returned by `app.GetBot()`. Each helper targets a specific Telegram update type:

```csharp
var bot = app.GetBot();

// Respond to the /greet command.
bot.OnCommand("greet", async (INavigatorClient client, Chat chat) =>
{
    await client.SendMessage(chat, "Hello!");
});

// Respond to any message matching a condition.
bot.OnMessage((Update _) => true, async (INavigatorClient client, Chat chat, Message message) =>
{
    await client.SendMessage(chat, $"Received message {message.MessageId}");
});
```

Available registration methods include `OnCommand`, `OnCommandPattern`, `OnMessage`, `OnCallbackQuery`, `OnInlineQuery`, `OnMessageReaction`, and many more specialized variants for specific Telegram message types. Each returns a `BotActionBuilder` that supports chaining:

```csharp
bot.OnMessage((Bot self, Message message) => self.IsRepliedTo(message))
    .SendText("I see you replied to me!");

bot.OnMessage((Bot self, Message message) => self.IsMentioned(message))
    .SendText("Thanks for the mention!");
```

`OnCommandPattern` accepts a regular expression instead of an exact string, enabling partial matches, optional parameters, or multi-word commands. Pattern-based actions default to `BelowNormal` priority so exact `OnCommand` matches take precedence:

```csharp
bot.OnCommand("happy")
    .SendText("I am happy, exact, concrete match!");

bot.OnCommandPattern("^happy.*")
    .SendText("I am happy, partial, pattern match!");
```

Builder helpers like `.SendText(...)`, `.SendPhoto(...)`, `.SendSticker(...)`, `.WithChatAction(...)`, `.WithCooldown(...)`, `.WithPriority(...)`, `.SetExclusivityLevel(...)`, `.AsExclusive()`, and `.AsNotExclusive()` let you configure action behavior without writing a full handler.

## Exclusivity

When `EnableMultipleActionsUsage()` is enabled and multiple actions match a single update, exclusivity levels control which of those actions actually execute. Each action has an `EExclusivityLevel` that determines how it interacts with other matched actions:

| Level      | Behavior                                                                                                                                                   |
|------------|------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `None`     | No exclusivity. The action always runs if matched. This is the default for most registration methods.                                                      |
| `Category` | Among matched actions in the same category (e.g. BotCommand), only the highest-priority exclusive action runs. Actions in other categories are unaffected. |
| `Global`   | If this action is the highest-priority matched action overall, all other actions are discarded.                                                            |

`OnCommand` and `OnCommandPattern` default to `Category`-level exclusivity, so only one command handler runs per update even when multiple commands match. All other registration methods default to `None`.

### Builder Methods

Use the builder API to set or override exclusivity on any action:

```csharp
// Set an explicit exclusivity level.
bot.OnMessage((Update _) => true, handler)
    .SetExclusivityLevel(EExclusivityLevel.Category);

// Shorthand for Global exclusivity.
bot.OnMessage((Update _) => true, handler)
    .AsExclusive();

// Remove exclusivity set by a registration helper (e.g. OnCommand defaults to Category).
bot.OnCommand("ping", handler)
    .AsNotExclusive();
```

> **Note:** exclusivity filtering only takes effect when `EnableMultipleActionsUsage()` is enabled. Without it, Navigator executes at most one action per update regardless of exclusivity settings.

## Handler Parameters

Handler and condition delegates support automatic parameter injection. Navigator resolves arguments from the incoming Telegram update and from the DI container:

| Parameter Type         | Source                                                           |
|------------------------|------------------------------------------------------------------|
| `INavigatorClient`     | Scoped Telegram client for sending messages.                     |
| `Chat` (Navigator)     | The chat extracted from the update.                              |
| `Update`               | The raw Telegram update.                                         |
| `Message`              | The message from the update, if present.                         |
| `string[]`             | Command arguments (for `OnCommand` handlers).                    |
| `Bot`                  | The bot entity, useful for `IsRepliedTo` / `IsMentioned` checks. |
| Any registered service | Resolved from the DI container.                                  |

Example combining injected parameters:

```csharp
bot.OnCommand("join", async (INavigatorClient client, Chat chat, string[] parameters) =>
{
    var result = string.Join(',', parameters);

    await client.SendMessage(chat, result);
});
```

## Extensions

Navigator ships several extension packages. Register them with `WithExtension<T>()`:

- **Navigator.Extensions.Cooldown** – configure per-action cooldowns.
- **Navigator.Extensions.Probabilities** – configure the probability of an action being executed.
- **Navigator.Extensions.Store** – persist `User`, `Chat`, and `Conversation` entities with Entity Framework Core.
- **Navigator.Extensions.Management** – provides a `/debug` command for introspecting Navigator trace data.

You can find them on NuGet by searching for `Navigator.Extensions.`.

### Cooldown

```csharp
configuration.WithExtension<CooldownExtension>();

// Then on an action:
bot.OnMessage((Update _) => true, async (INavigatorClient client, Chat chat) =>
{
    await client.SendMessage(chat, "Hello!");
}).WithCooldown(TimeSpan.FromSeconds(30));
```

### Store

```csharp
configuration.WithExtension<StoreExtension, StoreOptions>(options =>
{
    options.ConfigureStore(contextOptionsBuilder =>
    {
        contextOptionsBuilder.UseNpgsql(connectionString);
    });
});

// Handlers can then inject persisted entities:
bot.OnMessage((Update _) => true, async (INavigatorClient client, User user, Chat chat, INavigatorStore store) =>
{
    await client.SendMessage(chat,
        $"User {user.ExternalId} was first seen at {user.FirstActiveAt}.");
}).WithChatAction(ChatAction.Typing);
```

For a custom `DbContext`, use `options.ConfigureStore<TContext>(...)` and inject `INavigatorStore<TContext>`. See the [SampleWithCustomStore](src/samples/SampleWithCustomStore/Program.cs) for a full example.

### Management

```csharp
configuration.WithExtension<ManagementExtension, ManagementOptions>(cfg => { });

// Then on the bot catalog:
bot.RegisterManagementCommands();
```

The Management extension registers a `/debug` command. When used as a reply to any message, it returns a summary of Navigator trace data for that message, including trace count, total duration, actions triggered, and any errors or warnings. A button is also provided to download the full trace as a JSON document.

## Strategies

Navigator uses a strategy to decide how incoming updates are processed. By default, updates are processed immediately and sequentially as they arrive.

### Queued Strategy

The `Navigator.Strategies.Queued` package provides an alternative strategy that enqueues updates by a partition key derived from the Telegram context (chat, user, poll, etc.). Updates with the same key are processed sequentially, while updates with different keys can run in parallel.

```csharp
// Without options (unbounded queues):
configuration.WithStrategy<QueuedStrategy>();

// With options:
configuration.WithStrategy<QueuedStrategy, QueuedStrategyOptions>(opts =>
{
    opts.MaxMessagesPerQueue = 50;
});
```

`MaxMessagesPerQueue` controls the maximum number of pending updates per queue. Set to `0` (the default) for unbounded queues, or a positive value to apply backpressure when the queue is full.

## Samples

Working examples are available in the repository:

- [Sample](src/samples/Sample/Program.cs) – a basic bot with commands, message handlers, and cooldowns.
- [SampleWithStore](src/samples/SampleWithStore/Program.cs) – a bot with entity persistence via the Store extension.
- [SampleWithCustomStore](src/samples/SampleWithCustomStore/Program.cs) – a bot with a custom `DbContext` for extended storage.
- [SampleWithQueuedStrategy](src/samples/SampleWithQueuedStrategy/Program.cs) – a bot using the queued navigation strategy.

## License

Navigator Framework
Copyright (C) 2019-2026 Lucas Maximiliano Marino

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
