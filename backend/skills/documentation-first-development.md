# Documentation-First Development Skill

## Purpose

Ensure backend implementation follows official documentation, framework conventions, and production-minded engineering practices.

This skill should be used when introducing new framework features, configuring infrastructure, designing API behavior, or applying library-specific patterns.

---

## Core Principle

Use official documentation as the primary source of truth for framework and library usage.

Do not rely on memory, guesswork, copied snippets, or unofficial patterns when the official documentation provides clear guidance.

---

## Objectives

- Follow supported and idiomatic usage patterns
- Reduce framework misuse
- Avoid outdated or cargo-cult implementations
- Keep the codebase aligned with maintainable platform conventions
- Combine official guidance with project architecture rules

---

## Rules

- Prefer official documentation over blogs, random examples, or generated assumptions
- Use framework features in the way they are intended to be used
- Do not introduce patterns only because they look advanced
- Do not force framework capabilities into places where they do not fit the project architecture
- If the documentation offers multiple valid options, choose the one that best fits simplicity, clarity, and maintainability for this project

---

## Application Guidance

When implementing something related to a framework or library:

1. Identify the exact concern
   - API endpoint design
   - dependency injection
   - configuration
   - validation
   - persistence
   - authentication
   - background processing

2. Check official documentation first

3. Prefer stable, mainstream patterns over edge-case or highly customized approaches

4. Adapt the implementation to the repository architecture
   - Domain remains framework-agnostic
   - Application remains focused on use cases and contracts
   - Infrastructure hosts framework-specific concerns
   - Api remains an entry point

---

## What Good Looks Like

- ASP.NET Core features used idiomatically
- EF Core configured according to supported patterns
- dependency injection setup is straightforward
- configuration follows documented conventions
- code feels native to the stack, not improvised

---

## Anti-Patterns

- Copying patterns from random tutorials without understanding them
- Adding abstraction layers that fight the framework
- Using unofficial shortcuts for critical behavior
- Introducing deprecated or unclear patterns
- Treating generated code as authoritative without validating against official documentation