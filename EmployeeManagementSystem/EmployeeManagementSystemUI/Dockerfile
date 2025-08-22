# Stage 1: Build Angular app
FROM node:20 AS build
WORKDIR /app

# Copy package files first
COPY package*.json ./
RUN npm install

# Copy all frontend source files
COPY . ./

# Build production version
RUN npm run build --prod

# Stage 2: Serve with Nginx
FROM nginx:alpine

# Use correct outputPath from angular.json
COPY --from=build /app/dist/employee-management-system-ui /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
