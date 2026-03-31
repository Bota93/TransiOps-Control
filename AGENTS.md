# AGENTS.md

## Purpose

TransitOps Control is a production-minded transport operations platform built with enterprise-oriented standards.

This repository must be treated as a real software system with maintainability, clarity, and future deployment in mind. It must not be developed as a throwaway prototype or as a code-generation exercise.

Agents working in this repository should behave like experienced software engineers contributing to a modular business system.

---

## Agents Hierarchy

This file defines repository-wide rules and principles.

Module-specific agents (for example `backend/AGENTS.md` or `frontend/AGENTS.md`) extend and specialize these rules for their respective areas.

If there is a conflict:

1. This root file defines global priorities and principles
2. Module-specific agents define implementation rules for their scope

Module agents must not contradict global principles, but may refine or specialize them.

## Project Priorities

When making decisions, prioritize in this order:

1. Correctness
2. Clarity
3. Maintainability
4. Domain consistency
5. Delivery speed

Do not introduce complexity unless it solves a real problem.

---

## Engineering Principles

- Prefer explicit designs over clever abstractions
- Use consistent business and technical language
- Keep responsibilities clearly separated
- Build incrementally
- Avoid premature optimization
- Avoid premature architectural complexity
- Make decisions that are explainable in both technical and business terms
- Favor maintainable solutions over impressive-looking ones

---

## Working Model

Meaningful or structural work should usually follow this sequence:

1. Understand the problem
2. Clarify scope and constraints
3. Plan the work when needed
4. Implement
5. Validate
6. Update documentation if needed

Large or structural changes should be traceable through planning and project management artifacts.

---

## Planning Rules

Use planning before major implementation work, especially for structural, architectural, or unclear tasks.

A good plan should define:

- Goal
- Context
- Constraints
- Expected output

If a feature cannot be described clearly enough to plan, it is not ready for implementation.

Large tasks should be split into smaller, reviewable units.

---

## Documentation Rules

- Keep README aligned with the real project state
- Do not describe future features as if they already exist
- Document meaningful technical decisions that affect future work
- Prefer precise technical writing over vague or promotional wording
- Update documentation when architecture, workflow, or externally visible behavior changes

---

## Code Quality Rules

- Use meaningful and consistent names
- Avoid magic values
- Avoid hidden behavior
- Keep files focused on one responsibility
- Prefer straightforward code over unnecessary indirection
- Do not create abstractions without clear justification
- Write code that can be explained clearly in a review or interview

---

## AI-Assisted Development Rules

AI should support engineering judgment, not replace it.

Use AI to:

- clarify design options
- refine scope
- draft issues and documentation
- propose structures
- review code
- challenge assumptions
- accelerate well-defined implementation tasks

Do not use AI to blindly generate large parts of the system without understanding the result.

Every significant AI-generated change must be reviewed critically for:

- architectural fit
- naming quality
- maintainability
- unnecessary complexity
- correctness of assumptions

---

## Definition of Done

A task is not done unless:

- the agreed scope is completed
- the result is understandable
- the implementation has been validated appropriately
- relevant documentation is updated when required
- the change can be explained clearly

---

## Anti-Patterns To Avoid

- Over-engineering driven by appearance instead of real requirements
- Generic abstractions without real need
- Large unplanned code drops
- Misleading documentation
- Speed over understanding
- Technical decisions made only because a tool suggested them

## Modularity Note

This repository is expected to grow into multiple modules (backend, frontend, and potentially others).

Each module may define its own AGENTS.md file to specify local rules, while this file remains the source of global engineering principles.
