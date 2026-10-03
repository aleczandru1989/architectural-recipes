# 🛡️ Replication and ISR

## 📖 Concept
Each partition is copied to several brokers according to the topic's **replication factor**. One copy is the **leader**, which handles all reads and writes; the others are **followers** that replicate from it.

The **in-sync replicas (ISR)** are the replicas caught up with the leader. If the leader fails, a new leader is elected from the ISR, so no acknowledged data is lost.

Settings that work together:

- **`replication.factor`** → Number of copies; 3 is common in production.
- **`acks=all`** → The producer waits for all in-sync replicas.
- **`min.insync.replicas`** → Minimum ISR size needed to accept writes; with factor 3, a value of 2 tolerates one broker failure without losing acknowledged data.
- **Unclean leader election** → Keep disabled so an out-of-sync replica cannot become leader and drop data.

## 🛍️ Example
`orders.placed` uses replication factor 3 and `min.insync.replicas=2`. If one broker fails, writes continue; if two fail, producers using `acks=all` get errors rather than risking data loss.

## 📊 Diagram
The leader serves clients while followers replicate, and a follower takes over if the leader fails.

```mermaid
flowchart LR
    Producer(["Producer"])
    subgraph partition["**orders.placed, partition 0**"]
        Leader["Broker 1<br/>Leader"]
        F1["Broker 2<br/>Follower (in sync)"]
        F2["Broker 3<br/>Follower (in sync)"]
    end

    Producer -- Write --> Leader
    Leader -- Replicate --> F1
    Leader -- Replicate --> F2
    F1 -. Elected if leader fails .-> Leader

    classDef service fill:#d9f2d9,stroke:#333,stroke-width:1px;
    classDef store fill:#ffe5cc,stroke:#333,stroke-width:1px;
    classDef interface fill:#cce5ff,stroke:#333,stroke-width:2px;
    class Leader service;
    class F1,F2 store;
    class Producer interface;
    style partition fill:#fff9c4,stroke:#333,stroke-width:2px;
```

**Previous** → [Offsets and Commits](./07-offsets-and-commits.md)  
**Next** → [Delivery Semantics](./09-delivery-semantics.md)
