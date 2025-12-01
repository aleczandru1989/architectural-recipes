// server.js
const express = require('express');
const app = express();

app.get('/index.html', (req, res) => {
  res.send(`
    <html>
      <head>
        <esi:include src="/common/header"></esi:include>
      </head>
      <body>
        <div class="container">
          <esi:include src="/common/navbar"></esi:include>
        </div>
        <esi:include src="/common/footer"></esi:include>
      </body>
    </html>
  `);
});

app.get('/common/header', (req, res) => res.send('<header><h1>Header</h1></header>'));
app.get('/common/navbar', (req, res) => res.send('<nav>Navbar links</nav>'));
app.get('/common/footer', (req, res) => res.send('<footer>Footer content</footer>'));

app.listen(8081, () => console.log('Backend running on port 8081'));