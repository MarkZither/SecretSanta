const path = require('path');
const fs = require('fs');

const participantsService = {
  _participantList: null,

  init: function () {
    if (this._participantList == null) {
      const file = path.normalize('./public/jsons/participantsList.json')
      const obj = fs.readFileSync(file, 'utf8');
      this._participantList = JSON.parse(obj);
      this._participantList.totalCount = this._participantList.length
    }
  },

  getparticipantList: function () {
    this.init();
    return this._participantList;
  },

  getParticipantInfo: function (id) {
    const doc = this._participantList.data.find(f => f.id == id);
    return doc;
  },

  addNewParticipant: function (participant) {
    const file = path.normalize('./public/jsons/newParticipant.json')
    const obj = fs.readFileSync(file, 'utf8');
    const newParticipant = JSON.parse(obj);
    return newParticipant;
  },
}

module.exports = participantsService;