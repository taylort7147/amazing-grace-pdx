const { sequelize, User } = require("./models");
const bcrypt = require("bcrypt");

async function seed() {
  await sequelize.sync({ force: true });
  const password = await bcrypt.hash("admin123", 10);
  await User.create({ email: "admin@example.com", password, role: "admin" });
  console.log("Seed data created");
  process.exit();
}

seed();
