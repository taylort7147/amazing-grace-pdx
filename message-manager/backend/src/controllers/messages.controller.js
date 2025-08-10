const { messageDb } = require("../database");

function getMessageIncludes() {
    return [
        { model: messageDb.tables.Audio, as: "audio" },
        { model: messageDb.tables.Notes, as: "notes" },
        { model: messageDb.tables.Video, as: "video" },
        { model: messageDb.tables.Series, as: "series" }
    ];
}

exports.getAllMessages = async (req, res) => {
    const messages = await messageDb.tables.Message.findAll({
        include: getMessageIncludes()
    });
    res.json(messages);
};

exports.getMessageById = async (req, res) => {
    const message = await messageDb.tables.Message.findByPk(req.params.id, {
        include: getMessageIncludes()
    });
    if (!message) return res.sendStatus(404);
    res.json(message);
};

exports.createMessage = async (req, res) => {
    const message = await messageDb.tables.Message.create(req.body);
    res.status(201).json(message);
};

exports.deleteMessage = async (req, res) => {
    const result = await messageDb.tables.Message.destroy({
        where: { id: req.params.id }
    });
    if (!result) return res.sendStatus(404);
    res.sendStatus(204);
};

exports.updateMessage = async (req, res) => {
    const [updated] = await messageDb.tables.Message.update(req.body, {
        where: { id: req.params.id }
    });
    if (!updated) return res.sendStatus(404);
    res.sendStatus(204);
};
