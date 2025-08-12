import { DataTypes } from "sequelize";

import _Audio from "./Audio.js";
import _Notes from "./Notes.js";
import _Playlists from "./Playlists.js";
import _Sermons from "./Sermons.js";

export default function initModels(sequelize) {
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
