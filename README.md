# Shatably - Home Services & Renovation Platform

## Overview

Shatably is a comprehensive home services and renovation platform that connects homeowners with skilled professionals such as electricians, carpenters, painters, plumbers, and maids. The platform also offers home improvement products and custom design services for renovation projects.

## Features

- **Service Booking**: Schedule appointments with verified professionals.
- **Professional Listings**: Find skilled workers by specialty (electricians, carpenters, painters, etc.).
- **Product Marketplace**: Browse and purchase home improvement products.
- **Custom Design Service**: Select and customize design templates for renovation projects.
- **Order Management**: Track orders for products and services.
- **Review System**: Rate and review service providers.
- **Online Payments**: Secure payments through Stripe integration.
- **Authentication & User Roles**: ASP.NET Core Identity with Client and Worker roles.

## Technology Stack

- **Backend**: ASP.NET Core Web API (.NET 8)
- **Database**: Entity Framework Core with SQL Server
- **Architecture**: Onion Architecture (Service Manager pattern)

## Project Structure

```text
├── Shatably.API              # API entry point and configuration
├── Core                      # Business logic and domain models
│   ├── Domain                # Entities, value objects, and business rules
│   ├── Services              # Implementation of business logic
│   └── Services.Abstraction  # Service interfaces
├── Infrastructure            # External concerns
│   ├── Persistence           # Database configuration and data access
│   └── Presentation          # API controllers and presentation logic
└── Shared                    # Shared utilities and DTOs
```

## Key Entities

- **Client**: End users who book services and purchase products.
- **Worker**: Service providers with various specializations.
- **ServiceBooking**: Appointments between clients and workers.
- **Product**: Home improvement items for purchase.
- **Order/OrderItem**: Purchase records for products.
- **DesignTemplate**: Customizable designs for renovation projects.
- **RatingReview**: Feedback system for services and products.

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- SQL Server (Local or Express)
- Visual Studio 2022 or later (recommended)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/BadrYounis/Shatably.git
   ```

2. Navigate to the project directory:

   ```bash
   cd Shatably
   ```

3. Configure `appsettings.json` (see the Configuration section below).

4. Restore dependencies:

   ```bash
   dotnet restore
   ```

5. Apply database migrations:

   ```bash
   dotnet ef database update --project Infrastructure/Persistence --startup-project Shatably.API
   ```

6. Run the application:

   ```bash
   dotnet run --project Shatably.API
   ```

## API Documentation

Once the application is running, you can access the Swagger UI for API documentation at:

```text
https://localhost:{port}/swagger
```

## Configuration

Copy `Shatably.API/appsettings.example.json` to `Shatably.API/appsettings.json` and configure the database connection strings, Stripe, and JWT settings.

Keep `appsettings.json` local and untracked. Use .NET User Secrets or environment variables for credentials.

The following example uses placeholders only:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=Shatably;Trusted_Connection=True;TrustServerCertificate=True;",
    "IdentityConnectionString": "Server=localhost;Database=ShatablyIdentity;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "StripeSettings": {
    "SecretKey": "",
    "EndPointSecret": ""
  },
  "JwtOptions": {
    "issuer": "https://localhost:7242",
    "audience": "shatably-api",
    "secretKey": "",
    "expirationInDays": 7
  }
}
```

Supply Stripe and JWT secrets through .NET User Secrets or environment variables (for example, `JwtOptions__secretKey`).

## License

[MIT License](LICENSE)

## Author

**Badr El-Deen Younis** — [GitHub](https://github.com/BadrYounis) | [LinkedIn](https://linkedin.com/in/badryounis)