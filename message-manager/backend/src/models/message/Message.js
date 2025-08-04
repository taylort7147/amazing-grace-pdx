const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('Message', {
    Id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    Title: {
      type: DataTypes.STRING(120),
      allowNull: false
    },
    Description: {
      type: DataTypes.STRING(1024),
      allowNull: true
    },
    Date: {
      type: DataTypes.DATE,
      allowNull: false
    },
    VideoId: {
      type: DataTypes.INTEGER,
      allowNull: true
    },
    AudioId: {
      type: DataTypes.INTEGER,
      allowNull: true
    },
    NotesId: {
      type: DataTypes.INTEGER,
      allowNull: true
    },
    SeriesId: {
      type: DataTypes.INTEGER,
      allowNull: true,
      references: {
        model: 'Series',
        key: 'Id'
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
          { name: "SeriesId" },
        ]
      },
      {
        name: "PK_Message",
        unique: true,
        fields: [
          { name: "Id" },
        ]
      },
    ]
  });
};
