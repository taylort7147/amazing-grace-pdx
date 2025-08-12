export default function(sequelize, DataTypes) {
  return sequelize.define('Audio', {
    id: {
      autoIncrement: true,
      type: DataTypes.INTEGER,
      allowNull: false,
      primaryKey: true
    },
    audioId: {
      type: DataTypes.INTEGER,
      allowNull: false
    },
    nucleusAudioId: {
      type: DataTypes.TEXT,
      allowNull: true
    }
  }, {
    sequelize,
    tableName: 'Audio',
    schema: 'dbo',
    timestamps: false,
    indexes: [
      {
        name: "IX_Audio_AudioId",
        unique: true,
        fields: [
          { name: "audioId" },
        ]
      },
      {
        name: "PK_Audio",
        unique: true,
        fields: [
          { name: "id" },
        ]
      },
    ]
  });
};
