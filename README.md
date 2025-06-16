# 🧪 Ambev Developer Evaluation - Full API (Cart, Product, User, Auth)

This repository is part of the technical assessment for a Senior Fullstack .NET Developer position at Ambev Tech. The focus of this implementation is the **Full API**, including Cart, Product, User, and Auth functionalities, fully covering architectural structure, validation, logging, testing, and best practices.

---

## 📦 Overview

This API provides:

- Product management: creation, updates, pagination, sorting, filtering, and deletion.
- Cart management: creation, update, and deletion of shopping carts.
- User management: registration, updating user details, and retrieval.
- Authentication and authorization: token-based authentication for secure access.
- Paginated and sorted listings.
- Caching for optimized queries.
- FluentValidation and structured logging.

The system follows the principles of **Hexagonal Architecture (Ports & Adapters)**, **CQRS**, and **Result Pattern**.

---

## 🧱 Project Structure

- `Domain`: Entities, enums, domain-level validation, and value objects.
- `Application`: Commands, queries, DTOs, MediatR handlers, and business logic.
- `WebApi`: Controllers, mappings, request validators, and API endpoints.
- `ORM`: EF Core entity mappings, database migrations, and PostgreSQL setup.
- `Common`: Logging, validation, result pattern, and reusable interfaces.
- `IoC`: Dependency injection container setup (service registrations).
- `Auth`: Authentication and authorization logic (JWT token generation and validation).

---

## 🚀 How to Run the Project

### Requirements

- .NET 8 SDK
- Docker & Docker Compose

### Steps

```bash
# Start infrastructure (PostgreSQL + Redis)
> docker-compose up -d

# Restore dependencies
> dotnet restore Ambev.DeveloperEvaluation.sln

# Apply database migrations
> dotnet ef database update --project src/Ambev.DeveloperEvaluation.WebApi --startup-project src/Ambev.DeveloperEvaluation.WebApi

# Run the API
> dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
```

Swagger UI available at:

```
https://localhost:7181/swagger
```

---

## 🌱 Test Seed Data

When running the project for the first time, it automatically seeds the database with sample `Product` data to facilitate API testing and development.

---

## 📮 Available Endpoints

- `GET /api/products` - List all products (paginated and sorted)
- `GET /api/products/{id}` - Get product by ID
- `GET /api/products/category/{category}` - List products by category
- `GET /api/products/categories` - List all categories
- `POST /api/products` - Create a new product
- `PUT /api/products/{id}` - Update a product
- `DELETE /api/products/{id}` - Delete a product

- `GET /api/carts` - List all carts (paginated and sorted)
- `GET /api/carts/{id}` - Get cart by ID
- `POST /api/carts` - Create a new cart
- `DELETE /api/carts/{id}` - Delete a cart

- `GET /api/users` - List all users (paginated and sorted)
- `GET /api/users/{id}` - Get user by ID
- `POST /api/users` - Register a new user
- `PUT /api/users/{id}` - Update user details

- `POST /api/auth/login` - User login (returns JWT token)
- `POST /api/auth/register` - Register a new user

All endpoints include validation and consistent `ApiResponse` formatting.

---

## 🧪 Testing

Unit tests are located in the `Ambev.DeveloperEvaluation.Unit` project.

```bash
> dotnet test
```

- Unit tests for domain entities (`ProductTests.cs`, `CartTests.cs`, `UserTests.cs`)
- Validation scenarios
- Test data generation using Bogus (`ProductTestData.cs`, `CartTestData.cs`, `UserTestData.cs`)
- Integration tests for basic flow (Auth, User, Cart, Product)

---

## 🛠️ Technologies Used

- .NET 8
- MediatR (CQRS)
- FluentValidation
- AutoMapper
- Entity Framework Core
- PostgreSQL
- Redis (via `ICacheService` abstraction)
- JWT Authentication
- xUnit + Bogus
- Swagger

---

## 🎯 Architecture and Practices

- **Hexagonal Architecture (Ports & Adapters)**
- CQRS pattern for read and write operations
- Result Pattern used for returning standardized results (success/failure with specific types)
- AutoMapper for DTO mappings
- Global exception handling via middleware
- Structured logging with `ILogger<T>`
- Response caching with custom keys per parameter
- JWT-based Authentication and Authorization
- Validation and consistent API responses using FluentValidation

---

## ✅ Implemented Features

- Full `Products`, `Carts`, `Users`, and `Auth` APIs including all endpoints
- Domain entities, validators, repositories, and CQRS handlers for Cart, Product, User, and Auth
- Logging, caching, and validation
- JWT-based authentication and authorization
- Unit tests for domain rules and application logic
- PostgreSQL database with seeding

---

## 🚧 Not Implemented

- **Integration tests for external services**

---

## 🤝 Contact

Developed by [Fabrício da Silva Almeida](https://github.com/fabricioalmeida-me).

---

> _This project was developed as part of Ambev Tech's technical assessment._
