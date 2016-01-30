import express from 'express';

const router = express.Router();
// GET ENDPOINTS START
// place holder get for sanity

router.get('/game', (req, res) => {
  res.json({
    hello: 'world',
  });
});

// intended to get state of game or can be used to get an active state for sacrafices
router.get('/game/state', (req, res) => {
  res.json({
    state: 'running',
  });
});

// intended to get the current sacrafices;
router.get('/game/sacrifices', (req, res) => {
  res.json({
    activeSacrifices: [
      { user: 'jon' },
      { user: 'jane' },
    ],
  });
});

router.get('/game/sacrifices/new_sacrifices', (req, res) => {
  // Logic to select new sacrifices
  resetVotes();
  console.log('new players selected', res.body);
});

router.get('/game/:sacrifices/vote', (req, res) => {
  res.json();
});

router.get('/game/reset', (req, res) => {
  res.json();
});


// END GET ENDPOINTS
// POST ENDPOINTS
// END POST ENDPOINTS
// UPDATE ENDPOINTS
// END UPDATE ENDPOINTS
