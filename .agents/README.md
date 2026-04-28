# Agent Guide

This directory contains guidance for AI-assisted development in Navigator. The documentation in `docs/` is the source of truth; skills are short task-specific workflows that point agents to the right files.

## Skills

- `navigator-solution`: use for solution layout, project creation, project references, and validation workflow.
- `navigator-architecture`: use for core framework behavior, request lifecycle, dependency injection, strategies, pipelines, argument resolution, and tracing.
- `navigator-extension-authoring`: use for `Navigator.Extensions.*` packages, extension options, extension pipeline steps, builder APIs, tests, samples, packaging, and README updates.

## Documentation Map

- `docs/ARCHITECTURE.md`: runtime architecture and internal flow.
- `docs/EXTENSION-AUTHORING.md`: how to create and maintain extension packages.
- `.agents/PLANS.md`: requirements for long-running executable plans.

## Default Agent Workflow

1. Read the relevant skill.
2. Read the documentation linked by that skill.
3. Inspect the nearest existing implementation before editing.
4. Keep README examples aligned with user-facing API or behavior changes.
5. Run strict validation from `src/` before finishing:

```bash
dotnet restore
dotnet build -c Release --no-restore
dotnet test -c Release --no-build --verbosity normal
```
