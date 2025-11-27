# 🍽️ SufraSyncAPI - Restaurant Management System

A comprehensive RESTful API for restaurant order and inventory management built with ASP.NET Core 10. Features JWT authentication, role-based authorization, transactional order processing with automatic inventory tracking, and shopping cart functionality.

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=c-sharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-LocalDB-CC2927?logo=microsoft-sql-server)](https://www.microsoft.com/sql-server)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

---

## 📋 Table of Contents

- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Architecture](#-architecture)
- [Getting Started](#-getting-started)
- [API Documentation](#-api-documentation)
- [Database Schema](#-database-schema)
- [Authentication & Authorization](#-authentication--authorization)
- [Project Structure](#-project-structure)
- [Key Highlights](#-key-highlights)
- [Future Enhancements](#-future-enhancements)

---

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

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [SQL Server LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb) or SQL Server
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/kurokoxl/SufraSyncAPI.git
   cd SufraSyncAPI
   ```

2. **Configure User Secrets** (Recommended for JWT Key)
   ```bash
   cd SufraSyncAPI
   dotnet user-secrets init
   dotnet user-secrets set "Jwt:Key" "YourSuperSecretKeyHere-MustBeLongEnough!"
   dotnet user-secrets set "Jwt:Issuer" "SufraSyncAPI"
   dotnet user-secrets set "Jwt:Audience" "SufraSyncClient"
   ```

   *Alternatively, update `appsettings.json` (not recommended for production)*

3. **Update Database Connection String** (if needed)
   
   Edit `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SufraSyncAPI;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

4. **Apply Database Migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the Application**
   ```bash
   dotnet run
   ```

6. **Access the API**
   - Swagger UI: `https://localhost:7XXX/swagger`
   - API Base URL: `https://localhost:7XXX/api`

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

## 🗄️ Database Schema

### Core Entities

```
┌─────────────┐         ┌──────────────┐         ┌─────────────┐
│   Product   │────────→│ProductIngred │←────────│  Ingredient │
│             │         │   ient       │         │             │
│ ProductId   │         │ ProductId    │         │IngredientId │
│ Name        │         │IngredientId  │         │ Name        │
│ Price       │         │QuantityReq   │         │ Stock       │
│ CategoryId  │         │              │         │ Unit        │
└─────────────┘         └──────────────┘         └─────────────┘
      │                                                   
      │                                                   
      ↓                                                   
┌─────────────┐         ┌──────────────┐         ┌─────────────┐
│ OrderProduct│←────────│    Order     │────────→│Application  │
│             │         │              │         │    User     │
│ OrderId     │         │ OrderId      │         │             │
│ ProductId   │         │ UserId       │         │ UserId      │
│ Quantity    │         │ TotalAmount  │         │ Email       │
│ Price       │         │ OrderStatus  │         │ UserName    │
└─────────────┘         └──────────────┘         └─────────────┘
                                │
                                ↓
                        ┌──────────────┐
                        │   CartItem   │
                        │              │
                        │ UserId       │
                        │ ProductId    │
                        │ Quantity     │
                        └──────────────┘
```

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

## 🔮 Future Enhancements

### Performance & Scalability
- [ ] Add pagination for large datasets
- [ ] Implement caching (Redis) for product catalog
- [ ] Add database indexing optimization
- [ ] Implement API rate limiting

### Testing & Quality
- [ ] Add unit tests with xUnit
- [ ] Create integration tests
- [ ] Add load testing scenarios

### Features
- [ ] Email notifications for orders
- [ ] Order delivery tracking
- [ ] Product images upload
- [ ] Inventory alerts for low stock
- [ ] Reporting/analytics endpoints
- [ ] Customer reviews and ratings
- [ ] Discount codes and promotions

### DevOps & Infrastructure
- [ ] Implement global exception handling middleware
- [ ] Add structured logging (Serilog)
- [ ] Create health checks endpoint
- [ ] Docker containerization
- [ ] CI/CD pipeline setup (GitHub Actions)
- [ ] API versioning

### UI
- [ ] Create admin dashboard
- [ ] Build customer mobile app
- [ ] Add real-time order updates (SignalR)

---

## 📝 Sample Requests

### Complete Order Flow Example

```bash
# 1. Register User
curl -X POST https://localhost:7xxx/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "username": "customer1",
    "email": "customer@example.com",
    "password": "Password123!"
  }'

# 2. Login
curl -X POST https://localhost:7xxx/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "customer@example.com",
    "password": "Password123!"
  }'

# 3. View Products
curl -X GET https://localhost:7xxx/api/products

# 4. Add to Cart
curl -X POST https://localhost:7xxx/api/cart \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "productId": 1,
    "quantity": 2
  }'

# 5. Create Order
curl -X POST https://localhost:7xxx/api/orders \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "orderProducts": [
      {"productId": 1, "quantity": 2},
      {"productId": 2, "quantity": 1}
    ]
  }'

# 6. View My Orders
curl -X GET https://localhost:7xxx/api/orders/my-orders \
  -H "Authorization: Bearer YOUR_TOKEN"
```

---

## 🤝 Contributing

This is a portfolio project, but feedback and suggestions are welcome!

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 👤 Author

**Your Name**

- GitHub: [@kurokoxl](https://github.com/kurokoxl)
- LinkedIn: [Your LinkedIn Profile](https://linkedin.com/in/yourprofile)
- Portfolio: [Your Portfolio Website](https://yourportfolio.com)

---

## 🙏 Acknowledgments

- Built as a learning project to demonstrate ASP.NET Core proficiency
- Inspired by real-world restaurant POS systems
- Special thanks to the .NET community for excellent documentation

---

## 📊 Project Statistics

- **Lines of Code**: ~3,000+
- **API Endpoints**: 30+
- **Database Tables**: 10
- **Design Patterns Used**: 6+
- **Technologies**: 10+

---

## 🐛 Known Issues

- None at the moment. Please report any issues you find!

---

## 📞 Support

For questions or support, please:
- Open an issue on GitHub
- Contact via email: your.email@example.com

---

⭐ **If you found this project helpful or interesting, please consider giving it a star!**

---

*Last Updated: November 2025*
