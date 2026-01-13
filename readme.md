# ApiForge

**ApiForge** is an experimental, schema-driven API engine built for learning and architectural exploration.

The project focuses on **dynamic API contracts**, **runtime validation**, and **API telemetry**, rather than implementing a traditional business API.

>  This is a learning / research project, not a production-ready API gateway.

---

##  Project Goal

The main goal of ApiForge is to explore how a system can:

- define API endpoints dynamically (without code generation)
- validate request payloads at runtime using user-defined schemas
- expose a generic runtime API
- collect telemetry and statistics about API usage
- act as a **contract testing / mock API layer**

This project is intentionally **configuration-driven**, not data-driven.

---

##  Conceptual Model

ApiForge separates the system into two distinct worlds:

### 1 Admin / Configuration API

Used to:
- create, update, delete API endpoint definitions
- define request schemas
- manage endpoint status and limits
- inspect configured endpoints

### 2 Runtime API

Used by:
- developers testing integrations
- QA / sandbox environments
- contract validation tools

The runtime API:
- validates requests against schemas
- generates mock responses
- logs invocations
- collects statistics

It **does NOT store business data**.
