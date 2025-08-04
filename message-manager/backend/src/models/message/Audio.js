const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('Audio', {
    id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    streamUrl: {
      type: DataTypes.TEXT,
      allowNull: false
    },
    downloadUrl: {
      type: DataTypes.TEXT,
      allowNull: false
    },
    messageId: {
      type: DataTypes.INTEGER,
      allowNull: false,
      references: {
        model: 'Message',
        key: 'id'
      }
    }
  }, {
    sequelize,
    tableName: 'Audio',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_Audio_MessageId",
        unique: true,
        fields: [
          { name: "messageId" },
        ]
      },
      {
        name: "PK_Audio",
        unique: true,
        fields: [
          { name: "id" },
        ]
      },
    ]
  });
};
