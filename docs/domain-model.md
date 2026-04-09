# Domain Model

## Purpose

This document describes the current transport operations domain model implemented in `backend/TransitOps.Domain`.

It reflects the real state of the codebase as of the initial backend foundation stage.

## Scope

The current model covers:

- vehicles
- stops
- routes
- route composition through ordered stops
- trips and their basic lifecycle

It does not yet cover:

- incidents
- alerts
- telemetry
- scheduling optimization
- persistence mapping
- application use cases

## Validation Status

The current domain model is backed by a dedicated xUnit test project in `backend/TransitOps.Domain.Tests`.

The implemented tests currently validate:

- route editing and lifecycle rules
- ordered route stop behavior
- duplicate route stop prevention
- trip lifecycle transitions
- trip vehicle assignment constraints
- vehicle out-of-service restrictions

## Entities

### Vehicle

Represents a fleet vehicle that can be assigned to operations.

Current properties:

- `Id`
- `Code`
- `Capacity`
- `Status`

Current lifecycle intent:

- available vehicles can be assigned
- assigned vehicles can move into operation
- out-of-service vehicles cannot be assigned or started

### Stop

Represents a transport stop in the network.

Current properties:

- `Id`
- `Code`
- `Name`
- `Status`

Current lifecycle intent:

- stops can be active or inactive

### Route

Represents a transport route definition.

Current properties:

- `Id`
- `Code`
- `Name`
- `Status`
- ordered stop collection

`Route` is the aggregate root for route composition.

It owns the route stop collection and protects the main invariants around route editing and activation.

### RouteStop

Represents a stop inside a route.

Current properties:

- `StopId`
- `Sequence`

This type exists to model route composition explicitly instead of embedding `Stop` directly into `Route`.

### Trip

Represents an operational trip planned for a route.

Current properties:

- `Id`
- `RouteId`
- `AssignedVehicleId`
- `PlannedStartTime`
- `PlannedEndTime`
- `Status`

This is currently a simple operational lifecycle model without stop-by-stop progress tracking.

## Relationships

The current domain relationships are modeled through identifiers and aggregate ownership:

- A `Route` owns many `RouteStop`
- A `RouteStop` references one `Stop` through `StopId`
- A `Trip` references one `Route` through `RouteId`
- A `Trip` may reference one `Vehicle` through `AssignedVehicleId`

This keeps the domain simple and avoids unnecessary coupling between aggregates.

## Statuses

The current explicit statuses are:

- `VehicleStatus`
- `StopStatus`
- `RouteStatus`
- `TripStatus`

These statuses are implemented as enums in `backend/TransitOps.Domain/Enums`.

## Key Invariants

The current model explicitly protects the following business rules:

### Vehicle

- vehicle id cannot be empty
- vehicle code cannot be empty
- capacity must be greater than zero
- out-of-service vehicles cannot be assigned or started

### Stop

- stop id cannot be empty
- stop code cannot be empty
- stop name cannot be empty

### Route

- route id cannot be empty
- route code cannot be empty
- route name cannot be empty
- route stops can only be added or removed while the route is in `Draft`
- duplicate stops are not allowed inside the same route
- stop sequence must remain valid after changes
- a route needs at least two stops before activation
- route lifecycle transitions are explicit and invalid repeated transitions fail clearly
- only suspended routes can be resumed
- only active or suspended routes can be retired

### RouteStop

- stop id cannot be empty
- sequence must be greater than zero

### Trip

- trip id cannot be empty
- route id cannot be empty
- planned end time must be later than planned start time
- assigned vehicle starts as `null`
- vehicle assignment is only allowed while `Planned` or `Ready`
- a trip must have an assigned vehicle before it can move to `Ready`
- a trip can only start from `Ready`
- a trip can only complete from `InProgress`
- a trip can only cancel from `Planned` or `Ready`
- trip lifecycle transitions are explicit and invalid repeated transitions fail clearly
- completed and cancelled trips are terminal states
- cancelling a trip releases its assigned vehicle reference

## Architectural Notes

- The domain layer is framework-agnostic
- No EF Core or persistence concerns are present in the current model
- No DTOs or API concerns leak into the domain
- Business rules live in entities instead of controllers or infrastructure code

## Current Gaps

The following areas are intentionally not implemented yet:

- domain events
- repositories
- persistence mapping
- application use cases
- API contracts
- integration between the current domain model and future application workflows
- broader testing outside the current domain layer
