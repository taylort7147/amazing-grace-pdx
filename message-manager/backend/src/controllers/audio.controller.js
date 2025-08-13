import { messageDb } from "../database.js";

function getMessageIncludes() {
    return [
        { model: messageDb.tables.Audio, as: "audio" },
        { model: messageDb.tables.Notes, as: "notes" },
        { model: messageDb.tables.Video, as: "video" },
        { model: messageDb.tables.Series, as: "series" }
    ];
}

export const getAllAudio = async (req, res) => {
    const audio = await messageDb.tables.Audio.findAll({
        include: getMessageIncludes()
    });
    res.json(audio);
};

export const getAudioById = async (req, res) => {
    const audio = await messageDb.tables.Audio.findByPk(req.params.id, {
        include: getMessageIncludes()
    });
    if (!audio) return res.sendStatus(404);
    res.json(audio);
};

export const createAudio = async (req, res) => {
    const audio = await messageDb.tables.Audio.create(req.body);
    res.status(201).json(audio);
};

export const deleteAudio = async (req, res) => {
    const result = await messageDb.tables.Audio.destroy({
        where: { id: req.params.id }
    });
    if (!result) return res.sendStatus(404);
    res.sendStatus(204);
};

export const updateAudio = async (req, res) => {
    const [updated] = await messageDb.tables.Audio.update(req.body, {
        where: { id: req.params.id }
    });
    if (!updated) return res.sendStatus(404);
    res.sendStatus(204);
};
