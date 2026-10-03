# 🖥️ Broker and Cluster

## 📖 Concept
A **broker** is a Kafka server that stores partitions and serves producers and consumers. A **cluster** is a group of brokers working together, so storage and traffic are spread across machines and data survives the loss of one.

Clients connect to any broker to discover the cluster, then talk directly to the broker that leads each partition they use.

## 🛍️ Example
The retailer runs three brokers. The partitions of `orders.placed` are spread across them, so no single machine holds or serves all order traffic.

## 📊 Diagram
Partitions are distributed across brokers, and clients connect to the brokers that lead them.

```mermaid
flowchart TD
    Client(["Producers and Consumers"])
    subgraph cluster["**Kafka Cluster**"]
        B1["Broker 1<br/>orders-0"]
        B2["Broker 2<br/>orders-1"]
        B3["Broker 3<br/>orders-2"]
    end

    Client -- Bootstrap and data requests --> B1
    Client -- Data requests --> B2
    Client -- Data requests --> B3

    classDef broker fill:#d9f2d9,stroke:#333,stroke-width:1px;
    classDef interface fill:#cce5ff,stroke:#333,stroke-width:2px;
    class B1,B2,B3 broker;
    class Client interface;
    style cluster fill:#fff9c4,stroke:#333,stroke-width:2px;
```

**Next** → [Topic](./02-topic.md)
