const bcrypt = require("bcrypt");
const { identityDb } = require("../database");
const { createToken } = require("../middleware/auth");
const { registerSchema, loginSchema } = require("@message-manager/shared/schemas/auth.schema");

exports.register = async (req, res) => {
    const parsed = registerSchema.safeParse(req.body);
    if (!parsed.success) {
        return res.status(400).json({ errors: parsed.error.errors });
    }
    const { email, password } = parsed.data;
    const hashedPassword = await bcrypt.hash(password, 10);
    try {
        const user = await identityDb.tables.User.create({ email, password: hashedPassword });
        res.status(201).json({ message: "User created", user });
    } catch (err) {
        res.status(400).json({ error: "Email already in use" });
    }
};

exports.login = async (req, res) => {
    const parsed = loginSchema.safeParse(req.body);
    if (!parsed.success) {
        return res.status(400).json({ errors: parsed.error.errors });
    }
    const { email, password } = parsed.data;
    const user = await identityDb.tables.User.findOne({ where: { email } });
    if (!user || !(await bcrypt.compare(password, user.password))) return res.status(401).send("Invalid");
    const token = createToken({ id: user.id, role: user.role });
    res.json({ token, user });
};
