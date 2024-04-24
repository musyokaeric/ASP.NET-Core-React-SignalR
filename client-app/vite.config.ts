import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
    build: {
        outDir:'../Reactivities.API/wwwroot'
    },
    server: {
        port: 3000
    },
    plugins: [react()],
})
