# 📥 Consumer and Consumer Group

## 📖 Concept
A **consumer** reads records from topic partitions, pulling them at its own pace. Consumers that share a `group.id` form a **consumer group**: Kafka assigns each partition to exactly one consumer in the group, so the group splits the work.

- Different groups each receive all records of a topic (publish/subscribe).
- Within one group, each record is processed by one consumer (competing consumers).
- If a consumer joins or leaves, Kafka **rebalances** the partitions.
- More consumers than partitions leaves the extra consumers idle.

## 🛍️ Example
Billing runs two instances in the group `billing`, each reading some partitions of `orders.placed`. Inventory uses its own group `inventory` and reads every order too.

## 📊 Diagram
Each group gets every record; inside a group each partition has one consumer.

```mermaid
flowchart LR
    P0@{ shape: cyl, label: "Partition 0" }
    P1@{ shape: cyl, label: "Partition 1" }

    subgraph billing["**Group: billing**"]
        B1["Billing 1"]
        B2["Billing 2"]
    end
    subgraph inventory["**Group: inventory**"]
        I1["Inventory 1"]
    end

    P0 -- Consume --> B1
    P1 -- Consume --> B2
    P0 -- Consume --> I1
    P1 -- Consume --> I1

    classDef store fill:#ffe5cc,stroke:#333,stroke-width:1px;
    classDef service fill:#d9f2d9,stroke:#333,stroke-width:1px;
    class P0,P1 store;
    class B1,B2,I1 service;
    style billing fill:#cce5ff,stroke:#333,stroke-width:2px;
    style inventory fill:#cce5ff,stroke:#333,stroke-width:2px;
```

**Previous** → [Producer](./05-producer.md)  
**Next** → [Offsets and Commits](./07-offsets-and-commits.md)
