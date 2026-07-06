# Project Rules

## Stack
- Frontend: React + TypeScript, Ant Design, TanStack Router, inside an NX monorepo.
- Backend: .NET 8 Web API.

## TypeScript
- Always strict mode (`"strict": true`).
- Never use `any`. Use `unknown`, proper generics, or explicit types.
- Functional components only — no class components.
- Explicit return types on exported functions and components.

## Security
- Never commit secrets, API keys, tokens, or credentials.
- All configuration is read from environment variables.
- Use `.env.local` for local overrides and add it to `.gitignore`.

## NX Monorepo
- Respect project boundaries (`@nx/enforce-module-boundaries`).
- Prefer `nx affected` over building/testing everything.
- Keep scope tags up to date in `project.json`.
- Always use NX generators (`nx generate`) — never create files by hand.
- Pin exact dependency versions in `package.json` (no `^` or `~`).
- Keep NX and `@nx/*` plugins in sync; update with `nx migrate`.

## .NET Backend
- Follow RESTful conventions for routes and HTTP methods.
- Read all configuration via `IConfiguration` — never hardcode.
- Use dependency injection; avoid static state.

## General
- Small, focused, descriptive commits (Conventional Commits).
- Write tests alongside every new feature.
- Prefer explicit over implicit: avoid magic strings; use constants or enums.