# 🖥️ Broker, Connection and Channel

## 📖 Concept
The **broker** is the RabbitMQ server that receives, routes, and stores messages. Clients connect to it over AMQP (port `5672`, or `5671` with TLS).

- **Connection** → A long-lived TCP connection between an application and the broker. It is relatively expensive, so an application usually opens one or a few.
- **Channel** → A lightweight virtual connection inside a connection. Publishing, consuming, and declaring all happen on a channel. Use a separate channel per thread, since channels are not thread-safe in most clients.

A channel-level error closes only that channel, while a connection-level error closes the connection and every channel in it.

## 🛍️ Example
Order Service opens one connection to the broker and uses one channel to publish events. Billing opens one connection and one channel per consumer thread.

## 📊 Diagram
Many channels share one connection to the broker.

```mermaid
flowchart LR
    subgraph app["**Billing Service**"]
        Ch1["Channel 1<br/>consumer thread"]
        Ch2["Channel 2<br/>consumer thread"]
    end
    Connection["One TCP connection"]
    Broker["RabbitMQ Broker<br/>port 5672"]

    Ch1 --> Connection
    Ch2 --> Connection
    Connection -- AMQP --> Broker

    classDef service fill:#d9f2d9,stroke:#333,stroke-width:1px;
    classDef component fill:#cce5ff,stroke:#333,stroke-width:1px;
    class Broker service;
    class Ch1,Ch2,Connection component;
    style app fill:#fff9c4,stroke:#333,stroke-width:2px;
```

**Next** → [Virtual Host](./02-virtual-host.md)
