<h1 align="center">TransitOps Control</h1>

<p align="center">
  TransitOps Control is an operational transport monitoring and simulation platform designed to model real-world transport workflows with an enterprise-oriented architecture.
</p>

<p align="center">
  The system focuses on route operations, vehicle tracking, incident management, operational alerts, and service visibility through a backend-first architecture with a visual frontend.
</p>

<p align="center">
  This project is being built as a production-minded modular system, not as a basic CRUD demo. Its goal is to demonstrate sound engineering practices, realistic business modeling, and a system design that can evolve into a deployable application.
</p>

---

## Project Goals

- Build a transport operations platform with a realistic domain model
- Simulate vehicle movement and operational state transitions
- Expose a professional backend API
- Provide a visual frontend for monitoring routes, vehicles, incidents, and metrics
- Maintain a clean, scalable architecture suitable for future deployment

---

## Core Functional Areas

Planned functional areas include:

- Route management
- Stop and station management
- Fleet and vehicle status tracking
- Trip and service run management
- Incident registration and resolution
- Operational alerts
- Telemetry and simulated movement updates
- Dashboard and operational metrics

---

## Current Status

> Project is currently in the planning and foundation setup phase.

Completed so far:

- Repository created
- .NET solution initialized
- Backend projects created:
  - `TransitOps.Api`
  - `TransitOps.Domain`
  - `TransitOps.Application`
  - `TransitOps.Infrastructure`
- Initial project references being configured
- Planning workflow, agents, and skills definition in progress

---

## Architecture Overview

The backend follows a modular layered architecture:

| Layer | Responsibility |
| --- | --- |
| **TransitOps.Domain** | Core business domain, entities, enums, and business rules |
| **TransitOps.Application** | Use cases, contracts, application services, orchestration logic |
| **TransitOps.Infrastructure** | Persistence, framework integrations, database access, external concerns |
| **TransitOps.Api** | HTTP entry point, dependency injection, middleware, API exposure |

The frontend will be built separately and will consume the API as the primary client.

---

## Planned Tech Stack

| Area | Technologies |
| --- | --- |
| Backend | .NET<br />ASP.NET Core Web API<br />Entity Framework Core<br />PostgreSQL |
| Frontend | React<br />TypeScript<br />Vite |
| Infrastructure | Docker<br />Docker Compose |
| Tooling | GitHub Issues / Project<br />Notion for planning and technical decision tracking<br />AGENTS.md and project skills for AI-assisted development workflow |

---

## Working Principles

This project is being developed with the following principles:

- Clear separation of concerns
- Domain-driven naming
- Explicit business rules
- Thin HTTP layer
- Incremental delivery
- Documentation alongside implementation
- Production-minded decisions over portfolio shortcuts

---

## Roadmap

| Phase | Scope |
| --- | --- |
| Phase 1 — Foundation | Solution structure<br />Project references<br />Working conventions<br />Planning documents<br />Core domain design |
| Phase 2 — Backend Core | Routes<br />Stops<br />Vehicles<br />Trips<br />Incidents<br />Alerts |
| Phase 3 — Simulation | Background processing<br />Telemetry updates<br />State transitions<br />Live operational view |
| Phase 4 — Frontend | Dashboard<br />Map visualization<br />Tables and filters<br />Operational timeline |
| Phase 5 — Deployment and Documentation | API documentation<br />Technical decisions<br />Demo dataset<br />Deployment setup |

---

## Repository Structure

```text
backend/
frontend/
docs/
AGENTS.md
README.md
```

Structure will evolve as the project setup is completed.

## Notes

> This repository is intentionally being built with a strong planning and architecture phase before feature implementation. The objective is to establish a robust development workflow and a maintainable technical foundation from the start.
