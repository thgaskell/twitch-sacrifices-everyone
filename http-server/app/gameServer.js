import { Router } from 'express';
import { getNewGoats, tallyVotes } from '../game/methods';
import GAME from '../game';
import PHASE from '../game/phase';
const router = Router();
// GET ENDPOINTS START
// place holder get for sanity


// intended to get state of game or can be used to get an active state for goat
router.get('/game/state', (req, res) => {
  res.json(GAME);
});

router.get('/game/reset', (req, res) => {
  GAME.participants = [];
  GAME.votes = [];
  if (GAME.prevParticipants.length >= 6) {
    GAME.prevParticipants.shift();
    GAME.prevParticipants.shift();
  }
  GAME.goats = [];
  GAME.phase = PHASE.RESET;
  res.send('GAME RESET');
});


// intended to get the current goats;
router.get('/game/goats', (req, res) => {
  GAME.phase = PHASE.START;
  res.json(GAME.goats);
});

router.get('/game/start', (req, res) => {
  GAME.phase = PHASE.START;
  // Logic to select new goat
  if (GAME.participants.length < 3) {
    return res.json(null);
  }
  GAME.goats = getNewGoats(GAME.participants, GAME.prevParticipants, []);
  res.json(GAME.goats.join(' '));
});

router.get('/game/votes', (req, res) => {
  const votes = GAME.votes;
  const totalVotes = tallyVotes(votes); // {reduced name : votes, }
  res.json(totalVotes);
});


router.get('/game/stop', (req, res) => {
  GAME.phase = PHASE.STOP;
  res.json(GAME.votes);
});

// END GET ENDPOINTS
// POST ENDPOINTS
// END POST ENDPOINTS
// UPDATE ENDPOINTS
// END UPDATE ENDPOINTS
export default router;
