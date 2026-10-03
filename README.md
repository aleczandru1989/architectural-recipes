# Architectural Recipes

A curated collection of **architectural patterns and practices** captured as practical, reproducible **recipes**.  
This repository reflects **my personal understanding and experience** gained over years of hands-on work in software architecture, infrastructure, and system design.

---

## 🍲 What Are Architectural Recipes?

Just like cooking recipes, each entry here is a **step-by-step guide** to solving a recurring architectural challenge.  
They are not universal truths or official standards — they are **my interpretations and practices**, shaped by real-world troubleshooting, experimentation, and learning.

Each recipe includes:

- **Ingredients** → technologies, frameworks, and tools involved  
- **Preparation steps** → how to combine them effectively  
- **Serving suggestions** → variations, trade-offs, and when to apply them  

---

## 📚 Contents  

We demonstrate multiple recipes, each documented in its own folder:

- [Architectural Patterns](./architectural-patterns)
  - [Bulkhead Isolation](./architectural-patterns/bulkhead-isolation)
  - [Circuit Breaker](./architectural-patterns/circuit-breaker)
  - [Domain-Driven Design](./architectural-patterns/domain-driven-design)
  - [Rate Limiting](./architectural-patterns/rate-limiting)
  - [Saga Pattern](./architectural-patterns/saga-pattern)
- [Microfrontend Composition](./microfrontend-composition)  
  - [Client Side Integration Classic](./microfrontend-composition/client-side-integration-classic) 
  - [Server Side Integration via ESI](./microfrontend-composition/server-side-integration-esi) 
  
- [Asynchronous Communication](./asynchronous-communication)  
  - [Dead Letter Queue](./asynchronous-communication/dead-letter-queue)
  - [Transactional Outbox](./asynchronous-communication/transactional-outbox)
  - [Point to Point](./asynchronous-communication/point-to-point)
    - [Apache Kafka](./asynchronous-communication/point-to-point/Kafka)   
  - [Publish/Subscribe](./asynchronous-communication/publish-subscribe)
    - [Apache Kafka](./asynchronous-communication/publish-subscribe/Kafka)    
  - [Filtering and Routing](./asynchronous-communication/filtering-and-routing)
    - [Apache Kafka](./asynchronous-communication/filtering-and-routing/Kafka)  
  - [Aggregation and Batch Processing](./asynchronous-communication/aggregation-and-batch-processing)
    - [Apache Kafka](./asynchronous-communication/aggregation-and-batch-processing/Kafka)  
- [Microservices](./microservices)  
  - [Service Discovery & Load Balancing](./microservices/service-discovery-and-load-balacing) 
    - [Eureka](./microservices/service-discovery-and-load-balacing/Eureka) 
    - [Consul](./microservices/service-discovery-and-load-balacing/Consul) 
- [Technologies](./technologies)
  - [Message Brokers](./technologies/message-brokers)
    - [Kafka Fundamentals](./technologies/message-brokers/kafka-fundamentals)
    - [RabbitMQ Fundamentals](./technologies/message-brokers/rabbitmq-fundamentals)
    - [Kafka vs RabbitMQ](./technologies/message-brokers/kafka-vs-rabbitmq)
---

## 🎯 Goals

- Capture **my real-world practices** in a reusable format  
- Provide **clear, reproducible examples** for each recipe  
- Encourage **incremental experimentation** and adaptation  
- Serve as a **study map** for advanced architectural domains  

---
