# 🛒 Micro‑Frontends Case Study: E‑Commerce Application

## 📖 Overview
This repository explores **architectural recipes for micro‑frontend composition**.  
The goal is to solve the problem of building a modular application where independent MFEs (Micro‑Frontends) can be composed together into a unified user experience.

We demonstrate multiple composition techniques:
- **Links and Client‑Side Integration**
- **Module Federation**
- **Server‑Side Integration**
- **Run time Integration via custom components**
- **Edge‑Side Includes (ESI) / CDN Composition**

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
Each folder under `microfrontends/` contains a **specific recipe** with its own README and diagrams:

- `links-and-client-side-integration/` → Links, iFrames, and JavaScript transclusion  
- `module-federation/` → Component sharing via Webpack Module Federation  
- `serverside-integration/` → Server‑side HTML composition  
- `web-components/` → Composition using native Web Components

## 📚 Recipes
We demonstrate multiple composition techniques, each documented in its own folder:

- [`microfrontends/links-and-client-side-integration`](./links-and-client-side-integration) → Links and Client‑Side Integration
- [`microfrontends/module-federation`](./module-federation) → Module Federation
- [`microfrontends/serverside-integration`](./serverside-integration) → Server‑Side Integration
- [`microfrontends/web-components`](./web-components) → Run‑time Integration via Custom Components
- [`microfrontends/edge-side-includes`](./edge-side-includes) → Edge‑Side Includes (ESI) / CDN Composition