import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

const apiUrl = process.env.VITE_API_URL || 'http://localhost:3001';

export default defineConfig({
    plugins: [react()],
    server: {
        proxy: {
            '/api': {
                target: apiUrl,
                changeOrigin: true,
                secure: false,
            },
        },
    },
    build: {
        outDir: 'dist'
    },
    resolve: {
        alias: {
            '@': '/src',
        },
    },
    base: '/',
    appType: 'spa'
});
