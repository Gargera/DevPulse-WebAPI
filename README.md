# DevPulse

![.NET](https://img.shields.io/badge/.NET-9.0-blue)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-green)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-ORM-orange)
![Serilog](https://img.shields.io/badge/Logging-Serilog%2FSeq-red)

A modern, fully-featured blogging platform built with **Clean Architecture**, **ASP.NET Core**, and **Entity Framework Core**. DevPulse demonstrates enterprise-grade API design with authentication, authorization, comprehensive blog management, and structured logging capabilities.

## Overview

DevPulse is a RESTful blogging platform that enables users to create, manage, and discover blog posts. The application leverages **Clean Architecture** principles to ensure separation of concerns, maintainability, and scalability. It showcases modern .NET development practices and design patterns with professional-grade logging and monitoring.

**Key Highlights:**
- 🏗️ Clean Architecture with clear separation of concerns
- 🔐 JWT-based authentication and role-based authorization
- 📝 Full blog lifecycle management (Create, Read, Update, Delete)
- 🏷️ Category-based blog organization and filtering
- 🔒 User ownership authorization for blog content
- 🛠️ Enterprise design patterns (Generic Repository, Unit of Work, Dependency Injection)
- 📊 Global exception handling with standardized responses
- 📋 Structured logging with **Serilog** and **Seq** integration
- 📁 File storage and image management for profiles and blogs
- 🔄 Automatic data seeding and database migrations

## Key Features

### Authentication & Authorization
- **User Registration & Login** - Secure account creation with email verification
- **JWT Authentication** - Token-based authentication for protected endpoints
- **Role-Based Authorization** - Admin and User roles with granular access control
- **User Ownership Validation** - Users can only modify their own blogs

### User Profile Management
- **Get User Profile** - Authenticated users can view their profile information (FirstName, LastName, Email, ImageUrl)
- **Update User Profile** - Users can update their personal information and upload a profile image
- **Image Handling** - Automatic image upload to storage with old image cleanup

### Blog Management
- **Create Blogs** - Authenticated users can publish blogs with title, content, category, and optional images
- **Read Blogs** - Public access to browse all blogs or filter by category
- **Update Blogs** - Users can edit only their own published blogs
- **Delete Blogs** - Users can delete only their own blogs
- **User Blog Retrieval** - Authenticated users can view their own blog collection

### Category Management
- **Browse Categories** - Public access to view all available categories
- **Admin Management** - Admin users can create, update, and delete categories and update, delete any blog for any user

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
| **Middleware Pattern** | Cross-cutting concerns (exception handling, authentication, logging) |
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
│   │   └── GlobalExceptionMiddleware.cs  # Centralized error handling & logging
│   ├── ExtensionMethods/
│   │   └── WebApplicationRegister.cs     # App configuration extensions
│   ├── Program.cs                        # Application entry point with Serilog setup
│   ├── appsettings.json                  # Configuration (Serilog, JWT, Database)
│   ├── Properties/
│   │   └── launchSettings.json
│   └── wwwroot/                          # Static files directory
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
│   │   │   ├── IJwtService.cs
│   │   │   ├── IUserProfileService.cs
│   │   │   └── IFileStorageService.cs
│   │   ├── Repositories/
│   │   │   └── IGenericRepository.cs
│   │   ├── UnitOfWork/
│   │   │   └── IUnitOfWork.cs
│   │   └── DataSeeding/
│   │       └── IDataInitializer.cs
│   ├── DTOs/
│   │   ├── AccountDTOs/
│   │   │   ├── RegisterDto.cs
│   │   │   ├── LoginDto.cs
│   │   │   ├── UpdateUserDto.cs
│   │   │   └── GetProfileDto.cs
│   │   ├── BlogDTOs/
│   │   │   ├── CreateBlogDto.cs
│   │   │   ├── GetBlogDto.cs
│   │   │   └── UpdateBlogDto.cs
│   │   ├── CategoryDTOs/
│   │   │   ├── CreateCategoryDto.cs
│   │   │   ├── GetCategoryDto.cs
│   │   │   ├── GetCategoryWithoutBlogsDto.cs
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
│   │   ├── JwtService.cs
│   │   └── UserProfileService.cs
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
- `ImageUrl` - Profile picture URL
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
```
[AllowAnonymous]                      // Public endpoint
[Authorize]                           // Authenticated
[Authorize(Roles = "Admin")]          // Admin users only
```

## Logging & Monitoring

### Serilog Integration
DevPulse uses **Serilog** for structured, richly-detailed logging. The logging pipeline captures:

- **Application Events** - Service operations, business logic execution
- **HTTP Requests/Responses** - Request details and outcomes
- **Exceptions** - Detailed error information with stack traces
- **Authentication Events** - Login attempts and token operations

### Serilog Configuration
The application is configured to write logs to multiple sinks via `appsettings.json`:

```json
"Serilog": {
  "MinimumLevel": "Information",
  "WriteTo": [
    {
      "Name": "Console"
    },
    {
      "Name": "Seq",
      "Args": {
        "serverUrl": "http://localhost:5341"
      }
    }
  ]
}
```

#### Sinks Available:
- **Console Sink** - Real-time logs in the application console
- **Seq Sink** - Centralized log aggregation and analysis at `http://localhost:5341`

### Seq Server
Seq is a centralized structured log server that provides:
- **Log Aggregation** - Collect logs from all application instances
- **Querying** - Search and filter logs using powerful query language
- **Dashboards** - Visualize application health and metrics
- **Alerts** - Create alerts based on log patterns

#### Starting Seq (Docker)
```powershell
# Run Seq in Docker
docker run -d -e ACCEPT_EULA=Y -p 5341:80 datalust/seq

# Access Seq UI
http://localhost:5341
```

### GlobalExceptionMiddleware with Logging
The `GlobalExceptionMiddleware` catches all unhandled exceptions and:
- Logs exception details using Serilog
- Returns standardized error responses
- Maintains request context for debugging

## File Storage & Management

### Image Upload & Storage
DevPulse supports image uploads for user profiles and blog posts with professional file handling:

- **User Profile Images** - Upload profile pictures when creating/updating user profile
- **Blog Post Images** - Add featured images to blog posts
- **File Validation** - Server-side validation for file type and size
- **Organized Storage** - Files stored in dedicated folders (Users, Blogs) in `wwwroot`

### File Management Features
- **Automatic Cleanup** - Old images automatically removed when replaced
- **Path Management** - Images accessible via relative URLs
- **Error Handling** - Graceful error responses for upload failures
- **Storage Service** - `IFileStorageService` handles all file operations

## Technologies Used

### Core Framework
- **ASP.NET Core 9** - High-performance web framework
- **.NET 9** - Latest .NET runtime with performance improvements

### Data Access & ORM
- **Entity Framework Core 9.0.16** - Object-relational mapping
- **SQL Server** - Relational database
- **LINQ** - Data queries and operations

### Authentication & Security
- **ASP.NET Core Identity** - User management and authentication
- **JWT Bearer Authentication** - Token-based security
- **SymmetricSecurityKey** - Token encryption/decryption

### Dependency Injection & Mapping
- **Microsoft Dependency Injection** - Service registration and resolution
- **AutoMapper 16.1.1** - Object-to-object mapping for DTOs

### Logging & Monitoring
- **Serilog.AspNetCore 10.0.0** - Structured logging for ASP.NET Core
- **Serilog.Settings.Configuration 10.0.0** - Configuration-based Serilog setup
- **Serilog.Sinks.Seq 9.1.0** - Seq server sink for log aggregation

### API Documentation
- **Swagger/OpenAPI** - Interactive API documentation
- **Swagger UI** - Web-based API testing interface
- **Swashbuckle.AspNetCore 9.0.6** - Swagger tooling for ASP.NET Core

### Data Validation
- **System.ComponentModel.DataAnnotations** - Declarative validation rules
- **Custom Validation** - Email, StringLength, Required attributes

### Middleware & Error Handling
- **Global Exception Middleware** - Centralized error handling with logging
- **Custom Response Format** - Standardized API responses

## Dependencies Summary

### DevPulseApp (API Layer)
```
- Microsoft.AspNetCore.Authentication.JwtBearer 9.0.16
- Microsoft.AspNetCore.OpenApi 9.0.16
- Microsoft.EntityFrameworkCore.Tools 9.0.16
- Serilog.AspNetCore 10.0.0
- Serilog.Settings.Configuration 10.0.0
- Serilog.Sinks.Seq 9.1.0
- Swashbuckle.AspNetCore 9.0.6
```

### Infrastructure (Data Access)
```
- Microsoft.AspNetCore.Authentication.JwtBearer 9.0.16
- Microsoft.AspNetCore.Identity.EntityFrameworkCore 9.0.16
- Microsoft.EntityFrameworkCore 9.0.16
- Microsoft.EntityFrameworkCore.Design 9.0.16
- Microsoft.EntityFrameworkCore.SqlServer 9.0.16
```

### Application (Business Logic)
```
- AutoMapper 16.1.1
```

## Local Setup

### Prerequisites
- **.NET 9 SDK** - [Download](https://dotnet.microsoft.com/download)
- **SQL Server** - Any version (LocalDB, Developer Edition, or Express)
- **Visual Studio 2022** or **VS Code** with C# extension

### Installation Steps

1. **Clone the Repository**
```powershell
git clone https://github.com/Gargera/DevPulse-WebAPI.git
cd "DevPulse (Web API)"
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

4. **Setup Serilog Logging (Optional but Recommended)**
 - **Option A: Console Logging Only** - Default configuration logs to console
 - **Option B: With Seq Server** - For advanced log analysis:
```powershell
# Run Seq using Docker
docker run -d -e ACCEPT_EULA=Y -p 5341:80 datalust/seq

# Access Seq UI at http://localhost:5341
```
- Ensure `appsettings.json` has Seq sink configured:
   ```json
   "Serilog": {
	 "MinimumLevel": "Information",
	 "WriteTo": [
	   { "Name": "Console" },
	   {
		 "Name": "Seq",
		 "Args": {
		   "serverUrl": "http://localhost:5341"
		 }
	   }
	 ]
   }
   ```

5. **Restore Dependencies**
```powershell
dotnet restore
```

6. **Apply Migrations**
```powershell
dotnet ef database update --project Infrastructure --startup-project DevPulseApp
```

7. **Run the Application**
```powershell
dotnet run --project DevPulseApp
```

8. **Access the Application**
   - API: `https://localhost:5001`
   - Swagger UI: `https://localhost:5001/swagger`
   - Seq Dashboard: `http://localhost:5341` (if Seq is running)

### User Secrets Setup (Production Recommended)
Instead of hardcoding sensitive data in `appsettings.json`, use User Secrets:

```powershell
# Initialize user secrets
dotnet user-secrets init --project DevPulseApp

# Set connection string
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Your_Connection_String" --project DevPulseApp

# Set JWT key
dotnet user-secrets set "JWT:Key" "Your_JWT_Secret_Key" --project DevPulseApp

# Set Seq server URL (optional)
dotnet user-secrets set "Serilog:WriteTo:1:Args:serverUrl" "Your_Seq_Server_URL" --project DevPulseApp
```

## API Endpoints Overview

### Authentication & User Profile
- `POST /api/account/register` - Create new user account
- `POST /api/account/login` - Authenticate and receive JWT token
- `GET /api/account/UserProfile` - Get current user profile (authenticated)
- `PUT /api/account/update` - Update user profile with FirstName, LastName, and Image (authenticated)

### Blogs
- `GET /api/blog` - Get all blogs (public)
- `GET /api/blog/{id}` - Get blog by ID (public)
- `GET /api/blog/category/{categoryId}` - Filter blogs by category (public)
- `GET /api/blog/my-blogs` - Get current user's blogs (authenticated)
- `POST /api/blog` - Create new blog (authenticated)
- `PUT /api/blog/{id}` - Update blog (authenticated, owner and admins only)
- `DELETE /api/blog/{id}` - Delete blog (authenticated, owner and admins only)

### Categories
- `GET /api/category` - Get all categories (public)
- `GET /api/category/{id}` - Get category by ID (admin)
- `POST /api/category` - Create category (admin)
- `PUT /api/category/{id}` - Update category (admin)
- `DELETE /api/category/{id}` - Delete category (admin)

## Recent Enhancements

### User Profile & File Management (Latest Update)
✅ **User Profile Endpoints** - Get and update user profile information  
✅ **Profile Image Upload** - Support for user profile picture uploads  
✅ **File Storage Service** - Professional image handling and storage  
✅ **Automatic Cleanup** - Old files automatically removed when updated  
✅ **GetProfileDto & UpdateUserDto** - Dedicated DTOs for profile operations  
✅ **IUserProfileService** - Service layer for profile management  

### Logging & Monitoring 
✅ **Serilog Integration** - Added structured logging with Serilog  
✅ **Seq Server Support** - Integrated Seq for centralized log aggregation  
✅ **Multiple Sinks** - Console and Seq sinks for flexible logging  
✅ **Exception Logging** - Global exception middleware now logs all errors  
✅ **Configuration-Based Setup** - Logging configuration in `appsettings.json`

### Previous Updates
✅ **Clean Architecture Implementation**  
✅ **JWT Authentication & Authorization**  
✅ **Entity Framework Core Integration**  
✅ **AutoMapper for DTOs**  
✅ **Generic Repository Pattern**  
✅ **Unit of Work Pattern**  
✅ **Global Exception Handling**  
✅ **Data Seeding**

## Author

**Gargera**
- GitHub: [@Gargera](https://github.com/Gargera)
- Repository: [DevPulse-WebAPI](https://github.com/Gargera/DevPulse-WebAPI)

---

**DevPulse** © 2024-2026. Showcasing modern ASP.NET Core development practices, Clean Architecture principles, and enterprise-grade logging solutions.
