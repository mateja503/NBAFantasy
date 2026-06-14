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
dotnet user-secrets --project NBAFantasy set "Parameters:password" "<your-postgres-password>"
dotnet user-secrets --project NBAFantasy set "Parameters:balldontlie-apikey" "<your-rotated-key>"

# 2. Restore / build / test the whole solution
dotnet restore NBAFantasy.slnx
dotnet build   NBAFantasy.slnx -c Release
dotnet test    NBAFantasy.slnx -c Release

# 3. Run the app (Aspire AppHost)
dotnet run --project NBAFantasy/NBAFantasy.csproj
# then check the health endpoint (development only): GET https://localhost:<port>/alive
```

### Things to double-check
- **Package versions** in `NBA.ServiceDefaults.csproj` and `NBA.Tests.csproj` were chosen to match
  Aspire 13.1 / .NET 10. If `restore` complains, align them with the versions your other projects
  resolve.
- The login flow still compares passwords in **plaintext** (unchanged on purpose, to match seed
  data). Do not expose this publicly until the auth follow-up lands.

## Scoped follow-up (not in this batch)

1. **Authentication (JWT + hashing).** Hash passwords with `PasswordHasher<T>`, issue JWTs on login,
   add `[Authorize]` to mutating endpoints and the SignalR hubs, and read the user id from claims
   (removes the hardcoded `Commissioner = 1`). Requires a seed-data migration for existing users.
2. **Draft-timer migration.** Replace per-pick Hangfire scheduling with a Redis sorted-set delayed
   queue driven by a hosted service. Removes the 1s Postgres polling and scales to many concurrent
   drafts.
3. **Draft state durability.** Persist draft checkpoints to Postgres so a Redis eviction/restart
   mid-draft can be recovered (today the 3-day TTL key is the only copy).
4. **Startup seeding.** Move player back-fill in `ApplicationHostedService` out of per-replica
   startup into a one-shot job or guard it with a Redis distributed lock.
5. **Pagination + indexes** on the league/player list queries before real data volume.
