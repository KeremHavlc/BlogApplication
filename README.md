# BlogApplication
<p align="center">
  <img src="https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/C%23-239120?style=flat-square&logo=c-sharp&logoColor=white" />
  <img src="https://img.shields.io/badge/ASP.NET%20Core-5C2D91?style=flat-square&logo=asp.net&logoColor=white" />
  <img src="https://img.shields.io/badge/SQL%20Server-CC2927?style=flat-square&logo=microsoft-sql-server&logoColor=white" />
  <img src="https://img.shields.io/badge/JWT-000000?style=flat-square&logo=json-web-tokens&logoColor=white" />
  <img src="https://img.shields.io/badge/Entity%20Framework%20Core-512BD4?style=flat-square&logo=entity-framework&logoColor=white" />
</p>
## About the Project

BlogApplication is a social blogging platform where users can not only create, update, and delete blog posts but also engage with others by joining communities, sending friend requests, liking posts, and commenting. Users can upload images to enrich their content and interact within public communities that are managed exclusively by administrators. The backend is developed in C# using a clean layered architecture, with Microsoft SQL Server for reliable data storage and JWT for secure user authentication.
---

## Features


- User registration and login with JWT authentication  
- Create, update, and delete blog posts  
- Upload images to blog posts  
- Join public communities where posts can be shared  
- Communities are created exclusively by administrators  
- Send and receive friend requests  
- Like posts  
- Comment on posts  
- User-specific blog and social management  
- Clean, maintainable code with n-tier architecture  

---

## Technologies

- **Backend:** C# (.NET Core), N-Tier Architecture  
- **Database:** Microsoft SQL Server (MSSQL)  
- **Authentication:** JSON Web Tokens (JWT)  
- **ORM:** Entity Framework Core  

---

## Architecture Overview

The project uses a 5-layer architecture:

1. **Entity Layer:** Defines data models and entities  
2. **Data Access Layer:** Handles database interactions with Entity Framework  
3. **Business Logic Layer:** Contains all business rules and logic  
4. **Core Layer:** Shared utilities, interfaces, and helper classes  
5. **Presentation Layer:** API endpoints that communicate with the frontend  

---

## How to Run Backend

1. **Requirements:**  
   - You must have .NET SDK installed (for example, .NET 6 or higher)  
   - MSSQL Server must be running  

2. **Setup Database:**  
   - Database will be created automatically by Entity Framework Code First when you run migrations  
   - Run migrations to create database and tables  

3. **Change Connection String:**  
   - Open the `DbContext` class in the `DataAccess` project  
   - Update the connection string directly inside the DbContext or constructor, for example:  
     ```
     Server=YOUR_SERVER;Database=ExpenseDb;User Id=YOUR_USER;Password=YOUR_PASSWORD;
     ```

4. **Run Migrations:**  
   - Open terminal in backend project folder  
   - Run these commands to create and apply migrations:  
     ```bash
     dotnet ef migrations add InitialCreate
     dotnet ef database update
     ```

5. **Run Backend:**  
   - Run the backend API with:  
     ```bash
     dotnet run
     ```  
   - Backend will start

6. **Test API:**  
   - Use Postman or other tools to test API  
   - Or connect frontend to backend to check

## 🚀 Frontend

You can find the frontend code here:  
[🔗 BlogApplication Frontend Repository](https://github.com/KeremHavlc/BlogAppFront)
