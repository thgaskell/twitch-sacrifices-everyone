import { Router } from 'express';
import { enqueueGoats, getNewGoats } from '../game/methods';
import GAME from './game';
import PHASE from './game/phase'
const router = Router();
// GET ENDPOINTS START
// place holder get for sanity

router.get('/game', (req, res) => {
  res.json({
    hello: 'world',
  });
});

// intended to get state of game or can be used to get an active state for goat
router.get('/game/state', (req, res) => {
  res.json(GAME);
});

// intended to get the current goats;
router.get('/game/goats', (req, res) => {
  res.json(GAME.goats);
});

router.get('/game/start', (req, res) => {
  GAME.phase = PHASE.START;
  // Logic to select new goat
  GAME.goats = getNewGoats(GAME.participants, GAME.prevParticipants, []);
  res.json(GAME.goats);
});

router.get('/game/:goats/vote', (req, res) => {
  const votes = GAME.votes;
  votes.reduce((prev, curr) => {
    const tally = prev[curr.vote];
    if (!tally) {
      prev[curr.vote] = 0;
    }
    prev[curr.vote]++;
    return prev;
  }, {});
  res.json();
});

router.get('/game/reset', (req, res) => {
  GAME.participants = [];
  GAME.votes = [];
  if (GAME.prevParticipants.length >= 6) {
    GAME.prevParticipants.shift();
    GAME.prevParticipants.shift();
  }
  GAME.phase = PHASE.RESET;
  res.send('GAME RESET');
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
