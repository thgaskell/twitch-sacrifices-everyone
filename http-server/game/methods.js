import game from './index';

export function resetVotes() {
  // import tony's methods and use them;
  // console.log('reset votes');
}

export function getNewGoats(participants, prevParticipants, newParticipants = []) {
  let goat;
  let candidateGoat;
  // end condition base case
  if (newParticipants.length >= 2) {
    return newParticipants;
  }

  const random = Math.floor(Math.random() * participants.length);
  candidateGoat = participants[random];
  if (prevParticipants.indexOf(candidateGoat) === -1) {
    goat = participants.splice(participants.indexOf(candidateGoat), 1)[0];
    prevParticipants.push(goat);
    newParticipants.push(goat);
  }

  return getNewGoats(participants, prevParticipants, newParticipants);
}

export function enqueueGoats(goat) {
  const goatIndex = game.participants.indexOf(goat);
  if (goatIndex === -1) {
    game.participants.push(goat);
  }
  return game.participants;
}
