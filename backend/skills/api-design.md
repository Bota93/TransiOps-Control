# API Design Skill

## Purpose

Ensure the backend API is predictable, consistent, and aligned with production-minded REST design.

Use this skill when adding or changing endpoints, request models, response models, filtering, status codes, or route structure.

---

## API Design Principles

- Prefer clear resource-oriented naming
- Keep routes consistent
- Use HTTP semantics correctly
- Make request and response behavior predictable
- Design for maintainability, not novelty

---

## Route Design

- Use plural resource names where appropriate
- Keep route structure simple and hierarchical only when the relationship is real
- Avoid verbs in routes unless the action is not naturally resource-based
- Use versioned routes consistently

Examples:
- `/api/v1/routes`
- `/api/v1/vehicles`
- `/api/v1/incidents`
- `/api/v1/trips/{tripId}/start`

---

## Request and Response Design

- Use explicit request models
- Do not expose internal persistence structures directly
- Keep response models stable and understandable
- Return only what the client needs
- Separate transport models from domain concerns

---

## Status Codes

Use status codes intentionally:

- `200 OK` for successful reads and updates when appropriate
- `201 Created` for successful creation
- `204 No Content` for successful operations with no response body when appropriate
- `400 Bad Request` for malformed or invalid requests
- `401 Unauthorized` for missing or invalid authentication
- `403 Forbidden` for authenticated but unauthorized access
- `404 Not Found` when the target resource does not exist
- `409 Conflict` for state conflicts or business conflicts where appropriate
- `422 Unprocessable Entity` only if there is a clear and consistent reason to distinguish semantic validation from request malformation

Avoid casual or inconsistent status code use.

---

## Filtering and Querying

- Use query parameters for filtering
- Keep parameter names explicit
- Avoid overloading a single endpoint with unclear query behavior
- Add pagination when collection size can grow meaningfully
- Keep sorting and filtering conventions consistent across endpoints

---

## Error Handling

- Return consistent error structures
- Do not leak internal exceptions
- Make errors useful for the client without exposing implementation details
- Keep validation errors explicit

---

## Anti-Patterns

- Verb-heavy routes without need
- Exposing EF entities directly
- Inconsistent status codes
- Huge all-purpose endpoints
- Hidden side effects
- Breaking route conventions from one module to another