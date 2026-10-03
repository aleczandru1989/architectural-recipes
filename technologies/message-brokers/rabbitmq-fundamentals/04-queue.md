# 📦 Queue

## 📖 Concept
A **queue** buffers messages until a consumer processes them. Messages are delivered in first-in, first-out order and, once acknowledged, are removed. Queue properties include:

- **Durable** → The queue definition survives a broker restart.
- **Exclusive** → Used by one connection and deleted when it closes.
- **Auto-delete** → Deleted when its last consumer unsubscribes.
- **Arguments** → Settings such as message TTL, maximum length, dead letter exchange, and queue type.

Queue types include **classic**, **quorum** (replicated, recommended for important data), and **stream** (an append-only log). When one queue has several consumers, the broker spreads messages among them.

Ordering is guaranteed per queue with a single consumer; with several consumers, or after redelivery, processing order can differ.

## 🛍️ Example
The `billing` queue holds order messages until a Billing instance charges them. If Billing is down for a while, messages wait in the queue.

## 📊 Diagram
The queue stores messages between the exchange and the consumers.

```mermaid
flowchart LR
    Exchange@{ shape: diam, label: "Exchange<br/>orders" }
    Queue@{ shape: cyl, label: "Queue<br/>billing<br/>durable, quorum" }
    Consumer["Billing Service"]

    Exchange -- Route message --> Queue
    Queue -- Deliver in order --> Consumer
    Consumer -- Acknowledge --> Queue

    classDef store fill:#ffe5cc,stroke:#333,stroke-width:1px;
    classDef decision fill:#fff3cd,stroke:#333,stroke-width:1px;
    classDef service fill:#d9f2d9,stroke:#333,stroke-width:1px;
    class Queue store;
    class Exchange decision;
    class Consumer service;
```

**Previous** → [Exchange](./03-exchange.md)  
**Next** → [Binding and Routing Key](./05-binding-and-routing-key.md)
