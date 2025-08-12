import { identityDb } from "../database.js";


export const getAllUsers = async (req, res) => {
    const users = await identityDb.tables.User.findAll({ attributes: ["id", "email", "role"] });
    res.json(users);
};

export const getUserById = async (req, res) => {
    const user = await identityDb.tables.User.findByPk(req.params.id, { attributes: ["id", "email", "role"] });
    if (!user) return res.sendStatus(404);
    res.json(user);
}

export const promoteUser = async (req, res) => {
    const user = await identityDb.tables.User.findByPk(req.params.id);
    if (!user) return res.sendStatus(404);
    user.role = "admin";
    await user.save();
    res.json(user);
};
