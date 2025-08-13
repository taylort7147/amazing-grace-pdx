import express from "express";
import * as audioController from "../controllers/audio.controller.js";
import { authMiddleware } from "../middleware/auth.js";
import { audioSchema } from "@message-manager/shared/schemas/audio.schema.js";
import { validate } from "@message-manager/shared/validate.js";

const router = express.Router();

/**
 * @swagger
 * tags:
 *   name: Audio
 *   description: API endpoints for managing audio
 *
 * @module audioController
 */

/**
 * @swagger
 * /audio:
 *   get:
 *     summary: Get all audio entries
 *     tags: [Audio]
 *     security:
 *       - bearerAuth: []
 *     responses:
 *       200:
 *         description: Success
 */
router.get("/", authMiddleware, audioController.getAllAudio);

/**
 * @swagger
 * /audio/{id}:
 *   get:
 *     summary: Get an audio entry by ID
 *     tags: [Audio]
 *     security:
 *       - bearerAuth: []
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         description: The ID of the audio
 *         schema:
 *           type: string
 *     responses:
 *       200:
 *         description: Success
 */
router.get("/:id", authMiddleware, audioController.getAudioById);

/**
 * @swagger
 * /audio:
 *   post:
 *     summary: Create a new audio
 *     tags: [Audio]
 *     security:
 *       - bearerAuth: []
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/audio'
 *     responses:
 *       201:
 *         description: Created
 */
router.post("/", authMiddleware, validate(audioSchema), audioController.createAudio);

export default router;
