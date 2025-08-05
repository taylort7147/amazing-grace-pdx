const express = require("express");
const cors = require("cors");
const routes = require("./routes");
const authMiddleware = require("./middleware/auth");
const { messageDb, nucleusDb, identityDb } = require("./database");
require("dotenv").config();


async function main() {
    const app = express();

    app.use(cors());
    app.use(express.json());
    app.use("/api", routes);

    function adminOnly(req, res, next) {
        if (req.user.role !== "admin") return res.sendStatus(403);
        next();
    }


    // Creating routes

    app.get("/api/protected", authMiddleware, (req, res) => {
        res.send("Protected data");
    });

    app.get("/api/users", authMiddleware, adminOnly, async (req, res) => {
        const users = await identityDb.tables.User.findAll({ attributes: ["id", "email", "role"] });
        res.json(users);
    });

    app.post("/api/users/:id/promote", authMiddleware, adminOnly, async (req, res) => {
        const user = await identityDb.tables.User.findByPk(req.params.id);
        if (!user) return res.sendStatus(404);
        user.role = "admin";
        await user.save();
        res.json(user);
    });

    app.get("/api/messages", authMiddleware, adminOnly, async (req, res) => {
        const messages = await messageDb.tables.Message.findAll({
            include: [
                { model: messageDb.tables.Audio, as: "audio" },
                { model: messageDb.tables.Notes, as: "notes" },
                { model: messageDb.tables.Video, as: "video" },
                { model: messageDb.tables.Series, as: "series" }
            ]
        });
        res.json(messages);
    });


    console.log("Beginning listening for requests...");
    app.listen(3001, () => console.log("Server running on port 3001"));
}

main().catch((err) => {
    console.error("Error starting server:", err);
    process.exit(1);
});
