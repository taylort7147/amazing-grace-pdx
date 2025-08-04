const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('Notes', {
    id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    url: {
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
    tableName: 'Notes',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_Notes_MessageId",
        unique: true,
        fields: [
          { name: "messageId" },
        ]
      },
      {
        name: "PK_Notes",
        unique: true,
        fields: [
          { name: "id" },
        ]
      },
    ]
  });
};
