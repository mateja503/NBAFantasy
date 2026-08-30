---
name: adding-an-endpoint
description: Add a new HTTP endpoint to the NBAFantasy minimal API - the endpoint-group extension method, DTO mapping and v1 registration this repo expects. Use when creating or wiring up a new route under /v1.
---

# Adding an HTTP endpoint

1. Create `NBA.Api/Endpoints/XEndpoints.cs` with a `MapXEndpoints(this IEndpointRouteBuilder)` extension
   that opens a group: `builder.MapGroup("x").WithTags("x").RequireAuthorization()` (rule 3).
2. Map routes on that group; take services as DI parameters; map results through `EntityMappings` (rule 5).
3. Register it under `v1` in `Program.cs` (`v1.MapXEndpoints();`).

Signal failures by throwing `NBAException(message, ErrorCodes.X)` (rule 7). `GlobalExceptionHandler`
serializes an `NBAException` to `{ ErrorMessage, ErrorCode }`; any other exception becomes a 500
`ProblemDetails`.
