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

## 📊 Diagram
![Links and Client‑Side Integration Diagram](../diagrams/application.svg)

---

## ✅ Summary
This recipe demonstrates how to combine **links, iFrames, and JavaScript transclusion** to build a modular e‑commerce application.  

- **Links** → whole‑page navigation  
- **iFrames** → embed isolated MFEs  
- **Transclusion** → reuse components across MFEs  

Together, these techniques enable a flexible, scalable, and maintainable client‑side micro‑frontend architecture.