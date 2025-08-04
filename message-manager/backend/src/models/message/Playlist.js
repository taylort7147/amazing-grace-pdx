const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('Playlist', {
    id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    youTubePlaylistId: {
      type: DataTypes.STRING(64),
      allowNull: true
    },
    seriesId: {
      type: DataTypes.INTEGER,
      allowNull: false,
      references: {
        model: 'Series',
        key: 'id'
      }
    }
  }, {
    sequelize,
    tableName: 'Playlist',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_Playlist_SeriesId",
        unique: true,
        fields: [
          { name: "seriesId" },
        ]
      },
      {
        name: "PK_Playlist",
        unique: true,
        fields: [
          { name: "id" },
        ]
      },
    ]
  });
};
