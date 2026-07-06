# Service Desk Devin

A full-stack service desk application built as an NX 23 monorepo.

## Stack

| Layer | Technology |
|-------|-----------|
| Frontend | React 19, TypeScript, Vite 8, TanStack Router, Ant Design 6 |
| Backend | .NET 8 Web API, Swashbuckle (Swagger) |
| Shared types | TypeScript library (`@servicedesk/shared-types`) |
| Monorepo | NX 23 |

## Prerequisites

- Node.js 20+
- .NET 8 SDK

## Workspace structure

```
apps/
  web/          React + TypeScript frontend (port 4200)
  api/          .NET 8 Web API backend (port 5121)
libs/
  shared-types/ TypeScript interfaces shared between frontend and backend
```

## Getting started

Install dependencies (first time only):

```bash
npm install
```

Run frontend and backend in separate terminals:

```bash
# Terminal 1 — frontend (http://localhost:4200)
npx nx serve @servicedesk/web

# Terminal 2 — backend (http://localhost:5121)
npx nx serve api
```

Swagger UI is available at `http://localhost:5121/swagger` when the API is running in Development mode.

## API endpoints

| Method | Path | Description |
|--------|------|-------------|
| GET | `/health` | Health check |
| GET | `/tickets` | List all tickets |
| GET | `/tickets/{id}` | Get ticket by ID |
| POST | `/tickets` | Create a new ticket |

## All NX commands

### Build

```bash
# Build all projects
npx nx run-many -t build

# Build a single project
npx nx build @servicedesk/web
npx nx build api
```

### Serve / dev

```bash
npx nx serve @servicedesk/web       # Vite dev server with HMR
npx nx dev   @servicedesk/web       # alias for serve
npx nx serve api                    # dotnet run (requires build first)
```

### Test

```bash
# Run all tests
npx nx run-many -t test

# Frontend unit tests (Vitest)
npx nx test @servicedesk/web

# Backend tests (xUnit)
npx nx test api
```

### Lint & typecheck

```bash
npx nx run-many -t lint
npx nx typecheck @servicedesk/web
```

### Other

```bash
# Visualise the project graph
npx nx graph

# Clean .NET build artefacts
npx nx clean api

# Publish API for deployment
npx nx publish api
```

## Shared types

The `@servicedesk/shared-types` library is the single source of truth for the `Ticket` model. Both the frontend and the backend must stay in sync with it.

Key types: `Ticket`, `TicketStatus`, `TicketPriority`, `CreateTicketDto`, `UpdateTicketDto`.

Path alias available in all TypeScript projects:

```ts
import type { Ticket } from '@servicedesk/shared-types';
```

## Notes

- The API uses an **in-memory store** (seeded with 5 tickets on startup). Data resets on every restart. SQLite + EF Core migration is planned.
- CORS is configured to allow `http://localhost:4200` only.
- The TanStack Router `routeTree.gen.ts` file is auto-generated at build/serve time and is git-ignored.
