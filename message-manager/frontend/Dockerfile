# Stage 1: Build the app
FROM node:22 as build

WORKDIR /app

# Install pnpm globally
RUN npm install -g pnpm

# Copy all package.json and lockfile
COPY package.json pnpm-lock.yaml ./
COPY backend/package.json backend/
COPY frontend/package.json frontend/
COPY shared/package.json shared/

# Copy source code for all workspaces
COPY backend backend/
COPY frontend frontend/
COPY shared shared/
COPY . .


# Install dependencies for all workspaces
ENV CI=true
RUN pnpm install


# Build frontend workspace from root, filtered by frontend
WORKDIR /app/frontend
RUN pnpm run build


# Stage 2: Serve with nginx
FROM nginx:stable-alpine

COPY --from=build /app/frontend/dist /usr/share/nginx/html

# Optional: Copy custom nginx config
COPY frontend/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
