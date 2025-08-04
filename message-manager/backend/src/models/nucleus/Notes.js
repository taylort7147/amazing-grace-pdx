const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('Notes', {
    Id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    NotesId: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    NucleusNotesId: {
      type: DataTypes.TEXT,
      allowNull: true
    }
  }, {
    sequelize,
    tableName: 'Notes',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_Notes_NotesId",
        unique: true,
        fields: [
          { name: "NotesId" },
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
