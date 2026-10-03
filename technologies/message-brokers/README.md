# 📨 Message Brokers Recipes

## 📖 Overview
This section covers message brokers, the middleware that receives messages from producers and delivers them to consumers. The recipes explain the main concepts of each broker, one page at a time.

The goal is to give an ordered learning path per broker, so you can understand how it routes, stores, and delivers messages.

Knowing the building blocks of each broker makes it easier to choose between them and to apply the asynchronous communication patterns on top of them.

Read the broker you use, in order, and compare the two to see how a log-based broker differs from a routing-based one.

---

## 📂 Contents

- [Kafka Fundamentals](./kafka-fundamentals/README.md) → A numbered learning path through the main Apache Kafka concepts, from brokers and topics to delivery semantics, each with a diagram.
- [RabbitMQ Fundamentals](./rabbitmq-fundamentals/README.md) → A numbered learning path through the main RabbitMQ concepts, from exchanges and queues to acknowledgements and quorum queues, each with a diagram.
- [Kafka vs RabbitMQ](./kafka-vs-rabbitmq/README.md) → A concept-by-concept comparison table and use-case guidance for choosing between a log-based and a routing-based broker.
