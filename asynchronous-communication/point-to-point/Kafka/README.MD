# 📬 Point-to-Point with Kafka

## 📖 Overview
This recipe demonstrates **point-to-point asynchronous communication using Apache Kafka**.  
The goal is to show how a producer can send messages to a Kafka topic, and how consumers can reliably process those messages in a one-to-one delivery model.

Point-to-point via Kafka ensures that each message is consumed by exactly one member of a consumer group. This pattern is useful for distributing tasks evenly across multiple replicas while maintaining strict delivery guarantees.

---

## ⚙️ Functionalities
- 📨 **Producer**
  - Publishes messages to a designated Kafka topic.  
  - Operates independently of consumers, ensuring decoupling between message creation and processing.  
  - Can scale horizontally to handle high throughput by partitioning messages across multiple topic partitions.  

- 👥 **Consumers (Consumer Group: `app-consumer-group`)**
  - Five consumer replicas subscribe to the Kafka topic under the same group ID.  
  - Kafka ensures that each message is delivered to **exactly one consumer** within the group.  
  - Workload is balanced across replicas: if one consumer fails, others continue processing seamlessly.  
  - Consumers can commit offsets to track progress and guarantee at-least-once delivery.  

Together, the producer and consumer group demonstrate how Kafka enables reliable point-to-point communication: one producer sends messages, and multiple consumers share the work without duplication.

---

## 📊Diagram
```mermaid
flowchart TD
    Producer["Producer"]
    Kafka["Kafka"]
    
    subgraph replicas["Consumers"]
        C1["Replica 1"]
        C2["Replica 2"]
        C3["Replica 3"]
        C4["Replica 4"]
        C5["Replica 5"]
    end

    Producer --> Kafka
    Kafka --> |app-consumer-group| C1
    Kafka --> |app-consumer-group| C2
    Kafka --> |app-consumer-group| C3
    Kafka --> |app-consumer-group| C4
    Kafka --> |app-consumer-group| C5
```
---

## 🛠️ Technologies Used
This recipe leverages the following technologies:

- **Kafka** → Enables reliable message streaming and communication between producers and consumers.  
- **ASP.NET/.NET** → Provides the framework for building and running both producer and consumer applications.  
- **Docker** → Creates a containerized environment to simplify setup, testing, and deployment of the recipe.  
