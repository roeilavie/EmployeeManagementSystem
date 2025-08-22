\# Employee management system



A full-stack project with a .NET 8 backend API and an Angular frontend, fully containerized with Docker.  

No database is required — the backend uses JSON for storage.



---



\## Prerequisites



\- Docker Desktop (WindowsmacOS) or Docker Engine (Linux) installed  



---



\## Project structure 



EmployeeManagementSystem/

├── docker-compose.yml        # Orchestrates backend + frontend containers

├── backend/

│   ├── Dockerfile            # Multi-stage .NET backend Dockerfile

│   ├── EmployeeManagementSystemAPI.csproj

│   └── ...other backend files

├── EmployeeManagementSystemUI/

│   ├── Dockerfile            # Multi-stage Angular frontend Dockerfile

│   ├── package.json

│   └── src/



---



\## Build \& Run Instructions



1\. Clone or download the project



Ensure the folder structure is intact.



2\. Build and start containers



From the project root (where docker-compose.yml is):

docker-compose up --build



Access services:



Backend: http://localhost:8080

Frontend: http://localhost:4200



---



\## Stop containers

docker-compose down



