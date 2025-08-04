var DataTypes = require("sequelize").DataTypes;

var _Audio = require("./Audio");
var _BibleReferences = require("./BibleReferences");
var _Message = require("./Message");
var _Notes = require("./Notes");
var _Playlist = require("./Playlist");
var _Series = require("./Series");
var _Video = require("./Video");

function initModels(sequelize) {
  var Audio = _Audio(sequelize, DataTypes);
  var BibleReferences = _BibleReferences(sequelize, DataTypes);
  var Message = _Message(sequelize, DataTypes);
  var Notes = _Notes(sequelize, DataTypes);
  var Playlist = _Playlist(sequelize, DataTypes);
  var Series = _Series(sequelize, DataTypes);
  var Video = _Video(sequelize, DataTypes);

  Audio.belongsTo(Message, { as: "Message", foreignKey: "MessageId" });
  Message.hasMany(Audio, { as: "Audios", foreignKey: "MessageId" });
  BibleReferences.belongsTo(Message, { as: "Message", foreignKey: "MessageId" });
  Message.hasMany(BibleReferences, { as: "BibleReferences", foreignKey: "MessageId" });
  Notes.belongsTo(Message, { as: "Message", foreignKey: "MessageId" });
  Message.hasMany(Notes, { as: "Notes", foreignKey: "MessageId" });
  Video.belongsTo(Message, { as: "Message", foreignKey: "MessageId" });
  Message.hasMany(Video, { as: "Videos", foreignKey: "MessageId" });
  Message.belongsTo(Series, { as: "Series", foreignKey: "SeriesId" });
  Series.hasMany(Message, { as: "Messages", foreignKey: "SeriesId" });
  Playlist.belongsTo(Series, { as: "Series", foreignKey: "SeriesId" });
  Series.hasMany(Playlist, { as: "Playlists", foreignKey: "SeriesId" });

  return {
    Audio,
    BibleReferences,
    Message,
    Notes,
    Playlist,
    Series,
    Video
  };
}

module.exports = initModels;
module.exports.initModels = initModels;
module.exports.default = initModels;
