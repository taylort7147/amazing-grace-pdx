const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('Sermons', {
    Id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    MessageId: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    NucleusSermonId: {
      type: DataTypes.TEXT,
      allowNull: true
    },
    NucleusChurchId: {
      type: DataTypes.TEXT,
      allowNull: true
    },
    NucleusSermonEngineId: {
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
          { name: "MessageId" },
        ]
      },
      {
        name: "PK_Sermons",
        unique: true,
        fields: [
          { name: "Id" },
        ]
      },
    ]
  });
};
