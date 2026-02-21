# Microservices demo 

A .NET microservices architecture demo.  
Based on learning materials from M.Ozkaya

## Architecture

```
src/
├── BuildingBlocks/         # Shared kernel (CQRS, Behaviors, Exceptions)
└── Services/
    ├── Catalog/            # Product catalog API
    └── Basket/             # Shopping basket API
```

## Tech Stack

- **Runtime** — .NET 10, ASP.NET Core
- **Messaging** — MediatR (CQRS pattern)
- **Database** — PostgreSQL via Marten (document store)
- **Validation** — FluentValidation
- **Routing** — Carter
- **Containerization** — Docker, Docker Compose

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

## Getting Started

### 1. HTTPS Certificates

Required once per machine after cloning.

```powershell
dotnet dev-certs https --export-path .docker/certs/aspnetapp.pfx --password "Passw0rd!"
dotnet dev-certs https --trust
```

> Re-run this command if containers fail to start with a certificate error.

### 2. Run

```bash
docker-compose up --build
```

### 3. Stop

```bash
# Keep volumes (database data preserved)
docker-compose down

# Remove everything including volumes
docker-compose down -v
```

## Services

| Service | HTTP | HTTPS |
|---|---|---|
| Catalog.API | http://localhost:6000 | https://localhost:6060 |
| Basket.API | http://localhost:6001 | https://localhost:6061 |

## Health Checks

- Catalog: http://localhost:6000/health
- Basket: http://localhost:6001/health
