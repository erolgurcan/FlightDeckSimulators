# Use an official Node.js runtime as the base image
FROM node:16 as build

# Set the working directory in the container
WORKDIR /app

# Copy package.json and package-lock.json to the working directory
COPY package*.json ./

# Install Node.js dependencies
RUN npm install

# Copy the rest of the application source code to the working directory
COPY . .

# Build the Angular application (you can adjust the configuration as needed)
RUN npm run build

# Stage 2: Create a minimal Nginx image to serve the Angular application
FROM nginx:alpine

# Copy the built Angular app from the previous stage to the Nginx directory
COPY --from=build /app/dist/* /usr/share/nginx/html/

# Expose port 80 for the Nginx server
EXPOSE 80

# Start Nginx when the container is run
CMD ["nginx", "-g", "daemon off;"]
