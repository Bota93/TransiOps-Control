# Domain Modeling Skill

## Purpose

Guide the design of the backend domain model so it represents real transport operations clearly and safely.

Use this skill when creating entities, defining relationships, modeling statuses, or introducing business rules.

---

## Modeling Goal

The domain model should reflect real business concepts, not database tables or UI screens.

It must be understandable in operational terms and capable of protecting key business invariants.

---

## Core Principles

- Model business concepts, not implementation shortcuts
- Use clear domain language
- Protect invariants explicitly
- Prefer meaningful behavior over passive data containers when business rules exist
- Keep the model simple enough to reason about

---

## Design Guidance

When introducing a domain type, ask:

- Is this a real business concept?
- Does it have identity?
- Does it have lifecycle or rules?
- Does it need explicit state transitions?
- Is it being modeled for business reasons or only for persistence convenience?

---

## Entities

Use entities for concepts with identity and lifecycle, such as:
- Vehicle
- Route
- Stop
- Trip
- Incident

Entities should:
- expose meaningful behavior
- protect invalid transitions when relevant
- avoid becoming pure property bags if real rules exist

---

## Statuses and State Transitions

- Model statuses explicitly
- Use enums when they remain simple and stable
- Protect invalid transitions when the business requires it
- Avoid free-form status strings

Examples:
- VehicleStatus
- TripStatus
- IncidentStatus

---

## Invariants

Important rules should be enforced explicitly.

Examples:
- a trip cannot start without an assigned vehicle
- a route must contain at least one stop
- an incident cannot be resolved if it is already closed in an invalid state flow

Do not rely only on UI or API validation for business invariants.

---

## Anti-Patterns

- Modeling entities as database-first records
- Property bags with no behavior where rules clearly exist
- Generic status fields with arbitrary strings
- Business rules hidden in controllers or repository code
- Domain names driven by technical implementation details