# 🏛️ Architectural Patterns Recipes

## 📖 Overview
This category organizes pattern-level recipes that explain how to address recurring challenges in distributed and service-based systems.

Each child folder contains a focused recipe with its own problem statement, case study, application context, practices, and diagram. The overview here is a guide to those folders; the detailed pattern explanation belongs in each recipe README.

---

## 📂 Contents

- [Bulkhead Isolation](./bulkhead-isolation/README.md) → Partition resources per dependency or workload so one slow or overloaded part cannot exhaust shared capacity.
- [Circuit Breaker](./circuit-breaker/README.md) → Fail fast when a dependency is unhealthy, limit cascading failures, and probe for recovery.
- [Domain-Driven Design](./domain-driven-design/README.md) → A guide to strategic and tactical DDD concepts, each explained with a focused diagram.
- [Rate Limiting](./rate-limiting/README.md) → Control request rates to protect service capacity and apply caller quotas.
- [Saga Pattern](./saga-pattern/README.md) → A pattern-level recipe with an e-commerce case study, orchestration context, operational practices, and a workflow diagram.