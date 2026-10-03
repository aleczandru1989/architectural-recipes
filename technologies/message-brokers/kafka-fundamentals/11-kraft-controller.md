# 🧭 KRaft Controller

## 📖 Concept
Kafka needs a place to keep cluster metadata: which topics exist, where partition leaders are, and which brokers are alive. Older versions stored this in Apache ZooKeeper. Current Kafka uses **KRaft** (Kafka Raft), where a quorum of **controller** nodes keeps the metadata in an internal replicated log using the Raft consensus protocol. ZooKeeper is removed in Kafka 4.0.

- One controller is the active leader and handles changes such as electing partition leaders.
- Controllers can run on dedicated nodes or on the same nodes as brokers (combined mode, usually for small or development setups).
- Production clusters typically use three controllers, so the quorum survives one failure.

## 🛍️ Example
The retailer runs three dedicated controllers and several brokers. When a broker fails, the active controller elects new leaders for its partitions from the in-sync replicas.

## 📊 Diagram
Controllers keep the metadata and brokers follow it.

```mermaid
flowchart TD
    subgraph quorum["**Controller Quorum (KRaft)**"]
        C1["Controller 1<br/>Active"]
        C2["Controller 2"]
        C3["Controller 3"]
    end
    subgraph brokers["**Brokers**"]
        B1["Broker 1"]
        B2["Broker 2"]
        B3["Broker 3"]
    end

    C1 -- Replicate metadata log --> C2
    C1 -- Replicate metadata log --> C3
    C1 -- Metadata updates --> B1
    C1 -- Metadata updates --> B2
    C1 -- Metadata updates --> B3

    classDef service fill:#d9f2d9,stroke:#333,stroke-width:1px;
    classDef component fill:#cce5ff,stroke:#333,stroke-width:1px;
    class C1,C2,C3 component;
    class B1,B2,B3 service;
    style quorum fill:#fff9c4,stroke:#333,stroke-width:2px;
    style brokers fill:#fff9c4,stroke:#333,stroke-width:2px;
```

**Previous** → [Retention and Log Compaction](./10-retention-and-log-compaction.md)  
**Next** → [Kafka Connect and Kafka Streams](./12-kafka-connect-and-streams.md)
