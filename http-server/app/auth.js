import express from 'express';
import request from 'request';

const app = express();

app.get('/auth', (req, res) => {
  request({
    url: 'http://localhost:27017/',
    method: 'get',
  }, (err, data) => {
    if (err) {
      throw err;
    }
    console.log(data);
  });
  res.send('get all');
});

export default app;
