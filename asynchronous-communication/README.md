# 📡 Asynchronous Communication Recipes

## 📖 Overview
This section explores **architectural recipes for asynchronous communication patterns**.  

The goal is to solve the problem of coordinating distributed systems where components interact without requiring immediate responses or tight coupling.

Asynchronous communication enables systems to remain responsive, scalable, and resilient even under heavy load or network variability. By decoupling senders and receivers, it allows messages to be exchanged reliably while each participant continues its work independently. This approach is foundational in modern architectures such as event-driven systems, message queues, and streaming platforms.

These recipes provide practical examples of how asynchronous communication can be implemented across different scenarios. Each pattern demonstrates a way to structure message flow, handle delivery guarantees, and achieve flexibility in system design. Together, they form a toolkit for building robust distributed applications that can evolve and scale gracefully.

By studying and applying these recipes, architects and developers can better understand the trade-offs of each communication style. Whether the need is simple point-to-point messaging or complex aggregation pipelines, these examples serve as reproducible building blocks for real-world systems.

---

## 📂 Pattern Recipes

- [Dead Letter Queue](./dead-letter-queue/README.md) → Isolate messages that cannot be processed after the retry policy, then investigate and recover them safely.
- [Transactional Outbox](./transactional-outbox/README.md) → Save a data change and its outgoing event in one local transaction, then relay the event to the broker so it is not lost.
## ⏩ Communication Patterns

### Legend
- ✅ Native broker support
- ❌ Not supported at all
- ⚠️ Warning: Not broker-native, requires extension (Kafka Streams / ksqlDB, plugins, or consumer-side logic)

---

| Pattern Name            | Apache Kafka                                                                 | RabbitMQ                                                                 |
|-------------------------|-------------------------------------------------------------------------------|---------------------------------------------------------------------------|
| Publish/Subscribe       | ✅ Native via topics and consumer groups                                      | ✅ Native via exchanges and queues                                        |
| Point-to-Point          | ✅ Native (one consumer group → one consumer processes each message)          | ✅ Native via queues (work queues)                                        |
| Request/Reply           | ❌ Not native; emulated with request/response topics + correlation IDs        | ✅ Native via direct reply queues (AMQP RPC)                              |
| Routing (Content-based) | ⚠️ Requires Kafka Streams / consumer logic                                   | ✅ Native via header/topic exchanges                                      |
| Filtering               | ⚠️ Requires Kafka Streams / consumer-side filtering                          | ⚠️ Requires selectors or consumer-side filtering                          |
| Aggregation             | ⚠️ Requires Kafka Streams / ksqlDB                                           | ⚠️ Requires consumer logic or plugins                                     |
| Transformation          | ⚠️ Requires Kafka Streams / Connect transforms                               | ⚠️ Requires consumer logic or plugins                                     |
| Dead Letter Queue       | ❌ No native DLQ; emulate via DLQ topics                                      | ✅ Native via DLX (Dead Letter Exchange)                                  |
| Competing Consumers     | ✅ Native (multiple consumers in a group share partitions)                    | ✅ Native (multiple consumers share a queue, broker load-balances)        |
| Priority Queues         | ❌ Not native; emulate via separate topics                                    | ✅ Native support for priority queues                                     |
| Delayed / Scheduled Delivery | ❌ Not native; emulate with timestamps or external schedulers            | ⚠️ Requires delayed message exchange plugin                               |
| Transactional Messaging | ✅ Native (exactly-once semantics, transactional producers/consumers)         | ✅ Native (transactions and publisher confirms)                           |
| Replay / Event Sourcing | ✅ Native (consumers can rewind offsets, reprocess history)                   | ❌ Not native; consumed messages are gone unless re-queued                |
| Batch Processing        | ⚠️ Requires Kafka Streams (windowed aggregation)                             | ⚠️ Possible via consumer prefetch or plugins, but not broker-native       |


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

## ⏩ Request/Reply
Request/Reply is a messaging pattern where a producer sends a request and expects a reply. The broker delivers the request to a consumer, which processes it and returns a response, often using correlation IDs or a reply queue.
- Producer → Request Queue → Consumer → Reply Queue → Producer
- Ensures one‑to‑one request/response matching.
- Useful for RPC‑style communication (e.g., queries, synchronous workflows)

## ⏩ Routing (Content‑based)
Routing (Content‑based) is a messaging pattern where the broker inspects message headers or payload and directs each message to the appropriate destination. Unlike simple broadcast models, routing ensures that only the relevant consumers receive the message based on defined rules.
- Producer → Broker → Destination Queue(s)
- Routes messages by content (e.g., region=EU → EU service).
- Useful for selective delivery, reducing unnecessary traffic

## ⏩ Filtering
Filtering is a messaging pattern where consumers receive only the messages that match specific criteria. Instead of processing every message, subscribers define rules (such as headers or properties) to limit delivery to relevant events.
- Producer → Broker → Consumer (with filter)
- Ensures selective delivery based on conditions.
- Useful for reducing traffic and focusing on relevant data

## ⏩ Aggregation
Aggregation is a messaging pattern where multiple related messages are combined into a single, consolidated event. Instead of delivering each message individually, the broker or consumer groups them based on defined criteria (such as keys, counts, or time windows) and produces a summarized result.  
- Producer → Broker → Consumer (with aggregator)  
- Ensures consolidated delivery by merging multiple messages into one output.  
- Useful for reducing message volume and providing higher-level insights (totals, averages, summaries).

## ⏩ Transformation
Transformation is a messaging pattern where the content or structure of a message is modified before delivery. Instead of consuming raw events, subscribers receive adapted messages that are enriched, reformatted, or sanitized.  
- Producer → Broker → Consumer (with transformer)  
- Ensures messages are usable and consistent across systems.  
- Useful for interoperability, enrichment, and data quality improvements.


