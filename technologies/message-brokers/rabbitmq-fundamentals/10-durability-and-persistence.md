# 💾 Durability and Persistence

## 📖 Concept
Surviving a broker restart needs three things to line up:

- **Durable exchange** → The exchange definition is kept.
- **Durable queue** → The queue definition is kept.
- **Persistent message** → The message is written to disk (`delivery_mode = 2`).

If any one is missing, the data can be lost: a transient message in a durable queue is lost on restart, and a persistent message in a non-durable queue disappears with the queue. Persistence alone is not an instant guarantee, so combine it with publisher confirms.

For stronger guarantees across node failures, use **quorum queues**, which replicate messages across nodes with a Raft-based protocol. Classic queues on a single node lose data if that node's disk is lost. Persisting a message costs throughput, so reserve it for data that matters.

## 🛍️ Example
`order.placed` messages are persistent and go to a durable quorum queue, so they survive a restart. Live notification pings are transient, because losing one is acceptable.

## 📊 Diagram
Durable declarations and persistent messages are both needed.

```mermaid
flowchart LR
    Message["Persistent message<br/>delivery_mode 2"]
    Exchange@{ shape: diam, label: "Durable exchange" }
    Queue@{ shape: cyl, label: "Durable queue<br/>quorum" }
    Disk[("Disk and replicas")]
    Restart["Broker restart"]

    Message -- Publish --> Exchange
    Exchange -- Route --> Queue
    Queue -- Write --> Disk
    Restart -. Messages recovered .-> Queue

    classDef store fill:#ffe5cc,stroke:#333,stroke-width:1px;
    classDef decision fill:#fff3cd,stroke:#333,stroke-width:1px;
    classDef component fill:#cce5ff,stroke:#333,stroke-width:1px;
    classDef processor fill:#f2e6d9,stroke:#333,stroke-width:1px;
    class Queue,Disk store;
    class Exchange decision;
    class Message component;
    class Restart processor;
```

**Previous** → [Prefetch and Competing Consumers](./09-prefetch-and-competing-consumers.md)  
**Next** → [Dead Letter Exchange, TTL and Retries](./11-dead-letter-exchange-ttl-and-retries.md)
