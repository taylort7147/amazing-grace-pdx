const jwt = require("jsonwebtoken");
const bcrypt = require("bcrypt");
const { identityDb } = require("../database");

const SECRET = "supersecret";

function validateEmail(email) {
    return /.+@.+\..+/.test(email);
}

function validatePassword(password) {
    return typeof password === "string" && password.length >= 8;
}

exports.register = async (req, res) => {
    const { email, password } = req.body;
    if (!validateEmail(email)) return res.status(400).json({ error: "Invalid email format" });
    if (!validatePassword(password)) return res.status(400).json({ error: "Password must be at least 8 characters" });

    const hashedPassword = await bcrypt.hash(password, 10);
    try {
        const user = await identityDb.tables.User.create({ email, password: hashedPassword });
        res.status(201).json({ message: "User created", user });
    } catch (err) {
        res.status(400).json({ error: "Email already in use" });
    }
};

exports.login = async (req, res) => {
    const { email, password } = req.body;
    const user = await identityDb.tables.User.findOne({ where: { email } });
    if (!user || !(await bcrypt.compare(password, user.password))) return res.status(401).send("Invalid");
    const token = jwt.sign({ id: user.id, role: user.role }, SECRET);
    res.json({ token, user });
};
