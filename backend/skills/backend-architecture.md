# Backend Architecture Skill

## Purpose

Guide backend work so it remains consistent with the modular layered architecture of the project.

Use this skill when designing new modules, adding use cases, structuring code, or deciding where logic should live.

---

## Architectural Goal

The backend must remain understandable, modular, and maintainable.

The project uses a layered backend structure:

- Domain
- Application
- Infrastructure
- Api

This separation exists to protect business logic from framework noise and infrastructure coupling.

---

## Responsibilities by Layer

### Domain
Contains:
- core business entities
- business rules
- invariants
- domain enums and value concepts

Must not contain:
- HTTP concerns
- EF Core concerns
- serialization concerns
- framework-specific code

### Application
Contains:
- use cases
- orchestration logic
- interfaces required from infrastructure
- request/response models where appropriate
- explicit workflow validation

Must not contain:
- persistence implementation
- HTTP endpoint logic
- infrastructure wiring

### Infrastructure
Contains:
- database access
- EF Core configuration
- repository implementations
- external integrations
- framework-specific technical concerns

Must not contain:
- core business rules that belong to the domain

### Api
Contains:
- controllers or endpoints
- middleware
- dependency injection composition
- API-specific request handling

Must not contain:
- business logic
- persistence implementation details
- orchestration that belongs in application services or use cases

---

## Rules

- Keep boundaries explicit
- Prefer feature clarity over deep generic reuse
- Introduce abstractions only when repeated need is proven
- Keep the dependency flow clean
- Make each module explainable in terms of business behavior

---

## Decision Guidance

When placing code, ask:

1. Is this a business rule?  
   Put it in Domain or Application, depending on its nature.

2. Is this orchestration of a workflow?  
   Put it in Application.

3. Is this framework or database-specific?  
   Put it in Infrastructure.

4. Is this HTTP transport logic?  
   Put it in Api.

If the answer is unclear, the design is probably not ready.

---

## Anti-Patterns

- Fat controllers
- God application services
- EF Core leaking into domain entities
- Infrastructure-driven modeling
- Shared folders with unrelated logic
- Generic base classes without proven need