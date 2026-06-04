# DevPulse

![.NET](https://img.shields.io/badge/.NET-9.0-blue)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-green)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-ORM-orange)
![License](https://img.shields.io/badge/License-MIT-blue)

A modern, fully-featured blogging platform built with **Clean Architecture**, **ASP.NET Core**, and **Entity Framework Core**. DevPulse demonstrates enterprise-grade API design with authentication, authorization, and comprehensive blog management capabilities.

## Overview

DevPulse is a RESTful blogging platform that enables users to create, manage, and discover blog posts. The application leverages **Clean Architecture** principles to ensure separation of concerns, maintainability, and scalability. It serves as a portfolio project showcasing modern .NET development practices and design patterns.

**Key Highlights:**
- 🏗️ Clean Architecture with clear separation of concerns
- 🔐 JWT-based authentication and role-based authorization
- 📝 Full blog lifecycle management (Create, Read, Update, Delete)
- 🏷️ Category-based blog organization and filtering
- 🔒 User ownership authorization for blog content
- 🛠️ Enterprise design patterns (Generic Repository, Unit of Work, Dependency Injection)
- 📊 Global exception handling with standardized responses
- 🔄 Automatic data seeding and database migrations

## Key Features

### Authentication & Authorization
- **User Registration & Login** - Secure account creation with email verification
- **JWT Authentication** - Token-based authentication for protected endpoints
- **Role-Based Authorization** - Admin and User roles with granular access control
- **User Ownership Validation** - Users can only modify their own blogs

### Blog Management
- **Create Blogs** - Authenticated users can publish blogs with title, content, category, and optional images
- **Read Blogs** - Public access to browse all blogs or filter by category
- **Update Blogs** - Users can edit only their own published blogs
- **Delete Blogs** - Users can delete only their own blogs
- **User Blog Retrieval** - Authenticated users can view their own blog collection

### Category Management
- **Browse Categories** - Public access to view all available categories
- **Admin Management** - Admin users can create, update, and delete categories

### Data Validation & Security
- **Input Validation** - Data Annotations for comprehensive validation rules
- **Global Exception Handling** - Centralized error handling middleware
- **Standardized API Responses** - Consistent response format across all endpoints

## Architecture

DevPulse follows **Clean Architecture** principles with a layered approach:

```
┌─────────────────────────────────────────────────────┐
│              DevPulseApp (API Layer)                │
│        Controllers | Middleware | WebConfig         │
└────────────────────┬────────────────────────────────┘
					 │
┌────────────────────┴────────────────────────────────┐
│          Application (Business Logic)               │
│  Services | DTOs | Interfaces | Dependency Setup   │
└────────────────────┬────────────────────────────────┘
					 │
┌────────────────────┴────────────────────────────────┐
│         Infrastructure (Data Access)                │
│  UnitOfWork | Repositories | DbContext | Services  │
└────────────────────┬────────────────────────────────┘
					 │
┌────────────────────┴────────────────────────────────┐
│              Domain (Core Entities)                 │
│           ApplicationUser | Blog | Category         │
└─────────────────────────────────────────────────────┘
```

### Design Patterns Implemented

| Pattern | Purpose |
|---------|---------|
| **Generic Repository Pattern** | Centralized data access logic with reusable CRUD operations |
| **Unit of Work Pattern** | Transaction management and coordinated repository operations |
| **Dependency Injection** | Loose coupling and easy testability |
| **DTO Pattern** | Clean separation between API contracts and domain models |
| **Middleware Pattern** | Cross-cutting concerns (exception handling, authentication) |
| **Factory Pattern** | Service registration through extension methods |

## Project Structure

```
DevPulse/
├── DevPulseApp/                          # API Layer (ASP.NET Core Web API)
│   ├── Controllers/
│   │   ├── AccountController.cs          # Authentication endpoints
│   │   ├── BlogController.cs             # Blog CRUD and filtering
│   │   └── CategoryController.cs         # Category management
│   ├── Middlewares/
│   │   └── GlobalExceptionMiddleware.cs  # Centralized error handling
│   ├── ExtensionMethods/
│   │   └── WebApplicationRegister.cs     # App configuration extensions
│   ├── Program.cs                        # Application entry point
│   ├── appsettings.json                  # Configuration
│   └── Properties/
│       └── launchSettings.json
│
├── Application/                          # Business Logic Layer
│   ├── Services/
│   │   ├── BlogService.cs
│   │   └── CategoryService.cs
│   ├── Interfaces/
│   │   ├── Services/
│   │   │   ├── IAccountService.cs
│   │   │   ├── IBlogService.cs
│   │   │   ├── ICategoryService.cs
│   │   │   └── IJwtService.cs
│   │   ├── Repositories/
│   │   │   └── IGenericRepository.cs
│   │   ├── UnitOfWork/
│   │   │   └── IUnitOfWork.cs
│   │   └── DataSeeding/
│   │       └── IDataInitializer.cs
│   ├── DTOs/
│   │   ├── AccountDTOs/
│   │   │   ├── RegisterDto.cs
│   │   │   └── LoginDto.cs
│   │   ├── BlogDTOs/
│   │   │   ├── CreateBlogDto.cs
│   │   │   ├── GetBlogDto.cs
│   │   │   └── UpdateBlogDto.cs
│   │   ├── CategoryDTOs/
│   │   │   ├── CreateCategoryDto.cs
│   │   │   ├── GetCategoryDto.cs
│   │   │   └── UpdateCategoryDto.cs
│   │   └── JwtDTOs/
│   │       └── JwtDto.cs
│   ├── Mapper/
│   │   └── DomainProfile.cs              # AutoMapper configuration
│   └── DependencyInjection/
│       └── ApplicationServiceRegistration.cs
│
├── Infrastructure/                       # Data Access Layer
│   ├── Database/
│   │   ├── ApplicationDbContext.cs       # Entity Framework Core context
│   │   └── Migrations/
│   ├── Configurations/
│   │   ├── ApplicationUserConfiguration.cs
│   │   ├── BlogConfiguration.cs
│   │   └── CategoryConfiguration.cs
│   ├── Repositories/
│   │   └── GenericRepository.cs
│   ├── Services/
│   │   ├── AccountService.cs
│   │   └── JwtService.cs
│   ├── UnitOfWork/
│   │   └── UnitOfWork.cs
│   ├── DataSeeding/
│   │   └── DataInitializer.cs
│   └── DependencyInjection/
│       └── InfrastructureServiceRegistration.cs
│
└── Domain/                               # Core Domain Layer
	├── Entities/
	│   ├── BaseEntity.cs                 # Base entity with generic Id
	│   ├── ApplicationUser.cs            # User with Identity
	│   ├── Blog.cs                       # Blog entity
	│   └── Category.cs                   # Category entity
	└── Domain.csproj
```

## Main Entities

### ApplicationUser
Extends ASP.NET Core Identity User with profile information:
- `FirstName`, `LastName` - User profile
- `Blogs` - Collection of user's published blogs

### Blog
Core blog entity with rich content:
- `Title` - Blog title (3-200 characters)
- `Content` - Blog body (20-10,000 characters)
- `ImageUrl` - Optional featured image URL
- `CategoryId` - Foreign key to Category
- `UserId` - Foreign key to ApplicationUser (owner)
- `CreatedAt` - Publication timestamp

### Category
Content organization:
- `Name` - Category name
- `Blogs` - Collection of blogs in this category

## Authentication & Authorization

### JWT (JSON Web Tokens)
- **Token Generation** - Issued upon successful login
- **Token Validation** - Custom claims and signature verification
- **Configuration** - Issuer, Audience, and expiration settings in `appsettings.json`
- **Duration** - Configurable token lifetime (default: 30 days)

### Role-Based Access Control (RBAC)
- **Admin Role** - Category management, administrative operations
- **User Role** - Default role for registered users
- **Public Access** - Endpoints for anonymous users (blog browsing, category listing)

### Endpoint Authorization Examples
```csharp
[AllowAnonymous]                      // Public endpoint
[Authorize]                           // Authenticated users only
[Authorize(Roles = "Admin")]          // Admin users only
```

## Technologies Used

### Core Framework
- **ASP.NET Core 9** - High-performance web framework
- **.NET 9** - Latest .NET runtime

### Data Access & ORM
- **Entity Framework Core** - Object-relational mapping
- **SQL Server** - Relational database
- **LINQ** - Data queries and operations

### Authentication & Security
- **ASP.NET Core Identity** - User management and authentication
- **JWT Bearer Authentication** - Token-based security
- **SymmetricSecurityKey** - Token encryption/decryption

### Dependency Injection & Mapping
- **Microsoft Dependency Injection** - Service registration and resolution
- **AutoMapper** - Object-to-object mapping for DTOs

### API Documentation
- **Swagger/OpenAPI** - Interactive API documentation
- **Swagger UI** - Web-based API testing interface

### Data Validation
- **System.ComponentModel.DataAnnotations** - Declarative validation rules
- **Custom Validation** - Email, StringLength, Required attributes

### Middleware & Error Handling
- **Global Exception Middleware** - Centralized error handling
- **Custom Response Format** - Standardized API responses

## Local Setup

### Prerequisites
- **.NET 9 SDK** - [Download](https://dotnet.microsoft.com/download)
- **SQL Server** - Any version (LocalDB, Developer Edition, or Express)
- **Visual Studio 2022** or **VS Code** with C# extension

### Installation Steps

1. **Clone the Repository**
   ```powershell
   git clone https://github.com/Gargera/DevPulse-WebAPI.git
   cd DevPulse
   ```

2. **Configure Database Connection**
   - Open `DevPulseApp/appsettings.json`
   - Update `ConnectionStrings:DefaultConnection` with your SQL Server connection string:
	 ```json
	 "ConnectionStrings": {
	   "DefaultConnection": "Server=.;Database=DevPulseDb;Trusted_Connection=true;TrustServerCertificate=true;"
	 }
	 ```

3. **Configure JWT Settings**
   - Update JWT settings in `appsettings.json`:
	 ```json
	 "JWT": {
	   "Key": "your-secret-key-here-minimum-64-characters",
	   "Issuer": "DevPulseAPI",
	   "Audience": "DevPulseClient",
	   "DurationInDays": 30
	 }
	 ```
   - **Security Note**: Use `dotnet user-secrets` for sensitive configuration in production

4. **Restore Dependencies**
   ```powershell
   dotnet restore
   ```

5. **Apply Migrations**
   ```powershell
   dotnet ef database update --project Infrastructure --startup-project DevPulseApp
   ```

6. **Run the Application**
   ```powershell
   dotnet run --project DevPulseApp
   ```

7. **Access the API**
   - API: `https://localhost:5001`
   - Swagger UI: `https://localhost:5001/swagger`

### User Secrets Setup (Production Recommended)
Instead of hardcoding sensitive data in `appsettings.json`, use User Secrets:

```powershell
# Initialize user secrets
dotnet user-secrets init --project DevPulseApp

# Set connection string
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Your_Connection_String" --project DevPulseApp

# Set JWT key
dotnet user-secrets set "JWT:Key" "Your_JWT_Secret_Key" --project DevPulseApp
```

## API Endpoints Overview

### Authentication
- `POST /api/account/register` - Create new user account
- `POST /api/account/login` - Authenticate and receive JWT token

### Blogs
- `GET /api/blog` - Get all blogs (public)
- `GET /api/blog/{id}` - Get blog by ID (public)
- `GET /api/blog/category/{categoryId}` - Filter blogs by category (public)
- `GET /api/blog/my-blogs` - Get current user's blogs (authenticated)
- `POST /api/blog` - Create new blog (authenticated)
- `PUT /api/blog/{id}` - Update blog (authenticated, owner only)
- `DELETE /api/blog/{id}` - Delete blog (authenticated, owner only)

### Categories
- `GET /api/category` - Get all categories (public)
- `GET /api/category/{id}` - Get category by ID (admin)
- `POST /api/category` - Create category (admin)
- `PUT /api/category/{id}` - Update category (admin)
- `DELETE /api/category/{id}` - Delete category (admin)

## Author

**Gargera**
- GitHub: [@Gargera](https://github.com/Gargera)
- Repository: [DevPulse-WebAPI](https://github.com/Gargera/DevPulse-WebAPI)

---

**DevPulse** © 2024. Built as a portfolio project showcasing modern ASP.NET Core development practices and Clean Architecture principles.
