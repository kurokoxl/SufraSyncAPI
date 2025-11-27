# 🍽️ SufraSyncAPI - Restaurant Management System

A comprehensive RESTful API for restaurant order and inventory management built with ASP.NET Core 10. Features JWT authentication, role-based authorization, transactional order processing with automatic inventory tracking, and shopping cart functionality.

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=c-sharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-LocalDB-CC2927?logo=microsoft-sql-server)](https://www.microsoft.com/sql-server)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

## ✨ Features

### 🔐 Authentication & Authorization
- JWT-based authentication with ASP.NET Core Identity
- Role-based access control (Admin & User roles)
- Secure password hashing and token generation
- Claims-based authorization

### 🛒 Shopping Cart
- Persistent user cart sessions
- Add, update, and remove cart items
- Real-time price calculations
- Clear cart functionality

### 📦 Order Management
- **Transactional order processing** with automatic rollback on failure
- Real-time ingredient stock deduction
- Order status workflow (Pending → Preparing → Ready → Delivered → Cancelled)
- User-specific order history
- Admin order oversight

### 🍔 Product & Inventory Management
- Product catalog with categories
- Many-to-many product-ingredient relationships (recipe management)
- Ingredient stock tracking with unit of measurement (kg, pcs, etc.)
- Admin-only CRUD operations
- Automatic stock validation during order placement

### 📊 Business Logic
- Inventory deduction on order creation
- Concurrent order handling with database transactions
- Stock availability validation
- Dynamic price calculation
- Order cancellation with authorization checks

---

## 🛠️ Tech Stack

**Backend Framework:**
- ASP.NET Core 10.0
- C# 12.0
- Entity Framework Core 10.0

**Database:**
- SQL Server LocalDB
- Code-First Migrations

**Authentication:**
- ASP.NET Core Identity
- JWT Bearer Authentication
- IdentityModel.Tokens.Jwt

**Mapping & Validation:**
- AutoMapper 12.0.1
- Data Annotations

**API Documentation:**
- Swagger/OpenAPI
- Scalar API Documentation

**Design Patterns:**
- Repository Pattern
- Service Layer Pattern
- Dependency Injection
- DTO (Data Transfer Objects)

---

## 🏗️ Architecture

### Clean Architecture Layers

```
┌─────────────────────────────────────────┐
│         Controllers (API Layer)          │  ← HTTP Requests/Responses
├─────────────────────────────────────────┤
│        Services (Business Logic)         │  ← Business Rules & Validation
├─────────────────────────────────────────┤
│    Data Access (Repository/DbContext)    │  ← Database Operations
├─────────────────────────────────────────┤
│         Database (SQL Server)            │  ← Persistent Storage
└─────────────────────────────────────────┘
```

### Key Principles Applied
- ✅ **Separation of Concerns** - Controllers handle HTTP, Services handle business logic
- ✅ **SOLID Principles** - Interface-based design, dependency injection
- ✅ **DTO Pattern** - Entities never exposed directly to clients
- ✅ **Transaction Management** - ACID compliance for critical operations
- ✅ **AutoMapper** - Clean object-to-object mapping

---

## 📚 API Documentation

### Authentication Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/auth/register` | Register new user | ❌ |
| POST | `/api/auth/login` | Login and get JWT token | ❌ |

### Product Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/products` | Get all products (optional filter by category) | ❌ |
| GET | `/api/products/{id}` | Get product by ID with ingredients | ❌ |
| POST | `/api/products` | Create new product | ✅ Admin |
| PUT | `/api/products/{id}` | Update product | ✅ Admin |
| DELETE | `/api/products/{id}` | Delete product | ✅ Admin |
| POST | `/api/products/{id}/ingredients` | Add ingredient to product recipe | ✅ Admin |
| PUT | `/api/products/{id}/ingredients/{ingredientId}` | Update ingredient quantity | ✅ Admin |
| DELETE | `/api/products/{id}/ingredients/{ingredientId}` | Remove ingredient from recipe | ✅ Admin |

### Cart Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/cart` | Get user's cart | ✅ User |
| POST | `/api/cart` | Add item to cart | ✅ User |
| DELETE | `/api/cart/{productId}` | Remove item from cart | ✅ User |
| DELETE | `/api/cart/clear` | Clear entire cart | ✅ User |

### Order Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/orders` | Get all orders | ✅ Admin |
| GET | `/api/orders/{id}` | Get specific order | ✅ User/Admin |
| GET | `/api/orders/my-orders` | Get current user's orders | ✅ User |
| POST | `/api/orders` | Create new order | ✅ User |
| PUT | `/api/orders/{id}/advance-status` | Advance order status | ✅ Admin |
| PUT | `/api/orders/{id}/cancel` | Cancel order | ✅ User/Admin |

### Category & Ingredient Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/categories` | Get all categories | ❌ |
| POST | `/api/categories` | Create category | ✅ Admin |
| GET | `/api/ingredients` | Get all ingredients | ❌ |
| POST | `/api/ingredients` | Create ingredient | ✅ Admin |
| PUT | `/api/ingredients/{id}` | Update ingredient stock/unit | ✅ Admin |

---

### Key Relationships
- **Product ↔ Ingredient**: Many-to-Many (via ProductIngredient)
- **Product ↔ Order**: Many-to-Many (via OrderProduct)
- **User → Order**: One-to-Many
- **User → CartItem**: One-to-Many

---

## 🔐 Authentication & Authorization

### Registration & Login Flow

1. **Register**: `POST /api/auth/register`
   ```json
   {
     "username": "john_doe",
     "email": "john@example.com",
     "password": "SecurePassword123!"
   }
   ```

2. **Login**: `POST /api/auth/login`
   ```json
   {
     "email": "john@example.com",
     "password": "SecurePassword123!"
   }
   ```

3. **Response**: JWT Token
   ```json
   {
     "success": true,
     "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
     "username": "john_doe"
   }
   ```

4. **Use Token**: Add to requests
   ```http
   Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
   ```

### Role-Based Access

| Role | Permissions |
|------|-------------|
| **User** | View products, manage own cart, place orders, view own orders, cancel own orders |
| **Admin** | All User permissions + Manage products, ingredients, categories, view all orders, advance order status |

---

## 📁 Project Structure

```
SufraSyncAPI/
├── Controllers/           # API Controllers
│   ├── AuthController.cs
│   ├── ProductsController.cs
│   ├── OrdersController.cs
│   ├── CartController.cs
│   ├── CategoriesController.cs
│   └── IngredientController.cs
├── Services/             # Business Logic Layer
│   ├── AuthService.cs
│   ├── ProductService.cs
│   ├── OrderService.cs
│   ├── CartService.cs
│   └── Interfaces/
├── Models/               # Data Models
│   ├── Entities/        # Database Entities
│   ├── DTOs/            # Data Transfer Objects
│   └── Responses/       # API Response Wrappers
├── Data/                # Database Context & Configurations
│   ├── ApplicationDbContext.cs
│   └── Configurations/  # Entity Configurations
├── Mappings/            # AutoMapper Profiles
│   └── MappingProfile.cs
├── Migrations/          # EF Core Migrations
└── Program.cs           # App Entry Point
```

---

## 🎯 Key Highlights

### 1. Transactional Order Processing
Orders are processed atomically with automatic rollback on failure:

```csharp
using var transaction = await _context.Database.BeginTransactionAsync();
try
{
    // 1. Validate products exist
    // 2. Check ingredient stock availability
    // 3. Deduct inventory for each ingredient
    // 4. Create order and order items
    // 5. Calculate total amount
    await _context.SaveChangesAsync();
    await transaction.CommitAsync();
}
catch
{
    await transaction.RollbackAsync(); // Undo all changes
    throw;
}
```

### 2. Recipe Management
Products have recipes (ingredients + quantities) with units:
- **Beef Taco**: 0.1 kg Beef, 2 pcs Tortilla, 0.05 kg Lettuce, 0.03 kg Tomato
- Stock automatically calculated and deducted per order quantity
- Unit of measurement tracked at ingredient level (kg, pcs, liters, etc.)

### 3. Order Status Workflow
```
Pending → Preparing → Ready → Delivered
   ↓
Cancelled (User/Admin can cancel before delivery)
```

### 4. Authorization Logic
- Users can only view/cancel their own orders
- Admins have full access to all operations
- JWT claims contain user ID and roles
- Controller-level and action-level authorization

---
## 👤 Author

**Youssef Abdelazim**

- GitHub: [@kurokoxl](https://github.com/kurokoxl)
- LinkedIn: [@Youssef Abdelazim](https://www.linkedin.com/in/youssef-abdelazim-9b6a8325b/)
---
## 📊 Project Statistics

- **Lines of Code**: ~3,000+
- **API Endpoints**: 30+
- **Database Tables**: 10
- **Design Patterns Used**: 6+
- **Technologies**: 10+

⭐ **If you found this project helpful or interesting, please consider giving it a star!**

---

*Last Updated: November 2025*
