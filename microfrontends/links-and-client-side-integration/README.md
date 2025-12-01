# 📖 Case Study: Micro‑Frontend Composition with Links, JavaScript Transclusion, and iFrames

## 🍳 Recipe Overview
This recipe explores the **architectural problem of micro‑frontend composition**: how to build a modular application where independent MFEs (Micro‑Frontends) can be composed together into a unified user experience.  

The challenge is to balance **isolation** (each MFE is independently deployable) with **integration** (pages and components can be reused across contexts).  

We demonstrate the following composition techniques:

- **Links** → navigation between full MFEs  
- **iFrames** → embedding MFEs or component routes inside other MFEs  
- **JavaScript Transclusion** → directly mounting exposed components from one MFE into another  

---

## 🛒 Application: E‑Commerce Platform
This case study models a simplified **e‑commerce application** composed of multiple MFEs.

### 🔗 Application Diagram
![Application Diagram](./../diagrams/application.svg)  
---

## 📚 Case Study Description
The application is split into multiple MFEs, each responsible for a distinct domain:

- **AppShell MFE** → provides navigation and global layout  
- **Dashboard MFE** → aggregates components from other MFEs into a single page view  
- **Product Catalog MFE** → manages product listings and exposes a “Top 5 Products” component  
- **Purchase History MFE** → manages user purchase history and exposes a “Top 5 Purchases” component  

This separation allows independent deployment, scaling, and evolution of each domain while still delivering a cohesive user experience.

---

## ⚙️ Functionalities
- **Navigation** → handled by the AppShell MFE using links  
- **Dashboard Composition** → embeds components from other MFEs using JavaScript transclusion and iFrames  
- **Product Catalog** → provides both a full catalog page and a reusable “Top 5 Products” component  
- **Purchase History** → provides both a full history page and a reusable “Top 5 Purchases” component  

---

## 🧩 Architecture Breakdown

### 1. AppShell MFE
- Provides **global navigation** (links to Product Catalog and Purchase History pages)  
- Hosts the **Dashboard MFE**  

### 2. Dashboard MFE
- Contains a **Dashboard Page**  
- Embeds:  
  - **Top 5 Products Component** (loaded from Product Catalog MFE)  
  - **Top 5 Purchases Component** (loaded from Purchase History MFE)  
- Uses **JavaScript transclusion** to mount components directly  
- Uses **iFrames** for isolated embedding when needed  

### 3. Product Catalog MFE
- Provides a **Product Catalog Page** (full listing)  
- Exposes a **Top 5 Products Component** for reuse in the Dashboard  

### 4. Purchase History MFE
- Provides a **Purchase History Page** (full listing)  
- Exposes a **Top 5 Purchases Component** for reuse in the Dashboard  

---

## ✅ Summary
This recipe demonstrates how to combine **links, iFrames, and JavaScript transclusion** to build a modular e‑commerce application.  

- **Links** → whole‑page navigation  
- **iFrames** → embed isolated MFEs  
- **Transclusion** → reuse components across MFEs  

Together, these techniques enable a flexible, scalable, and maintainable micro‑frontend architecture.