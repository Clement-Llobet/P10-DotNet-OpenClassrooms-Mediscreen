# Running Mediscreen App with Docker

This document provides detailed instructions for deploying and running the Mediscreen application's services using Docker and Docker Compose. This project utilizes several technologies, including SQL Server, MongoDB, Redis, as well as APIs and user interfaces developed in .NET.

## Prerequisites

1. **Docker**: Make sure Docker is installed on your machine. You can download it from Docker Hub.
2. **Docker Compose**: Docker Compose is included with Docker Desktop, but if you're using Docker on a server, ensure that Compose is installed.

## Steps to Launch the Application

1. **Clone the Repository.**  

2. **Configure Secrets.**  
Ensure that the `mongodb_connection_string` secret is created and available. You can do this with the following command:  
   `echo "mongodb+srv://<USERNAME>:<PASSWORD>@mediscreen-p10-dotnet-o.izgrecn.mongodb.net/?retryWrites=true&w=majority" | docker secret create mongodb_connection_string -`

3. **Build Docker Images.**  
You need to build the images for the `mediscreen.api` and `mediscreen.ui` services from their respective Dockerfiles. Make sure you are in the directory containing the `docker-compose.yml` file.  
   `docker-compose build`

4. **Start the Services.**. 
Use the following command to start all the services defined in the `docker-compose.yml` file:  
   `docker-compose up`  

You can also add `-d` to run the containers in detached mode:  
   `docker-compose up -d` 

5. **Access the Services.**  
Once the services are up and running, you can access:
- API: http://localhost:65255
- UI: http://localhost:65258
- MongoDB: Port 27017 (commonly used for internal connections).
- Redis: Port 6379.

## Managing Containers. 
To stop the services, use:  
`docker-compose down`  

To view the logs of the containers, use:  
`docker-compose logs`



