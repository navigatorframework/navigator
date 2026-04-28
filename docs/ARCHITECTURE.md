# Navigator Architecture

This document explains how Navigator processes Telegram updates and where contributors should make changes. It is written for human contributors and AI agents.

## Repository Orientation

Navigator is a .NET Telegram bot framework. The solution lives under `src/` and is pinned by `src/global.json`. The main projects are:

- `src/Navigator.Abstractions/`: public contracts shared by the framework, extensions, strategies, and user code.
- `src/Navigator/`: core runtime, dependency injection, action builders, catalog, classifiers, pipelines, strategies, tracing, and webhook mapping.
- `src/Navigator.Extensions.*`: optional extension packages.
- `src/Navigator.Strategies.*`: optional strategy packages that replace update processing behavior.
- `src/Navigator.Testing/`: xUnit tests for core and package behavior.
- `src/samples/`: runnable examples for user-facing workflows.

Use `docs/EXTENSION-AUTHORING.md` for extension-specific work.

## Startup And Service Registration

Applications register Navigator with `AddNavigator` from `src/Navigator/ServiceCollectionExtensions.cs`. That method:

1. Builds a `NavigatorConfiguration` from the user callback.
2. Registers core services such as `INavigatorClient`, `BotActionCatalogFactory`, `IUpdateClassifier`, argument resolvers, tracing, and pipeline builders.
3. Registers default pipeline steps based on `NavigatorOptions`.
4. Registers `DefaultNavigationStrategy`.
5. Runs configured strategies and extensions through `NavigatorConfiguration.Configure`.
6. Registers `INavigatorStrategy` as the default strategy if no custom strategy was configured.
7. Imports configured options into `IOptions<NavigatorOptions>`.

`NavigatorConfiguration` in `src/Navigator/Configuration/NavigatorConfiguration.cs` stores requested extensions and the optional strategy. `WithExtension<T>()` appends extension definitions. `WithStrategy<T>()` replaces the active strategy because only one strategy can process updates.

## Webhook Entry Point

`MapNavigator()` in `src/Navigator/Configuration/EndpointRouteBuilderExtensions.cs` maps a POST endpoint using `NavigatorOptions.GetWebHookEndpointOrDefault()`. The endpoint receives a Telegram `Update`, takes the current `HttpContext.TraceIdentifier` as the update identifier, and calls `INavigatorStrategy.Invoke(update, identifier)`.

The endpoint logs unhandled exceptions. The default execution pipeline catches handler exceptions inside action execution, so not every action failure reaches the endpoint.

## Strategy Layer

A strategy decides how an incoming update is processed.

The default strategy is `src/Navigator/Strategy/DefaultNavigationStrategy.cs`. It processes one update immediately:

1. Starts a trace and adds update tags such as update id, chat id, message id, user id, and update type.
2. Builds a `NavigatorUpdateContext`.
3. Builds and invokes the resolution pipeline.
4. Converts resolved actions into execution contexts.
5. Builds and invokes the execution pipeline for each action.

Custom strategy packages live under `src/Navigator.Strategies.*`. For example, `Navigator.Strategies.Queued` enqueues updates by a Telegram-derived key, and a hosted worker later runs the normal processing flow.

Do not bypass `INavigatorStrategy` for update processing. If behavior changes how updates are scheduled, ordered, retried, or partitioned, it probably belongs in a strategy package. If behavior changes which actions match or how they execute, it probably belongs in pipeline steps or core action code.

## Action Registration And Catalog

User code calls `app.GetBot()` to retrieve the action catalog and registers actions with helpers such as `OnCommand`, `OnMessage`, and `OnCallbackQuery`.

The relevant code is in:

- `src/Navigator/Catalog/`
- `src/Navigator/Catalog/Factory/`
- `src/Navigator/Actions/Builder/`
- `src/Navigator/Actions/Builder/Extensions/`
- `src/Navigator.Abstractions/Actions/`

Action builders store metadata in `BotActionInformation`, including category, priority, exclusivity level, chat action, condition input types, handler input types, and extension-specific options. The catalog later retrieves actions by `UpdateCategory`.

When changing public registration helpers, update README examples and add tests that prove the action is classified, resolved, and invoked as expected.

## Classification

`IUpdateClassifier` classifies a Telegram `Update` into an `UpdateCategory`. The default implementation is in `src/Navigator/Classifier/UpdateClassifier.cs`.

The resolution pipeline uses the category to retrieve relevant actions from the catalog. If a new Telegram update type or subkind is supported, update the classifier, action registration helpers, tests, and README or sample docs as needed.

## Pipelines

Navigator has two pipelines:

- Resolution pipeline: selects and filters candidate actions for an update.
- Execution pipeline: invokes one selected action and cross-cutting behavior around it.

Pipeline builders live in `src/Navigator/Pipelines/Builder/`. Steps live in `src/Navigator/Pipelines/Steps/` or extension packages.

Steps are selected by marker interfaces from `src/Navigator.Abstractions/Pipelines/Steps/`:

- `IActionResolutionPipelineStepBefore`
- `IActionResolutionMainStep`
- `IActionResolutionPipelineStepAfter`
- `IActionExecutionPipelineStepBefore`
- `IActionExecutionMainStep`
- `IActionExecutionPipelineStepAfter`

`DefaultActionResolutionMainStep` classifies the update and loads relevant actions into `NavigatorActionResolutionContext.Actions`.

`DefaultActionExecutionMainStep` resolves handler arguments and invokes the action handler. It logs and traces handler exceptions, then continues the pipeline.

Use before or after step interfaces to place extension behavior around the main step. Use the priority attributes already present in the codebase when relative ordering matters.

## Multiple Actions And Exclusivity

Navigator can execute at most one action by default. When `EnableMultipleActionsUsage()` is enabled, resolution uses `FilterExclusiveActionsPipelineStep` instead of `FilterByMultipleActionsPipelineStep`.

Exclusivity is action metadata:

- `None`: the action does not exclude other actions.
- `Category`: only the highest-priority exclusive action in the same category remains.
- `Global`: if the highest-priority action is global, all other actions are removed.

Tests for this behavior are in `src/Navigator.Testing/Pipelines/Steps/FilterExclusiveActionsPipelineStepTests.cs`.

## Argument Resolution

Handlers and conditions can declare parameters. `ActionArgumentProvider` in `src/Navigator/Actions/Arguments/ActionArgumentProvider.cs` resolves each requested type.

Resolution order is:

1. Registered `IArgumentResolver` implementations.
2. The scoped dependency injection container.

Built-in resolvers live in `src/Navigator/Actions/Arguments/Resolvers/`. Extension packages can add resolvers when they introduce injectable domain objects, such as Store entities.

When adding a resolver, test both supported and unsupported parameter types. A resolver should return `null` when it cannot handle a type so later resolvers or DI can try.

## Tracing And Introspection

Tracing contracts live in `src/Navigator.Abstractions/Introspection/`. The default implementation lives in `src/Navigator/Introspection/`.

Core strategies and pipeline steps use `INavigatorTracerFactory<T>` to add tags and record errors. The default sink is `MemoryCacheNavigatorTracerSink`, registered as both `INavigatorTracerSink` and `INavigatorTraceReader`.

When adding meaningful runtime behavior, prefer adding trace tags near the behavior rather than exposing internal state through public APIs.

## Core Change Checklist

For core changes:

1. Identify the layer first: configuration, strategy, catalog, classifier, resolution, execution, argument resolution, or tracing.
2. Inspect the nearest existing implementation and tests before editing.
3. Keep public abstractions stable unless the feature needs a contract change.
4. Update README examples when public APIs or user-visible behavior change.
5. Add or update focused tests in `src/Navigator.Testing`.
6. Run strict validation from `src/`.

```bash
dotnet restore
dotnet build -c Release --no-restore
dotnet test -c Release --no-build --verbosity normal
```

## Common Mistakes To Avoid

- Registering services only in samples when they are required by framework behavior.
- Adding extension behavior directly to core when it can live in an extension package.
- Changing pipeline behavior without testing ordering and interaction with multiple actions.
- Returning a non-null argument from an argument resolver for a type it does not own.
- Updating public APIs without README examples.
- Assuming all package projects are implementation details. Extension and strategy projects are usually independently publishable NuGet packages.
