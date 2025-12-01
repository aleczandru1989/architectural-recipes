class TopOrders extends HTMLElement {
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
        ul {
          list-style: none;
          padding: 0;
          margin: 0;
          font-family: sans-serif;
        }
        li {
          padding: 12px;
          margin-bottom: 8px;
          border-radius: 6px;
          font-weight: bold;
          color: white;
        }
        .o1 { background-color: #e53935; } /* red */
        .o2 { background-color: #8e24aa; } /* purple */
        .o3 { background-color: #3949ab; } /* blue */
        .o4 { background-color: #43a047; } /* green */
        .o5 { background-color: #fbc02d; color: black; } /* yellow */
      </style>
      <h3>Top Orders</h3>
      <ul>
        <li class="o1">Order 1</li>
        <li class="o2">Order 2</li>
        <li class="o3">Order 3</li>
        <li class="o4">Order 4</li>
        <li class="o5">Order 5</li>
      </ul>
    `;
  }
}

customElements.define('top-orders', TopOrders);