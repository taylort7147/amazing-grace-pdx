import { messageDb } from "../database.js";

function getMessageIncludes() {
    return [
        { model: messageDb.tables.Audio, as: "audio" },
        { model: messageDb.tables.Notes, as: "notes" },
        { model: messageDb.tables.Video, as: "video" },
        { model: messageDb.tables.Series, as: "series" }
    ];
}

export const getAllSeries = async (req, res) => {
    const series = await messageDb.tables.Series.findAll();
    res.json(series);
};

export const getSeriesById = async (req, res) => {
    const series = await messageDb.tables.Series.findByPk(req.params.id);
    if (!series) return res.sendStatus(404);
    res.json(series);
};

export const createSeries = async (req, res) => {
    const series = await messageDb.tables.Series.create(req.body);
    res.status(201).json(series);
};

export const deleteSeries = async (req, res) => {
    const result = await messageDb.tables.Series.destroy({
        where: { id: req.params.id }
    });
    if (!result) return res.sendStatus(404);
    res.sendStatus(204);
};

export const updateSeries = async (req, res) => {
    const [updated] = await messageDb.tables.Series.update(req.body, {
        where: { id: req.params.id }
    });
    if (!updated) return res.sendStatus(404);
    res.sendStatus(204);
};
