# Simple Frontend + Backend Application

A simple application consisting of a Vite TypeScript React frontend and a C# ASP.NET Core backend API, created for a DevOps assignment.

## Project Structure

This repository contains two main parts:

### Frontend (React + TypeScript + Vite)

The frontend is built with:
- React 19
- TypeScript
- Vite 6
- Modern ES modules

### Backend (ASP.NET Core)

The backend is built with:
- C# 12
- ASP.NET Core 9.0
- REST API principles
- Controller-based architecture

## Features

- Simple React UI that connects to a backend API
- C# backend with a basic API endpoint
- CORS configuration for secure communication
- API response display in the frontend
- Error handling and loading states
- Containerized with Docker for easy deployment
- Optimized Docker images for production

## Running with Docker Compose

This project is fully containerized and can be run using Docker Compose.

### Prerequisites

- Docker and Docker Compose installed on your machine

### Steps to Run

1. Navigate to the project root directory
2. Build and start the containers:

```bash
docker-compose up -d --build
```

3. Access the application:
   - Frontend: http://localhost:8080
   - Backend API: http://localhost:5001

### Commands

- Start containers: `docker-compose up -d`
- Stop containers: `docker-compose down`
- View logs: `docker-compose logs -f`
- Rebuild containers: `docker-compose up -d --build`
- View specific container logs: `docker-compose logs -f [service-name]`
- Check container health: `docker ps`
- Inspect container details: `docker inspect [container-id]`
- Check image sizes: `docker images | grep via-ca`
- Clean up unused containers: `docker system prune`
- Clean up unused volumes: `docker volume prune`

## Docker Optimization

The Docker containers are optimized for:

- Small image sizes using multi-stage builds (Frontend: ~50MB, Backend: ~128MB)
- Security with non-root users where applicable
- Resource constraints with limits on CPU and memory
- Health checks for container monitoring
- Production-ready configurations
- Minimal dependencies to reduce attack surface

### Frontend Container Optimization
- Multi-stage build with separate dependency, build, and production stages
- Alpine-based Node.js for building
- Alpine-based Nginx for serving static content
- Compressed static assets with gzip in Nginx
- Optimized frontend build with production flags

### Backend Container Optimization
- Multi-stage build with SDK for building, runtime image for deployment
- Alpine-based .NET images for smaller size
- Environment variable optimization
- Proper layering to maximize caching
- Health check endpoints for monitoring
- Minimal dependencies to reduce attack surface

## Running the Application

### Prerequisites

- Node.js (v16+) and npm for the frontend
- .NET SDK 9.0 for the backend

### Backend (C# API)

1. Navigate to the backend directory:
   ```bash
   cd backend
   ```

2. Run the application:
   ```bash
   dotnet run --launch-profile https
   ```

The API will be available at:
- HTTPS: https://localhost:7118
- HTTP: http://localhost:5174

### Frontend (React)

1. Navigate to the frontend directory:
   ```bash
   cd frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   npm run dev
   ```

The frontend will be available at http://localhost:5173.

## API Endpoints

### Message API

- **GET /api/message**
  - Returns a simple message
  - Response format: `{ "message": "Hello from C# backend!" }`

## Project Objectives

This project serves as a foundation for:
- Setting up CI/CD pipelines
- Implementing containerization with Docker
- Creating infrastructure as code
- Configuring monitoring and logging
- Implementing deployment strategies

## Development

### Adding New API Endpoints

To add a new endpoint to the C# backend:

1. Create a new controller in the `Controllers` directory or extend the existing `MessageController.cs`
2. Add appropriate routes and methods
3. Configure any necessary services in `Program.cs`

### Extending the Frontend

To add new features to the React frontend:

1. Modify the `App.tsx` or create new components in the `src` directory
2. Add any necessary API calls to communicate with the backend
3. Update the UI to display the results

## License

This project is for educational purposes.

## Contributors

Created for a DevOps assignment.
