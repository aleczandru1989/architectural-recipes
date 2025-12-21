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
    subgraph External_Network [External Traffic]
        User([User Browser]) --> Nginx[NGINX / External LB]
    end

    subgraph Service_Discovery [Service Discovery Layer]
        Eureka[**Eureka Server**<br/>localhost:8761]
    end

    subgraph Internal_Network [Internal Ecosystem]
        %% Resilient Gateway Layer
        subgraph Gateway_Cluster [API Gateway Cluster]
            GW1[Gateway Inst 1]
            GW2[Gateway Inst 2]
        end
        
        %% Catalog Service Cluster (The Caller)
        subgraph Catalog_Cluster [Catalog Service Cluster]
            Cat1[Catalog Inst 1]
            Cat2[Catalog Inst 2]
            Cat3[Catalog Inst 3]
            LB_Logic{Steeltoe <br/> LB Logic}
        end

        %% Order Service Cluster (The Dependency)
        subgraph Order_Cluster [Order Service Cluster]
            Ord1[Order Inst 1]
            Ord2[Order Inst 2]
            Ord3[Order Inst 3]
        end

        %% 1. Discovery Steps
        Gateway_Cluster -- "1. Fetch Registry" --> Eureka
        Catalog_Cluster -- "3. Fetch Registry" --> Eureka
        
        %% 2. Entry Traffic
        Nginx -.-> GW1 & GW2
        GW1 & GW2 -. "2. Route to Catalog" .-> Catalog_Cluster
        
        %% 4. DIRECT DEPENDENCY & LOAD BALANCING
        %% Showing that Catalog Service handles the distribution
        Cat1 ==> LB_Logic
        Cat2 ==> LB_Logic
        Cat3 ==> LB_Logic

        LB_Logic == "Balanced Request 1" ==> Ord1
        LB_Logic == "Balanced Request 2" ==> Ord2
        LB_Logic == "Balanced Request 3" ==> Ord3
        LB_Logic == "Balanced Request 4" ==> Ord1
    end

    %% Styling
    classDef default stroke:#333,stroke-width:2px;
    class Eureka,GW1,GW2,Cat1,Cat2,Cat3,Ord1,Ord2,Ord3,LB_Logic default;
    classDef cluster_style fill:#fdfdfd,stroke:#999,stroke-dasharray: 5 5;
    class Gateway_Cluster,Catalog_Cluster,Order_Cluster cluster_style;
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
