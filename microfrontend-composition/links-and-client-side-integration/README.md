# 🔗 Recipe: Links and Client‑Side Integration

## 📖 Problem
How can we compose micro‑frontends at the **client side** using:
- **Links** for navigation between full MFEs  
- **iFrames** for embedding isolated MFEs or component routes  
- **JavaScript Transclusion** for mounting components directly into another MFE  

---

## 🛍️ Case Study: E‑Commerce Application
We apply this recipe to our e‑commerce case study:

- **AppShell MFE** → provides navigation links to Dashboard, Product Catalog and Purchase History pages  
- **Dashboard MFE** → contains a dashboard page that embeds components from other MFEs  
  - **Top 5 Products Component** loaded from Product Catalog MFE  
  - **Top 5 Purchases Component** loaded from Purchase History MFE  
- **Product Catalog MFE** → provides both a full catalog page and a reusable “Top 5 Products” component  
- **Purchase History MFE** → provides both a full history page and a reusable “Top 5 Purchases” component  

---

## ⚙️ Functionalities
- **Navigation (Links)** → AppShell routes to Dashboard, Product Catalog and Purchase History pages  
- **iFrames** → In AppShell we use iFrames to load the Dashboard, Product Catalog and Purchase History  
- **JavaScript Transclusion** → Dashboard mounts exposed components directly into its page  

---

## 📊Diagram
![Links and Client‑Side Integration Diagram](../diagrams/application.svg)

---

## 🛠️ Technologies Used
This recipe is implemented using:
- **Angular** → framework for building MFEs and handling routing/navigation  
- **JavaScript** → for dynamic transclusion and component mounting  
- **CSS** → for styling and consistent UI across MFEs  
- **Docker & Docker Compose** → for containerized deployment and orchestration of MFEs  

---

## ▶️ How to Use
To run the example locally you will have to have docker installed on your machine:

1. **Clone the repository**
   ```bash
   git clone https://github.com/aleczandru1989/architectural-recipes.git.git
   cd architectural-recipes/microfrontends/links-and-client-side-integration

2. **Start all MFEs with Docker Compose**
   ```bash
   docker compose -f docker-compose.yml up -d

3. **Access the application**
   - AppShell - http://localhost:5000
   - Dashboard - http://localhost:5001
   - Product Catalog - http://localhost:5002
   - Purchase History - http://localhost:5003