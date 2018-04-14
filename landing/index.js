var express = require('express');

const port = 8568;
//const hostname = '127.0.0.1';
var server = express();
//35.169.200.151

server.set('view engine', 'ejs');
server.use('/public', express.static('public'));

server.get('/', (req, res) => {
  res.render('index');
});

server.get('/docs', (req, res) => {
  res.render('docs');
});

server.get('/*', (req, res) => {
  res.render('fourohfour');
});

server.listen(port);
