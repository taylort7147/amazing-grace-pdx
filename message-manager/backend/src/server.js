const express = require("express");
const cors = require("cors");
const routes = require("./routes");

require("dotenv").config();

const app = express();

app.use(cors());
app.use(express.json());
app.use("/api", routes);

console.log("Beginning listening for requests...");
app.listen(3001, () => console.log("Server running on port 3001"));
