# 🧩 Partition

## 📖 Concept
A topic is split into one or more **partitions**. Each partition is an ordered, append-only log stored on a broker. Kafka guarantees order **within a partition**, not across the whole topic.

Partitions are the unit of parallelism: each partition is read by at most one consumer in a group, so the partition count limits how many consumers in a group can work at once. When a record has a key, the same key always goes to the same partition (while the partition count is unchanged), which keeps related records in order.

## 🛍️ Example
`orders.placed` has three partitions and uses the customer ID as the key. All orders from one customer land in the same partition and stay in order, while different customers are processed in parallel.

## 📊 Diagram
Records with the same key always go to the same partition.

```mermaid
flowchart LR
    Producer(["Order Service"])
    subgraph topic["**Topic: orders.placed**"]
        P0@{ shape: cyl, label: "Partition 0<br/>customer A" }
        P1@{ shape: cyl, label: "Partition 1<br/>customer B" }
        P2@{ shape: cyl, label: "Partition 2<br/>customer C" }
    end

    Producer -- Key = customer A --> P0
    Producer -- Key = customer B --> P1
    Producer -- Key = customer C --> P2

    classDef store fill:#ffe5cc,stroke:#333,stroke-width:1px;
    classDef interface fill:#cce5ff,stroke:#333,stroke-width:2px;
    class P0,P1,P2 store;
    class Producer interface;
    style topic fill:#fff9c4,stroke:#333,stroke-width:2px;
```

**Previous** → [Topic](./02-topic.md)  
**Next** → [Record](./04-record.md)
