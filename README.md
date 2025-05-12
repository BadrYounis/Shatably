# Shatably - Home Services & Renovation Platform

## Overview
Shatably is a comprehensive home services and renovation platform that connects homeowners with skilled professionals such as electricians, carpenters, painters, plumbers, and maids. The platform also offers home improvement products and custom design services for renovation projects.

## Features
- **Service Booking**: Schedule appointments with verified professionals
- **Professional Listings**: Find skilled workers by specialty (electricians, carpenters, painters, etc.)
- **Product Marketplace**: Browse and purchase home improvement products
- **Custom Design Service**: Select and customize design templates for renovation projects
- **Order Management**: Track orders for products and services
- **Review System**: Rate and review service providers

## Technology Stack
- **Backend**: ASP.NET Core Web API (.NET 7+)
- **Database**: Entity Framework Core with SQL Server
- **Architecture**: Clean Architecture with Domain-Driven Design

## Project Structure
```
├── Shatably.API            # API entry point and configuration
├── Core                    # Business logic and domain models
│   ├── Domain              # Entities, value objects, and business rules
│   ├── Services            # Implementation of business logic
│   └── Services.Abstraction # Service interfaces
├── Infrastructure          # External concerns
│   ├── Persistence         # Database configuration and data access
│   └── Presentation        # API controllers and presentation logic
└── Shared                  # Shared utilities and DTOs
```

## Key Entities
- **Client**: End users who book services and purchase products
- **Worker**: Service providers with various specializations
- **ServiceBooking**: Appointments between clients and workers
- **Product**: Home improvement items for purchase
- **Order/OrderItem**: Purchase records for products
- **DesignTemplate**: Customizable designs for renovation projects
- **RatingReview**: Feedback system for services and products

## Getting Started

### Prerequisites
- .NET 7.0 SDK or later
- SQL Server (Local or Express)
- Visual Studio 2022 or later (recommended)

### Installation
1. Clone the repository
   ```
   git clone https://github.com/yourusername/Shatably.git
   ```

2. Navigate to the project directory
   ```
   cd Shatably
   ```

3. Restore dependencies
   ```
   dotnet restore
   ```

4. Update the connection string in `Shatably.API/appsettings.json`

5. Apply database migrations
   ```
   dotnet ef database update --project Infrastructure/Persistence --startup-project Shatably.API
   ```

6. Run the application
   ```
   dotnet run --project Shatably.API
   ```

## API Documentation
Once the application is running, you can access the Swagger UI for API documentation at:
```
https://localhost:{port}/swagger
```

## License
[MIT License](LICENSE)

## Author
[Badr El-Deen /Shatably]