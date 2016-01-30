import express from 'express';
import CONFIG from './config';
import auth from './app/auth';

const app = express();
app.use(auth);
app.get('/', (req, res) => {
  res.send('hello world');
});

const server = app.listen(CONFIG.PORT, () => {
  console.log(`Server listining on ${server.address().port}`);
});

export default app;
