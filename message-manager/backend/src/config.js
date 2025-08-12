
import process from "process";

const protocol = process.env.VITE_PROTOCOL || 'http';
const host = process.env.VITE_HOST || 'localhost';
const port = process.env.VITE_PORT || 3001;
const apiEndpoint = process.env.VITE_API_ENDPOINT || '/api';
const apiUrl = `${protocol}://${host}:${port}${apiEndpoint}`;

export default {
    protocol,
    host,
    port,
    apiEndpoint,
    apiUrl
};
