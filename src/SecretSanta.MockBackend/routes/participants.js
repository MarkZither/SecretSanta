var express = require('express');
var router = express.Router();

/* GET participant listing. */
router.get('/', function(req, res, next) {
  res.setHeader('Content-Type', 'application/json');
  const participantsService = require('../services/participantsService');
  const participants = participantsService.getparticipantList();
  res.send(participants);
});

/* POST new participant. */
router.post('/', function(req, res, next) {
  res.setHeader('Content-Type', 'application/json');
  const participantsService = require('../services/participantsService');
  const participant = participantsService.addNewParticipant(req.body);
  res.send(participant);
});

module.exports = router;
