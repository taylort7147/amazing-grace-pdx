const express = require("express");
const jwt = require("jsonwebtoken");
const cors = require("cors");
const bcrypt = require("bcrypt");
const { Sequelize, DataTypes, QueryTypes } = require("sequelize");
require("dotenv").config();
const { initializeMessageDatabase, initializeNucleusDatabase, initializeIdentityDatabase } = require("./database");
const { initModels: initMessageModels } = require("./models/message/init-models");
const { initModels: initNucleusModels } = require("./models/nucleus/init-models");
const { initModels : initIdentityModels } = require("./models/identity/init-models");

async function main() {
    const app = express();
    const SECRET = "supersecret";

    app.use(cors());
    app.use(express.json());

    // Connecting to databases
    const sequelizeMessage = await initializeMessageDatabase();
    const sequelizeNucleus = await initializeNucleusDatabase();
    const sequelizeIdentity = await initializeIdentityDatabase();
    const { Message, Audio, Notes, Video, Series } = initMessageModels(sequelizeMessage, DataTypes);
    const { User } = initIdentityModels(sequelizeIdentity);


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


    // Creating routes
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

    app.get("/api/messages", authMiddleware, adminOnly, async (req, res) => {
        const messages = await Message.findAll({include: [
            {model: Audio, as: "audio"},
            {model: Notes, as: "notes"},
            {model: Video, as: "video"},
            {model: Series, as: "series"}
        ]});
        res.json(messages);
    });


    console.log("Beginning listening for requests...");
    app.listen(3001, () => console.log("Server running on port 3001"));
}

main().catch((err) => {
    console.error("Error starting server:", err);
    process.exit(1);
});
