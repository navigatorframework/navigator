---
name: navigator-solution
description: Use when working inside the Navigator repository to create, modify, or validate .NET projects, solution structure, tests, samples, extensions, or strategies. This skill defines the required `dotnet` CLI workflow, naming conventions, project placement, and validation commands for this solution.
---

# Navigator Solution

This skill applies to work inside the `Navigator` repository and assumes the solution root contains `src/Navigator.sln`.

## Working Rules

- Use the `dotnet` CLI for project and solution operations. Do not hand-edit `.sln` files or create projects through IDE-only flows when `dotnet` provides the command.
- Run solution-level commands from `src/`, where `Navigator.sln` and `global.json` live.
- Respect `global.json`. The repository is pinned to `.NET SDK 10.0.0`, and existing projects target `net10.0`.
- Keep changes aligned with the current solution layout and naming patterns instead of inventing a new structure.

## Repository Shape

The solution is organized under `src/`:

- `Navigator/` and `Navigator.Abstractions/` contain the core libraries.
- `Navigator.Extensions.*` contains extension packages.
- `Navigator.Strategies.*` contains strategy packages.
- `Navigator.Testing/` contains the test project.
- `samples/` contains runnable sample applications.

When adding new code, place it according to that structure:

- New packable framework libraries belong beside the existing top-level projects in `src/`.
- New extension packages should follow `Navigator.Extensions.<Name>`.
- New strategy packages should follow `Navigator.Strategies.<Name>`.
- New samples should live under `src/samples/<SampleName>/`.
- Prefer extending `Navigator.Testing` for tests unless a separate test project is clearly justified.

## Required CLI Workflow

For new projects, always start with the `dotnet` CLI.

Common commands:

```bash
cd src
dotnet new classlib -n Navigator.Extensions.Example
dotnet new xunit -n Navigator.FeatureName.Tests
dotnet new web -n SampleWithExample -o samples/SampleWithExample
dotnet sln Navigator.sln add Navigator.Extensions.Example/Navigator.Extensions.Example.csproj
dotnet sln Navigator.sln add samples/SampleWithExample/SampleWithExample.csproj
dotnet add samples/SampleWithExample/SampleWithExample.csproj reference Navigator/Navigator.csproj
```

Use CLI commands for these operations:

- project creation: `dotnet new`
- solution membership: `dotnet sln add`
- project references: `dotnet add <project> reference <project>`
- package references: `dotnet add <project> package <package>`

Only edit project files manually when the CLI cannot express the needed metadata cleanly.

## Project Conventions

Match the conventions already used in this repository:

- Target `net10.0`.
- Enable nullable reference types.
- Use implicit usings where the surrounding project style already does.
- Packable libraries usually include package metadata and XML documentation generation.
- Sample applications are not packable.
- Test projects should stay non-packable and use the existing xUnit-based stack unless there is a concrete reason to change it.

Before adding a new project, inspect a nearby existing project of the same kind:

- core library: `src/Navigator/Navigator.csproj`
- extension library: `src/Navigator.Extensions.Store/Navigator.Extensions.Store.csproj`
- strategy library: `src/Navigator.Strategies.Queued/Navigator.Strategies.Queued.csproj`
- test project: `src/Navigator.Testing/Navigator.Testing.csproj`
- sample app: `src/samples/Sample/Sample.csproj`

## Validation

Prefer the same validation flow used by CI. Run from `src/`:

```bash
dotnet restore
dotnet build -c Release --no-restore
dotnet test -c Release --no-build --verbosity normal
```

If the change is scoped to one project, a narrower build or test command is acceptable while iterating, but finish with the broadest validation that is practical for the touched area.

## Guardrails

- Do not move projects out of `src/` or create parallel solution layouts.
- Do not introduce Visual Studio-specific setup steps when an equivalent `dotnet` workflow exists.
- Do not add a new sample, extension, or strategy with inconsistent naming.
- Do not manually patch the solution file for routine add/remove operations that `dotnet sln` can perform safely.

## Default Approach

When asked to add a new capability to this repository:

1. Identify whether it belongs in a core library, extension, strategy, sample, or test.
2. Create any new project with `dotnet new`.
3. Add it to `src/Navigator.sln` with `dotnet sln add`.
4. Add references with `dotnet add reference`.
5. Match the nearest existing project’s `.csproj` conventions.
6. Validate with `dotnet restore`, `dotnet build`, and `dotnet test` from `src/`.
