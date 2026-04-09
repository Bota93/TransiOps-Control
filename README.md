<h1 align="center">TransitOps Control</h1>

<p align="center">
  TransitOps Control is a production-minded transport operations platform being built around a clear backend domain model and a layered architecture.
</p>

<p align="center">
  The project focuses on representing real operational concepts such as routes, stops, vehicles, and trips before expanding into use cases, persistence, and API behavior.
</p>

---

## Project Goals

- Build a transport operations platform with a clear and maintainable business model
- Model operational concepts and lifecycle rules explicitly in the domain layer
- Evolve the backend through clean architectural boundaries
- Provide a frontend client on top of a stable backend API
- Keep the system explainable in both technical and business terms

---

## Current Status

TransitOps Control is in the early backend foundation stage.

Implemented so far:

- Repository conventions and engineering rules through `AGENTS.md`
- Layered backend solution structure in `backend/`
- Initial domain model in `TransitOps.Domain`
- Core domain entities:
  - `Vehicle`
  - `Stop`
  - `Route`
  - `RouteStop`
  - `Trip`
- Explicit status enums for the current domain concepts:
  - `VehicleStatus`
  - `StopStatus`
  - `RouteStatus`
  - `TripStatus`

Still pending:

- Application use cases in `TransitOps.Application`
- Persistence and EF Core implementation in `TransitOps.Infrastructure`
- Real HTTP endpoints in `TransitOps.Api`
- Frontend implementation
- Broader backend test coverage beyond the current domain layer

---

## Backend Architecture

The backend follows a layered structure:

| Layer | Responsibility | Current State |
| --- | --- | --- |
| **TransitOps.Domain** | Core business concepts, state transitions, and invariants | Initial domain model implemented |
| **TransitOps.Application** | Use cases, orchestration, contracts | Project created, no real use cases yet |
| **TransitOps.Infrastructure** | Persistence, EF Core, integrations | Project created, no persistence implementation yet |
| **TransitOps.Api** | HTTP entry points, configuration, composition root | Base ASP.NET Core template, no transport operations endpoints yet |

This separation exists to keep business rules independent from transport, database, and framework concerns.

---

## Current Domain Model

The initial domain model covers the core transport operation concepts:

- `Vehicle`
  - Identified by `Id`
  - Tracks fleet code, capacity, and operational status
- `Stop`
  - Identified by `Id`
  - Tracks stop code, name, and active/inactive state
- `Route`
  - Aggregate root for route definition
  - Owns an ordered collection of `RouteStop`
  - Controls stop sequencing and route lifecycle transitions
- `RouteStop`
  - Represents the inclusion of a stop inside a route
  - Stores `StopId` and `Sequence`
- `Trip`
  - Represents a planned or running service instance for a route
  - References a route and an optionally assigned vehicle
  - Controls assignment and trip lifecycle transitions

Key business rules currently modeled in the domain include:

- Route stops can only be modified while a route is in `Draft`
- A route needs at least two stops before it can be activated
- Trip vehicle assignment is only allowed while the trip is `Planned` or `Ready`
- A trip cannot be marked as ready without an assigned vehicle
- Completed and cancelled trips are terminal states

More detail is documented in [docs/domain-model.md](/c:/Proyectos/TransiOps-Control/docs/domain-model.md).

---

## Repository Structure

```text
backend/
  AGENTS.md
  skills/
  TransitOps.slnx
  TransitOps.Api/
  TransitOps.Application/
  TransitOps.Domain/
  TransitOps.Infrastructure/
docs/
frontend/
AGENTS.md
README.md
```

---

## Planned Next Steps

- Replace template code in `TransitOps.Api` with real application wiring
- Introduce first application use cases around routes, stops, vehicles, and trips
- Add persistence in `TransitOps.Infrastructure` once the application workflows are defined
- Expand test coverage as new application and infrastructure behaviors are introduced

---

## Working Principles

This repository is being developed with the following principles:

- Correctness before speed
- Clear business language
- Explicit invariants and state transitions
- Thin API and framework-agnostic domain
- Incremental delivery
- Documentation that matches the real state of the project
