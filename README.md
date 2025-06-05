# Via Tabloid Application

A modern tabloid web application with features for managing news stories. Built with React, TypeScript, ASP.NET Core, and MS SQL Server.

## Features

- View, add, edit, and delete tabloid stories
- Categorize stories and manage their publication status
- Full-stack application with frontend and backend components
- Responsive design for mobile and desktop viewing

## Requirements

- Docker and Docker Compose
- .NET SDK 9.0 or later (for local development)
- Node.js 18 or later (for local development)
- npm (for local development)

## Running the Application

There are two ways to run this application:

### 1. Using Docker (Recommended)

This method runs everything in containers, including the frontend, backend, and database.

```bash
# Clone the repository (if you haven't already)
git clone <repository-url>
cd via-ca

```bash
# Clone the repository (if you haven't already)
git clone <repository-url>
cd via-ca

# Run the helper script
./run.sh
# Select option 2 when prompted
```

This will start:
- SQL Server in Docker (accessible at localhost:1434)
- Backend API server at http://localhost:5001
- Frontend development server with hot-reloading at http://localhost:5173

## Database Connection

- Connection string for Docker environment:
  ```
  Server=sqlserver;Database=TabloidDb;User Id=sa;Password=StrongP@ssw0rd!;TrustServerCertificate=True;
  ```

- Connection string for local development:
  ```
  Server=localhost,1434;Database=TabloidDb;User Id=sa;Password=StrongP@ssw0rd!;TrustServerCertificate=True;
  ```

## Architecture

- **Frontend**: React + TypeScript + Vite
  - React 19
  - TypeScript
  - Vite 6
  - Modern ES modules

- **Backend**: ASP.NET Core with Entity Framework Core
  - C# 12
  - ASP.NET Core 9.0
  - REST API principles
  - Controller-based architecture
  
- **Database**: Microsoft SQL Server (Azure SQL Edge for cross-platform compatibility)

## Folder Structure

```
/
├── frontend/           # React frontend application
├── backend/            # ASP.NET Core API
├── docker-compose.yml  # Docker configuration
└── run.sh              # Helper script to run the application
```

## Development

### Frontend Development

```bash
cd frontend
npm install
npm run dev
```

### Backend Development

```bash
cd backend
dotnet restore
dotnet run
```

## License

[MIT](LICENSE)

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
