# 🌐 E-commerce Demo

## 📋 Project Overview
This project aims to develop a scalable and modular e-commerce platform using .NET Core 9, Domain-Driven Design (DDD), and microservices architecture.

## 🛠️ Technical Stack
- **Backend**: .NET Core 9
- **Frontend**: React (Comming soon)
- **Relational Database**: PostgreSQL
- **Distributed Caching**: Redis
- **Message Broker**: RabbitMq
- **Microservices**: Aspire for orchestration

## 📚 Domain-Driven Design Principles
- **Entities**: Define the core entities such as Product, Customer, Order, and Payment, each representing a unique object within the e-commerce domain.
- **Value Objects**: Identify attributes like Address and PaymentDetails that don't have an identity but add meaning to entities.
- **Aggregates**: Group related entities and value objects into aggregates to enforce consistency within their boundaries.
- **Repositories**: Implement repositories for data access, ensuring separation of concerns and encapsulating data retrieval logic.
- **Services**: Create domain services for complex business operations that don't naturally fit within a specific entity or value object.
- **Event Sourcing**: Use event sourcing to capture state changes as a sequence of events, allowing for better traceability and flexibility in handling complex business scenarios.