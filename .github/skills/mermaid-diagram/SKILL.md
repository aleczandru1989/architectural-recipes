---
name: mermaid-diagram
description: Write or fix Mermaid diagrams for architectural-recipes READMEs using the repo palette and shapes. Use when adding a "📊 Diagram" section, converting a diagram, or restyling one.
---

# Mermaid diagram for recipes

## Rules

- Mermaid only, inside a ```` ```mermaid ```` fence under `## 📊 Diagram`.
- `flowchart LR` for message flows, `flowchart TD` for topologies.
- Bold subgraph labels, labelled edges, one `classDef` per role.

## Palette

| Role | Fill |
|------|------|
| Service / group | `#d9f2d9` |
| Data store / page | `#ffe5cc` |
| Interface / component | `#cce5ff` |
| Decision | `#fff3cd` |
| Processor (filter, aggregator) | `#f2e6d9` |
| Outer network container | `#fff9c4` |

Stroke is `#333`.

## Message flow example

```mermaid
flowchart LR
    Producer["Producer"]
    Topic@{ shape: curv-trap, label: "Kafka <br/> app.order.publish" }
    Decision@{ shape: diam, label: "Matches filter?" }

    subgraph replicas["**Consumers**"]
        C1["Replica 1"]
        C2["Replica 2"]
    end

    Producer -- Produce Message --> Topic
    Topic -- Consume Message --> Decision
    Decision -- Yes --> replicas

    style replicas fill:#d9f2d9,stroke:#333,stroke-width:2px;
    style Decision fill:#fff3cd,stroke:#333,stroke-width:2px;
```

## Topology example

```mermaid
flowchart TD
    classDef service fill:#d9f2d9,stroke:#333,stroke-width:1px;
    classDef db fill:#ffe5cc,stroke:#333,stroke-width:1px;
    classDef interface fill:#cce5ff,stroke:#333,stroke-width:2px;

    Client([Web/Mobile App]) --> Gateway["API Gateway"]
    Gateway --> API["Orders API"]
    API --- DB[(Orders DB)]

    class API,Gateway service;
    class DB db;
    class Client interface;
```

## Checklist

1. Every node has a readable label; no raw IDs.
2. Every edge describes the action or protocol where it helps.
3. Colors come from the palette only.
4. Diagram renders in the Mermaid preview.
