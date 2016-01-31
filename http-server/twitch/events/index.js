import game from '../../game';
import PHASE from '../../game/phase';
import { enqueueGoats } from '../../game/methods';

/**
 *  Events can be found at:
 *  https://node-irc.readthedocs.org/en/latest/API.html#events
 */

/**
 * Emitted when the server sends the initial 001 line,
 * indicating you’ve connected to the server.
 * @param  {Object} message [description]
 */
export function onRegistered(message) {
  console.log(message);
}

/**
 * Parses all the messages to the channel.
 * @param  {String}nick     The incoming user's name.
 * @param  {String} text    The incoming user's message.
 * @param  {Object} message The raw IRC message.
 */
export function onMessageChannel(nick, text, message) {
  switch (game.phase) {
    case PHASE.RESET:
      return enqueueGoats(nick);
    default:
  }
}

export function onError(message) {
  console.error(message);
}
