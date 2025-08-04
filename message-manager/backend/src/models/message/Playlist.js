const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('Playlist', {
    Id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    YouTubePlaylistId: {
      type: DataTypes.STRING(64),
      allowNull: true
    },
    SeriesId: {
      type: DataTypes.INTEGER,
      allowNull: false,
      references: {
        model: 'Series',
        key: 'Id'
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
          { name: "SeriesId" },
        ]
      },
      {
        name: "PK_Playlist",
        unique: true,
        fields: [
          { name: "Id" },
        ]
      },
    ]
  });
};
