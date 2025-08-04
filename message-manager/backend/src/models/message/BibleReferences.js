const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('BibleReferences', {
    id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    startBook: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    startChapter: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    startVerse: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    endBook: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    endChapter: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    endVerse: {
      type: DataTypes.INTEGER,
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
    tableName: 'BibleReferences',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_BibleReferences_MessageId",
        fields: [
          { name: "messageId" },
        ]
      },
      {
        name: "PK_BibleReferences",
        unique: true,
        fields: [
          { name: "id" },
        ]
      },
    ]
  });
};
