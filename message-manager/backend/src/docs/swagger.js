import swaggerJsdoc from "swagger-jsdoc";
import swaggerUi from "swagger-ui-express";
import zodPkg from "@asteasolutions/zod-to-openapi";
console.log("@asteasolutions/zod-to-openapi", zodPkg);
const { OpenApiGeneratorV3, OpenAPIRegistry } = zodPkg;
import appConfig from "../config.js";


const endpointFiles = ["./src/routes/*.routes.js"];

// 1. Import your Zod schemas from shared
import {
    messageSchema,
    loginSchema,
    registerSchema
} from "@message-manager/shared/schemas/index.js";

// 2. Create registry and register schemas
const registry = new OpenAPIRegistry();
registry.register("message", messageSchema);
registry.register("login", loginSchema);
registry.register("register", registerSchema);

// 3. Generate schema definitions from Zod
const generator = new OpenApiGeneratorV3(registry.definitions);
const zodSchemas = generator.generateComponents().schemas;

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
            schemas: zodSchemas
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

export default setupSwagger;
