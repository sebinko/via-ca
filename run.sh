#!/bin/bash

# Color definitions
GREEN='\033[0;32m'
BLUE='\033[0;34m'
YELLOW='\033[1;33m'
RED='\033[0;31m'
NC='\033[0m' # No Color

echo -e "${BLUE}=== Via Tabloid Application Launcher ===${NC}"
echo ""

# Function to check if Docker is running
check_docker() {
  if ! docker info >/dev/null 2>&1; then
    echo -e "${RED}Error: Docker is not running. Please start Docker and try again.${NC}"
    exit 1
  fi
}

# Function to start the application in Docker mode
start_docker() {
  echo -e "${GREEN}Starting the application in Docker mode...${NC}"
  
  # Stop any existing containers for this app
  echo -e "${YELLOW}Stopping any existing containers...${NC}"
  docker-compose down
  
  # Build and start containers
  echo -e "${YELLOW}Building and starting containers...${NC}"
  docker-compose up --build
}

# Function to start the application in local development mode
start_local() {
  echo -e "${GREEN}Starting the application in local development mode...${NC}"
  
  # Check if Docker is running for the database
  check_docker
  
  # Start only the SQL Server container if not already running
  echo -e "${YELLOW}Ensuring SQL Server container is running...${NC}"
  if [ -z "$(docker ps -q -f name=via-ca-sqlserver)" ]; then
    echo -e "${YELLOW}Starting SQL Server container...${NC}"
    docker-compose up -d sqlserver
    echo -e "${YELLOW}Waiting for SQL Server to start...${NC}"
    sleep 10
  else
    echo -e "${GREEN}SQL Server container is already running.${NC}"
  fi
  
  # Start backend and frontend in separate terminals
  echo -e "${YELLOW}Starting backend API...${NC}"
  osascript -e 'tell app "Terminal" to do script "cd '$PWD'/backend && dotnet run --urls=http://localhost:5001"'
  
  echo -e "${YELLOW}Starting frontend development server...${NC}"
  osascript -e 'tell app "Terminal" to do script "cd '$PWD'/frontend && npm run dev"'
  
  echo -e "${GREEN}Development servers started.${NC}"
  echo -e "${GREEN}Backend API: ${NC}http://localhost:5001"
  echo -e "${GREEN}Frontend: ${NC}http://localhost:5173"
}

# Function to build images inside Minikube and deploy via Kubernetes
start_minikube() {
  check_docker
  echo -e "${GREEN}Starting in Minikube mode...${NC}"
  minikube start
  echo -e "${BLUE}Switching Docker CLI to Minikube daemon...${NC}"
  eval "$(minikube -p minikube docker-env)"
  echo -e "${YELLOW}Building backend image into Minikube...${NC}"
  docker build -t backend:latest -f backend/Dockerfile backend
  echo -e "${YELLOW}Building frontend image into Minikube...${NC}"
  docker build -t frontend:latest -f frontend/Dockerfile frontend
  echo -e "${YELLOW}Applying Kubernetes manifests...${NC}"
  kubectl apply -f k8s/
  echo -e "${GREEN}Waiting for backend deployment...${NC}"
  kubectl rollout status deployment/backend
  echo -e "${GREEN}Waiting for frontend deployment...${NC}"
  kubectl rollout status deployment/frontend
  echo -e "${GREEN}Opening frontend service in browser...${NC}"
  minikube service frontend-service
}

# Auto-select mode if first argument provided
if [ "$#" -gt 0 ]; then
  case "$1" in
    docker) check_docker; start_docker; exit 0;;
    local) start_local; exit 0;;
    minikube) check_docker; start_minikube; exit 0;;
    *) echo -e "${RED}Unknown mode '$1'. Valid options: docker, local, minikube.${NC}"; exit 1;;
  esac
fi

# Show menu
echo "Please select an option:"
echo "1) Start in Docker mode (full containerized environment)"
echo "2) Start in local development mode (DB in Docker, backend & frontend local)"
echo "3) Start in Minikube mode (build local images into Minikube & deploy)"
echo "4) Exit"
 
read -p "Enter your choice (1-4): " choice

case $choice in
  1)
    check_docker
    start_docker
    ;;
  2)
    start_local
    ;;
  3)
    check_docker
    start_minikube
    ;;
  4)
    echo -e "${BLUE}Exiting...${NC}"
    exit 0
    ;;
  *)
    echo -e "${RED}Invalid choice. Exiting.${NC}"
    exit 1
    ;;
esac
