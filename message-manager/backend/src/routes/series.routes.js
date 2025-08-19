import express from "express";
import * as seriesController from "../controllers/series.controller.js";
import { authMiddleware } from "../middleware/auth.js";
import { seriesSchema } from "@message-manager/shared/schemas/index.js";
import { validate } from "@message-manager/shared/validate.js";

const router = express.Router();

/**
 * @swagger
 * tags:
 *   name: Series
 *   description: API endpoints for managing series
 *
 * @module seriesController
 */

/**
 * @swagger
 * /series:
 *   get:
 *     summary: Get all series entries
 *     tags: [Series]
 *     security:
 *       - bearerAuth: []
 *     responses:
 *       200:
 *         description: Success
 */
router.get("/", authMiddleware, seriesController.getAllSeries);

/**
 * @swagger
 * /series/{id}:
 *   get:
 *     summary: Get a series entry by ID
 *     tags: [Series]
 *     security:
 *       - bearerAuth: []
 *     parameters:
 *       - in: path
 *         name: id
 *         required: true
 *         description: The ID of the series
 *         schema:
 *           type: int
 *     responses:
 *       200:
 *         description: Success
 */
router.get("/:id", authMiddleware, seriesController.getSeriesById);

/**
 * @swagger
 * /series:
 *   post:
 *     summary: Create a new series
 *     tags: [Series]
 *     security:
 *       - bearerAuth: []
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/series'
 *     responses:
 *       201:
 *         description: Created
 */
router.post("/", authMiddleware, validate(seriesSchema), seriesController.createSeries);

export default router;
