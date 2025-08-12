import express from "express";
import cors from "cors";
import routes from "./routes/index.js";
import setupSwagger from "./docs/swagger.js";
import "dotenv/config";

const app = express();

app.use(cors());
app.use(express.json());
app.use("/api", routes);

setupSwagger(app);

console.log("Beginning listening for requests...");
app.listen(3001, () => console.log("Server running on port 3001"));
