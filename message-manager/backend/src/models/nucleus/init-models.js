var DataTypes = require("sequelize").DataTypes;

var _Audio = require("./Audio");
var _Notes = require("./Notes");
var _Playlists = require("./Playlists");
var _Sermons = require("./Sermons");

function initModels(sequelize) {
  var Audio = _Audio(sequelize, DataTypes);
  var Notes = _Notes(sequelize, DataTypes);
  var Playlists = _Playlists(sequelize, DataTypes);
  var Sermons = _Sermons(sequelize, DataTypes);

  return {
    Audio,
    Notes,
    Playlists,
    Sermons,
  };
}

module.exports = initModels;
module.exports.initModels = initModels;
module.exports.default = initModels;
