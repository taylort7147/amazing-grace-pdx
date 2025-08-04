const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('Playlists', {
    Id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    SeriesId: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    NucleusPlaylistId: {
      type: DataTypes.TEXT,
      allowNull: true
    },
    NucleusChurchId: {
      type: DataTypes.TEXT,
      allowNull: true
    },
    NucleusSermonEngineId: {
      type: DataTypes.TEXT,
      allowNull: true
    }
  }, {
    sequelize,
    tableName: 'Playlists',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_Playlists_SeriesId",
        unique: true,
        fields: [
          { name: "SeriesId" },
        ]
      },
      {
        name: "PK_Playlists",
        unique: true,
        fields: [
          { name: "Id" },
        ]
      },
    ]
  });
};
