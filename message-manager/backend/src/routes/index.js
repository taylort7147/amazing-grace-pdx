import express from "express";
const router = express.Router();

import audioRoutes from "./audio.routes.js";
import authRoutes from "./auth.routes.js";
import usersRoutes from "./users.routes.js";
import messagesRoutes from "./messages.routes.js";
import seriesRoutes from "./series.routes.js";

router.use("/auth", authRoutes);
router.use("/users", usersRoutes);
router.use("/messages", messagesRoutes);
router.use("/audio", audioRoutes);
router.use("/series", seriesRoutes);

export default router;
