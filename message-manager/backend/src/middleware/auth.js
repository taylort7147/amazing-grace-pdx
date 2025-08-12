
import jwt from "jsonwebtoken";

const SECRET = "supersecret"; // This should be stored in an environment variable in production

export function authMiddleware(req, res, next) {
    const authHeader = req.headers["authorization"];
    const token = authHeader && authHeader.split(" ")[1];
    if (!token) return res.sendStatus(401);
    jwt.verify(token, SECRET, (err, user) => {
        if (err) return res.sendStatus(403);
        req.user = user;
        next();
    });
}

export function createToken(payload) {
    return jwt.sign(payload, SECRET, { expiresIn: "7d" });
}

export function adminOnly(req, res, next) {
    if (req.user.role !== "admin") return res.sendStatus(403);
    next();
}
