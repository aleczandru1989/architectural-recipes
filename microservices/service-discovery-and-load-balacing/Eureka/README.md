# 🔗 Recipe: Service Discovery and Client-Side Load Balancing via Netflix Eureka

## 📖 Problem
In a microservices architecture, services are often dynamic; they scale up and down, and their IP addresses change frequently (especially in containerized environments like Docker or AWS). Hardcoding these addresses makes the system brittle and impossible to scale.

This recipe demonstrates how to use **Netflix Eureka** as a Service Registry to allow microservices to find each other by name rather than IP, and how to implement **Client-Side Load Balancing** in **ASP.NET Core** (using Steeltoe) to ensure high availability and resilience without the need for an internal hardware load balancer.

---

## ⚙️ Functionalities
- 📂 **Dynamic Service Registry**: Services automatically register and de-register themselves with Eureka.
- 🗺️ **Service Discovery**: Microservices look up "phonebook" entries to find other services by name (e.g., `ORDER-SERVICE`).
- ⚖️ **Client-Side Load Balancing**: The calling application (ASP.NET) chooses which instance to call, reducing network hops and latency.
- 💓 **Health Monitoring**: Eureka tracks service health via heartbeats, ensuring traffic is only sent to "Healthy" instances.
- 🛡️ **Self-Preservation**: Eureka protects the registry during network glitches to prevent catastrophic service eviction.

---

## 📊 Diagram

```mermaid
flowchart TD
    subgraph External_Network [External Traffic Layer]
        User([User Browser]) --> Nginx[NGINX / External LB]
    end

    subgraph Internal_Network [Internal Ecosystem Layer]
        
        %% Service Discovery Cluster
        subgraph Discovery_Layer [Service Discovery]
            Eureka[**Eureka Server**]
        end

        %% Catalog Service Cluster
        subgraph Catalog_Cluster [Catalog Service Cluster]
            Cat1[Catalog Inst 1]
            Cat2[Catalog Inst 2]
            Cat3[Catalog Inst 3]
            LB_Logic{Steeltoe <br/> LB Logic}
            
            Cat1 & Cat2 & Cat3 --> LB_Logic
        end

        %% Order Service Cluster
        subgraph Order_Cluster [Order Service Cluster]
            Ord1[Order Inst 1]
            Ord2[Order Inst 2]
            Ord3[Order Inst 3]
        end

        %% 1. SIMPLIFIED DISCOVERY FLOW
        %% Using a single junction to represent the Fetch/Response for all instances
        Cat1 & Cat2 & Cat3 <== "1. Fetch Service Registry & Response" ==> Eureka
        
        %% 2. ENTRY TRAFFIC
        Nginx -. "2. Balanced Ingress" .-> Cat1 & Cat2 & Cat3
        
        %% 3. BALANCED INTERNAL CALL
        LB_Logic ==>|3. Client-Side LB Call| Ord1 & Ord2 & Ord3
    end

    %% Styling
    classDef default stroke:#333,stroke-width:2px;
    class Eureka,Cat1,Cat2,Cat3,Ord1,Ord2,Ord3,LB_Logic default;
    
    %% Outer Containers - Yellow Background
    style External_Network fill:#fff9c4,stroke:#fbc02d,stroke-width:2px
    style Internal_Network fill:#fff9c4,stroke:#fbc02d,stroke-width:2px

    %% Inner Service Clusters & Discovery - White Background
    style Catalog_Cluster fill:#FFFFFF,stroke:#999,stroke-width:2px,stroke-dasharray: 5 5
    style Order_Cluster fill:#FFFFFF,stroke:#999,stroke-width:2px,stroke-dasharray: 5 5
    style Discovery_Layer fill:#FFFFFF,stroke:#999,stroke-width:2px,stroke-dasharray: 5 5
    
    %% Logic Node Highlight
    style LB_Logic fill:#e1f5fe,stroke:#01579b
```

## 🛠️ Technologies Used

**🛰️ Netflix Eureka Server**  
- Serves as the central service registry for all microservices.

**🌉 ASP.NET Core & Steeltoe**  
- Allows .NET services to register as Eureka clients.  
- Provides internal load balancing and service discovery.

**🌀 NGINX**  
- Functions as the external, high‑level load balancer.  
- Distributes incoming traffic across the API Gateway cluster.

**🐳 Docker Compose**  
- Orchestrates the entire multi‑container microservices environment.

## ▶️ How to Use

Follow these steps to run the recipe locally:

1. **Clone the repository**
   ```bash
   git clone https://github.com/aleczandru1989/architectural-recipes.git.git


2. **Navigate to recipe**
   ```bash
   cd microservices/service-discovery/Eureka


3. **Run Docker Compose** 
   ```bash
   docker compose up -d --build

5. **Run Browser**
    ```bash
    curl http://localhost:8081/product/order
