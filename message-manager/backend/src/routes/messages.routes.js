const express = require("express");
const router = express.Router();
const messagesController = require("../controllers/messages.controller");
const { authMiddleware } = require("../middleware/auth");

router.get("/", authMiddleware, messagesController.getAllMessages);
module.exports = router;
