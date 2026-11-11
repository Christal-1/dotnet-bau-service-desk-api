# BAU Service Desk

A full-stack service desk application built with **C#, ASP.NET Core Web API, Entity Framework Core, SQLite, Swagger, and JavaScript**.

## Features

- Responsive management dashboard.
- Create, view, search, filter, update, and delete tickets.
- KPI cards and ticket status overview.
- REST API with controller-based endpoints.
- Entity Framework Core and SQLite integration.
- Dependency injection.
- Swagger/OpenAPI documentation.
- Health-check endpoint.

## Technology Stack

- .NET 8 / C#
- ASP.NET Core Web API
- Entity Framework Core 8
- SQLite
- HTML, CSS and JavaScript
- Swagger / Swashbuckle

## Getting Started

**Prerequisite:** .NET 8 SDK.

Run from the folder containing `BaUServiceDeskApi.csproj`:

```powershell
dotnet restore
dotnet build
dotnet run
```

**Application URLs:**
- Dashboard: http://localhost:5203/
- Swagger: http://localhost:5203/swagger
- Health: http://localhost:5203/health

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/Tickets` | Get all tickets |
| GET | `/api/Tickets/{id}` | Get ticket by ID |
| POST | `/api/Tickets` | Create a ticket |
| GET | `/api/Tickets?status=Open` | Filter tickets by status |
| PATCH | `/api/Tickets/{id}/status` | Update ticket status |
| DELETE | `/api/Tickets/{id}` | Delete a ticket |

## Business Rules

- Tickets can be created, viewed, filtered, updated, and deleted.
- Resolved and closed ticket rules are enforced by the service layer.

## Continuous Integration

GitHub Actions automatically restores and builds the project on pushes and pull requests to `main`.

Workflow: `.github/workflows/dotnet.yml`

## Notes

- SQLite database is created automatically on first run.
- This is a learning and portfolio project, not a production service desk system.
- Uses fictional sample data only.
