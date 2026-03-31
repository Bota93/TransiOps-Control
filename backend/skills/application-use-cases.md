# Application Use Cases Skill

## Purpose

Guide the design of application-layer use cases so they remain explicit, maintainable, and aligned with backend architecture.

Use this skill when implementing commands, queries, orchestration logic, request/response models, workflow validation, and application service coordination.

---

## Role of the Application Layer

The Application layer coordinates business workflows.

It is responsible for:
- orchestrating domain behavior
- defining application contracts
- handling workflow-level validation
- coordinating repositories and external interfaces through abstractions
- shaping request/response boundaries for use cases

It is not responsible for:
- HTTP transport concerns
- database implementation details
- framework-specific infrastructure wiring
- holding business rules that clearly belong to the domain model

---

## Design Goals

Application code should be:

- explicit
- easy to follow
- centered around business use cases
- testable
- free of accidental infrastructure coupling

A use case should describe what the system is trying to achieve, not just move data around.

---

## Use Case Structure

A use case should usually make clear:

1. the input it accepts
2. the validation it performs at application level
3. the business entities or services it uses
4. the side effects it triggers
5. the output it returns

Typical examples:
- CreateRoute
- AssignVehicleToTrip
- StartTrip
- ReportIncident
- ResolveIncident

---

## Contracts and Dependencies

- Define interfaces in Application when Infrastructure must implement them
- Depend on abstractions for persistence and external communication
- Keep dependency direction clean
- Do not let EF Core, SQL, or HTTP details leak into use case logic

Examples of valid application contracts:
- `IVehicleRepository`
- `ITripRepository`
- `IUnitOfWork` if justified
- `IClock` if time control becomes important
- `IEventPublisher` only if a real use case exists

---

## Request and Response Models

- Use explicit request models for use case input
- Use explicit response models when returning data across boundaries
- Keep DTOs focused on application needs
- Do not reuse domain entities as transport contracts by default
- Avoid bloated all-purpose DTOs

Application DTOs should express workflow intent clearly.

---

## Validation Guidance

Separate validation concerns properly:

- input shape and transport validation belong near the boundary
- workflow validation belongs in Application
- core business invariants belong in Domain

Examples of application-level validation:
- assigned vehicle must exist before starting a trip
- route must exist before trip creation
- incident command must reference an existing trip or vehicle when required

Do not push all validation into controllers.
Do not overload domain entities with concerns that belong to use case orchestration.

---

## Orchestration Rules

- Keep use cases focused on one business goal
- Do not turn application services into god objects
- Prefer explicit dependencies over hidden service locators
- Keep workflow steps understandable
- When a use case becomes too large, split by business behavior, not by arbitrary technical layers

---

## Anti-Patterns

- Application services that only proxy repository calls with no real use-case language
- Controllers containing orchestration logic
- Use cases coupled directly to EF Core or DbContext
- Reusing one DTO for create, update, and read indiscriminately
- Mixing transport validation, workflow validation, and domain invariants without distinction

---

## Completion Checklist

Before closing a task involving the Application layer, verify:

- the use case has a clear business purpose
- dependencies point only to allowed abstractions
- request/response models are explicit
- workflow validation is handled in the correct place
- business invariants are not misplaced
- the implementation can be explained as a business flow, not just a sequence of method calls