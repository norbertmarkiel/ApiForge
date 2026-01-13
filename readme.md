# ApiForge

**ApiForge** is an experimental, schema-driven API engine built for learning and architectural exploration.

The project focuses on **dynamic API contracts**, **runtime validation**, and **API telemetry**, rather than implementing a traditional business API.

> ?? This is a learning / research project, not a production-ready API gateway.

---

## ?? Project Goal

The main goal of ApiForge is to explore how a system can:

- define API endpoints dynamically (without code generation)
- validate request payloads at runtime using user-defined schemas
- expose a generic runtime API
- collect telemetry and statistics about API usage
- act as a **contract testing / mock API layer**

This project is intentionally **configuration-driven**, not data-driven.

---

## ?? Conceptual Model

ApiForge separates the system into two distinct worlds:

### 1?? Admin / Configuration API
Used to:
- create, update, delete API endpoint definitions
- define request schemas
- manage endpoint status and limits
- inspect configured endpoints

### 2?? Runtime API
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

---

## ?? Architecture Overview
Client
?
Runtime API (/runtime/**)
?
API Key validation (planned)
?
Endpoint lookup
?
Schema validation
?
Mock response
?
Telemetry / statistics

Admin operations are isolated under `/admin`.

---

## ?? Core Concepts

### EndpointDefinition
Defines a dynamic API endpoint:
- unique `Id`
- `Path` used only for runtime routing
- enabled / disabled status
- supported protocols
- request rate limits
- request schema

### SchemaDefinition
Defines a request contract:
- dynamic fields
- required / optional fields
- type validation at runtime

### Runtime Invocation
Each runtime call produces telemetry:
- request count
- validation success / failure
- payload size
- response time
- client identity (planned)

---

## ?? Implemented Features

### ? Admin API
- `POST   /admin/endpoints` – register endpoint
- `GET    /admin/endpoints` – list endpoints
- `GET    /admin/endpoints/{id}` – endpoint details
- `PUT    /admin/endpoints/{id}` – update endpoint
- `DELETE /admin/endpoints/{id}` – remove endpoint

### ? Runtime API
- `POST /runtime/{**path}` – dynamic endpoint execution
- runtime schema validation
- mock response generation

### ? Engine
- dynamic routing using `{**path}`
- schema-based request validation
- in-memory registry (for learning purposes)

---

## ?? Example Flow

### 1?? Register endpoint (Admin)

```http
POST /admin/endpoints
			{
  "name": "Create order",
  "description": "Creates a new order",
  "path": "/orders/create",
  "status": "Enabled",
  "protocols": "Http1",
  "rateLimit": { "requestsPerSecond": 100 },
  "schema": {
    "fields": [
      { "name": "orderId", "type": "String", "isRequired": true },
      { "name": "amount", "type": "Number", "isRequired": true }
    ]
  }
}

2?? Call runtime endpoint