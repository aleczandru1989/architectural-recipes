# 📡 Asynchronous Communication Recipes

## 📖 Overview
This section explores **architectural recipes for asynchronous communication patterns**.  

The goal is to solve the problem of coordinating distributed systems where components interact without requiring immediate responses or tight coupling.

Asynchronous communication enables systems to remain responsive, scalable, and resilient even under heavy load or network variability. By decoupling senders and receivers, it allows messages to be exchanged reliably while each participant continues its work independently. This approach is foundational in modern architectures such as event-driven systems, message queues, and streaming platforms.

These recipes provide practical examples of how asynchronous communication can be implemented across different scenarios. Each pattern demonstrates a way to structure message flow, handle delivery guarantees, and achieve flexibility in system design. Together, they form a toolkit for building robust distributed applications that can evolve and scale gracefully.

By studying and applying these recipes, architects and developers can better understand the trade-offs of each communication style. Whether the need is simple point-to-point messaging or complex aggregation pipelines, these examples serve as reproducible building blocks for real-world systems.

---
## ⏩ Communication Patterns

| Pattern Name            | Apache Kafka                                                                 | RabbitMQ                                                                 |
|-------------------------|-------------------------------------------------------------------------------|---------------------------------------------------------------------------|
| Publish/Subscribe       | ✅ Native via topics and consumer groups                                      | ✅ Native via exchanges and queues                                        |
| Point-to-Point          | ✅ Achieved with one consumer group (only one consumer processes each message)| ✅ Native via queues (work queues)                                        |
| Request/Reply           | ❌ Not native; emulated with request/response topics + correlation IDs        | ✅ Native via direct reply queues (AMQP RPC)                              |
| Routing (Content-based) | ❌ Not broker-level; must be done in consumers or Kafka Streams               | ✅ Supported via header/topic exchanges                                   |
| Filtering               | ✅ Consumers filter messages; Kafka Streams supports advanced filtering       | ✅ Supported via bindings, selectors, and consumer-side filtering         |
| Aggregation             | ✅ Supported via Kafka Streams / ksqlDB                                       | ✅ Supported via plugins or consumer logic                                |
| Transformation          | ✅ Supported via Kafka Streams / Connect transforms                           | ✅ Supported via message converters, plugins, or consumer logic           |
| Dead Letter Queue       | ✅ Supported via DLQ topics (commonly used in Kafka Connect)                  | ✅ Native DLX (Dead Letter Exchange) support                              |
| Competing Consumers     | ✅ Multiple consumers in a group share partitions                             | ✅ Multiple consumers share a queue, broker load-balances                 |
| Priority Queues         | ❌ Not native; emulate via separate topics                                    | ✅ Native support for priority queues                                     |
| Delayed / Scheduled Delivery | ❌ No native delay; emulate with timestamps or external schedulers       | ✅ Supported via delayed message exchange plugin                          |
| Transactional Messaging | ✅ Exactly-once semantics, transactional producers/consumers                  | ✅ Transactions and publisher confirms                                    |
| Replay / Event Sourcing | ✅ Consumers can rewind offsets, reprocess history                            | ❌ Not native; messages consumed are gone unless re-queued                |
| Batch Processing        | ✅ Native batch polling, windowed aggregation via Streams                     | ✅ Possible via consumer prefetch, but less central                       |

## ⏩ Point to Point
Point‑to‑point communication is a messaging pattern where a message is sent to a specific queue, and only one consumer receives and processes it. Unlike publish/subscribe models where multiple subscribers may consume the same message, point‑to‑point ensures exclusive delivery — once a consumer reads the message, it is removed from the queue and cannot be consumed again.
- Producer → Queue → Consumer
- Guarantees one‑to‑one delivery.
- Useful for task distribution (e.g., job processing, work queues).
- Ensures load balancing when multiple consumers listen to the same queue, since each message is delivered to only one of them.

## ⏩ Publish/Subscribe (Pub/Sub)
Publish/Subscribe (pub/sub) communication is a messaging pattern where producers (publishers) send messages to a topic, and multiple consumers (subscribers) can receive those messages. Unlike point‑to‑point, where only one consumer processes a message, pub/sub enables one‑to‑many delivery — every subscriber to a topic gets a copy of the message.
- Producer → Topic → Multiple Subscribers
- Guarantees broadcast delivery to all active subscribers.
- Useful for event distribution (e.g., notifications, real‑time updates).
- Promotes loose coupling, since publishers don’t need to know who the subscribers are.

