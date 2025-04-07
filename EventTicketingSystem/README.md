# Event Ticketing System API

A .NET 8 Web API for event listing and ticket sales with NHibernate data access.


## Project Overview

This is a simple ASP.NET Core Web API that provides endpoints for:
- Retrieving upcoming events (next 30, 60, or 180 days)
- Getting events by ID
- Retrieving ticket sales for specific events
- Viewing top selling events by ticket count and revenue

The application is built with .NET 8, uses NHibernate for data access, and follows a clean architecture approach with repository and service layers.


## Project Structure

```
EventTicketingSystem/
│
├── Controllers/               # API Controllers
│   ├── EventsController.cs    # Endpoints for event data
│   ├── TicketsController.cs   # Endpoints for ticket data
│   └── SalesController.cs     # Endpoints for sales analytics
│
├── DTOs/                      # Data Transfer Objects
│   ├── EventDto.cs
│   ├── TicketSaleDto.cs
│   └── TopSellingEventDto.cs
│
├── Infrastructure/            # Infrastructure concerns
│   └── NHibernateHelper.cs    # NHibernate setup and configuration
│
├── Mappings/                  # NHibernate mappings
│   ├── EventMap.cs            # Mapping for Event entity
│   └── TicketSaleMap.cs       # Mapping for TicketSale entity
│
├── Models/                    # Domain models
│   ├── Event.cs               # Event entity
│   └── TicketSale.cs          # TicketSale entity
│
├── Repositories/              # Data access layer
│   ├── Interfaces/
│   │   ├── IEventRepository.cs
│   │   └── ITicketSaleRepository.cs
│   │
│   ├── EventRepository.cs
│   └── TicketSaleRepository.cs
│
├── Services/                  # Business logic layer
│   ├── Interfaces/
│   │   ├── IEventService.cs
│   │   └── ITicketService.cs
│   │
│   ├── EventService.cs
│   └── TicketService.cs
│
├── Tests/                     # Unit tests
│   └── Services/
│       ├── EventServiceTests.cs
│       └── TicketServiceTests.cs
│
├── Program.cs                 # Application bootstrap and DI configuration
└── events_database.db         # SQLite database
```


## Database Schema

The application uses a SQLite database with the following tables:

### Events Table
- `Id` - TEXT PRIMARY KEY 
- `Name` - TEXT NOT NULL 
- `StartsOn` - DATETIME NOT NULL 
- `EndsOn` - DATETIME NOT NULL 
- `Location` - TEXT NOT NULL

### TicketSales Table
- `Id` - TEXT PRIMARY KEY 
- `EventId` - TEXT NOT NULL 
- `UserId` - TEXT NOT NULL 
- `PurchaseDate` - DATETIME NOT NULL 
- `PriceInCents` - INTEGER NOT NULL


## API Endpoints

### Events

1. **Get Upcoming Events**
   - URL: `/api/events`
   - Method: `GET`
   - Query Parameters: `days` (can be 30, 60, or 180, defaults to 30)
   - Example: `/api/events?days=60`
   - Description: Returns all events coming up in the next 30, 60, or 180 days

2. **Get Event by ID**
   - URL: `/api/events/{id}`
   - Method: `GET`
   - Example: `/api/events/411eca5c-9be4-4a84-a2be-688167e71899`
   - Description: Returns details of a specific event

### Tickets

1. **Get Tickets for an Event**
   - URL: `/api/tickets/event/{eventId}`
   - Method: `GET`
   - Example: `/api/tickets/event/411eca5c-9be4-4a84-a2be-688167e71899`
   - Description: Returns all ticket sales for a specific event

### Sales Analytics

1. **Get Top Selling Events by Count**
   - URL: `/api/sales/top-by-count`
   - Method: `GET`
   - Query Parameters: `count` (number of results to return, defaults to 5)
   - Example: `/api/sales/top-by-count?count=10`
   - Description: Returns top events by number of tickets sold

2. **Get Top Selling Events by Revenue**
   - URL: `/api/sales/top-by-revenue`
   - Method: `GET`
   - Query Parameters: `count` (number of results to return, defaults to 5)
   - Example: `/api/sales/top-by-revenue?count=10`
   - Description: Returns top events by total revenue generated

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQLite

### Installation

1. Clone this repository
2. Place the SQLite database file (`events_database.db`) in the project root directory

### Running the Application

```bash
cd EventTicketingSystem
dotnet build
dotnet run
```

The API will be available at:
- `http://localhost:5134`

### Running the Tests

```bash
cd EventTicketingSystem
dotnet build
dotnet test
```

## Technical Details

### Dependency Injection

The application uses ASP.NET Core's built-in dependency injection container. Services and repositories are registered in the `Program.cs` file with appropriate lifetimes:

- Repositories are registered as singletons to share the NHibernate session factory
- Services are registered as scoped to ensure proper resource management

### NHibernate Configuration

NHibernate is configured to work with SQLite in the `NHibernateHelper.cs` file. The mappings use the NHibernate.Mapping.ByCode approach for type-safe mappings.

Key aspects:
- Entity mappings are defined in the `Mappings` folder
- The Events entity has a simple mapping
- The TicketSale entity has a more complex mapping with a many-to-one relationship to Events
- The relationship is implemented using a navigation property

### API Design

The API follows RESTful principles and includes:
- Proper HTTP status codes
- Parameter validation
- Error handling and logging
- JSON formatting of responses

### CORS Configuration

The API is configured to allow CORS requests from `http://localhost:3000` to support frontend development.

## Future Improvements

- Add authentication and authorization
- Implement pagination for endpoints returning lists
- Add caching for frequently accessed data
- Implement more comprehensive logging and monitoring
- Create a more sophisticated API response wrapper
