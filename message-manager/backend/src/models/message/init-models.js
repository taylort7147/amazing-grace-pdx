import { DataTypes } from "sequelize";

import _Audio from "./Audio.js";
import _BibleReferences from "./BibleReferences.js";
import _Message from "./Message.js";
import _Notes from "./Notes.js";
import _Playlist from "./Playlist.js";
import _Series from "./Series.js";
import _Video from "./Video.js";

export default function initModels(sequelize) {
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
