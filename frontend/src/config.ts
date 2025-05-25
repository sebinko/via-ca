// Configuration for API endpoints based on environment
const config = {
  // API URL is different based on where the application is running
  apiBaseUrl: import.meta.env.PROD 
    ? '/api' // In production (Docker), API requests go through Nginx proxy
    : 'http://localhost:5001/api' // In development, direct to backend server
}

export default config;
