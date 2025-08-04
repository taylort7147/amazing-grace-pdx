const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('Notes', {
    id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    notesId: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    nucleusNotesId: {
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
          { name: "notesId" },
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
