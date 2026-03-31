# Backend Testing Skill

## Purpose

Guide backend testing so it validates meaningful business behavior, protects critical flows, and avoids shallow or wasteful tests.

Use this skill when deciding what to test, which test level to use, and how to keep tests aligned with system value.

---

## Testing Goal

Tests should increase confidence in the system.

They should protect:
- business rules
- important workflows
- integration boundaries
- externally visible behavior

They should not exist only to inflate coverage metrics.

---

## Testing Strategy by Layer

### Domain
Test:
- business invariants
- state transitions
- entity behavior
- domain rule enforcement

Good examples:
- a trip cannot start without an assigned vehicle
- invalid status transitions are rejected
- route rules are enforced

Do not test:
- trivial getters/setters
- passive data with no logic

---

### Application
Test:
- use case orchestration
- workflow validation
- expected outcomes of commands/queries
- coordination between domain and abstractions

Good examples:
- starting a trip loads the correct entities and updates the right state
- resolving an incident follows the expected flow
- invalid requests fail at the correct boundary

Do not test:
- internal implementation trivia
- framework behavior already guaranteed by the platform

---

### Infrastructure
Test:
- persistence mappings when the risk is meaningful
- repository behavior that contains real query logic
- integration-specific behavior
- configuration that can fail in realistic ways

Good examples:
- EF Core mappings for key aggregates
- repository queries used by important dashboards or operational flows
- migration-sensitive persistence behavior when relevant

Do not over-test:
- framework internals
- boilerplate that adds no confidence

---

### API
Test:
- contract behavior
- status codes
- request/response flow
- authentication/authorization behavior when relevant
- integration of the HTTP boundary with the application layer

Good examples:
- valid request returns expected response code
- missing resource returns 404
- invalid request returns consistent error shape
- protected endpoint rejects unauthorized access

Do not test:
- controller internals as isolated trivia when integration tests cover the real behavior better

---

## Test Level Guidance

Choose the lightest test that gives meaningful confidence.

### Prefer domain/unit tests for:
- pure business logic
- state transitions
- invariants

### Prefer application tests for:
- use case orchestration
- validation flow
- repository abstraction coordination

### Prefer integration tests for:
- API behavior
- persistence interactions
- wiring across multiple layers

Do not default to end-to-end style tests for everything.

---

## Quality Rules

- Test behavior, not implementation noise
- Keep test names descriptive
- Arrange test data clearly
- Avoid overly brittle tests
- Use deterministic inputs
- Keep tests easy to read and explain
- Prefer a small number of meaningful tests over many shallow ones

---

## Anti-Patterns

- Testing private implementation details indirectly in fragile ways
- Verifying mocks excessively instead of business outcomes
- Writing many trivial tests for no-risk code
- Coupling tests too tightly to refactors
- Using integration tests where a focused domain test would be clearer
- Using unit tests to fake confidence in behavior that only integration can validate

---

## Completion Checklist

Before closing a backend task, verify:

- the most important business behavior is covered
- the chosen test level matches the risk
- tests validate outcomes, not just interactions
- test names describe behavior clearly
- the test suite adds confidence instead of noise