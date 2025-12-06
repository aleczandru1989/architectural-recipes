# 📬 Point-to-Point Communication

## 📖 Overview
This section explores **point-to-point asynchronous communication**.  
The goal is to solve the problem of delivering a message reliably from one sender to exactly one receiver.

Point-to-point communication ensures that each message is consumed by a single destination, avoiding duplication and guaranteeing targeted delivery. This pattern is especially useful when tasks must be processed independently, workloads need to be distributed evenly, or when strict one-to-one correspondence between producer and consumer is required.

By decoupling sender and receiver, point-to-point messaging provides resilience and scalability. The sender does not need to know the details of the consumer, and the messaging infrastructure ensures that the message is routed correctly. This makes it a foundational building block for distributed systems where reliability and simplicity are key.

---

## 📚 Recipes
We demonstrate point-to-point communication using the following messaging technologies:

- 📨 [Kafka](./Kafka) → Point-to-point messaging with Apache Kafka  
- 🐇 [RabbitMQ](./RabbitMQ) → Point-to-point messaging with RabbitMQ  

