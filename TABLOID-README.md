# Tabloid App

A full stack tabloid application built with React (frontend) and ASP.NET Core (backend), using MS SQL Server for data storage.

## Features

- Create, read, update, and delete tabloid story items
- Browse stories with filtering and sorting options
- Responsive design for mobile and desktop
- Full database persistence with MS SQL Server
- Docker support for easy deployment

## Tech Stack

### Frontend
- React
- TypeScript
- React Router
- Axios
- CSS3

### Backend
- ASP.NET Core 9
- Entity Framework Core
- RESTful API
- SQL Server

### DevOps
- Docker
- Docker Compose

## Running the Application

### With Docker (Recommended)

The easiest way to run the application is using Docker Compose:

1. Make sure you have Docker and Docker Compose installed
2. Clone this repository
3. Run the following command from the project root:

```bash
docker-compose up -d
```

This will start:
- SQL Server database on port 1433
- Backend API on port 5001
- Frontend on port 8080

Access the application at: http://localhost:8080

### Development Mode

To run the application in development mode:

1. **Backend**:
   ```bash
   cd backend
   dotnet restore
   dotnet run
   ```
   The API will be available at http://localhost:5001

2. **Frontend**:
   ```bash
   cd frontend
   npm install
   npm run dev
   ```
   The frontend will be available at http://localhost:5173

## API Endpoints

- `GET /api/StoryItems` - Get all story items
- `GET /api/StoryItems/{id}` - Get a specific story item
- `POST /api/StoryItems` - Create a new story item
- `PUT /api/StoryItems/{id}` - Update an existing story item
- `DELETE /api/StoryItems/{id}` - Delete a story item
