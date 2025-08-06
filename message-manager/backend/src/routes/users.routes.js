const express = require("express");
const router = express.Router();
const usersController = require("../controllers/users.controller");
const { authMiddleware, adminOnly } = require("../middleware/auth");

router.get("/", authMiddleware, adminOnly, usersController.getAllUsers);
router.post("/:id/promote", authMiddleware, adminOnly, usersController.promoteUser);

module.exports = router;
