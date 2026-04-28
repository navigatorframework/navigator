---
name: navigator-extension-authoring
description: Use when creating, modifying, testing, packaging, or documenting Navigator.Extensions.* packages, extension options, extension services, pipeline steps, action builder methods, extension samples, or extension-specific README examples.
---

# Navigator Extension Authoring

Use this skill for work in `src/Navigator.Extensions.*` or for core changes made to support extension packages.

## Required Reading

Read `docs/EXTENSION-AUTHORING.md` before editing. It defines the expected package shape, extension registration contracts, options patterns, pipeline step placement, tests, samples, README updates, packaging expectations, and strict validation commands.

Read `docs/ARCHITECTURE.md` as well when the extension changes action resolution, action execution, argument resolution, tracing, or dependency injection contracts.

## Workflow

1. Classify the extension work:
   - no configuration: implement `INavigatorExtension`
   - user configuration: implement `INavigatorExtension<TOptions>` with an options type implementing `INavigatorExtensionOptions`
   - action behavior: add builder extension methods that store options on `BotActionInformation`
   - update filtering or processing: add a resolution or execution pipeline step
   - external state: register the required services and document lifetimes
2. Match a nearby package:
   - simple pipeline extension: `src/Navigator.Extensions.Cooldown/`
   - action filtering extension: `src/Navigator.Extensions.Probabilities/`
   - EF Core and custom options: `src/Navigator.Extensions.Store/`
   - command-style management extension: `src/Navigator.Extensions.Management/`
3. Update or add tests in `src/Navigator.Testing` for the extension's observable behavior.
4. Update README examples when the extension's public API or behavior changes.
5. Add or update a sample under `src/samples/` when the extension introduces a major user workflow.
6. Validate strictly from `src/`:

```bash
dotnet restore
dotnet build -c Release --no-restore
dotnet test -c Release --no-build --verbosity normal
```

## Guardrails

- Treat extension packages as independently publishable NuGet packages unless the user says otherwise.
- Keep extension-specific implementation inside the extension project; put only reusable contracts in `Navigator.Abstractions`.
- Do not add package-only behavior that cannot be demonstrated by tests or a sample.
- Do not require users to register internal extension services manually after calling `WithExtension`.
- Do not introduce user-visible API changes without updating README usage.
