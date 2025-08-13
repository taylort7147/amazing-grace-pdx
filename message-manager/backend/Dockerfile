FROM node:22 as build

WORKDIR /app

RUN npm install -g pnpm

# Copy package manifests first (cache layer)
COPY pnpm-workspace.yaml ./
COPY package.json pnpm-lock.yaml ./
COPY backend/package.json backend/
COPY frontend/package.json frontend/
COPY shared/package.json shared/

# Install dependencies
RUN pnpm config set node-linker hoisted
RUN pnpm install

# Copy the rest of the source code for all workspaces
COPY backend backend/
COPY frontend frontend/
COPY shared shared/

EXPOSE 3001

CMD ["pnpm", "--filter", "backend", "start"]
