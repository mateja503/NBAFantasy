# Graph Report - NBAFantasy  (2026-08-30)

## Corpus Check
- 190 files · ~55,748 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2093 nodes · 3822 edges · 149 communities (129 shown, 20 thin omitted)
- Extraction: 92% EXTRACTED · 8% INFERRED · 0% AMBIGUOUS · INFERRED: 291 edges (avg confidence: 0.82)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `6fe35248`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- ShortenJobExpirationFilter
- DraftRedisOperations
- .ProposeAsync
- PlayerRedisOperations
- Project Rules & Vendor Licenses
- NBAException
- Player
- PlayersFilterSearch
- .League
- DraftService
- PlayerSearchInput
- TradeService
- BoxScoreStatsBuilder
- TradeBetweenTeams
- AuthTokenIssuer
- League & Stats Value Requests
- TradeHubFixture
- NbaFantasyContext
- NBA.Data.Context
- Applicationuser
- ApplicationDefaults.Exceptions
- PlayerDto
- GameDto
- .BuildHub
- Team
- League
- PlayerShort
- DraftTimerHostedService
- NBA.Data.Redis.Entities
- ExternalClients.Response
- NBA.Api.DTOs
- AppHost Launch Settings
- JwtOptions
- create-objects-nba-schema.sql
- LeagueDto
- GameShort
- ExternalClients Project Files
- BallDontLieClientWireMockTests
- PlayerInfoResponse
- BallDontLieClient
- API Launch Profiles
- Playoff
- Transaction
- .DeleteAsync
- PlayerStatsResponse
- Per-League Stats Values
- Task
- Usertrophie
- NBA.Data Package References
- Aspire Manifest OTEL Config
- Auth Request DTOs
- PlayerService
- LoginDto
- Draftsnapshot
- GameInfoResponse
- GameService
- TeamDto
- .InitializeAsync
- .ToPlayerDb
- GameRedisOperations
- Trade
- UserTeamDto
- Test Project Packages
- Argon2idPasswordHasherTests
- NbaCalendar
- ITradeOrchestrator
- EntityMappings
- NBA.Api Package References
- BallDontLieWireMockFixture
- Player
- PlayerManager
- ServiceDefaults Packages
- Entity Mapping Tests
- Aspire Postgres Connection
- Aspire Postgres Container
- .EnsureRehydratedAsync
- ServiceDefaults Extensions
- ChatHub
- Dictionary
- AppHost Packages
- TradeDto
- TeamInfoResponse
- DraftState
- .SeedLeaguePool
- ScheduledGames
- Aspire HTTPS Bindings
- ApplicationHostedService
- DraftTimerProcessor
- Draft Type Enum
- Draft Status Enum
- TradeOutcome
- NBA.Service.Player
- AppHost Hosting Packages
- ApplicationDefaults.Options
- Aspire Server Bindings
- Argon2idPasswordHasher
- .CreateLeagueWithPoolAsync
- LeagueTeamDto
- Aspire Password Parameters
- League Team Insert Request
- Draft Request DTO
- DraftLifecycleService
- Chat Schema DDL
- NbaFantasyRedis
- Aspire HTTP Bindings
- NBA.Data.Entities
- Infrastructure Init Entry
- Naming Rule
- .MapLeaguEndpoints
- IBallDontLieClient
- .Generate
- MetaData
- JwtTokenServiceTests
- LockRedisOperations
- .LoginAsync
- Userleague
- League
- Q: Tell me how individual players are stored in redis
- .MapPlayerEndpoints
- Playoffbracket
- .BucketByDay
- .AddPlayersToDb
- Transactionleague
- ScheduledGamesDto
- Team
- GetGamesResponse
- .ToPositionCodes
- IEndpointRouteBuilder
- HashSet
- IDatabase
- adding-an-endpoint/SKILL.md
- first-time-setup/SKILL.md
- IEnumerable
- DraftBoardTeams
- Queue
- Task
- JsonOptions
- TeamData
- DateTime
- IReadOnlyList
- Fact

## God Nodes (most connected - your core abstractions)
1. `NbaFantasyContext` - 102 edges
2. `TradeBetweenTeams` - 49 edges
3. `Trade` - 41 edges
4. `PlayerSearchInput` - 40 edges
5. `PlayersFilterSearch` - 38 edges
6. `NBAException` - 37 edges
7. `NBA.Data.Entities` - 36 edges
8. `NBA.Data.Redis.Entities` - 34 edges
9. `NBA.Data.Context` - 32 edges
10. `DraftState` - 32 edges

## Surprising Connections (you probably didn't know these)
- `AuthTokenIssuer` --references--> `NbaFantasyContext`  [EXTRACTED]
  NBA.Api/Authentication/AuthTokenIssuer.cs → NBA.Data/Context/NbaFantasyContext.cs
- `AuthService` --references--> `NbaFantasyContext`  [EXTRACTED]
  NBA.Service/Authentication/AuthService.cs → NBA.Data/Context/NbaFantasyContext.cs
- `BoxScoreCalculationService` --references--> `NbaFantasyContext`  [EXTRACTED]
  NBA.Service/CalculateBoxScore/BoxScoreCalculationService.cs → NBA.Data/Context/NbaFantasyContext.cs
- `DraftLifecycleService` --references--> `NbaFantasyContext`  [EXTRACTED]
  NBA.Service/Draft/DraftLifecycleService.cs → NBA.Data/Context/NbaFantasyContext.cs
- `DraftService` --references--> `NbaFantasyContext`  [EXTRACTED]
  NBA.Service/Draft/DraftService.cs → NBA.Data/Context/NbaFantasyContext.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Draft Pick Processing Flow** — claude_drafthub, claude_playermanager, claude_draftmanager, claude_draftstate [EXTRACTED 0.95]
- **Draft Timer & Deadline Coordination** — claude_drafttimerhostedservice, claude_draftredisoperations, claude_draftmanager, claude_draft_realtime [EXTRACTED 0.95]
- **External HTTP Resilience Strategy** — claude_resilience_pipeline_rule, claude_externalclients, claude_nba_servicedefaults [INFERRED 0.85]

## Communities (149 total, 20 thin omitted)

### Community 0 - "ShortenJobExpirationFilter"
Cohesion: 0.15
Nodes (11): ApplyStateContext, NBA.Api.HangFire, NBA.Api, IApplyStateFilter, IConfiguration, IWriteOnlyTransaction, JobFilterAttribute, HttpResponseMessage (+3 more)

### Community 1 - "DraftRedisOperations"
Cohesion: 0.06
Nodes (31): List, DraftBoardTeams, CurrentRound, DraftOrder, onTheClockTeam, TeamDraftBoard, Pick, TeamId (+23 more)

### Community 2 - ".ProposeAsync"
Cohesion: 0.15
Nodes (13): Accepted, IServiceCollection, TradeExtention, Guid, IOptions, List, Task, TradeManager (+5 more)

### Community 3 - "PlayerRedisOperations"
Cohesion: 0.11
Nodes (15): HashSet, IDatabase, IEnumerable, JsonSerializerOptions, List, Player, Task, PlayerRedisOperations (+7 more)

### Community 4 - "Project Rules & Vendor Licenses"
Cohesion: 0.05
Nodes (51): Adapter (static mapper), Adding an HTTP Endpoint Flow, ApplicationDefaults, ApplicationOptions, Argon2Options, Aspire AppHost, Auth & Tests, Authenticate Everything Rule (+43 more)

### Community 5 - "NBAException"
Cohesion: 0.09
Nodes (32): Action, NBAException, ErrorCode, AuthenticateResult, ClaimsPrincipal, Exception, HttpMessageHandler, HttpRequestMessage (+24 more)

### Community 6 - "Player"
Cohesion: 0.05
Nodes (43): DateTime, ICollection, Player, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow (+35 more)

### Community 7 - "PlayersFilterSearch"
Cohesion: 0.05
Nodes (38): NBA.Api.Requests.Player, DateTime, PlayersFilterSearch, allowdrop, gameready, irlteamid, irlteamname, islock (+30 more)

### Community 8 - ".League"
Cohesion: 0.26
Nodes (6): IOptions, Task, DraftHub, IOptions, Task, DraftManager

### Community 9 - "DraftService"
Cohesion: 0.14
Nodes (12): ApplicationOptions, DraftManager, JsonOptions, DraftSnapshotService, IServiceCollection, DraftExtention, DraftOptions, DraftSnapshotService (+4 more)

### Community 10 - "PlayerSearchInput"
Cohesion: 0.06
Nodes (36): PlayerSearchInput, Allowdrop, Gameready, Irlteamid, Irlteamname, Islock, LeagueId, MaxAssists (+28 more)

### Community 11 - "TradeService"
Cohesion: 0.15
Nodes (13): Created, IQueryable, IEndpointRouteBuilder, TradeEndpoints, Trade, DateTime, Guid, ILogger (+5 more)

### Community 12 - "BoxScoreStatsBuilder"
Cohesion: 0.07
Nodes (17): BoxScoreStatsBuilder, PlayerStats, ast, blk, fg3a, fg3m, fga, fgm (+9 more)

### Community 13 - "TradeBetweenTeams"
Cohesion: 0.06
Nodes (40): IHubCallerClients, Method, List, Task, ITradeHubClient, DateTimeOffset, Guid, List (+32 more)

### Community 14 - "AuthTokenIssuer"
Cohesion: 0.29
Nodes (7): DateTime, IOptions, Task, AuthTokenIssuer, TokenPair, IEndpointRouteBuilder, AuthenticationEndpoints

### Community 15 - "League & Stats Value Requests"
Cohesion: 0.07
Nodes (27): NBA.Api.Requests.League, NBA.Api.Requests.StatValue, LeagueRequest, Autostart, DraftStyle, LeagueName, LeagueType, ScoringSystem (+19 more)

### Community 16 - "TradeHubFixture"
Cohesion: 0.06
Nodes (50): NBA.Api.SignalR, HubConnection, HubException, HubInvocationContext, ICollectionFixture, IConnectionMultiplexer, IHost, IHubFilter (+42 more)

### Community 17 - "NbaFantasyContext"
Cohesion: 0.07
Nodes (38): DbContext, DbSet, ModelBuilder, Applicationuser, Draftsnapshot, League, Leagueplayer, Player (+30 more)

### Community 18 - "NBA.Data.Context"
Cohesion: 0.14
Nodes (9): NBA.Api.Draft, NBA.Api.SignalR.Clients, NBA.Data.Context, NBA.Data.Redis.Enumerations, NBA.Service.Draft, NBA.Api.SignalR.Hubs, NBA.Data.Redis, JsonSerializerOptions (+1 more)

### Community 19 - "Applicationuser"
Cohesion: 0.17
Nodes (11): ICollection, Applicationuser, Email, Managerlevel, Password, Teams, Userid, Userleagues (+3 more)

### Community 20 - "ApplicationDefaults.Exceptions"
Cohesion: 0.13
Nodes (9): ErrorCodes, ApplicationDefaults.LogDefaults, ApplicationDefaults.Exceptions, NBA.Api.Requests.Team, NBA.Api.Authentication, NBA.Service.Team, NBA.Api.Endpoints, TeamRequest (+1 more)

### Community 21 - "PlayerDto"
Cohesion: 0.08
Nodes (24): DateTime, PlayerDto, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow, Gameready (+16 more)

### Community 22 - "GameDto"
Cohesion: 0.12
Nodes (17): DateTime, GameDto, Date, GameId, HomeTeam, Postponed, Postseason, StartTime (+9 more)

### Community 23 - ".BuildHub"
Cohesion: 0.33
Nodes (8): Clients, OfferedToLeague, Superseded, Fact, Hub, Task, TradeHubPublishTests, LeagueGroup

### Community 24 - "Team"
Cohesion: 0.09
Nodes (24): IEndpointRouteBuilder, TeamEndpoints, ICollection, Team, Approved, Categoryleaguepoints, Islock, Lastweekpoints (+16 more)

### Community 25 - "League"
Cohesion: 0.09
Nodes (22): ICollection, League, Autostart, Commissioner, Draftcompleted, Draftsnapshot, Draftstyle, Leagueid (+14 more)

### Community 26 - "PlayerShort"
Cohesion: 0.12
Nodes (19): IEnumerable, List, PlayerShortDto, FullName, PlayerId, Position, PlayerShortMappings, PlayerShort (+11 more)

### Community 27 - "DraftTimerHostedService"
Cohesion: 0.31
Nodes (7): BackgroundService, CancellationToken, ILogger, IServiceProvider, Task, TimeSpan, DraftTimerHostedService

### Community 28 - "NBA.Data.Redis.Entities"
Cohesion: 0.14
Nodes (9): NBA.Data.Redis.Operations, NBA.Data.Redis.Scopes, NBA.Tests.Fakes, NBA.Data.Redis.Keys, NBA.Data.Redis.Entities, NBA.Service.Trade, NBA.Data.Redis.Dtos, NBA.Data.Constants (+1 more)

### Community 29 - "ExternalClients.Response"
Cohesion: 0.19
Nodes (6): ApplicationDefaults.Time, ExternalClients.Response, NBA.Api.HostedService, ExternalClients, NBA.Service.Game, ExternalClients.Poco

### Community 30 - "NBA.Api.DTOs"
Cohesion: 0.18
Nodes (5): NBA.Api.DTOs, List, DraftOrderDto, Round, Teams

### Community 31 - "AppHost Launch Settings"
Cohesion: 0.13
Nodes (18): ASPIRE_DASHBOARD_OTLP_ENDPOINT_URL, ASPIRE_RESOURCE_SERVICE_ENDPOINT_URL, ASPNETCORE_ENVIRONMENT, DOTNET_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables (+10 more)

### Community 32 - "JwtOptions"
Cohesion: 0.17
Nodes (12): JwtOptions, AccessTokenMinutes, Audience, Issuer, RefreshTokenDays, SigningKey, InvalidOperationException, DateTime (+4 more)

### Community 33 - "create-objects-nba-schema.sql"
Cohesion: 0.22
Nodes (18): nba.applicationuser, nba.draftsnapshot, nba.league, nba.leagueplayer, nba.player, nba.playermemento, nba.playoff, nba.playoffbracket (+10 more)

### Community 34 - "LeagueDto"
Cohesion: 0.15
Nodes (13): LeagueDto, Autostart, Commissioner, CommissionersTeam, Draftstyle, Leagueid, Name, Seasonyear (+5 more)

### Community 35 - "GameShort"
Cohesion: 0.12
Nodes (17): DateTime, GameShort, Date, GameId, HomeTeam, Postponed, Postseason, StartTime (+9 more)

### Community 36 - "ExternalClients Project Files"
Cohesion: 0.13
Nodes (16): ApplicationDefaults, net10.0, Microsoft.NET.Sdk, BoxScoreBuilder, net10.0, Microsoft.NET.Sdk, ExternalClients, net10.0 (+8 more)

### Community 37 - "BallDontLieClientWireMockTests"
Cohesion: 0.16
Nodes (15): IClassFixture, IRequestMessage, IResponseBuilder, Fact, HttpStatusCode, InlineData, JsonException, OperationCanceledException (+7 more)

### Community 38 - "PlayerInfoResponse"
Cohesion: 0.14
Nodes (14): PlayerInfoResponse, college, country, draft_number, draft_round, draft_year, first_name, height (+6 more)

### Community 39 - "BallDontLieClient"
Cohesion: 0.23
Nodes (9): CancellationToken, DateOnly, HttpResponseMessage, List, Task, BallDontLieClient, HttpClient, ResiliencePipeline (+1 more)

### Community 40 - "API Launch Profiles"
Cohesion: 0.13
Nodes (15): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, applicationUrl, commandName (+7 more)

### Community 41 - "Playoff"
Cohesion: 0.25
Nodes (7): ICollection, Playoff, League, Leagueid, Playoffbrackets, Playoffid, Totalrounds

### Community 42 - "Transaction"
Cohesion: 0.22
Nodes (8): DateTime, ICollection, Transaction, Transactionid, Transactionleagues, Transactionstatus, Tscreated, Typetransaction

### Community 43 - ".DeleteAsync"
Cohesion: 0.19
Nodes (5): Leagueplayer, Statsvalue, List, Task, FreeAgencyService

### Community 44 - "PlayerStatsResponse"
Cohesion: 0.13
Nodes (14): PlayerStatsResponse, ast, blk, fg3a, fg3m, fga, fgm, fta (+6 more)

### Community 45 - "Per-League Stats Values"
Cohesion: 0.13
Nodes (14): Statsvalue, Assistsvalue, Blocksvalue, Fieldgoalvaluemade, Fieldgoalvaluemissed, Freethrowvaluemade, Freethrowvaluemissed, League (+6 more)

### Community 46 - "Task"
Cohesion: 0.11
Nodes (11): Applicationuser, CancellationToken, Draftsnapshot, League, List, Player, Task, Team (+3 more)

### Community 47 - "Usertrophie"
Cohesion: 0.14
Nodes (12): ICollection, Trophie, Trophieid, Typetrophie, Usertrophies, Xp, Usertrophie, Trophie (+4 more)

### Community 48 - "NBA.Data Package References"
Cohesion: 0.14
Nodes (13): net10.0, Aspire.Hosting.Redis (13.1.2), Microsoft.Extensions.Configuration.Abstractions (10.0.0), Microsoft.NET.Sdk, MessagePack (2.5.302), Microsoft.EntityFrameworkCore (10.0.0), Microsoft.EntityFrameworkCore.Design (10.0.0), Microsoft.Extensions.Configuration (10.0.0) (+5 more)

### Community 49 - "Aspire Manifest OTEL Config"
Cohesion: 0.14
Nodes (14): ASPNETCORE_FORWARDEDHEADERS_ENABLED, ConnectionStrings__nbafantasydb, HTTP_PORTS, NBAFANTASYDB_DATABASENAME, NBAFANTASYDB_HOST, NBAFANTASYDB_JDBCCONNECTIONSTRING, NBAFANTASYDB_PASSWORD, NBAFANTASYDB_PORT (+6 more)

### Community 50 - "Auth Request DTOs"
Cohesion: 0.15
Nodes (10): NBA.Api.Requests.Authentication, LoginRequestNBA, Password, Username, RefreshRequest, RefreshToken, SignUpRequest, Email (+2 more)

### Community 51 - "PlayerService"
Cohesion: 0.18
Nodes (12): AutomaticRetry, BoxScoreCalculationService, DateTime, IBallDontLieClient, JobDisplayName, List, NbaFantasyRedis, PagedResult (+4 more)

### Community 52 - "LoginDto"
Cohesion: 0.22
Nodes (8): List, LoginDto, Leagues, RefreshToken, Teams, Token, Userid, Username

### Community 53 - "Draftsnapshot"
Cohesion: 0.29
Nodes (6): DateTime, Draftsnapshot, Draftstate, Draftteams, Leagueid, Tsupdated

### Community 54 - "GameInfoResponse"
Cohesion: 0.15
Nodes (13): DateTime, GameInfoResponse, date, datetime, home_team, home_team_score, id, postponed (+5 more)

### Community 55 - "GameService"
Cohesion: 0.23
Nodes (9): IBackgroundJobClient, IEndpointRouteBuilder, GameEndpoints, CancellationToken, DateOnly, IOptions, List, Task (+1 more)

### Community 56 - "TeamDto"
Cohesion: 0.22
Nodes (9): TeamDto, Categoryleaguepoints, Competesinleague, Islock, Lastweekpoints, Name, Seed, Teamid (+1 more)

### Community 57 - ".InitializeAsync"
Cohesion: 0.20
Nodes (9): ApplicationOptions, CenterLimit, MaxPlayersPerTeam, ProposedTradeTtlMinutes, AuthenticationHandler, AuthenticationSchemeOptions, IOptions, RosterValidator (+1 more)

### Community 58 - ".ToPlayerDb"
Cohesion: 0.19
Nodes (7): List, PlayerData, Adapter, Fact, InlineData, Theory, AdapterTests

### Community 59 - "GameRedisOperations"
Cohesion: 0.32
Nodes (5): IDatabase, JsonSerializerOptions, Task, TimeSpan, GameRedisOperations

### Community 60 - "Trade"
Cohesion: 0.12
Nodes (15): DateTime, Guid, List, Trade, Fromteam, Fromteamid, League, Leagueid (+7 more)

### Community 61 - "UserTeamDto"
Cohesion: 0.17
Nodes (12): List, UserTeamDto, Categoryleaguepoints, Islock, Lastweekpoints, Leagueid, Leaguename, Name (+4 more)

### Community 62 - "Test Project Packages"
Cohesion: 0.17
Nodes (12): NBA.Tests, net10.0, Microsoft.NET.Sdk, coverlet.collector (6.0.2), Microsoft.AspNetCore.SignalR.Client (10.0.0), Microsoft.AspNetCore.TestHost (10.0.0), Microsoft.EntityFrameworkCore.InMemory (10.0.0), Microsoft.NET.Test.Sdk (17.12.0) (+4 more)

### Community 64 - "NbaCalendar"
Cohesion: 0.23
Nodes (5): NbaCalendar, DateOnly, InlineData, Theory, TimeZoneInfo

### Community 65 - "ITradeOrchestrator"
Cohesion: 0.43
Nodes (4): Guid, List, Task, ITradeOrchestrator

### Community 66 - "EntityMappings"
Cohesion: 0.28
Nodes (3): List, Team, EntityMappings

### Community 67 - "NBA.Api Package References"
Cohesion: 0.18
Nodes (10): net10.0, Aspire.StackExchange.Redis (13.1.2), Microsoft.Extensions.Http.Resilience (10.1.0), Aspire.Npgsql.EntityFrameworkCore.PostgreSQL (13.1.0), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.0), Microsoft.AspNetCore.OpenApi (10.0.0), Microsoft.AspNetCore.SignalR.StackExchangeRedis (10.0.5), Microsoft.OpenApi (2.7.5) (+2 more)

### Community 68 - "BallDontLieWireMockFixture"
Cohesion: 0.18
Nodes (9): IAsyncLifetime, HttpResponseMessage, IOptions, Task, BallDontLieWireMockFixture, Client, Server, ServiceProvider (+1 more)

### Community 69 - "Player"
Cohesion: 0.10
Nodes (9): NBA.Service.FreeAgency, NBA.Service.CalculateBoxScore, NBA.Service.Builder, Player, BoxScoreCalculationBuilder, Dictionary, List, Task (+1 more)

### Community 70 - "PlayerManager"
Cohesion: 0.31
Nodes (6): IOptions, JsonOptions, JsonSerializerOptions, List, Task, PlayerManager

### Community 71 - "ServiceDefaults Packages"
Cohesion: 0.20
Nodes (10): NBA.ServiceDefaults, net10.0, Microsoft.Extensions.Http.Resilience (10.1.0), Microsoft.NET.Sdk, Microsoft.Extensions.ServiceDiscovery (10.7.0), OpenTelemetry.Exporter.OpenTelemetryProtocol (1.16.0), OpenTelemetry.Extensions.Hosting (1.16.0), OpenTelemetry.Instrumentation.AspNetCore (1.15.2) (+2 more)

### Community 72 - "Entity Mapping Tests"
Cohesion: 0.29
Nodes (4): Fact, InlineData, Theory, EntityMappingsTests

### Community 73 - "Aspire Postgres Connection"
Cohesion: 0.20
Nodes (9): connectionString, type, filter, type, value, resources, nbafantasydb, password-uri-encoded (+1 more)

### Community 74 - "Aspire Postgres Container"
Cohesion: 0.20
Nodes (10): POSTGRES_HOST_AUTH_METHOD, POSTGRES_INITDB_ARGS, POSTGRES_PASSWORD, POSTGRES_USER, bindMounts, connectionString, env, image (+2 more)

### Community 75 - ".EnsureRehydratedAsync"
Cohesion: 0.15
Nodes (10): DraftOptions, DraftPickTime, Rounds, ShowTeamDraftBoardCount, Dictionary, IOptions, JsonSerializerOptions, Queue (+2 more)

### Community 76 - "ServiceDefaults Extensions"
Cohesion: 0.22
Nodes (3): Microsoft.Extensions.Hosting, Extensions, WebApplication

### Community 77 - "ChatHub"
Cohesion: 0.25
Nodes (5): Hub, Task, IChatHubClient, Task, ChatHub

### Community 79 - "AppHost Packages"
Cohesion: 0.22
Nodes (8): net10.0, Aspire.Hosting.Redis (13.1.2), Aspire.StackExchange.Redis (13.1.2), Microsoft.NET.Sdk, Aspire.Hosting.AppHost (13.1.0), Aspire.Hosting.PostgreSQL (13.1.0), CommunityToolkit.Aspire.Hosting.NodeJS.Extensions (9.9.0), OpenTelemetry.Api (1.16.0)

### Community 80 - "TradeDto"
Cohesion: 0.12
Nodes (18): DateTime, Guid, List, TradeDto, Fromteamid, Leagueid, Playerids, Status (+10 more)

### Community 81 - "TeamInfoResponse"
Cohesion: 0.17
Nodes (12): List, GetAllTeamsResponse, data, meta, TeamInfoResponse, abbreviation, city, conference (+4 more)

### Community 82 - "DraftState"
Cohesion: 0.15
Nodes (12): Task, IDraftHubClient, DateTime, Dictionary, List, DraftState, DraftBoardTeams, DraftedPlayersPerTeam (+4 more)

### Community 83 - ".SeedLeaguePool"
Cohesion: 0.20
Nodes (6): LeaguePlayerData, IServiceCollection, LeaguePlayerExtention, List, Task, LeaguePlayerService

### Community 84 - "ScheduledGames"
Cohesion: 0.24
Nodes (9): List, ScheduledGames, RestOfWeek, Today, Tomorrow, DateOnly, Task, TimeSpan (+1 more)

### Community 85 - "Aspire HTTPS Bindings"
Cohesion: 0.25
Nodes (8): https, protocol, scheme, transport, bindings, path, type, nba-api

### Community 86 - "ApplicationHostedService"
Cohesion: 0.06
Nodes (30): ErrorResponse, ErrorCode, ErrorMessage, Log, message, request, response, BallDontLieClientOptions (+22 more)

### Community 87 - "DraftTimerProcessor"
Cohesion: 0.24
Nodes (6): IHubContext, IOptions, Task, DraftTimerProcessor, IEndpointRouteBuilder, DraftEndpoints

### Community 88 - "Draft Type Enum"
Cohesion: 0.29
Nodes (6): DraftType, Auction, Linear, Offline, RRR, Snake

### Community 89 - "Draft Status Enum"
Cohesion: 0.29
Nodes (6): DraftStatus, DraftCompleted, DraftEnded, DraftStarted, Initial, Paused

### Community 90 - "TradeOutcome"
Cohesion: 0.27
Nodes (8): IReadOnlyList, Rejected, TradeEvent, TradeOutcome, Guid, List, TradeData, FakeTradeOrchestrator

### Community 91 - "NBA.Service.Player"
Cohesion: 0.33
Nodes (3): NBA.Service.League, NBA.Service.Player, NBA.Service.LeaguePlayer

### Community 92 - "AppHost Hosting Packages"
Cohesion: 0.29
Nodes (7): NBA.Service, net10.0, Aspire.Hosting.Redis (13.1.2), Microsoft.Extensions.Options (10.0.3), Microsoft.NET.Sdk, Isopoh.Cryptography.Argon2 (1.1.10), Microsoft.Extensions.Identity.Core (10.0.0)

### Community 93 - "ApplicationDefaults.Options"
Cohesion: 0.31
Nodes (3): NBA.Tests.Integration, NBA.Service.Roster, ApplicationDefaults.Options

### Community 94 - "Aspire Server Bindings"
Cohesion: 0.29
Nodes (7): tcp, bindings, port, protocol, scheme, targetPort, transport

### Community 95 - "Argon2idPasswordHasher"
Cohesion: 0.18
Nodes (8): Argon2Options, DegreeOfParallelism, Iterations, MemoryKib, IPasswordHasher, IOptions, Argon2idPasswordHasher, PasswordVerificationResult

### Community 96 - ".CreateLeagueWithPoolAsync"
Cohesion: 0.32
Nodes (6): Fact, League, Task, LeaguePlayerSeedTests, NBAException, TradeHubFixture

### Community 97 - "LeagueTeamDto"
Cohesion: 0.33
Nodes (5): LeagueTeamDto, Approved, Leagueid, Leagueteamid, Teamid

### Community 98 - "Aspire Password Parameters"
Cohesion: 0.33
Nodes (6): value, inputs, type, value, password, type

### Community 99 - "League Team Insert Request"
Cohesion: 0.40
Nodes (4): NBA.Api.Requests.LeagueTeam, LeagueTeamInsertRequest, LeagueId, TeamName

### Community 100 - "Draft Request DTO"
Cohesion: 0.40
Nodes (4): NBA.Api.Requests.Draft, DraftRequest, LeagueId, StartDraft

### Community 101 - "DraftLifecycleService"
Cohesion: 0.13
Nodes (16): DraftBoardTeams, IEndpointRouteBuilder, TestingEndpoints, Dictionary, DraftOptions, DraftSnapshotService, IOptions, List (+8 more)

### Community 102 - "Chat Schema DDL"
Cohesion: 0.70
Nodes (4): chat.conversationparticipants, chat.messages, chat.rooms, nba.applicationuser

### Community 103 - "NbaFantasyRedis"
Cohesion: 0.14
Nodes (14): Lazy, IDatabase, NbaFantasyRedis, Auth, Draft, Game, Lock, Player (+6 more)

### Community 104 - "Aspire HTTP Bindings"
Cohesion: 0.50
Nodes (4): http, protocol, scheme, transport

### Community 105 - "NBA.Data.Entities"
Cohesion: 0.13
Nodes (8): NBA.Data.Entities, NBA.Data.Enumerations, NBA.Service.Authentication, NBA.Api.Mappings, NBA.Tests, NBA.Service, PlayerPositionExtensions, BoxScoreEvaluation

### Community 116 - ".MapLeaguEndpoints"
Cohesion: 0.22
Nodes (11): IEndpointRouteBuilder, LeagueEndpoints, League, PagedResult, Task, CreateLeagueInput, JoinLeagueInput, JoinLeagueResult (+3 more)

### Community 117 - "IBallDontLieClient"
Cohesion: 0.39
Nodes (5): CancellationToken, DateOnly, List, Task, IBallDontLieClient

### Community 118 - ".Generate"
Cohesion: 0.38
Nodes (3): RefreshTokenGenerator, Fact, RefreshTokenGeneratorTests

### Community 119 - "MetaData"
Cohesion: 0.22
Nodes (8): MetaData, Next_cursor, Per_page, Prev_Cursor, List, GetAllPlayersResponse, data, meta

### Community 120 - "JwtTokenServiceTests"
Cohesion: 0.53
Nodes (3): Fact, Task, JwtTokenServiceTests

### Community 121 - "LockRedisOperations"
Cohesion: 0.33
Nodes (4): IDatabase, Task, TimeSpan, LockRedisOperations

### Community 122 - ".LoginAsync"
Cohesion: 0.33
Nodes (5): IPasswordHasher, List, Task, AuthService, LoginResult

### Community 123 - "Userleague"
Cohesion: 0.29
Nodes (6): Userleague, League, Leagueid, User, Userid, Userleagueid

### Community 124 - "League"
Cohesion: 0.25
Nodes (7): League, Leagueplayer, Isfreeagent, League, Leagueid, Leagueplayerid, Playerid

### Community 125 - "Q: Tell me how individual players are stored in redis"
Cohesion: 0.40
Nodes (4): Answer, Outcome, Q: Tell me how individual players are stored in redis, Source Nodes

### Community 126 - ".MapPlayerEndpoints"
Cohesion: 0.25
Nodes (5): IEndpointRouteBuilder, PlayerEndpoints, IReadOnlyList, PagedResult, TotalPages

### Community 127 - "Playoffbracket"
Cohesion: 0.25
Nodes (7): Playoffbracket, Playoff, Playoffbracketid, Playoffid, Playoffround, Team1, Team2

### Community 129 - ".AddPlayersToDb"
Cohesion: 0.29
Nodes (5): GetAllPlayersResponse, MetaData, List, PlayerFilter, CancellationToken

### Community 130 - "Transactionleague"
Cohesion: 0.29
Nodes (6): Transactionleague, League, Leagueid, Transaction, Transactionid, Transactionleagueid

### Community 131 - "ScheduledGamesDto"
Cohesion: 0.33
Nodes (5): List, ScheduledGamesDto, RestOfWeek, Today, Tomorrow

### Community 132 - "Team"
Cohesion: 0.40
Nodes (5): Team, abbreviation, city, full_name, id

### Community 133 - "GetGamesResponse"
Cohesion: 0.50
Nodes (4): List, GetGamesResponse, data, meta

## Knowledge Gaps
- **705 isolated node(s):** `ErrorCodes`, `Applicationusers`, `Draftsnapshots`, `Leagues`, `Leagueplayers` (+700 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **20 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `NbaFantasyContext` connect `NbaFantasyContext` to `DraftService`, `TradeService`, `AuthTokenIssuer`, `TradeHubFixture`, `Team`, `PlayerShort`, `.DeleteAsync`, `Task`, `PlayerService`, `.InitializeAsync`, `Player`, `PlayerManager`, `.EnsureRehydratedAsync`, `.SeedLeaguePool`, `ApplicationHostedService`, `.CreateLeagueWithPoolAsync`, `DraftLifecycleService`, `.MapLeaguEndpoints`, `.LoginAsync`?**
  _High betweenness centrality (0.105) - this node is a cross-community bridge._
- **Why does `NbaFantasyRedis` connect `NbaFantasyRedis` to `DraftRedisOperations`, `.ProposeAsync`, `PlayerRedisOperations`, `PlayerManager`, `.League`, `GameRedisOperations`, `.EnsureRehydratedAsync`, `TradeBetweenTeams`, `AuthTokenIssuer`, `TradeHubFixture`, `ScheduledGames`, `ApplicationHostedService`, `DraftTimerProcessor`, `LockRedisOperations`, `DraftTimerHostedService`, `NBA.Data.Redis.Entities`, `.InitializeAsync`?**
  _High betweenness centrality (0.094) - this node is a cross-community bridge._
- **Why does `NBAException` connect `NBAException` to `.ProposeAsync`, `BallDontLieClientWireMockTests`, `BallDontLieClient`, `.League`, `.EnsureRehydratedAsync`, `TradeService`, `AuthTokenIssuer`, `DraftTimerProcessor`, `Team`, `.InitializeAsync`, `.LoginAsync`?**
  _High betweenness centrality (0.071) - this node is a cross-community bridge._
- **What connects `ErrorCodes`, `Applicationusers`, `Draftsnapshots` to the rest of the system?**
  _705 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `ShortenJobExpirationFilter` be split into smaller, more focused modules?**
  _Cohesion score 0.14705882352941177 - nodes in this community are weakly interconnected._
- **Should `DraftRedisOperations` be split into smaller, more focused modules?**
  _Cohesion score 0.057971014492753624 - nodes in this community are weakly interconnected._
- **Should `PlayerRedisOperations` be split into smaller, more focused modules?**
  _Cohesion score 0.11463414634146342 - nodes in this community are weakly interconnected._