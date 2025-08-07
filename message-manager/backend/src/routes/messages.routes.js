const express = require("express");
const router = express.Router();
const messagesController = require("../controllers/messages.controller");
const { authMiddleware } = require("../middleware/auth");

/**
 * @swagger
 * /messages:
 *   get:
 *     summary: Get all message entries
 *     security:
 *       - bearerAuth: []
 *     responses:
 *       200:
 *         description: Success
 */
router.get("/", authMiddleware, messagesController.getAllMessages);
module.exports = router;
