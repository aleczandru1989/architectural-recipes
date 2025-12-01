// product-catalog-top5.js
class TopRecommendedProducts extends HTMLElement {
  constructor() {
    super();
    this.attachShadow({ mode: 'open' });
    this.shadowRoot.innerHTML = `
      <style>
        h3 {
          color: #1976d2;
          font-family: sans-serif;
          margin-bottom: 16px;
        }
        .container {
          display: flex;
          gap: 10px;
        }
        .product {
          flex: 1;
          padding: 20px;
          text-align: center;
          font-weight: bold;
          color: white;
          border-radius: 8px;
          font-family: sans-serif;
        }
        .p1 { background-color: #e53935; } /* red */
        .p2 { background-color: #8e24aa; } /* purple */
        .p3 { background-color: #3949ab; } /* blue */
        .p4 { background-color: #43a047; } /* green */
        .p5 { background-color: #fbc02d; color: black; } /* yellow */
      </style>
      <h3>Top Recommended Products</h3>
      <div class="container">
        <div class="product p1">Product 1</div>
        <div class="product p2">Product 2</div>
        <div class="product p3">Product 3</div>
        <div class="product p4">Product 4</div>
        <div class="product p5">Product 5</div>
      </div>
    `;
  }
}

customElements.define('top-recommended-products', TopRecommendedProducts);