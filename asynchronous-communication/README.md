# Asynchronous Communication Recipes

## Overview

This repository explores architectural recipes for asynchronous communication patterns.  
The goal is to solve the problem of coordinating distributed systems where components interact without requiring immediate responses or tight coupling.

Asynchronous communication enables systems to remain responsive, scalable, and resilient even under heavy load or network variability. By decoupling senders and receivers, it allows messages to be exchanged reliably while each participant continues its work independently. This approach is foundational in modern architectures such as event-driven systems, message queues, and streaming platforms.

These recipes provide practical examples of how asynchronous communication can be implemented across different scenarios. Each pattern demonstrates a way to structure message flow, handle delivery guarantees, and achieve flexibility in system design. Together, they form a toolkit for building robust distributed applications that can evolve and scale gracefully.

By studying and applying these recipes, architects and developers can better understand the trade-offs of each communication style. Whether the need is simple point-to-point messaging or complex aggregation pipelines, these examples serve as reproducible building blocks for real-world systems.

---

## Recipes

- [Point-to-Point](./point-to-point/README.md)  
- [Publish-Subscribe](./publish-subscribe/README.md)  
- [Request-Reply](./request-reply/README.md)  
- [Fan-Out](./fan-out/README.md)  
- [Routing](./routing/README.md)  
- [Filtering](./filtering/README.md)  
- [Aggregation](./aggregation/README.md)  
- [Replication](./replication/README.md)  