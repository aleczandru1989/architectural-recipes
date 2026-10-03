# AGENTS.md

Guidance for AI agents working in this repository.

## Purpose

`architectural-recipes` is a documentation-first collection of architectural patterns captured as reproducible "recipes". Each recipe is a README plus a runnable example (Docker Compose, usually .NET or Node.js). The content reflects the author's personal practice, so keep a first-person-neutral, practical tone and avoid presenting recipes as official standards.

## Folder model

```
README.md                          Root index (level 1)
<category>/README.md               Category overview (level 2)
<category>/<pattern>/README.md     Pattern recipe (level 3)
<category>/<pattern>/<Tech>/       Implementation recipe (level 4)
    README.md
    docker-compose.yml
    app/                           Source code (.sln, Dockerfile per service)
```

Not every category uses every level. Examples:

- `asynchronous-communication/` → category README with a comparison table, then `point-to-point/Kafka/`.
- `microservices/` → pattern-style README (level 3 template), then `service-discovery-and-load-balacing/Eureka/`.
- `microfrontend-composition/` → category README, then recipes directly (`client-side-integration-classic/`).

When a folder such as `microservices/api-gateway/` defines a specific pattern, use the level 3 template. When a folder holds a concrete technology implementation (`Kafka`, `Eureka`, `Consul`, `RabbitMQ`), use the level 4 template.

## Documentation style

- Language: English. Headings start with an emoji, followed by the title.
- Separate major sections with `---`.
- List items use bold lead-in terms: `- **Term** → explanation`.
- Intro paragraphs state the problem and the goal in 2-4 sentences, then the benefit.
- Technologies are described in one line each, bold name first.
- Code and commands go in fenced blocks with a language tag. Every fence must be closed.
- Use the clone URL `https://github.com/aleczandru1989/architectural-recipes.git`.
- `cd` paths in "How to Use" must match real folders.
- File name is always `README.md`.
- Folder names are kebab-case; technology folders are PascalCase (`Kafka`, `Eureka`).

## Diagrams: Mermaid only

All diagrams use Mermaid in a ```` ```mermaid ```` fence under a `## 📊 Diagram` heading. Do not embed image diagrams.

- Use `flowchart LR` for message flows and `flowchart TD` for topologies.
- Group with `subgraph` and bold labels: `subgraph x["**Consumers**"]`.
- Label edges with the action (`-- Produce Message -->`).
- Palette: `#d9f2d9` services/groups, `#ffe5cc` data stores/pages, `#cce5ff` interfaces/components, `#fff3cd` decisions, `#f2e6d9` processors, `#fff9c4` outer network containers. Use stroke `#333`.
- Shapes: `@{ shape: curv-trap }` for Kafka topics, `diam` for decisions, `cyl` for buffers, `[( )]` for databases, `([ ])` for clients.
- Apply styles with `classDef` + `class`, or `style` for single subgraphs.

See the `mermaid-diagram` skill for examples.

## README templates

| Level | Heading | Sections, in order |
|-------|---------|--------------------|
| 1 Root | `# Architectural Recipes` | Intro → `🍲 What Are Architectural Recipes?` → `📚 Contents` (tree of links that shows what the child folders contain) → `🎯 Goals` |
| 2 Category | `# {emoji} {Category} Recipes` | `📖 Overview` (4 short paragraphs: scope, goal, benefit, how to use) → optional comparison table with legend (✅ native, ❌ none, ⚠️ needs extension) → one short entry per child folder describing the recipe content and linking to it; keep the pattern definition and detailed explanation in that child README |
| 3 Pattern | `# 🔗 Recipe: {Pattern}` | `📖 Problem` → `🛍️ Case Study` → `🛍️ Application Context` → `⚙️ General Practices` → `📊 Diagram` |
| 4 Implementation | `# {emoji} Recipe: {Title}` or `# 📬 {Pattern} with {Tech}` | `📖 Problem` or `📖 Overview` → `⚙️ Functionalities` → `📊 Diagram` → `🛠️ Technologies Used` → `▶️ How to Use` |

"How to Use" steps: clone, `cd` to the recipe, `docker compose up -d` (add `--build` when images are built locally), then the verification step (URL, Swagger endpoint, AKHQ topic, logs).

## Rules for changes

- New recipe: **always** add it to the root `README.md` `📚 Contents` tree (under its category, as a link to the real folder) and to the parent category README, in the same change that creates the recipe. This applies to every level: pattern recipes, technology implementations, and new categories.
- A recipe split into several pages (for example numbered concept files) is linked from the root Contents by its folder; list the individual pages in that recipe's own README.
- A recipe is not finished until both indexes link to it and the links resolve.
- Parent/root READMEs introduce and link to their child folders; keep detailed pattern or implementation descriptions in the child README.
- Keep the root Contents links in sync with real folder names.
- Do not rename folders without updating every link.
- Do not edit `bin/`, `obj/` or `*.user` files.
- Keep ports consistent: producer `5000`, AKHQ `8080` in the Kafka recipes.

## Skills

- `.github/skills/new-recipe/` → scaffold a level 3 or level 4 recipe from templates.
- `.github/skills/mermaid-diagram/` → write diagrams following the palette and shapes above.

## Known issues (fix when touching the file)

- `README.MD` / `Readme.md` casing in `microservices/` and `Consul/`; rename to `README.md`.
- Folder name typo `service-discovery-and-load-balacing` (and its root links).
- Clone URL ending `.git.git` in recipe READMEs.
- Unclosed code fences in "How to Use" sections.
- Duplicate `Technologies Used` section in `server-side-integration-esi/README.md`.
- Stale `cd` paths: `filtering/Kafka`, `aggregation/Kafka`, `service-discovery/Eureka`.
- Typos: "Consummer", "Compoennts".
- `microservices/api-gateway/README.MD` is empty; `Consul` and `RabbitMQ` are stubs.
