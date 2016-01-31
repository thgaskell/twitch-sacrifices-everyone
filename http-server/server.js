import express from 'express';
import CONFIG from './config';
import gameServer from './app/gameServer';
import { connect } from './twitch';
import tunnel from './tunnel';

const app = express();
app.use(gameServer);
app.get('/', (req, res) => {
  res.send('hello world');
});

function startHTTPServer() {
  const server = app.listen(CONFIG.PORT, () => {
    console.log(`Server listining on ${server.address().port}`);
  });
}

function startTCPServer() {
  tunnel.listen(3001, () => {
    console.log('listening on port 3001');
    connect(startHTTPServer);
  });
}

// Soon...
startTCPServer();

export default app;
