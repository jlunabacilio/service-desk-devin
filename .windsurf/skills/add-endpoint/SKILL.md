---
name: add-endpoint
description: Add a CRUD endpoint to apps/api (.NET 8) with xUnit tests,
  reflecting the Ticket model from libs/shared-types.
tags: [backend, dotnet, endpoint, tests]
---

# Add an API endpoint

Use this procedure whenever adding a new endpoint to the backend.

## Before you start
- [ ] Identify the HTTP verb and route (REST).
- [ ] Confirm the `Ticket` model lives in `libs/shared-types` (don't duplicate it).
- [ ] Confirm the in-memory store already exists in apps/api.

## Steps
1. Add the action to the controller following the existing REST conventions.
2. Reflect the `Ticket` type from the shared library; never redefine the model.
3. Handle the edge cases: 404 if it doesn't exist, 400 if the body is invalid.
4. Add xUnit tests covering the happy path and the edge cases (404, 400).
5. Run `npx nx test api` and confirm everything passes.
6. Run `/review` to verify convention compliance.

## Success criteria
The endpoint responds correctly, the xUnit tests pass, and the Ticket model was not duplicated.