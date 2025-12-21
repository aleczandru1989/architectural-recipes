# 🔗 Recipe: Service Discovery and Configuration Management via HashiCorp Consul

## 📖 Problem

In a microservices architecture, services are often dynamic; they scale up and down, and their IP addresses change frequently (especially in containerized environments like Docker or Kubernetes). Hardcoding these addresses makes the system brittle and impossible to scale.

This recipe demonstrates how to use HashiCorp Consul as a Service Registry to allow microservices to find each other by name rather than IP, and how to implement Client-Side Load Balancing in ASP.NET Core (using Steeltoe or Consul.NET). Consul also provides health checking and a Key/Value store, ensuring traffic only reaches healthy instances while simplifying central configuration.