# 🔗 Recipe: Classic Client Side Integration
## 📖 Problem
How can we compose micro‑frontends at the **client side** using:
- **Links** for navigation between full MFEs  
- **iFrames** for embedding isolated MFEs or component routes  
- **JavaScript Transclusion via Web Components** for mounting components directly into another MFE  

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
- **JavaScript  via Web Compoennts** → Dashboard mounts exposed components directly into its page from Product Catalog and Purchase History
---

## 🛍️ Application Context
The case study models a simplified **e‑commerce application** composed of multiple MFEs:

- **AppShell MFE** → provides navigation and global layout  
- **Dashboard MFE** → aggregates components from other MFEs into a single page view  
- **Product Catalog MFE** → manages product listings and exposes a “Top 5 Products” component  
- **Purchase History MFE** → manages user purchase history and exposes a “Top 5 Purchases” component  

---

## 🔗 Diagram
```mermaid
flowchart LR
    %% Define reusable styles
    classDef mfe fill:#d9f2d9,stroke:#333,stroke-width:1px;
    classDef page fill:#ffe5cc,stroke:#333,stroke-width:1px;
    classDef component fill:#cce5ff,stroke:#333,stroke-width:1px;

    %% AppShell
    subgraph AppShell ["**Shell MFE**"]
        NavBar["Navigation"]
    end

    %% DashboardMFE
    subgraph DashboardMFE ["**Dashboard MFE**"]
        PageDashboard["Dashboard Page"]
        RecommendationComponent["Top 5 Products Component Placeholder"]
        PurchaseHistoryComponent["Top 5 Purchase History Component Placeholder"]
    end

    %% ProductCatalogMFE
    subgraph ProductCatalogMFE ["**Product Catalog MFE**"]
        Top5Products["Top 5 Products Component"]
        PageCatalog["Product Catalog Page"]
    end

    %% PurchaseHistoryMFE
    subgraph PurchaseHistoryMFE ["**Product History MFE**"]
        PageHistory["Purchase History Page"]
        Top5History["Top 5 Purchase History Component"]
    end

    %% Edges
    NavBar -->|localhost:5001| PageDashboard
    NavBar -->|localhost:5002/product-catalogs| PageCatalog
    NavBar -->|localhost:5003/purchase-histories| PageHistory

    RecommendationComponent -->|localhost:5002/components/top-5| Top5Products
    PurchaseHistoryComponent -->|localhost:5003/components/top-5| Top5History

    %% Apply classes
    class AppShell,DashboardMFE,ProductCatalogMFE,PurchaseHistoryMFE mfe;
    class PageDashboard,PageCatalog,PageHistory page;
    class NavBar,Top5Products,Top5History,RecommendationComponent,PurchaseHistoryComponent component;
```
---

## 🛠️ Technologies Used
This recipe is implemented using:
- **HTML** →   for structural markup for each microfrontend (pages, containers, component placeholders).
- **JavaScript** → for dynamic transclusion and component mounting  
- **CSS** → for styling and consistent UI across MFEs  

---

## ▶️ How to Use
To run the example locally you will need to have node.js install and execute the following commands:

1. **Clone the repository**
   ```bash
   git clone https://github.com/aleczandru1989/architectural-recipes.git.git

2. **Navigate to recipe**
   ```bash
   cd architectural-recipes/microfrontend-composition/client-side-integration-classic/app

3. **Install Packages**
   ```bash
   npm install

4. **Start all MFEs**
   ```bash
   npm run start

5. **Access the application**
   - AppShell - http://localhost:5000
   - Dashboard - http://localhost:5001
   - Product Catalog - http://localhost:5002
   - Purchase History - http://localhost:5003