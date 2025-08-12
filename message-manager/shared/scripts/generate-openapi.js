// packages/shared/src/generate-openapi.js
import { zodToOpenAPI } from "@asteasolutions/zod-to-openapi";
import { fs } from "fs";
import { path } from "path";
import { schemas } from "./schemas.js";

// Convert all Zod schemas to OpenAPI components
const openApiSchemas = {};

for (const [name, schema] of Object.entries(schemas)) {
  // Expect all exports ending with "Schema" to be Zod schemas
  if (name.endsWith("Schema")) {
    const cleanName = name.replace(/Schema$/, "");
    openApiSchemas[cleanName] = zodToOpenAPI(schema, cleanName);
  }
}

// Build the final OpenAPI components object
const swaggerComponents = {
  components: {
    schemas: openApiSchemas
  }
};

// Write it to a JSON file in the shared package
const outputPath = path.join(__dirname, "../openapi-schemas.json");
fs.writeFileSync(outputPath, JSON.stringify(swaggerComponents, null, 2));

console.log(`✅ Generated OpenAPI schemas at ${outputPath}`);
