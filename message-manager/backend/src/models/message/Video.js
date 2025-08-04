const Sequelize = require('sequelize');
module.exports = function(sequelize, DataTypes) {
  return sequelize.define('Video', {
    id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    youTubeVideoId: {
      type: DataTypes.TEXT,
      allowNull: false
    },
    messageStartTimeSeconds: {
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
    tableName: 'Video',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_Video_MessageId",
        unique: true,
        fields: [
          { name: "messageId" },
        ]
      },
      {
        name: "PK_Video",
        unique: true,
        fields: [
          { name: "id" },
        ]
      },
    ]
  });
};
