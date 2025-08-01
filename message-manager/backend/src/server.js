const express = require("express");
const jwt = require("jsonwebtoken");
const cors = require("cors");
const bcrypt = require("bcrypt");
const { Sequelize, DataTypes, QueryTypes } = require("sequelize");
require("dotenv").config();

const app = express();
const SECRET = "supersecret";

const {
    DB_HOST = "localhost",
    DB_PORT = 1433,
    DB_USER = "sa",
    DB_PASSWORD = "Password123",
    DB_NAME = "mydb",
    DB_DIALECT = "mssql",
} = process.env;

// Connect to server WITHOUT specifying a database initially
const sequelizeMaster = new Sequelize("", DB_USER, DB_PASSWORD, {
    host: DB_HOST,
    port: DB_PORT,
    dialect: DB_DIALECT,
    dialectOptions: {
        options: {
            encrypt: false,
            trustServerCertificate: true,
        },
    },
    logging: false,
});

async function ensureDatabase() {
    const result = await sequelizeMaster.query(
        `SELECT database_id FROM sys.databases WHERE Name = :dbName`,
        {
            replacements: { dbName: DB_NAME },
            type: QueryTypes.SELECT,
        }
    );

    if (result.length === 0) {
        console.log(`Database "${DB_NAME}" not found, creating...`);
        await sequelizeMaster.query(`CREATE DATABASE [${DB_NAME}]`);
        console.log(`Database "${DB_NAME}" created.`);
    } else {
        console.log(`Database "${DB_NAME}" exists.`);
    }
}

// Now connect to the actual database
const sequelize = new Sequelize(DB_NAME, DB_USER, DB_PASSWORD, {
    host: DB_HOST,
    port: DB_PORT,
    dialect: DB_DIALECT,
    dialectOptions: {
        options: {
            encrypt: false,
            trustServerCertificate: true,
        },
    },
    logging: false,
});

const User = sequelize.define("User", {
    email: { type: DataTypes.STRING, unique: true, allowNull: false },
    password: { type: DataTypes.STRING, allowNull: false },
    role: { type: DataTypes.STRING, defaultValue: "user" },
});

app.use(cors());
app.use(express.json());

function validateEmail(email) {
    return /.+@.+\..+/.test(email);
}

function validatePassword(password) {
    return typeof password === "string" && password.length >= 8;
}

function authMiddleware(req, res, next) {
    const authHeader = req.headers["authorization"];
    const token = authHeader && authHeader.split(" ")[1];
    if (!token) return res.sendStatus(401);
    jwt.verify(token, SECRET, (err, user) => {
        if (err) return res.sendStatus(403);
        req.user = user;
        next();
    });
}

function adminOnly(req, res, next) {
    if (req.user.role !== "admin") return res.sendStatus(403);
    next();
}

app.post("/api/auth/register", async (req, res) => {
    const { email, password } = req.body;
    if (!validateEmail(email)) return res.status(400).json({ error: "Invalid email format" });
    if (!validatePassword(password)) return res.status(400).json({ error: "Password must be at least 8 characters" });

    const hashedPassword = await bcrypt.hash(password, 10);
    try {
        const user = await User.create({ email, password: hashedPassword });
        res.status(201).json({ message: "User created", user });
    } catch (err) {
        res.status(400).json({ error: "Email already in use" });
    }
});

app.post("/api/auth/login", async (req, res) => {
    const { email, password } = req.body;
    const user = await User.findOne({ where: { email } });
    if (!user || !(await bcrypt.compare(password, user.password))) return res.status(401).send("Invalid");
    const token = jwt.sign({ id: user.id, role: user.role }, SECRET);
    res.json({ token, user });
});

app.get("/api/protected", authMiddleware, (req, res) => {
    res.send("Protected data");
});

app.get("/api/users", authMiddleware, adminOnly, async (req, res) => {
    const users = await User.findAll({ attributes: ["id", "email", "role"] });
    res.json(users);
});

app.post("/api/users/:id/promote", authMiddleware, adminOnly, async (req, res) => {
    const user = await User.findByPk(req.params.id);
    if (!user) return res.sendStatus(404);
    user.role = "admin";
    await user.save();
    res.json(user);
});

const connectWithRetry = async (retries = 10, delay = 3000) => {
    while (retries) {
        try {
            await sequelizeMaster.authenticate();
            console.log("Connected to master DB");

            await ensureDatabase();

            await sequelize.authenticate();
            console.log(`Connected to DB "${DB_NAME}"`);

            await sequelize.sync();

            app.listen(3001, () => console.log("Server running on port 3001"));
            return;
        } catch (err) {
            console.log(`DB connection failed. Retries left: ${retries - 1}`, err.message);
            retries--;
            await new Promise((res) => setTimeout(res, delay));
        }
    }
    throw new Error("Could not connect to the database");
};

connectWithRetry();
