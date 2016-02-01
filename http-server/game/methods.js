import game from './index';

export function resetVotes() {
  // import tony's methods and use them;
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
    game.participants.push(goat.toLowerCase());
  }
  return game.participants;
}

export function castVote(name, choice) {
  const vote = game.votes.find((_vote) => _vote.name === name);
  if (vote) {
    vote.choice = choice;
  } else {
    game.votes.push({ name, choice });
  }
  return game.votes;
}

export function tallyVotes(votes) {
  return votes.reduce((prev, curr) => {
    const tally = prev[curr.choice];
    if (!tally) {
      prev[curr.choice] = 0;
    }
    prev[curr.choice]++;
    return prev;
  }, {});
}

export function parseChoice(msg, goats) {
  msg = msg.toLowerCase().trim();
  msg = msg.split(/\s+/);
  const choice = msg[0];
  if (goats.indexOf(choice) > -1) {
    return choice;
  }
  return null;
}
