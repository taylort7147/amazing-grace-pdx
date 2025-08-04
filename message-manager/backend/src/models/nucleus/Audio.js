const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('Audio', {
    Id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    AudioId: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    NucleusAudioId: {
      type: DataTypes.TEXT,
      allowNull: true
    }
  }, {
    sequelize,
    tableName: 'Audio',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_Audio_AudioId",
        unique: true,
        fields: [
          { name: "AudioId" },
        ]
      },
      {
        name: "PK_Audio",
        unique: true,
        fields: [
          { name: "Id" },
        ]
      },
    ]
  });
};
