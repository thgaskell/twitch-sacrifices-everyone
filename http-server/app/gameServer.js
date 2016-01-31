import { Router } from 'express';
import { resetVotes, getNewGoats } from '../game/methods';
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
  res.json({
    state: 'running',
  });
});

// intended to get the current goat;
router.get('/game/goat', (req, res) => {
  res.json({
    activeGoats: [
      { user: 'jon' },
      { user: 'jane' },
    ],
  });
});

router.get('/game/start', (req, res) => {
  // Logic to select new goat
  // game.resetVotes();
  const goats = getNewGoats([{ user: 'jon' }, { user: 'jane' }]);
  res.json(goats);
});

router.get('/game/:goats/vote', (req, res) => {
  res.json();
});

router.get('/game/reset', (req, res) => {
  res.json();
});

router.get('/game/start', (req, res) => {
  res.json();
});

router.get('/game/stop', (req, res) => {
  res.json();
});


// END GET ENDPOINTS
// POST ENDPOINTS
// END POST ENDPOINTS
// UPDATE ENDPOINTS
// END UPDATE ENDPOINTS
export default router;
