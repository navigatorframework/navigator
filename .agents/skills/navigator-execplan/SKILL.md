---
name: navigator-execplan
description: Use when the user asks to plan a Navigator feature, write an implementation plan, create or update an ExecPlan, break down a substantial change into milestones, or prepare a self-contained specification before coding.
---

# Navigator ExecPlan

Use this skill when the user asks for a plan for a feature or substantial repository change.

## Required Reading

Read `.agents/PLANS.md` before writing, updating, or implementing an ExecPlan. That file is the source of truth for the required structure, tone, self-contained context, progress tracking, validation, and living-document rules.

If the plan involves core runtime behavior, also use `navigator-architecture` and read `docs/ARCHITECTURE.md`.

If the plan involves `Navigator.Extensions.*`, also use `navigator-extension-authoring` and read `docs/EXTENSION-AUTHORING.md`.

If the plan involves project or solution layout, also use `navigator-solution`.

## Workflow

1. Clarify scope when needed. Ask concise questions if the expected behavior, public API, package target, or validation outcome is ambiguous.
2. Read the relevant source files before planning. Do not write a generic plan detached from the current code.
3. Produce a self-contained ExecPlan that a novice could follow with only the repository and the plan file.
4. Include user-visible purpose, exact files, concrete steps, validation commands, acceptance behavior, idempotence, and recovery notes.
5. Maintain the required living sections:
   - `Progress`
   - `Surprises & Discoveries`
   - `Decision Log`
   - `Outcomes & Retrospective`
6. If the user asks only for a plan, do not start implementation unless they explicitly ask to proceed.

## Formatting Rules

- Follow `.agents/PLANS.md` exactly.
- When returning an ExecPlan directly in chat, wrap the whole plan in one fenced `md` code block.
- When writing an ExecPlan to a Markdown file that contains only the plan, omit the outer fence.
- Do not nest triple-backtick fences inside an ExecPlan.
- Prefer prose-first milestones with checkboxes only in `Progress`.

## Validation Policy

Plans for code changes should end with strict validation from `src/` unless there is a documented reason not to:

```bash
dotnet restore
dotnet build -c Release --no-restore
dotnet test -c Release --no-build --verbosity normal
```

For documentation-only plans, specify a concrete review or consistency check instead of pretending a build proves the doc change.
