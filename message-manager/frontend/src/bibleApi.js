import axios from "axios";

const bibleApi = axios.create({
    baseURL: `${import.meta.env.VITE_BIBLE_API_URL || 'http://localhost:5000'}/api`,
});

export default bibleApi;
