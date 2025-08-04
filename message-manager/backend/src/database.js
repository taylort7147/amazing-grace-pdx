const { Sequelize, QueryTypes } = require("sequelize");
const { initModels: initMessageModels } = require("./models/message/init-models");
const { initModels: initNucleusModels } = require("./models/nucleus/init-models");
const { initModels: initIdentityModels } = require("./models/identity/init-models");

const connectToDatabase = async (sequelize, retries = 10, delay = 3000) => {
    const dbName = sequelize.config.database || "master";
    while (retries) {
        try {
            await sequelize.authenticate();
            console.log("Connected to master DB");
            return;
        } catch (err) {
            console.log(`DB connection failed. Retries left: ${retries - 1}`, err.message);
            retries--;
            await new Promise((res) => setTimeout(res, delay));
        }
    }
    throw new Error(`Could not connect to the database ${dbName}`);
};


async function tryCreateDatabase(sequelize, dbName) {
    const result = await sequelize.query(
        `SELECT database_id FROM sys.databases WHERE Name = :dbName`,
        {
            replacements: { dbName: dbName },
            type: QueryTypes.SELECT,
        }
    );

    if (result.length === 0) {
        console.log(`Database "${dbName}" not found, creating...`);
        await sequelize.query(`CREATE DATABASE [${dbName}]`);
        console.log(`Database "${dbName}" created.`);
    } else {
        console.log(`Database "${dbName}" exists.`);
    }
}

async function initializeDatabase(dbName, dbUser, dbPassword, dbHost, dbPort, dbDialect) {
    // Connect to the master database
    const sequelizeMaster = new Sequelize("", dbUser, dbPassword, {
        host: dbHost,
        port: dbPort,
        dialect: dbDialect,
        dialectOptions: {
            options: {
                encrypt: false,
                trustServerCertificate: true,
            },
        },
        logging: false,
    });
    await connectToDatabase(sequelizeMaster);

    // Create the database if it doesn't exist
    await tryCreateDatabase(sequelizeMaster, dbName);
    sequelizeMaster.close();

    // Now connect to the actual database
    const sequelize = new Sequelize(
        dbName,
        dbUser,
        dbPassword,
        {
            host: dbHost,
            port: dbPort,
            dialect: dbDialect,
            dialectOptions: {
                options: {
                    encrypt: false,
                    trustServerCertificate: true,
                },
            },
            logging: false,
        }
    );
    await connectToDatabase(sequelize);
    return sequelize;
}

const {
    IDENTITY_DB_HOST = "localhost",
    IDENTITY_DB_PORT = 1433,
    IDENTITY_DB_USER = "sa",
    IDENTITY_DB_PASSWORD = "Password123",
    IDENTITY_DB_NAME = "identity-db",
    IDENTITY_DB_DIALECT = "mssql",

    MESSAGE_DB_HOST = "localhost",
    MESSAGE_DB_PORT = 1433,
    MESSAGE_DB_USER = "sa",
    MESSAGE_DB_PASSWORD = "Password123",
    MESSAGE_DB_NAME = "message-db",
    MESSAGE_DB_DIALECT = "mssql",

    NUCLEUS_DB_HOST = "localhost",
    NUCLEUS_DB_PORT = 1433,
    NUCLEUS_DB_USER = "sa",
    NUCLEUS_DB_PASSWORD = "Password123",
    NUCLEUS_DB_NAME = "nucleus-db",
    NUCLEUS_DB_DIALECT = "mssql",
} = process.env;


async function initializeMessageDatabase() {

    console.log("Connecting to message database...");
    const sequelize = await initializeDatabase(
        MESSAGE_DB_NAME,
        MESSAGE_DB_USER,
        MESSAGE_DB_PASSWORD,
        MESSAGE_DB_HOST,
        MESSAGE_DB_PORT,
        MESSAGE_DB_DIALECT
    );
    console.log("Connected to Message database");

    console.log("Initializing Message models...");
    initMessageModels(sequelize);
    console.log("Message models initialized");

    console.log("Synchronizing Message models...");
    await sequelize.sync();
    console.log("Message models synchronized");

    return sequelize;
}

async function initializeNucleusDatabase() {
    console.log("Connecting to Nucleus database...");
    const sequelize = await initializeDatabase(
        NUCLEUS_DB_NAME,
        NUCLEUS_DB_USER,
        NUCLEUS_DB_PASSWORD,
        NUCLEUS_DB_HOST,
        NUCLEUS_DB_PORT,
        NUCLEUS_DB_DIALECT
    );
    console.log("Connected to Nucleus database");

    console.log("Initializing Nucleus models...");
    initNucleusModels(sequelize);
    console.log("Nucleus models initialized");

    console.log("Synchronizing Nucleus models...");
    await sequelize.sync();
    console.log("Nucleus models synchronized");

    return sequelize;
}
async function initializeIdentityDatabase() {
    console.log("Connecting to Identity database...");
    const sequelize = await initializeDatabase(
        IDENTITY_DB_NAME,
        IDENTITY_DB_USER,
        IDENTITY_DB_PASSWORD,
        IDENTITY_DB_HOST,
        IDENTITY_DB_PORT,
        IDENTITY_DB_DIALECT
    );
    console.log("Connected to Identity database");

    console.log("Initializing Identity models...");
    initIdentityModels(sequelize);
    console.log("Identity models initialized");

    console.log("Synchronizing Identity models...");
    await sequelize.sync();
    console.log("Identity models synchronized");

    return sequelize;
}

module.exports = {
    connectToDatabase,
    initializeDatabase,
    initializeMessageDatabase,
    initializeNucleusDatabase,
    initializeIdentityDatabase
};
