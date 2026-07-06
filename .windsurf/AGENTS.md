# AGENTS.md

Base context of this repo for the agents.

## Project
ServiceDesk — a ticket board. NX monorepo with a React frontend and a
.NET 8 backend. Practice project: prioritize clarity and keeping build,
tests, and pipeline green.

## Structure
- apps/web — React + TypeScript (Ant Design, TanStack Router)
- apps/api — .NET 8 Web API
- libs/shared-types — shared TS types (single source of the Ticket model)

## Rules
- TypeScript strict, no any, functional components only.
- The Ticket model lives in libs/shared-types; FE and BE reflect it, don't duplicate it.
- Configuration via environment variables. Never commit secrets.
- Conventional Commits. Keep CI green; don't disable tests to make it pass.
- Before large changes: plan and create a checkpoint. Don't merge or
  push --force without asking.