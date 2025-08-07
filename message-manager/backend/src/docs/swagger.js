const swaggerJsdoc = require("swagger-jsdoc");
const swaggerUi = require("swagger-ui-express");
const appConfig = require("../config");


const endpointFiles = ["./src/routes/*.routes.js"];

const swaggerOptions = {
    definition: {
        openapi: "3.0.0",
        info: {
            title: "My API",
            version: "1.0.0",
        },
        components: {
            securitySchemes: {
                bearerAuth: {
                    type: "http",
                    scheme: "bearer",
                    bearerFormat: "JWT",
                },
            },
        },
        security: [
            {
                bearerAuth: [],
            },
        ],
        servers: [{ url: appConfig.apiUrl }],
    },
    apis: endpointFiles

};

const swaggerSpec = swaggerJsdoc(swaggerOptions);

function setupSwagger(app) {
    app.use("/docs", swaggerUi.serve, swaggerUi.setup(swaggerSpec));
}

module.exports = setupSwagger;
