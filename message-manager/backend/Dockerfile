FROM node:22 as build

WORKDIR /app

# Install pnpm globally once
RUN npm install -g pnpm

# Copy package.json and pnpm-lock.yaml for all workspaces first
COPY package.json pnpm-lock.yaml ./
COPY backend/package.json backend/
COPY frontend/package.json frontend/
COPY shared/package.json shared/

# Install dependencies - this layer is cached unless package files change
RUN pnpm install

# Now copy all source code (after dependencies installed)
COPY backend backend/
COPY frontend frontend/
COPY shared shared/

# Set working directory to backend (or frontend if building frontend)
WORKDIR /app/backend

# Expose port and default cmd
EXPOSE 3001
CMD ["npm", "start"]
