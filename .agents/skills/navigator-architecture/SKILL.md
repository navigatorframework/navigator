---
name: navigator-architecture
description: Use when working on Navigator core behavior, request processing, dependency injection, action resolution, execution pipelines, argument resolution, tracing, webhook handling, or any change that needs understanding of the framework runtime architecture.
---

# Navigator Architecture

Use this skill before changing core runtime behavior or when an issue may involve more than one layer of Navigator's request lifecycle.

## Required Reading

Read `docs/ARCHITECTURE.md` first. It is the source of truth for how webhook updates move through the framework, where services are registered, how strategies use the resolution and execution pipelines, how handler arguments are resolved, and where tracing fits.

If the task involves extension packages, also use `navigator-extension-authoring` and read `docs/EXTENSION-AUTHORING.md`.

## Workflow

1. Identify the layer being changed:
   - webhook endpoint and startup: `src/Navigator/Configuration/`
   - dependency injection: `src/Navigator/ServiceCollectionExtensions.cs`
   - strategy orchestration: `src/Navigator/Strategy/`
   - action catalog and builders: `src/Navigator/Catalog/`, `src/Navigator/Actions/`
   - classification: `src/Navigator/Classifier/`
   - pipeline composition and steps: `src/Navigator/Pipelines/`
   - public contracts: `src/Navigator.Abstractions/`
   - tracing: `src/Navigator/Introspection/` and `src/Navigator.Abstractions/Introspection/`
2. Inspect the nearest existing implementation before editing.
3. Preserve public abstractions unless the requested change truly requires a contract change.
4. Update README examples when user-facing behavior, configuration, or public APIs change.
5. Add or update focused tests in `src/Navigator.Testing`.
6. Validate strictly from `src/`:

```bash
dotnet restore
dotnet build -c Release --no-restore
dotnet test -c Release --no-build --verbosity normal
```

## Guardrails

- Do not bypass `INavigatorStrategy` for update processing.
- Do not register services in samples to compensate for missing framework registrations.
- Do not make pipeline ordering implicit in registration order when priority or before/main/after step interfaces are the intended mechanism.
- Do not silently change exception behavior in the execution pipeline; document and test any deliberate change.
- Do not edit `src/Navigator.sln` manually for project operations when `dotnet sln` can do it.
