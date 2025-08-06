const express = require("express");
const router = express.Router();

const authRoutes = require("./auth.routes");
const usersRoutes = require("./users.routes");
const messagesRoutes = require("./messages.routes");

router.use("/auth", authRoutes);
router.use("/users", usersRoutes);
router.use("/messages", messagesRoutes);

module.exports = router;
