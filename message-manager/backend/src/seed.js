const { initializeIdentityDatabase } = require("./database");
const { initModels } = require("./models/identity/init-models");

const bcrypt = require("bcrypt");

async function seed() {
  const sequelize = await initializeIdentityDatabase();
  const { User } = initModels(sequelize);
  await sequelize.sync({ force: true });
  const password = await bcrypt.hash("admin123", 10);
  await User.create({ email: "admin@example.com", password, role: "admin" });
  console.log("Seed data created");
  process.exit();
}

seed();
