
const jwt = require("jsonwebtoken");

const SECRET = "supersecret"; // This should be stored in an environment variable in production

function authMiddleware(req, res, next) {
    const authHeader = req.headers["authorization"];
    const token = authHeader && authHeader.split(" ")[1];
    if (!token) return res.sendStatus(401);
    jwt.verify(token, SECRET, (err, user) => {
        if (err) return res.sendStatus(403);
        req.user = user;
        next();
    });
}

function createToken(payload) {
    return jwt.sign(payload, SECRET, { expiresIn: "7d" });
}

function adminOnly(req, res, next) {
    if (req.user.role !== "admin") return res.sendStatus(403);
    next();
}

module.exports = { authMiddleware, adminOnly, createToken };
