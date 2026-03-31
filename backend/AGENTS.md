# backend/AGENTS.md

## Purpose

The backend is the operational core of TransitOps Control.

It must be designed as a maintainable, production-minded modular system with clear boundaries between business logic, application orchestration, infrastructure concerns, and HTTP exposure.

The backend should prioritize clarity, domain consistency, and operational correctness over unnecessary abstraction.

---

## Architectural Boundaries

Respect the following project boundaries:

- `TransitOps.Domain` contains core business concepts, rules, and invariants
- `TransitOps.Application` contains use cases, contracts, orchestration, and application-level workflows
- `TransitOps.Infrastructure` contains persistence, framework integrations, and external concerns
- `TransitOps.Api` contains HTTP entry points, configuration, middleware, and dependency wiring

Do not break these boundaries without a strong and explicit reason.

---

## Dependency Rules

Allowed dependency flow:

- Api -> Application
- Api -> Infrastructure
- Application -> Domain
- Infrastructure -> Application
- Infrastructure -> Domain

Forbidden dependency directions include:

- Domain -> any other project
- Application -> Infrastructure
- Infrastructure -> Api

The domain must remain the most stable and isolated part of the backend.

---

## Domain Rules

- Keep the domain model framework-agnostic
- Use domain language consistently
- Protect business invariants explicitly
- Avoid anemic entities when business rules clearly belong to the domain
- Do not move core business rules into controllers, EF configurations, or utility classes
- Prefer clear aggregates and responsibilities over generic reusable models

Domain code should represent the transport operation model, not persistence or HTTP concerns.

---

## Application Rules

- Application coordinates use cases
- Application may define interfaces required from infrastructure
- Application should not contain persistence implementation details
- Keep use cases explicit and understandable
- Use DTOs or request/response models where appropriate at the application boundary
- Validation should be explicit and close to the application workflow

Application code should orchestrate behavior, not become a dumping ground for random logic.

---

## Infrastructure Rules

- Infrastructure implements persistence and integration concerns
- Infrastructure may depend on Application contracts and Domain types
- Keep EF Core and database concerns out of Domain
- Keep infrastructure code replaceable in principle, even if not immediately swapped in practice
- Organize persistence code around the domain model and use cases, not around accidental technical shortcuts

---

## API Rules

- The API is an entry point, not the business layer
- Keep controllers or endpoints thin
- Do not place business rules in HTTP handlers
- Use clear resource naming and predictable response behavior
- Keep request handling explicit
- Favor consistency in status codes, error structures, and route naming

The API should expose the system cleanly, not contain the system.

---

## Validation Rules

- Validate inputs explicitly
- Separate transport/input validation from domain invariants when possible
- Do not rely on hidden framework behavior for critical validation
- Fail clearly when business rules are violated

---

## Backend Quality Rules

- Prefer understandable modules over overly generic shared helpers
- Avoid god services
- Avoid fat controllers
- Avoid leaking persistence models into higher layers
- Avoid speculative abstractions
- Keep code reviewable and traceable to business behavior

---

## Testing Guidance

- Test business-critical logic first
- Prefer tests that validate business behavior over implementation trivia
- Keep test boundaries clear
- Do not add shallow tests just to inflate coverage
- Prioritize meaningful tests for domain rules, application workflows, and critical integration paths

---

## Definition of Done

Backend work is not done unless:

- architecture boundaries are respected
- the behavior is understandable
- validation is appropriate
- persistence or API impact is handled correctly
- relevant documentation is updated when needed
- the change can be defended technically