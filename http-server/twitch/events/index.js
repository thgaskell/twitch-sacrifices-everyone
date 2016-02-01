import game from '../../game';
import PHASE from '../../game/phase';
import { enqueueGoats, castVote, parseChoice, tallyVotes } from '../../game/methods';

import { broadcast } from '../../tunnel';

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
export function onMessageChannel(nick, text) {
  switch (game.phase) {
    case PHASE.RESET:
      return enqueueGoats(nick);
    case PHASE.START:
      const goatIndex = game.goats.indexOf(nick);
      if (goatIndex < 0) {
        const choice = parseChoice(text, game.goats);
        if (choice) {
          const votes = castVote(nick, choice);
          const goatTally = tallyVotes(votes);
          for (let goat in goatTally) {
            if (goatTally.hasOwnProperty(goat)) {
              broadcast(`!${goat}:${goatTally[goat]}`);
            }
          }
        }
        return;
      }
      return broadcast(`${nick}:${text.trim()}`);
    default:
     return;
  }
}

export function onError(message) {
  console.error(message);
}
