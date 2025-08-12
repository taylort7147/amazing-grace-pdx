import bcrypt from "bcrypt";
import { identityDb } from "../database.js";
import { createToken } from "../middleware/auth.js";

export const register = async (req, res) => {
    const { email, password } = req.validatedBody;
    const hashedPassword = await bcrypt.hash(password, 10);
    try {
        const user = await identityDb.tables.User.create({ email, password: hashedPassword });
        res.status(201).json({ message: "User created", user });
    } catch (err) {
        res.status(400).json({ error: "Email already in use" });
    }
};

export const login = async (req, res) => {
    const { email, password } = req.validatedBody;
    const user = await identityDb.tables.User.findOne({ where: { email } });
    if (!user || !(await bcrypt.compare(password, user.password))) return res.status(401).send("Invalid");
    const token = createToken({ id: user.id, role: user.role });
    res.json({ token, user });
};
