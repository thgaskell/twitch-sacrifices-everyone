import { createServer } from 'net';

const connections = [];

function connectionListener(connection) {
  connection.setEncoding('utf8');
  connections.push(connection);
  connection.on('end', () => {
    const connectionIndex = connections.indexOf(connection);
    connections.splice(connectionIndex, 1);
  });
}

export function broadcast(message) {
  connections.forEach((connection) => {
    connection.write(`${message}\n`);
  });
}

const server = createServer(connectionListener);

export default server;
