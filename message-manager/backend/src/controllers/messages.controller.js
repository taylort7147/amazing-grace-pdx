import { messageDb } from "../database.js";

/**
 * Deletes a sub-record from the specified table.
 * 
 * @param {*} messageId 
 * @param {*} table 
 * @param {*} transaction 
 */
const deleteSubRecord = async (messageId, table, transaction) => {
    const oldRecord = await table.findOne({ where: { messageId: messageId }, transaction });
    if (oldRecord) {
        const deletedCount = await table.destroy({
            where: { id: oldRecord.id },
            transaction
        });
        if (deletedCount !== 1) {
            throw new Error(`Failed to delete ${table.name}`);
        }
    }
};

const deleteSubRecords = async (messageId, table, transaction) => {
    const oldRecords = await table.findAll({ where: { messageId: messageId }, transaction });
    if (oldRecords) {
        for (const oldRecord of oldRecords) {
            const deletedCount = await table.destroy({
                where: { id: oldRecord.id },
                transaction
            });
            if (deletedCount !== 1) {
                throw new Error(`Failed to delete ${table.name}`);
            }
        }
    }
};

/**
 * Updates a sub-record in the specified table.
 * 
 * If the record exists and this is a new record, update it
 * If the record does not exist, create it.
 * If the record exists but the new record is null, delete it.
 * 
 * @param {*} messageId
 * @param {*} record
 * @param {*} table
 * @param {*} transaction
 */
const updateSubRecord = async (messageId, record, table, transaction) => {
    const oldRecord = await table.findOne({ where: { messageId: messageId }, transaction });
    if (oldRecord && record) {
        // Update record
        const [affectedCount] = await table.update(
            record,
            { where: { id: oldRecord.id }, transaction }
        );
        if (affectedCount !== 1) {
            throw new Error(`Failed to update ${table.name}`);
        }
    }
    else if (oldRecord && !record) {
        // Delete record
        const deletedCount = await table.destroy({
            where: { id: oldRecord.id },
            transaction
        });
        if (deletedCount !== 1) {
            throw new Error(`Failed to delete ${table.name}`);
        }
    }
    else if (!oldRecord && record) {
        // Create record
        record.messageId = messageId;
        const created = await table.create(record, { transaction });
        if (!created.id) {
            throw new Error(`Failed to create ${table.name}`);
        }
    }
};

/**
 * Updates Bible references for a message. Only additions and deletions are supported.
 * 
 * @param {*} messageId 
 * @param {*} records
 * @param {*} table
 * @param {*} transaction 
 */
const updateBibleReferences = async (messageId, records, table, transaction) => {
    const oldRecords = await table.findAll({ where: { messageId: messageId }, transaction });
    if (oldRecords && records) {
        // Update records
        // First delete any old records that are not in the new records
        for (const oldRecord of oldRecords) {
            if (!records.find(r => r.id === oldRecord.id)) {
                const deletedCount = await table.destroy({
                    where: { id: oldRecord.id },
                    transaction
                });
                if (deletedCount !== 1) {
                    throw new Error(`Failed to delete ${table.name}`);
                }
            }
        }
        // Then create the new records
        for (const record of records) {
            record.messageId = messageId;
            const created = await table.create(record, { transaction });
            if (!created.id) {
                throw new Error(`Failed to create ${table.name}`);
            }
        }
    }
    else if (oldRecords && !records) {
        // Delete all old records
        for (const oldRecord of oldRecords) {
            const deletedCount = await table.destroy({
                where: { id: oldRecord.id },
                transaction
            });
            if (deletedCount !== 1) {
                throw new Error(`Failed to delete ${table.name}`);
            }
        }
    }
    else if (!oldRecords && records) {
        // Create all new records
        for (const record of records) {
            record.messageId = messageId;
            const created = await table.create(record, { transaction });
            if (!created.id) {
                throw new Error(`Failed to create ${table.name}`);
            }
        }
    }
};

/**
 * Get the list of included models for a message.
 * @returns {Array} The list of included models.
 */
function getMessageIncludes() {
    return [
        { model: messageDb.tables.Audio, as: "audio" },
        { model: messageDb.tables.Notes, as: "notes" },
        { model: messageDb.tables.Video, as: "video" },
        { model: messageDb.tables.Series, as: "series" },
        { model: messageDb.tables.BibleReferences, as: "bibleReferences" }
    ];
}

/**
 * Get all messages.
 * @param {*} req 
 * @param {*} res 
 */
export const getAllMessages = async (req, res) => {
    const messages = await messageDb.tables.Message.findAll({
        include: getMessageIncludes()
    });
    res.json(messages);
};

/**
 * Get a message by ID.
 * @param {*} req 
 * @param {*} res 
 */
export const getMessageById = async (req, res) => {
    const message = await messageDb.tables.Message.findByPk(req.params.id, {
        include: getMessageIncludes()
    });
    if (!message) return res.sendStatus(404);
    res.json(message);
};

/**
 * Create a new message.
 * @param {*} req 
 * @param {*} res 
 * @returns 
 */
export const createMessage = async (req, res) => {
    try {
        await messageDb.sequelize.transaction(async (t) => {
            const message = await messageDb.tables.Message.create(req.body, { transaction: t });
            if (!message.id) {
                throw new Error("Failed to create message");
            }
            await updateSubRecord(message.id, message.notes, messageDb.tables.Notes, t);
            await updateSubRecord(message.id, message.audio, messageDb.tables.Audio, t);
            await updateSubRecord(message.id, message.video, messageDb.tables.Video, t);
            await updateBibleReferences(message.id, message.bibleReferences, messageDb.tables.BibleReferences, t);
            // No need to call commit/rollback explicitly — Sequelize handles it
            return res.status(201).json(message);
        });
    } catch (error) {
        console.error("Error updating message:", error);
        return res.sendStatus(500);
    }
};

/**
 * Delete a message by ID.
 * @param {*} req 
 * @param {*} res 
 * @returns 
 */
export const deleteMessage = async (req, res) => {
    const id = req.params.id;
    const message = await messageDb.tables.Message.findByPk(id);
    if (!message) return res.sendStatus(404);
    try {
        await messageDb.sequelize.transaction(async (t) => {

            await deleteSubRecord(id, messageDb.tables.Notes, t);
            await deleteSubRecord(id, messageDb.tables.Audio, t);
            await deleteSubRecord(id, messageDb.tables.Video, t);
            await deleteSubRecords(id, messageDb.tables.BibleReferences, t);

            const result = await messageDb.tables.Message.destroy({ where: { id }, transaction: t });
            if (!result) return res.sendStatus(404);
        });
        // No need to call commit/rollback explicitly — Sequelize handles it
    } catch (error) {
        console.error("Error deleting message:", error);
        return res.sendStatus(500);
    }
    res.sendStatus(204);
};

/**
 * Update a message by ID.
 * @param {*} req 
 * @param {*} res 
 * @returns 
 */
export const updateMessage = async (req, res) => {
    try {
        await messageDb.sequelize.transaction(async (t) => {
            const { id } = req.params;
            const message = req.body;

            await updateSubRecord(id, message.notes, messageDb.tables.Notes, t);
            await updateSubRecord(id, message.audio, messageDb.tables.Audio, t);
            await updateSubRecord(id, message.video, messageDb.tables.Video, t);
            await updateBibleReferences(id, message.bibleReferences, messageDb.tables.BibleReferences, t);

            // Update message
            const [affectedCount] = await messageDb.tables.Message.update(message, {
                where: { id }
            }, { transaction: t });
            if (affectedCount !== 1) {
                throw new Error("Failed to update message");
            }
            // No need to call commit/rollback explicitly — Sequelize handles it
        });
    } catch (error) {
        console.error("Error updating message:", error);
        return res.sendStatus(500);
    }
    res.sendStatus(204);
};
