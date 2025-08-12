import express from "express";
const router = express.Router();

import authRoutes from "./auth.routes.js";
import usersRoutes from "./users.routes.js";
import messagesRoutes from "./messages.routes.js";

router.use("/auth", authRoutes);
router.use("/users", usersRoutes);
router.use("/messages", messagesRoutes);

export default router;
