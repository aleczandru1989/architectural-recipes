---
name: new-recipe
description: Scaffold a new architectural recipe (pattern-level or technology implementation) with README, docker-compose and app folder, and update the root and category indexes. Use when adding a pattern such as api-gateway or a technology folder such as Kafka, Consul or RabbitMQ.
---

# New recipe

Follow `AGENTS.md` for style. Use `mermaid-diagram` for the diagram.

## Choose the template

- Folder defines a pattern (e.g. `microservices/api-gateway/`) → [pattern template](./assets/pattern-readme.md).
- Folder is a technology implementation (e.g. `.../Kafka/`, `.../Consul/`) → [implementation template](./assets/implementation-readme.md).

## Steps

1. Read a sibling recipe of the same level to match tone and depth.
2. Create the folder (kebab-case for patterns, PascalCase for technologies).
3. Create `README.md` from the chosen template; fill every section, no placeholders left.
4. For implementations, add `docker-compose.yml` and `app/` (solution, one project per service, a Dockerfile each).
5. Update the parent folder README to introduce what its child folders contain and link to them. Keep pattern definitions, implementation details, and other recipe-specific explanations in the child README rather than duplicating them in the parent overview.
6. Add the folder to the root `README.md` Contents tree and, where applicable, the parent category README.
7. Verify the `How to Use` steps: clone URL, `cd` path, compose command, verification step.
8. Confirm the Mermaid diagram renders and all code fences are closed.
