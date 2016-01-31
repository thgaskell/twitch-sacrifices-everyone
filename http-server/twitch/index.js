import irc from 'irc';

import { CHANNEL, USER, PASS } from './config';
import {
  onRegistered,
  onMessageChannel,
  onError,
} from './events';

const client = new irc.Client('irc.twitch.tv', USER, {
  channels: [CHANNEL],
  password: PASS,
  autoJoin: false,
});

export function connect(callback) {
  // Add listeners
  client.addListener('registered', onRegistered);
  client.addListener(`message${CHANNEL}`, onMessageChannel);
  client.addListener('error', onError);

  return callback(client);
}
