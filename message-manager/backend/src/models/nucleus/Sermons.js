const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('Sermons', {
    id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    messageId: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    nucleusSermonId: {
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
    tableName: 'Sermons',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_Sermons_MessageId",
        unique: true,
        fields: [
          { name: "messageId" },
        ]
      },
      {
        name: "PK_Sermons",
        unique: true,
        fields: [
          { name: "id" },
        ]
      },
    ]
  });
};
