import express from 'express';
import CONFIG from './config';
import auth from './app/auth';
import gameServer from './app/gameServer';

const app = express();
app.use(auth);
app.use(gameServer)
app.get('/', (req, res) => {
  res.send('hello world');
});

const server = app.listen(CONFIG.PORT, () => {
  console.log(`Server listining on ${server.address().port}`);
});

export default app;
