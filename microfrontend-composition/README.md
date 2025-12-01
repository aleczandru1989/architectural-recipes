# 🛒 Micro‑Frontends Case Study: E‑Commerce Application

## 📖 Overview
This repository explores **architectural recipes for micro‑frontend composition**.  
The goal is to solve the problem of building a modular application where independent MFEs (Micro‑Frontends) can be composed together into a unified user experience.

We demonstrate multiple composition techniques:
- **Client‑Side Integration Classic**
---

## 🛍️ Application Context
The case study models a simplified **e‑commerce application** composed of multiple MFEs:

- **AppShell MFE** → provides navigation and global layout  
- **Dashboard MFE** → aggregates components from other MFEs into a single page view  
- **Product Catalog MFE** → manages product listings and exposes a “Top 5 Products” component  
- **Purchase History MFE** → manages user purchase history and exposes a “Top 5 Purchases” component  

---

## 🔗 Application Diagram
![Application Diagram](./diagrams/application.svg)

---

## 📚 Recipes
We demonstrate multiple composition techniques, each documented in its own folder:

- [`microfrontends/client-side-integration-classic`](./client-side-integration-classic) → Links and Client‑Side Integration