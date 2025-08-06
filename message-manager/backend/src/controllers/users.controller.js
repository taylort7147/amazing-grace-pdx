const { identityDb } = require("../database");


exports.getAllUsers = async (req, res) => {
    const users = await identityDb.tables.User.findAll({ attributes: ["id", "email", "role"] });
    res.json(users);
};

exports.promoteUser = async (req, res) => {
    const user = await identityDb.tables.User.findByPk(req.params.id);
    if (!user) return res.sendStatus(404);
    user.role = "admin";
    await user.save();
    res.json(user);
};
