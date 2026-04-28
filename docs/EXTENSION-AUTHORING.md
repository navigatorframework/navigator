# Navigator Extension Authoring

This document explains how to create and maintain `Navigator.Extensions.*` packages. It is written for human contributors and AI agents.

## When To Build An Extension

Use an extension package for optional behavior that users opt into with `configuration.WithExtension<TExtension>()`. Good extension candidates include action filters, action metadata, additional handler argument sources, persistence, diagnostics, management commands, or integration with another library.

Use core changes instead when the behavior is required for every Navigator app, changes fundamental request processing, or introduces a public abstraction that multiple packages need.

## Existing Extension Models

Use the closest package as a template:

- `src/Navigator.Extensions.Cooldown/`: simple extension with action metadata and resolution/execution pipeline steps.
- `src/Navigator.Extensions.Probabilities/`: action filtering based on per-action options.
- `src/Navigator.Extensions.Store/`: EF Core persistence, custom options, services, and argument resolvers.
- `src/Navigator.Extensions.Management/`: command-style management features and formatter services.

Extension packages are usually independent NuGet packages. Match package metadata and README packaging conventions from nearby extension `.csproj` files.

## Project Shape

Extension projects live under `src/Navigator.Extensions.<Name>/` and are added to `src/Navigator.sln`.

Use `dotnet` commands from `src/` for project and solution operations:

```bash
dotnet new classlib -n Navigator.Extensions.Example
dotnet sln Navigator.sln add Navigator.Extensions.Example/Navigator.Extensions.Example.csproj
dotnet add Navigator.Extensions.Example/Navigator.Extensions.Example.csproj reference Navigator.Abstractions/Navigator.Abstractions.csproj
```

Most extension projects should:

- target `net10.0`
- enable nullable reference types
- generate XML documentation
- include package metadata
- include `../../README.md` as `PackageReadmeFile`
- reference `Navigator.Abstractions`

Only reference `Navigator` when the extension truly needs core implementation types. Prefer abstractions for package boundaries.

## Extension Registration Contracts

Use `INavigatorExtension` when the extension has no user-provided options:

```csharp
public class ExampleExtension : INavigatorExtension
{
    public void Configure(IServiceCollection services, NavigatorOptions options)
    {
        services.AddScoped<INavigatorPipelineStep, ExampleStep>();
    }
}
```

Use `INavigatorExtension<TOptions>` when users configure extension options:

```csharp
public class ExampleExtension : INavigatorExtension<ExampleOptions>
{
    public void Configure(IServiceCollection services, NavigatorOptions navigatorOptions, ExampleOptions extensionOptions)
    {
        services.AddSingleton(extensionOptions);
        services.AddScoped<INavigatorPipelineStep, ExampleStep>();
    }
}
```

Options types implement `INavigatorExtensionOptions`. If action builders need to read extension options later, store them through `NavigatorOptions.SetExtensionOptions<TExtension, TOptions>()`, which `NavigatorConfiguration.WithExtension<TExtension, TOptions>()` already does.

Users should not need to manually register internal extension services after calling `WithExtension`.

## Action Metadata And Builder Methods

Extensions often add fluent methods to `IBotActionBuilder`, such as `.WithCooldown(...)`. These methods should store extension-specific metadata on the action builder or final `BotActionInformation`.

Keep builder APIs small and explicit. If a method changes user-facing behavior, update README examples and add tests that prove the metadata affects action resolution or execution.

Prefer extension-specific option keys that are stable and namespaced. Avoid string keys that could collide with core builder keys or other extensions.

## Pipeline Steps

Use a resolution step when the extension decides which actions remain candidates. Examples include cooldown and probability filtering.

Use an execution step when the extension runs behavior around a selected action. Examples include setting cooldown state after an action runs or sending a management response.

Choose the correct marker interface:

- `IActionResolutionPipelineStepBefore`: before actions are classified and loaded.
- `IActionResolutionPipelineStepAfter`: after actions are loaded.
- `IActionExecutionPipelineStepBefore`: before the handler runs.
- `IActionExecutionPipelineStepAfter`: after the handler runs.

Most filtering extensions should run after the main resolution step, because they need `NavigatorActionResolutionContext.Actions` to be populated.

Call `await next()` unless the extension intentionally short-circuits the pipeline. Any short-circuit must be documented and tested.

## Services And Lifetimes

Use lifetimes that match the state being managed:

- `Scoped`: per update or per request behavior.
- `Singleton`: stateless services, immutable configuration, or shared state designed for concurrency.
- `HostedService`: background processing owned by the package.

Be careful with singleton pipeline steps that hold mutable state. If shared state is needed, make concurrency behavior explicit and tested.

## Argument Resolvers

Extensions can register `IArgumentResolver` implementations to make new handler parameter types available.

Resolvers should:

- return a value only for types they own
- return `null` for unsupported types
- avoid throwing for ordinary unsupported update shapes
- use scoped services when resolving request-specific data

Store is the main model for this pattern.

## Samples And README Updates

Update README examples whenever the extension changes public API, configuration, package behavior, or user-facing semantics.

Add or update a sample under `src/samples/` when a feature needs more than a short README snippet to understand. Extension samples should show the complete application setup, including `AddNavigator`, `WithExtension`, relevant appsettings, action registration, and `MapNavigator`.

Do not put secrets in samples. Use configuration placeholders such as `TELEGRAM_TOKEN` and `BASE_WEBHOOK_URL`.

## Testing

Add or update tests in `src/Navigator.Testing`. Keep tests focused on observable behavior:

- extension registration adds the expected services or pipeline steps
- builder methods store the expected metadata
- resolution steps keep or remove the correct actions
- execution steps run before or after handlers as intended
- argument resolvers return expected values and defer unsupported types
- options are honored

Follow existing test style: xUnit, FluentAssertions, and NSubstitute.

## Packaging

Extension packages are usually publishable NuGet packages. Match nearby `.csproj` metadata:

- `Title`
- `Authors`
- `Description`
- `PackageProjectUrl`
- `RepositoryUrl`
- `PackageIconUrl`
- `PackageReadmeFile`
- `PackageTags`
- `PackageRequireLicenseAcceptance`
- `PackageLicenseExpression`
- `Copyright`

If an extension introduces a package dependency, keep it in the extension package unless core or abstractions require it.

## Strict Validation

Run strict validation from `src/` before finishing extension work:

```bash
dotnet restore
dotnet build -c Release --no-restore
dotnet test -c Release --no-build --verbosity normal
```

Scoped commands are acceptable while iterating, but final validation should use the full sequence above.

## Extension Change Checklist

1. Confirm the behavior belongs in an extension.
2. Inspect the closest existing extension package.
3. Keep package boundaries clean and prefer `Navigator.Abstractions`.
4. Register all required services inside the extension.
5. Add builder methods, options, pipeline steps, resolvers, or services only as needed.
6. Update README examples for public behavior.
7. Add or update a sample for larger workflows.
8. Add or update tests.
9. Run strict validation.

## Common Mistakes To Avoid

- Making users register services manually after `WithExtension`.
- Adding extension-specific behavior directly to core.
- Adding package dependencies to `Navigator` when only one extension needs them.
- Forgetting README examples after changing fluent APIs.
- Writing a pipeline step that depends on action metadata but runs before actions are loaded.
- Returning unsupported argument types from a resolver instead of `null`.
- Treating extension packages as internal projects when they are usually shipped independently.
