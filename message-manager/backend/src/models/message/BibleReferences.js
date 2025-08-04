const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('BibleReferences', {
    Id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    StartBook: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    StartChapter: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    StartVerse: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    EndBook: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    EndChapter: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    EndVerse: {
      type: DataTypes.INTEGER,
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
    tableName: 'BibleReferences',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_BibleReferences_MessageId",
        fields: [
          { name: "MessageId" },
        ]
      },
      {
        name: "PK_BibleReferences",
        unique: true,
        fields: [
          { name: "Id" },
        ]
      },
    ]
  });
};
