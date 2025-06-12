# 🧪 Ambev Developer Evaluation - Product API

This repository is part of the technical assessment for a Senior Fullstack .NET Developer position at Ambev Tech. The focus of this implementation is the **Product API**, fully covering architectural structure, validation, logging, testing, and best practices.

---

## 📦 Overview

This API provides:

- Product creation
- Product updates
- Paginated and sorted listings
- Filtering by category
- Product deletion
- Listing all distinct product categories
- Caching for optimized queries
- FluentValidation and structured logging

The system follows the principles of **Hexagonal Architecture (Ports & Adapters)**.

---

## 🧱 Project Structure

- `Domain`: Entities, enums and domain-level validation
- `Application`: Commands, queries, DTOs and MediatR handlers
- `WebApi`: Controllers, mappings and request validators
- `ORM`: EF Core entity mappings and database migrations
- `Common`: Logging, validation and reusable interfaces
- `IoC`: Dependency injection container setup (service registrations)

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

All endpoints include validation and consistent `ApiResponse` formatting.

---

## 🧪 Testing

Unit tests are located in the `Ambev.DeveloperEvaluation.Unit` project.

```bash
> dotnet test
```

- Unit tests for domain entities (`ProductTests.cs`)
- Validation scenarios
- Test data generation using Bogus (`ProductTestData.cs`)

---

## 🛠️ Technologies Used

- .NET 8
- MediatR (CQRS)
- FluentValidation
- AutoMapper
- Entity Framework Core
- PostgreSQL
- Redis (via `ICacheService` abstraction)
- xUnit + Bogus
- Swagger

---

## 🎯 Architecture and Practices

- **Hexagonal Architecture (Ports & Adapters)**
- CQRS pattern
- AutoMapper for DTO mappings
- Global exception handling via middleware
- Structured logging with `ILogger<T>`
- Response caching with custom keys per parameter

---

## ✅ Implemented Features

- Full `Products` API including all endpoints
- Domain entities, validators, repositories and CQRS handlers
- Logging and caching
- Unit tests for domain rules
- PostgreSQL database with seeding

---

## 🚧 Not Implemented

- `Cart` and `User` APIs
- Integration tests

**Reason:** Focused on delivering a high-quality implementation of the `Product` feature, covering all non-functional requirements and architecture standards.

---

## 🤝 Contact

Developed by [Fabrício da Silva Almeida](https://github.com/fabricioalmeida-me).

---

> *This project was developed as part of Ambev Tech's technical assessment.*

