const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('Video', {
    Id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    YouTubeVideoId: {
      type: DataTypes.TEXT,
      allowNull: false
    },
    MessageStartTimeSeconds: {
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
    tableName: 'Video',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_Video_MessageId",
        unique: true,
        fields: [
          { name: "MessageId" },
        ]
      },
      {
        name: "PK_Video",
        unique: true,
        fields: [
          { name: "Id" },
        ]
      },
    ]
  });
};
