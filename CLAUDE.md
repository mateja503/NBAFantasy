# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

NBAFantasy is a .NET 10 fantasy-basketball backend orchestrated with .NET Aspire. It exposes a minimal
API with real-time draft and chat over SignalR, backed by PostgreSQL (EF Core) and Redis, and pulls NBA
data from the external balldontlie API.

## Project rules

These are mandatory conventions for this repository.

1. **Naming.** Use camelCase for local variables, parameters, and request/DTO members (e.g.
   `request.teamName`). The one exception is the scaffolded database entities in `NBA.Data/Entities/`:
   their properties mirror the all-lowercase Postgres column names, so each is a single lowercase token
   with only the leading letter capitalized as C# requires — `Playerid`, `Irlteamname`, `Tscreated`,
   never `PlayerId` / `IrlTeamName`. Do not introduce camel-hump casing on entity properties.
2. **Schema changes go in `infrastructure/`.** There are no EF migrations — the database is built from the
   SQL scripts under `C:\My Projects\NBAFantasy\infrastructure` (`db/create`, `db/seed`), which are the
   source of truth. When adding a table, adding a column, or changing a column type, edit those SQL scripts
   (and the matching entity in `NBA.Data/Entities/`). Name every new table and column in lowercase.
3. **Authenticate everything.** Every HTTP endpoint and every SignalR connection must require
   authentication. Apply `.RequireAuthorization()` to each endpoint group (see `TeamEndpoints`) and
   `[Authorize]` to every hub (see `ChatHub`). The only allowed anonymous endpoints are `/health`,
   `/alive`, `/redis-check`, and the login/signup endpoints.
4. **Service vs Manager.** Put logic in a `*Service` when it changes or manipulates data that lives in
   PostgreSQL; put logic in a `*Manager` when it works against Redis.
5. **DTOs at the API boundary.** Never return EF entities from endpoints — always map to a response DTO
   via the mappers in `NBA.Api/Mappings/`. Request DTOs use camelCase (e.g. `TeamRequest.teamName`);
   response DTOs mirror the entity casing they map from (e.g. `TeamDto.Teamid`, `TeamDto.Name`).
6. **Outbound HTTP uses the resilience pipeline.** Any new external/outbound HTTP call must go through the
   named `external-api-shield` pipeline (retry + concurrency limiter).
7. **Errors.** Throw `NBAException` with an `ErrorCodes` value rather than ad-hoc exceptions; if a suitable
   `ErrorCode` does not exist, add it. The `GlobalExceptionHandler` translates these into responses.
8. **Redis access layer.** `NbaFantasyRedis` (`NBA.Data/Context/NbaFantasyRedis.cs`) is a Facade: each
   `*Operations` dependency is injected as `Lazy<T>` and exposed as a property. The `*Operations` classes
    perform logical operations that correspond only to Redis. All Redis keys are defined in
   `NBA.Data/Redis/Keys/RedisKeys.cs` as `public static` key builders; never inline a key string elsewhere.

## Commands

- **Run the whole system:** `dotnet run --project NBAFantasy` — the Aspire AppHost provisions Redis and
  Postgres (in containers) and launches the API. It also serves the Aspire dashboard. Docker must be running.
- **Build:** `dotnet build NBAFantasy.slnx`
- **Test (all):** `dotnet test NBA.Tests/NBA.Tests.csproj`
- **Test (single):** `dotnet test NBA.Tests/NBA.Tests.csproj --filter "FullyQualifiedName~ClassName.MethodName"`

### First-time secret setup
The AppHost reads secrets as Aspire parameters from user-secrets (on the `NBAFantasy` project). Run once:
```
cd NBAFantasy
dotnet user-secrets set "Parameters:postgress-password" "<value>"
dotnet user-secrets set "Parameters:balldontlie-apikey" "<value>"
dotnet user-secrets set "Parameters:jwt-signing-key" "<value>"
```

## Architecture

Aspire AppHost (`NBAFantasy/AppHost.cs`) is the composition root: it defines the Redis (`redis-cache`)
and Postgres (`nbafantasydb`) resources, injects the balldontlie API key and JWT signing key into the
API as environment variables, and starts `NBA.Api`. Because the AppHost references the other projects as
*orchestration resources*, their NuGet packages do not flow into it transitively — pin package fixes in
the AppHost directly when needed.

Project layering (depend downward):

- **NBA.Api** — ASP.NET Core minimal API (`Program.cs`). Endpoints are grouped under `/v1` and registered
  via extension methods in `Endpoints/*Endpoints.cs`. Real-time features use SignalR hubs `/draftHub` and
  `/chatHub`. JWT bearer auth with refresh tokens; OpenAPI + Scalar UI in Development. Hangfire dashboard
  is mounted here.
- **NBA.Service** — business logic, paired as `*Service`/`*Manager` (`Draft`, `Player`, `Trade`) — see
  rule 4 for the split. Argon2id password hashing lives here. The static `Adapter` maps balldontlie
  `PlayerInfoResponse` → `Player` entity / `PlayerShort` Redis shape, handling the position
  `string ↔ PlayerPositionEnum ↔ string` round trip — a separate layer from the entity→DTO `EntityMappings`
  in rule 5.
- **NBA.Data** — persistence over two stores: EF Core `NbaFantasyContext` (scaffolded `partial`, no
  migrations — see rule 2) on PostgreSQL/Npgsql, and a Redis layer under `Redis/` accessed via the
  `NbaFantasyRedis` facade (rule 8).
- **ExternalClients** — `BallDontLieClient`, a typed HttpClient for the balldontlie API.
- **BoxScoreFactory** (project `BoxScoreBuilder.csproj`, assembly `BoxScoreBuilder`) — `BoxScoreStatsBuilder`,
  a fluent builder that fabricates **randomized dummy** player game stats (`random.Next(...)`). It's a
  stand-in until the balldontlie player-stats subscription is paid for; the actual fantasy scoring that
  weights raw stats lives in `NBA.Service` (`BoxScoreCalculationService` → `BoxScoreCalculationBuilder`),
  currently using the hardcoded multipliers in `BoxScoreEvaluation`. **TODO (undecided):** whether scoring
  should instead read the per-league `Statsvalue` table — not yet decided, so leave both paths in place.
- **ApplicationDefaults** — shared options POCOs (`JwtOptions`, `DraftOptions`, `Argon2Options`,
  `BallDontLieClientOptions`, …), exception types (`NBAException`, `ErrorCodes`), and logging defaults.
- **NBA.ServiceDefaults** — Aspire cross-cutting defaults. Every service calls `AddServiceDefaults()` and
  `MapDefaultEndpoints()`: OpenTelemetry, health checks (`/health`, `/alive`, dev-only), default HTTP
  resilience, and service discovery.

### Cross-cutting concepts

- **Draft (real-time):** the live draft is the most intricate flow. Draft state lives in Redis
  (`DraftRedisOperations`, `DraftState`); clients act over the SignalR `DraftHub`; and
  `DraftTimerHostedService` polls Redis for pick deadlines — this intentionally replaces per-pick Hangfire
  jobs. `DraftManager` coordinates the moving parts.
- **Background jobs:** Hangfire with PostgreSQL storage (`hangfire` schema), configured in
  `NBA.Api/Extentions.cs` (`RegisterHangFire`). Schedule polling runs at 1s, which bounds draft-timer precision.
- **Resilience:** beyond rule 6's `external-api-shield`, ServiceDefaults applies Aspire's standard
  resilience handler to all HttpClients.
- **SignalR auth:** browsers can't set the Authorization header on a WebSocket, so the JWT is read from the
  `?access_token=` query string for `/draftHub` and `/chatHub` (see `OnMessageReceived` in `Program.cs`).
- **Database schemas:** Postgres hosts `nba` (app data), `chat`, and `hangfire`.

## Configuration

`appsettings.json` sections bind to Options POCOs (`ApplicationDefaults/Options`, wired in `Program.cs`):

| Section | Options class | Key settings |
| --- | --- | --- |
| `ExternalClients:BallDontLie` | `BallDontLieClientOptions` | `BaseUrl`, `ApiKey`, `Per_Page` |
| `ApplicationSettings:Draft` | `DraftOptions` | `Rounds`, `DraftPickTime` (seconds), `ShowTeamDraftBoardCount` |
| `ApplicationSettings` | `ApplicationOptions` | `CenterLimit`, `MaxPlayersPerTeam` |
| `Jwt` | `JwtOptions` | `Issuer`, `Audience`, `SigningKey`, `AccessTokenMinutes`, `RefreshTokenDays` |
| `Argon2` | `Argon2Options` | `MemoryKib`, `Iterations`, `DegreeOfParallelism` |

`ApiKey`, `Jwt:SigningKey`, and the Postgres password are blank in `appsettings.json` — supplied from
user-secrets in dev and injected by the AppHost as env vars (`ExternalClients__BallDontLie__ApiKey`,
`Jwt__SigningKey`). `Cors:AllowedOrigins` is bound explicitly (credentials are allowed, so no wildcard).

## Adding an HTTP endpoint

1. Create `NBA.Api/Endpoints/XEndpoints.cs` with a `MapXEndpoints(this IEndpointRouteBuilder)` extension
   that opens a group: `builder.MapGroup("x").WithTags("x").RequireAuthorization()` (rule 3).
2. Map routes on that group; take services as DI parameters; map results through `EntityMappings` (rule 5).
3. Register it under `v1` in `Program.cs` (`v1.MapXEndpoints();`).

Signal failures by throwing `NBAException(message, ErrorCodes.X)` (rule 7). `GlobalExceptionHandler`
serializes an `NBAException` to `{ ErrorMessage, ErrorCode }`; any other exception becomes a 500
`ProblemDetails`.

## Draft runtime flow

A pick over `DraftHub.DraftPlayer` persists the pick (`PlayerManager.AddDraftedPlayers`), advances state
(`DraftManager.ResetTimer` → `NextPick`), pushes the new `DraftState` to the league's SignalR group, then
re-arms the deadline (`ArmNextDeadlineAsync`). All deadlines live in a single Redis sorted set
`draft:timers` (member = leagueId, score = unix-ms expiry); `DraftTimerHostedService` polls it every second
and auto-picks when a league's deadline passes. Clients connect with `?leagueId=` and are added to the
SignalR group named after the leagueId; `start-draft` / `end-draft` are REST endpoints under `/v1/draft`.

## Auth & tests

- Login/refresh issue a JWT access token plus a refresh token via `AuthTokenIssuer`. Refresh tokens are
  stored in Redis keyed by their SHA-256 hash (`auth:refresh:<hash>`), never in clear text.
  `MapInboundClaims = false` keeps claim names as issued (`sub`, `unique_name`).
- `NBA.Tests` are pure unit tests (mapping, adapter, JWT, Argon2 hashing) — they need no Docker/Postgres/
  Redis, so `dotnet test` runs standalone without the Aspire stack.
