const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('Audio', {
    Id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    StreamUrl: {
      type: DataTypes.TEXT,
      allowNull: false
    },
    DownloadUrl: {
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
    tableName: 'Audio',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_Audio_MessageId",
        unique: true,
        fields: [
          { name: "MessageId" },
        ]
      },
      {
        name: "PK_Audio",
        unique: true,
        fields: [
          { name: "Id" },
        ]
      },
    ]
  });
};
