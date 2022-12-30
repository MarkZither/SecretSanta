var express = require('express');
var router = express.Router();

/* GET users listing. */
router.get('/', function(req, res, next) {
  res.setHeader('Content-Type', 'application/json');
  const participantsService = require('../services/participantsService');
  const participants = participantsService.getparticipantList();
  res.send(participants);
});

module.exports = router;
