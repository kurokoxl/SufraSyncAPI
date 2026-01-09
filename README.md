# 🍽️ SufraSyncAPI - Restaurant Management System

A comprehensive RESTful API for restaurant order and inventory management built with ASP.NET Core 10. Features JWT authentication, role-based authorization, transactional order processing with automatic inventory tracking, and shopping cart functionality.

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=c-sharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-LocalDB-CC2927?logo=microsoft-sql-server)](https://www.microsoft.com/sql-server)



<img width="1539" height="1222" alt="Screenshot 2025-11-27 115011" src="https://github.com/user-attachments/assets/b058a19f-b219-4c25-a830-8a78a510c6bc" />
<img width="1604" height="966" alt="Screenshot 2025-11-27 115027" src="https://github.com/user-attachments/assets/4cb22123-7839-4ac6-b219-3d13f61d2bb6" />


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



- GitHub: [@kurokoxl](https://github.com/kurokoxl)
- LinkedIn: [@Youssef Abdelazim](https://www.linkedin.com/in/youssef-abdelazim-9b6a8325b/)
---


*Last Updated: November 2025*



