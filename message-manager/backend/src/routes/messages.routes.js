import express from "express";
import * as messagesController from "../controllers/messages.controller.js";
import { authMiddleware } from "../middleware/auth.js";
import { messageSchema } from "@message-manager/shared/schemas/messages.schema.js";
import { validate } from "@message-manager/shared/validate.js";

const router = express.Router();

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

/**
 * @swagger
 * /messages:
 *   post:
 *     summary: Create a new message
 *     tags: [Messages]
 *     security:
 *       - bearerAuth: []
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/Message'
 *     responses:
 *       201:
 *         description: Created
 */
router.post("/", authMiddleware, validate(messageSchema), messagesController.createMessage);

export default router;
