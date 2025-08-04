const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('Notes', {
    Id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    Url: {
      type: DataTypes.TEXT,
      allowNull: false
    },
    MessageId: {
      type: DataTypes.INTEGER,
      allowNull: false,
      references: {
        model: 'Message',
        key: 'Id'
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
          { name: "MessageId" },
        ]
      },
      {
        name: "PK_Notes",
        unique: true,
        fields: [
          { name: "Id" },
        ]
      },
    ]
  });
};
