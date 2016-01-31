import PHASE from './phase';

const state = {
  participants: [],
  prevParticipants: [],
  phase: PHASE.RESET,
  votes: [], // {username: String, vote : String, PHASE: status }
  goats: [],
};

export default state;
