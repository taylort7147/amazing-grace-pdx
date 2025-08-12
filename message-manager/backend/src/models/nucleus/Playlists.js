export default function(sequelize, DataTypes) {
  return sequelize.define('Playlists', {
    id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    seriesId: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    nucleusPlaylistId: {
      type: DataTypes.TEXT,
      allowNull: true
    },
    nucleusChurchId: {
      type: DataTypes.TEXT,
      allowNull: true
    },
    nucleusSermonEngineId: {
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
          { name: "seriesId" },
        ]
      },
      {
        name: "PK_Playlists",
        unique: true,
        fields: [
          { name: "id" },
        ]
      },
    ]
  });
};
