import { identityDb } from ("./database.js");
import { bcrypt } from ("bcrypt");

async function seed() {
  await identityDb.ready;
  await identityDb.sequelize.sync({ force: true });
  const password = await bcrypt.hash("admin123", 10);
  await identityDb.tables.User.create({ email: "admin@example.com", password, role: "admin" });
  console.log("Seed data created");
  process.exit();
}

seed();
