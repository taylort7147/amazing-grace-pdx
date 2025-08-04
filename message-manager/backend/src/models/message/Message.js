const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('Message', {
    id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    title: {
      type: DataTypes.STRING(120),
      allowNull: false
    },
    description: {
      type: DataTypes.STRING(1024),
      allowNull: true
    },
    date: {
      type: DataTypes.DATE,
      allowNull: false
    },
    videoId: {
      type: DataTypes.INTEGER,
      allowNull: true
    },
    audioId: {
      type: DataTypes.INTEGER,
      allowNull: true
    },
    notesId: {
      type: DataTypes.INTEGER,
      allowNull: true
    },
    seriesId: {
      type: DataTypes.INTEGER,
      allowNull: true,
      references: {
        model: 'Series',
        key: 'id'
      }
    }
  }, {
    sequelize,
    tableName: 'Message',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_Message_SeriesId",
        fields: [
          { name: "seriesId" },
        ]
      },
      {
        name: "PK_Message",
        unique: true,
        fields: [
          { name: "id" },
        ]
      },
    ]
  });
};
