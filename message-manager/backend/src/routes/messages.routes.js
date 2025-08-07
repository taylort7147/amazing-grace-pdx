const express = require("express");
const router = express.Router();
const messagesController = require("../controllers/messages.controller");
const { authMiddleware } = require("../middleware/auth");

/**
 * @swagger
 * tags:
 *   name: Messages
 *   description: API endpoints for managing messages
 *
 * @module messagesController
 */

/**
 * @swagger
 * /messages:
 *   get:
 *     summary: Get all message entries
 *     tags: [Messages]
 *     security:
 *       - bearerAuth: []
 *     responses:
 *       200:
 *         description: Success
 */
router.get("/", authMiddleware, messagesController.getAllMessages);

/**
 * @swagger
 * /messages/{id}:
 *   get:
 *     summary: Get a message entry by ID
 *     tags: [Messages]
 *     security:
 *       - bearerAuth: []
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         description: The ID of the message
 *         schema:
 *           type: string
 *     responses:
 *       200:
 *         description: Success
 */
router.get("/:id", authMiddleware, messagesController.getMessageById);

module.exports = router;
