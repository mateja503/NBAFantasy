# Refactor notes — architecture remediation (safe batch)

This change set addresses the architecture review. It is the **low-risk, high-value batch**.
The two behaviour-changing rewrites (JWT auth, draft-timer migration) are intentionally left as
a reviewed follow-up and are scoped at the bottom of this file.

> These changes could not be compiled in the authoring environment (no .NET 10 SDK there).
> Run the verification steps below locally before committing.

## What changed

### Security / configuration
- Removed the committed BallDontLie **API key** and the **Postgres password** from
  `NBA.Api/appsettings.json` and `NBAFantasy/AppHost.cs`. They are now resolved from
  user-secrets (dev) / environment variables (prod).
- Removed the unused, password-bearing `ConnectionStrings:DefaultConnection` (the app uses the
  Aspire-injected `nbafantasydb` connection).
- CORS origins now come from `Cors:AllowedOrigins` in configuration instead of a hardcoded URL.

> **Action required:** the old API key is in git history and must be treated as compromised.
> Rotate it in the BallDontLie dashboard, then set the new values (see below).

### Observability / platform
- New **`NBA.ServiceDefaults`** project (the standard Aspire shared project): OpenTelemetry
  traces/metrics/logs, `/health` and `/alive` health checks, service discovery, and default HTTP
  resilience. Wired into `NBA.Api` via `builder.AddServiceDefaults()` and `app.MapDefaultEndpoints()`.
- `app.UseExceptionHandler()` was commented out, so the registered `GlobalExceptionHandler` never
  ran. It is now enabled.

### Background jobs (Hangfire)
- Removed the contradictory 1000-hour global job expiration; retention is now owned solely by
  `ShortenJobExpirationFilter` (set to 1 day).
- Raised `QueuePollInterval` to 5s to cut constant Postgres polling. `SchedulePollingInterval`
  stays at 1s because it bounds draft-pick timer precision (see the follow-up).
- Added a Redis **distributed lock** around the draft-cycle critical section
  (`DraftRedisOperations.TryAcquireDraftCycleLock`) so the timer firing and a simultaneous manual
  pick (or two Hangfire servers) can't both advance the draft.
- Removed the stray `using k8s.ClientSets;` (and other unused usings) from `DraftJobs.cs`.

### Separation of concerns
- New service layer: `LeagueService`, `TeamService` (`NBA.Service.League`) and `AuthService`
  (`NBA.Service.Authentication`). Business logic and validation moved out of the endpoints.
- `LeagueEndpoints`, `TeamEndpoints`, `AuthenticationEndpoints` are now thin: bind → delegate → map.
- Centralized entity→DTO mapping in `NBA.Api/Mappings/EntityMappings.cs`, replacing the blocks that
  were copy-pasted across three endpoint files.
- Services depend only inward (Api → Service → Data); they return entities, the API maps to DTOs.

### Tests / CI
- New **`NBA.Tests`** (xUnit): mapping round-trips for `Addapter` and `EntityMappings`.
- `jenkins/build` now runs a real `dotnet test` stage (previously `echo "Tests passed"`), publishes
  results, and tags images with the **immutable commit SHA** in addition to `latest`.

## Local verification

```bash
# 1. Set the secrets (run once, from the repo root)
dotnet user-secrets --project NBAFantasy set "Parameters:postgress-password" "<your-postgres-password>"
dotnet user-secrets --project NBAFantasy set "Parameters:balldontlie-apikey" "<your-rotated-key>"
dotnet user-secrets --project NBAFantasy set "Parameters:jwt-signing-key" "<random 32+ character string>"

# 2. Restore / build / test the whole solution
dotnet restore NBAFantasy.slnx
dotnet build   NBAFantasy.slnx -c Release
dotnet test    NBAFantasy.slnx -c Release

# 3. Run the app (Aspire AppHost)
dotnet run --project NBAFantasy/NBAFantasy.csproj
# then check the health endpoint (development only): GET https://localhost:<port>/alive
```

### Things to double-check
- **Package versions** in the new `.csproj` files (`NBA.ServiceDefaults`, `NBA.Tests`, the
  `Microsoft.AspNetCore.Authentication.JwtBearer` and `Microsoft.Extensions.Identity.Core`
  references) were chosen to match Aspire 13.1 / .NET 10. If `restore` complains, align them with
  the versions your other projects resolve.

## Authentication (JWT + hashing) — DONE

- Passwords are hashed with **Argon2id** (memory-hard; resists GPU/ASIC cracking) via an
  `Argon2idPasswordHasher : IPasswordHasher<Applicationuser>` backed by `Isopoh.Cryptography.Argon2`.
  Work factors come from the `Argon2` config section (OWASP minimums: 19 MiB, t=2, p=1) and are
  embedded in each PHC-encoded hash, so they can be raised later without breaking old hashes.
  Existing seed users with plaintext passwords are **migrated on first successful login** — the
  plaintext is matched once, then replaced with an Argon2id hash. No manual data migration needed.
- `POST /v1/auth/register` creates a user (hashed) and returns a token; `POST /v1/auth/login`
  verifies and returns a token. Both are anonymous; everything else now requires a bearer token.
- `LeagueService` uses the authenticated user id from the token (`sub` claim) — the hardcoded
  `Commissioner = 1` is gone, and `/league/join` ignores any user id in the request body.
- SignalR `DraftHub`/`ChatHub` are `[Authorize]`d; clients pass the token as
  `?access_token=...` (handled in `JwtBearerEvents.OnMessageReceived`).
- **Refresh tokens with rotation (DONE).** `login`/`register` also return a `refreshToken`.
  Refresh tokens are opaque 256-bit random strings, stored in Redis **by SHA-256 hash** (never in
  clear text) with a TTL of `Jwt:RefreshTokenDays`. `POST /v1/auth/refresh` exchanges one for a new
  pair and the old one is consumed atomically (GETDEL) — single-use, so a replayed/stolen token is
  rejected. `POST /v1/auth/logout` revokes the refresh token. Access tokens stay stateless and
  short-lived (`Jwt:AccessTokenMinutes`).

How to use it:

```bash
# Register (or log in) to get an access + refresh token
curl -k -X POST https://localhost:<port>/v1/auth/register \
  -H "Content-Type: application/json" \
  -d '{"username":"coachK","email":"c@k.com","password":"s3cret!"}'

# Call a protected endpoint
curl -k https://localhost:<port>/v1/league -H "Authorization: Bearer <accessToken>"

# When the access token expires, swap the refresh token for a fresh pair
curl -k -X POST https://localhost:<port>/v1/auth/refresh \
  -H "Content-Type: application/json" -d '{"refreshToken":"<refreshToken>"}'
```

Next hardening step (not done): move from the symmetric HS256 key to asymmetric RS256 if other
services need to validate tokens independently, and add a refresh-token-per-device index so a user
can revoke all sessions at once.

## Draft-timer migration — DONE

The pick timer no longer uses Hangfire. It now runs on a Redis sorted set + a polling background
service:

- **Schedule:** a single ZSET `draft:timers` holds `member = leagueId`, `score = unix-ms deadline`.
  Arming/re-arming a pick is one `ZADD` that overwrites the league's score — so "reset timer" and
  "manual pick" need no delete-then-reschedule dance (`DraftRedisOperations.ScheduleDraftTimer`).
- **Claim:** `ClaimDueDraftTimer` runs a Lua `ZRANGEBYSCORE + ZREM` so a due timer is handed to
  exactly one app instance even with multiple replicas.
- **Process:** `DraftTimerHostedService` polls once per second, draining all due timers, and runs
  `DraftTimerProcessor.AdvanceAsync` (reset clock → next pick → broadcast → arm next deadline) under
  the existing per-league Redis lock. `start-draft` calls `StartDraftAsync`; `DraftManager.EndDraft`
  cancels the timer.
- **Why:** one O(log n) Redis scan per second covers every concurrent draft, versus Hangfire
  polling Postgres for one delayed job per pick. Hangfire stays only for the recurring daily-games
  job.
- `NBA.Api/HangFire/DraftJobs.cs` is now an empty placeholder (automatic deletion was declined) —
  delete it from the project when convenient.

Verification note: the timer path needs a real Redis, so cover it with an integration test
(Testcontainers Redis) rather than a unit test — the existing unit suite can't exercise the Lua
claim script.

## Draft-state durability — DONE

Redis is still the live store for speed, but the draft is now checkpointed to Postgres so a Redis
eviction/restart mid-draft can be recovered.

- **Table:** `nba.draftsnapshot` (one row per league: `draftstate` + `draftteams` JSON, `tsupdated`),
  added to `infrastructure/db/create/create-objects-nba-schema.sql`, the `Draftsnapshot` entity, and
  configured in `NbaFantasyContext.OnModelCreatingPartial`.
- **Write-through:** `DraftSnapshotService.PersistAsync` mirrors the current Redis state + order into
  Postgres after each structural change (`CreateDraftState`, `UpdaterDraftState`, `NextPick`).
- **Read-through recovery:** `EnsureRehydratedAsync` is a no-op when Redis is healthy (one GET); on a
  miss it restores state + order from the snapshot and re-arms the pick timer if the draft was
  running. It is called before every order/state read — including at the top of
  `DraftService.DraftOrder`, which is the important one: without it a Redis flush would fall through
  and **re-randomize the draft order**. `EndDraft` deletes the snapshot.

Apply-to-existing-DBs note: the table is created by the init script on a fresh database. If you have
an existing Postgres volume, run the `CREATE TABLE nba.draftsnapshot (...)` statement once by hand.

Verification: like the timer, the recovery path needs a real Redis + Postgres, so cover it with an
integration test (flush Redis mid-draft, assert the order and pick are preserved) rather than a unit
test.

## Startup seeding — DONE

`ApplicationHostedService` ran the player back-fill on every replica's boot, so multiple instances
would race to seed Postgres and re-populate Redis. It is now guarded by a reusable Redis distributed
lock (`NbaFantasyRedis.Lock` → `LockRedisOperations`, key `startup:player-seed:lock`, 10-min expiry):

- Exactly one replica acquires the lock and runs the seed/load; the others log and skip, since
  Postgres and Redis are shared. A single instance always wins, so behaviour there is unchanged.
- The lock token is random and released by its owner only (Redis `SET NX` semantics), so an expired
  lock can't be released out from under a new holder. The existing `AnyAsync` DB check is still the
  backstop against duplicate rows.

Known trade-off: skipping replicas finish startup immediately, so they may briefly serve before the
lock-holder has finished populating shared Redis. Acceptable at this stage; the fully-correct version
makes non-holders wait on a "players ready" flag (or moves seeding to a one-shot init job / migration
step run before the API scales out).

## Scoped follow-up (remaining)

1. **Pagination + indexes** on the league/player list queries before real data volume.
