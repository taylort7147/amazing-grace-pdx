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

  Audio.belongsTo(Message, { as: "message", foreignKey: "messageId" });
  Message.hasOne(Audio, { as: "audio", foreignKey: "messageId" });
  BibleReferences.belongsTo(Message, { as: "message", foreignKey: "messageId" });
  Message.hasMany(BibleReferences, { as: "bibleReferences", foreignKey: "messageId" });
  Notes.belongsTo(Message, { as: "message", foreignKey: "messageId" });
  Message.hasOne(Notes, { as: "notes", foreignKey: "messageId" });
  Video.belongsTo(Message, { as: "message", foreignKey: "messageId" });
  Message.hasOne(Video, { as: "video", foreignKey: "messageId" });
  Message.belongsTo(Series, { as: "series", foreignKey: "seriesId" });
  Series.hasMany(Message, { as: "messages", foreignKey: "seriesId" });
  Playlist.belongsTo(Series, { as: "series", foreignKey: "seriesId" });
  Series.hasOne(Playlist, { as: "playlist", foreignKey: "seriesId" });

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
