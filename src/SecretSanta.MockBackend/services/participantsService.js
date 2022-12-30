const path = require('path');


const participantsService = {
  _participantList: null,

  init: function () {
    if (this._participantList == null) {
      const fs = require('fs');
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
  }
}

module.exports = participantsService;