import express from 'express';
import CONFIG from './config';
import auth from './app/auth';
import { connect } from './twitch';

const app = express();
app.use(auth);
app.get('/', (req, res) => {
  res.send('hello world');
});

connect((twitchClient) => {
  const server = app.listen(CONFIG.PORT, () => {
    console.log(`Server listining on ${server.address().port}`);
  });
});

export default app;
