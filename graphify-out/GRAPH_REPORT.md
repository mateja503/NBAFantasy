# Graph Report - NBAFantasy  (2026-08-31)

## Corpus Check
- 196 files · ~57,437 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2094 nodes · 3916 edges · 140 communities (124 shown, 16 thin omitted)
- Extraction: 92% EXTRACTED · 8% INFERRED · 0% AMBIGUOUS · INFERRED: 300 edges (avg confidence: 0.82)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `92f89759`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- .GetAllPlayers
- DraftRedisOperations
- NBAException
- PlayerShort
- Project Rules & Vendor Licenses
- .CreateClient
- Player
- PlayersFilterSearch
- .League
- DraftSnapshotService
- PlayerSearchInput
- Playermemento
- BoxScoreStatsBuilder
- RecordingTradeHubClients
- LeagueDto
- League & Stats Value Requests
- TradeHubFixture
- NbaFantasyContext
- NBA.Data.Context
- Applicationuser
- Team
- PlayerDto
- GameDto
- TradeRedisOperations
- Player
- League
- DraftEndDraftTests
- DraftTimerHostedService
- NBA.Data.Redis.Entities
- NBA.Service.Player
- GameService
- AppHost Launch Settings
- ApplicationHostedService.cs
- create-objects-nba-schema.sql
- MetaData
- GameShort
- ExternalClients Project Files
- BallDontLieClientWireMockTests
- PlayerInfoResponse
- IBallDontLieClient
- API Launch Profiles
- Playoff
- Transaction
- AuthTokenIssuer
- PlayerStatsResponse
- Per-League Stats Values
- NbaCalendar
- .OnModelCreating
- NBA.Data Package References
- Aspire Manifest OTEL Config
- NBA.Api.Requests.Authentication
- .GetPlayersGameStats
- NBA.Api.DTOs
- BallDontLieClient
- GameInfoResponse
- Trade
- TeamDto
- FreeAgencyEndpoints.cs
- .ToPlayerDb
- JwtTokenService
- NBA.Data.Entities
- UserTeamDto
- Test Project Packages
- TradeBetweenTeams
- ScheduledGames
- ExternalClients
- EntityMappings
- NBA.Api Package References
- Leagueplayer
- LoginDto
- PlayerManager
- ServiceDefaults Packages
- Entity Mapping Tests
- Aspire Postgres Connection
- Aspire Postgres Container
- DraftService
- ServiceDefaults Extensions
- GameRedisOperations
- NBA.Data.Enumerations
- AppHost Packages
- PlayerService
- TeamInfoResponse
- DraftState
- .BucketByDay
- .ToggleFreeAgencyStatus
- Aspire HTTPS Bindings
- ApplicationHostedService
- NBA.Api.SignalR.Clients
- NBA.Api.Authentication
- Draft Status Enum
- DraftBoardTeams
- ShortenJobExpirationFilter
- AppHost Hosting Packages
- LeagueTrades
- Aspire Server Bindings
- Draftsnapshot
- Fact
- LeagueTeamDto
- Aspire Password Parameters
- League Team Insert Request
- Draft Request DTO
- DraftLifecycleService
- Chat Schema DDL
- NbaFantasyRedis
- Aspire HTTP Bindings
- LeaguePlayerSeedTests
- Infrastructure Init Entry
- Naming Rule
- Task
- .RegisterPlayer
- .RegisterHangFire
- .Generate
- Argon2idPasswordHasher
- Userleague
- Recorder
- JwtOptions
- .PerformCalculations
- Q: Tell me how individual players are stored in redis
- LockRedisOperations
- LeagueScope
- .CreateToken
- TeamData
- IServiceCollection
- IOptions
- JsonOptions
- JsonSerializerOptions
- CancellationToken
- DateTime
- IReadOnlyList
- adding-an-endpoint/SKILL.md
- first-time-setup/SKILL.md

## God Nodes (most connected - your core abstractions)
1. `NbaFantasyContext` - 97 edges
2. `TradeBetweenTeams` - 49 edges
3. `NBAException` - 42 edges
4. `Trade` - 41 edges
5. `PlayerSearchInput` - 40 edges
6. `NBA.Data.Entities` - 40 edges
7. `PlayersFilterSearch` - 38 edges
8. `NBA.Data.Redis.Entities` - 36 edges
9. `Applicationuser` - 35 edges
10. `Team` - 34 edges

## Surprising Connections (you probably didn't know these)
- `DraftTimerProcessor` --references--> `DraftLifecycleService`  [EXTRACTED]
  NBA.Api/Draft/DraftTimerProcessor.cs → NBA.Service/Draft/DraftLifecycleService.cs
- `DraftHub` --references--> `DraftLifecycleService`  [EXTRACTED]
  NBA.Api/SignalR/Hubs/DraftHub.cs → NBA.Service/Draft/DraftLifecycleService.cs
- `DraftLifecycleService` --references--> `DraftOptions`  [EXTRACTED]
  NBA.Service/Draft/DraftLifecycleService.cs → ApplicationDefaults/Options/DraftOptions.cs
- `DraftLifecycleService` --references--> `NbaFantasyContext`  [EXTRACTED]
  NBA.Service/Draft/DraftLifecycleService.cs → NBA.Data/Context/NbaFantasyContext.cs
- `DraftLifecycleService` --references--> `NbaFantasyRedis`  [EXTRACTED]
  NBA.Service/Draft/DraftLifecycleService.cs → NBA.Data/Context/NbaFantasyRedis.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Draft Pick Processing Flow** — claude_drafthub, claude_playermanager, claude_draftmanager, claude_draftstate [EXTRACTED 0.95]
- **Draft Timer & Deadline Coordination** — claude_drafttimerhostedservice, claude_draftredisoperations, claude_draftmanager, claude_draft_realtime [EXTRACTED 0.95]
- **External HTTP Resilience Strategy** — claude_resilience_pipeline_rule, claude_externalclients, claude_nba_servicedefaults [INFERRED 0.85]

## Communities (140 total, 16 thin omitted)

### Community 0 - ".GetAllPlayers"
Cohesion: 0.33
Nodes (3): List, Task, PlayerData

### Community 1 - "DraftRedisOperations"
Cohesion: 0.09
Nodes (17): DateTimeOffset, Dictionary, IDatabase, JsonSerializerOptions, List, Queue, Task, TimeSpan (+9 more)

### Community 2 - "NBAException"
Cohesion: 0.05
Nodes (43): NBAException, ErrorCode, ApplicationOptions, CenterLimit, MaxPlayersPerTeam, ProposedTradeTtlMinutes, AuthenticateResult, AuthenticationHandler (+35 more)

### Community 3 - "PlayerShort"
Cohesion: 0.07
Nodes (26): PlayerShort, FullName, PlayerId, Position, RedisKeys, IDatabase, Task, TimeSpan (+18 more)

### Community 4 - "Project Rules & Vendor Licenses"
Cohesion: 0.05
Nodes (51): Adapter (static mapper), Adding an HTTP Endpoint Flow, ApplicationDefaults, ApplicationOptions, Argon2Options, Aspire AppHost, Auth & Tests, Authenticate Everything Rule (+43 more)

### Community 5 - ".CreateClient"
Cohesion: 0.11
Nodes (26): Action, HttpMessageHandler, HttpRequestMessage, Fact, HttpResponseMessage, HttpStatusCode, InlineData, JsonException (+18 more)

### Community 6 - "Player"
Cohesion: 0.07
Nodes (28): DateTime, ICollection, Player, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow (+20 more)

### Community 7 - "PlayersFilterSearch"
Cohesion: 0.04
Nodes (42): IEndpointRouteBuilder, PlayerEndpoints, DateTime, PlayersFilterSearch, allowdrop, gameready, irlteamid, irlteamname (+34 more)

### Community 8 - ".League"
Cohesion: 0.23
Nodes (6): Task, IEndpointRouteBuilder, IOptions, Task, DraftManager, PlayerShortDto

### Community 9 - "DraftSnapshotService"
Cohesion: 0.22
Nodes (7): IServiceCollection, Dictionary, IOptions, JsonSerializerOptions, Queue, Task, DraftSnapshotService

### Community 10 - "PlayerSearchInput"
Cohesion: 0.06
Nodes (36): PlayerSearchInput, Allowdrop, Gameready, Irlteamid, Irlteamname, Islock, LeagueId, MaxAssists (+28 more)

### Community 11 - "Playermemento"
Cohesion: 0.12
Nodes (15): DateTime, Playermemento, Assists, Blocks, Fieldgoalperc, Freethrowperc, Player, Playermemontoid (+7 more)

### Community 12 - "BoxScoreStatsBuilder"
Cohesion: 0.07
Nodes (15): BoxScoreStatsBuilder, PlayerStats, ast, blk, fg3a, fg3m, fga, fgm (+7 more)

### Community 13 - "RecordingTradeHubClients"
Cohesion: 0.16
Nodes (12): IHubCallerClients, Method, ITradeHubClient, IReadOnlyList, List, Trade, RecordingTradeHubClients, All (+4 more)

### Community 14 - "LeagueDto"
Cohesion: 0.15
Nodes (13): LeagueDto, Autostart, Commissioner, CommissionersTeam, Draftstyle, Leagueid, Name, Seasonyear (+5 more)

### Community 15 - "League & Stats Value Requests"
Cohesion: 0.07
Nodes (27): NBA.Api.Requests.League, NBA.Api.Requests.StatValue, LeagueRequest, Autostart, DraftStyle, LeagueName, LeagueType, ScoringSystem (+19 more)

### Community 16 - "TradeHubFixture"
Cohesion: 0.07
Nodes (44): NBA.Api.SignalR, HubConnection, HubException, HubInvocationContext, ICollectionFixture, IConnectionMultiplexer, IHost, IHubFilter (+36 more)

### Community 17 - "NbaFantasyContext"
Cohesion: 0.09
Nodes (22): DbContext, DbSet, NbaFantasyContext, Applicationusers, Draftsnapshots, Leagueplayers, Leagues, Playermementos (+14 more)

### Community 18 - "NBA.Data.Context"
Cohesion: 0.15
Nodes (12): ErrorCodes, NBA.Api.Draft, NBA.Data.Context, NBA.Data.Redis.Enumerations, NBA.Service.Draft, NBA.Api.SignalR.Hubs, ApplicationDefaults.Exceptions, NBA.Tests.Integration (+4 more)

### Community 19 - "Applicationuser"
Cohesion: 0.12
Nodes (16): ICollection, Applicationuser, Email, Managerlevel, Password, Teams, Userid, Userleagues (+8 more)

### Community 20 - "Team"
Cohesion: 0.12
Nodes (17): ICollection, Team, Approved, Categoryleaguepoints, Islock, Lastweekpoints, League, Leagueid (+9 more)

### Community 21 - "PlayerDto"
Cohesion: 0.08
Nodes (24): DateTime, PlayerDto, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow, Gameready (+16 more)

### Community 22 - "GameDto"
Cohesion: 0.09
Nodes (22): DateTime, List, GameDto, Date, GameId, HomeTeam, Postponed, Postseason (+14 more)

### Community 23 - "TradeRedisOperations"
Cohesion: 0.20
Nodes (9): Guid, IDatabase, JsonSerializerOptions, List, Task, TimeSpan, TradeRedisOperations, RedisKey (+1 more)

### Community 24 - "Player"
Cohesion: 0.16
Nodes (3): NBA.Service.Builder, Player, BoxScoreCalculationBuilder

### Community 25 - "League"
Cohesion: 0.10
Nodes (21): ICollection, League, Autostart, Commissioner, Draftcompleted, Draftsnapshot, Draftstyle, Leagueid (+13 more)

### Community 26 - "DraftEndDraftTests"
Cohesion: 0.22
Nodes (11): IEnumerable, List, PlayerShortDto, FullName, PlayerId, Position, PlayerShortMappings, Fact (+3 more)

### Community 27 - "DraftTimerHostedService"
Cohesion: 0.31
Nodes (7): BackgroundService, CancellationToken, ILogger, IServiceProvider, Task, TimeSpan, DraftTimerHostedService

### Community 28 - "NBA.Data.Redis.Entities"
Cohesion: 0.11
Nodes (11): NBA.Data.Redis.Operations, NBA.Data.Redis.Scopes, NBA.Data.Redis.Keys, NBA.Data.Redis.Entities, NBA.Service.Trade, NBA.Data.Redis.Dtos, NBA.Data.Redis, NBA.Data.Constants (+3 more)

### Community 29 - "NBA.Service.Player"
Cohesion: 0.13
Nodes (9): ApplicationDefaults.Time, ExternalClients.Response, BoxScoreBuilder, NBA.Service.Game, NBA.Service.Player, BoxScoreBuilder.Model, NBA.Service, BoxScoreEvaluation (+1 more)

### Community 30 - "GameService"
Cohesion: 0.33
Nodes (7): IBackgroundJobClient, CancellationToken, DateOnly, IOptions, List, Task, GameService

### Community 31 - "AppHost Launch Settings"
Cohesion: 0.13
Nodes (18): ASPIRE_DASHBOARD_OTLP_ENDPOINT_URL, ASPIRE_RESOURCE_SERVICE_ENDPOINT_URL, ASPNETCORE_ENVIRONMENT, DOTNET_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables (+10 more)

### Community 32 - "ApplicationHostedService.cs"
Cohesion: 0.25
Nodes (5): NBA.Api.HostedService, ApplicationDefaults.LogDefaults, IExceptionHandler, GlobalExceptionHandler, ILogger

### Community 33 - "create-objects-nba-schema.sql"
Cohesion: 0.22
Nodes (18): nba.applicationuser, nba.draftsnapshot, nba.league, nba.leagueplayer, nba.player, nba.playermemento, nba.playoff, nba.playoffbracket (+10 more)

### Community 34 - "MetaData"
Cohesion: 0.14
Nodes (13): ExternalClients.Poco, MetaData, Next_cursor, Per_page, Prev_Cursor, List, GetGamesResponse, data (+5 more)

### Community 35 - "GameShort"
Cohesion: 0.12
Nodes (17): DateTime, GameShort, Date, GameId, HomeTeam, Postponed, Postseason, StartTime (+9 more)

### Community 36 - "ExternalClients Project Files"
Cohesion: 0.13
Nodes (16): ApplicationDefaults, net10.0, Microsoft.NET.Sdk, BoxScoreBuilder, net10.0, Microsoft.NET.Sdk, ExternalClients, net10.0 (+8 more)

### Community 37 - "BallDontLieClientWireMockTests"
Cohesion: 0.10
Nodes (24): IAsyncLifetime, IClassFixture, IRequestMessage, IResponseBuilder, Fact, HttpStatusCode, InlineData, JsonException (+16 more)

### Community 38 - "PlayerInfoResponse"
Cohesion: 0.12
Nodes (15): PlayerInfoResponse, college, country, draft_number, draft_round, draft_year, first_name, height (+7 more)

### Community 39 - "IBallDontLieClient"
Cohesion: 0.33
Nodes (5): CancellationToken, DateOnly, List, Task, IBallDontLieClient

### Community 40 - "API Launch Profiles"
Cohesion: 0.13
Nodes (15): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, applicationUrl, commandName (+7 more)

### Community 41 - "Playoff"
Cohesion: 0.12
Nodes (14): ICollection, Playoff, League, Leagueid, Playoffbrackets, Playoffid, Totalrounds, Playoffbracket (+6 more)

### Community 42 - "Transaction"
Cohesion: 0.12
Nodes (14): DateTime, ICollection, Transaction, Transactionid, Transactionleagues, Transactionstatus, Tscreated, Typetransaction (+6 more)

### Community 43 - "AuthTokenIssuer"
Cohesion: 0.30
Nodes (6): DateTime, IOptions, Task, AuthTokenIssuer, TokenPair, IEndpointRouteBuilder

### Community 44 - "PlayerStatsResponse"
Cohesion: 0.13
Nodes (14): PlayerStatsResponse, ast, blk, fg3a, fg3m, fga, fgm, fta (+6 more)

### Community 45 - "Per-League Stats Values"
Cohesion: 0.13
Nodes (14): Statsvalue, Assistsvalue, Blocksvalue, Fieldgoalvaluemade, Fieldgoalvaluemissed, Freethrowvaluemade, Freethrowvaluemissed, League (+6 more)

### Community 46 - "NbaCalendar"
Cohesion: 0.23
Nodes (5): NbaCalendar, DateOnly, InlineData, Theory, TimeZoneInfo

### Community 47 - ".OnModelCreating"
Cohesion: 0.12
Nodes (15): ModelBuilder, Player, Trade, ICollection, Trophie, Trophieid, Typetrophie, Usertrophies (+7 more)

### Community 48 - "NBA.Data Package References"
Cohesion: 0.14
Nodes (13): net10.0, Aspire.Hosting.Redis (13.1.2), Microsoft.Extensions.Configuration.Abstractions (10.0.0), Microsoft.NET.Sdk, MessagePack (2.5.302), Microsoft.EntityFrameworkCore (10.0.0), Microsoft.EntityFrameworkCore.Design (10.0.0), Microsoft.Extensions.Configuration (10.0.0) (+5 more)

### Community 49 - "Aspire Manifest OTEL Config"
Cohesion: 0.14
Nodes (14): ASPNETCORE_FORWARDEDHEADERS_ENABLED, ConnectionStrings__nbafantasydb, HTTP_PORTS, NBAFANTASYDB_DATABASENAME, NBAFANTASYDB_HOST, NBAFANTASYDB_JDBCCONNECTIONSTRING, NBAFANTASYDB_PASSWORD, NBAFANTASYDB_PORT (+6 more)

### Community 50 - "NBA.Api.Requests.Authentication"
Cohesion: 0.15
Nodes (10): NBA.Api.Requests.Authentication, LoginRequestNBA, Password, Username, RefreshRequest, RefreshToken, SignUpRequest, Email (+2 more)

### Community 51 - ".GetPlayersGameStats"
Cohesion: 0.24
Nodes (8): AutomaticRetry, CancellationToken, GetAllPlayersResponse, JobDisplayName, MetaData, List, Task, PlayerStatsResponse

### Community 52 - "NBA.Api.DTOs"
Cohesion: 0.18
Nodes (5): NBA.Api.DTOs, List, DraftOrderDto, Round, Teams

### Community 53 - "BallDontLieClient"
Cohesion: 0.23
Nodes (9): CancellationToken, DateOnly, HttpResponseMessage, List, Task, BallDontLieClient, HttpClient, ResiliencePipeline (+1 more)

### Community 54 - "GameInfoResponse"
Cohesion: 0.11
Nodes (18): DateTime, GameInfoResponse, date, datetime, home_team, home_team_score, id, postponed (+10 more)

### Community 55 - "Trade"
Cohesion: 0.05
Nodes (52): Clients, DateTime, Guid, List, TradeDto, Fromteamid, Leagueid, Playerids (+44 more)

### Community 56 - "TeamDto"
Cohesion: 0.22
Nodes (9): TeamDto, Categoryleaguepoints, Competesinleague, Islock, Lastweekpoints, Name, Seed, Teamid (+1 more)

### Community 57 - "FreeAgencyEndpoints.cs"
Cohesion: 0.22
Nodes (6): NBA.Api.Requests.FreeAgency, NBA.Service.FreeAgency, List, FreeAgencyPickUpRequest, leagueId, playerIds

### Community 58 - ".ToPlayerDb"
Cohesion: 0.17
Nodes (8): List, PlayerData, Adapter, PlayerData, Fact, InlineData, Theory, AdapterTests

### Community 59 - "JwtTokenService"
Cohesion: 0.27
Nodes (6): InvalidOperationException, IOptions, JwtTokenService, Fact, Task, JwtTokenServiceTests

### Community 60 - "NBA.Data.Entities"
Cohesion: 0.22
Nodes (3): NBA.Data.Entities, NBA.Service.Authentication, NBA.Tests

### Community 61 - "UserTeamDto"
Cohesion: 0.17
Nodes (12): List, UserTeamDto, Categoryleaguepoints, Islock, Lastweekpoints, Leagueid, Leaguename, Name (+4 more)

### Community 62 - "Test Project Packages"
Cohesion: 0.17
Nodes (12): NBA.Tests, net10.0, Microsoft.NET.Sdk, coverlet.collector (6.0.2), Microsoft.AspNetCore.SignalR.Client (10.0.0), Microsoft.AspNetCore.TestHost (10.0.0), Microsoft.EntityFrameworkCore.InMemory (10.0.0), Microsoft.NET.Test.Sdk (17.12.0) (+4 more)

### Community 63 - "TradeBetweenTeams"
Cohesion: 0.13
Nodes (13): List, Task, DateTimeOffset, Guid, List, TradeBetweenTeams, FromTeam, PlayersIds (+5 more)

### Community 64 - "ScheduledGames"
Cohesion: 0.24
Nodes (9): List, ScheduledGames, RestOfWeek, Today, Tomorrow, DateOnly, Task, TimeSpan (+1 more)

### Community 65 - "ExternalClients"
Cohesion: 0.20
Nodes (4): ExternalClients, NBA.Tests.Fakes, IEndpointRouteBuilder, TestingEndpoints

### Community 66 - "EntityMappings"
Cohesion: 0.22
Nodes (3): List, Team, EntityMappings

### Community 67 - "NBA.Api Package References"
Cohesion: 0.18
Nodes (10): net10.0, Aspire.StackExchange.Redis (13.1.2), Microsoft.Extensions.Http.Resilience (10.1.0), Aspire.Npgsql.EntityFrameworkCore.PostgreSQL (13.1.0), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.0), Microsoft.AspNetCore.OpenApi (10.0.0), Microsoft.AspNetCore.SignalR.StackExchangeRedis (10.0.5), Microsoft.OpenApi (2.7.5) (+2 more)

### Community 68 - "Leagueplayer"
Cohesion: 0.18
Nodes (9): LeaguePlayerData, Leagueplayer, Isfreeagent, League, Leagueid, Leagueplayerid, Playerid, List (+1 more)

### Community 69 - "LoginDto"
Cohesion: 0.22
Nodes (8): List, LoginDto, Leagues, RefreshToken, Teams, Token, Userid, Username

### Community 70 - "PlayerManager"
Cohesion: 0.22
Nodes (8): IOptions, JsonOptions, JsonSerializerOptions, List, NbaFantasyRedis, Task, PlayerManager, PlayerInfoResponse

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

### Community 75 - "DraftService"
Cohesion: 0.19
Nodes (13): DraftOptions, DraftPickTime, Rounds, ShowTeamDraftBoardCount, IHubContext, IOptions, DraftTimerProcessor, IOptions (+5 more)

### Community 76 - "ServiceDefaults Extensions"
Cohesion: 0.22
Nodes (3): Microsoft.Extensions.Hosting, Extensions, WebApplication

### Community 77 - "GameRedisOperations"
Cohesion: 0.28
Nodes (5): IDatabase, JsonSerializerOptions, Task, TimeSpan, GameRedisOperations

### Community 78 - "NBA.Data.Enumerations"
Cohesion: 0.18
Nodes (8): NBA.Data.Enumerations, DraftType, Auction, Linear, Offline, RRR, Snake, PlayerPositionExtensions

### Community 79 - "AppHost Packages"
Cohesion: 0.22
Nodes (8): net10.0, Aspire.Hosting.Redis (13.1.2), Aspire.StackExchange.Redis (13.1.2), Microsoft.NET.Sdk, Aspire.Hosting.AppHost (13.1.0), Aspire.Hosting.PostgreSQL (13.1.0), CommunityToolkit.Aspire.Hosting.NodeJS.Extensions (9.9.0), OpenTelemetry.Api (1.16.0)

### Community 80 - "PlayerService"
Cohesion: 0.22
Nodes (8): BoxScoreCalculationService, DateTime, IBallDontLieClient, IReadOnlyList, NbaFantasyContext, PagedResult, PlayerService, PlayerPositionEnum

### Community 81 - "TeamInfoResponse"
Cohesion: 0.17
Nodes (12): List, GetAllTeamsResponse, data, meta, TeamInfoResponse, abbreviation, city, conference (+4 more)

### Community 82 - "DraftState"
Cohesion: 0.16
Nodes (11): Task, DateTime, Dictionary, List, DraftState, DraftBoardTeams, DraftedPlayersPerTeam, DraftPlayers (+3 more)

### Community 84 - ".ToggleFreeAgencyStatus"
Cohesion: 0.21
Nodes (7): IEndpointRouteBuilder, FreeAgencyEndpoints, IServiceCollection, FreeAgencyExtention, List, Task, FreeAgencyService

### Community 85 - "Aspire HTTPS Bindings"
Cohesion: 0.25
Nodes (8): https, protocol, scheme, transport, bindings, path, type, nba-api

### Community 86 - "ApplicationHostedService"
Cohesion: 0.06
Nodes (27): ErrorResponse, ErrorCode, ErrorMessage, Log, message, request, response, BallDontLieClientOptions (+19 more)

### Community 87 - "NBA.Api.SignalR.Clients"
Cohesion: 0.18
Nodes (6): NBA.Api.SignalR.Clients, Hub, Task, IChatHubClient, Task, ChatHub

### Community 88 - "NBA.Api.Authentication"
Cohesion: 0.09
Nodes (16): NBA.Api.Mappings, NBA.Api.Requests.Team, NBA.Api.Authentication, NBA.Api.Requests.Player, NBA.Service.Team, NBA.Api.Endpoints, ClaimsPrincipalExtensions, AuthenticationEndpoints (+8 more)

### Community 89 - "Draft Status Enum"
Cohesion: 0.29
Nodes (6): DraftStatus, DraftCompleted, DraftEnded, DraftStarted, Initial, Paused

### Community 90 - "DraftBoardTeams"
Cohesion: 0.20
Nodes (7): Task, IDraftHubClient, List, DraftBoardTeams, CurrentRound, DraftOrder, onTheClockTeam

### Community 91 - "ShortenJobExpirationFilter"
Cohesion: 0.28
Nodes (6): ApplyStateContext, NBA.Api.HangFire, IApplyStateFilter, IWriteOnlyTransaction, JobFilterAttribute, ShortenJobExpirationFilter

### Community 92 - "AppHost Hosting Packages"
Cohesion: 0.29
Nodes (7): NBA.Service, net10.0, Aspire.Hosting.Redis (13.1.2), Microsoft.Extensions.Options (10.0.3), Microsoft.NET.Sdk, Isopoh.Cryptography.Argon2 (1.1.10), Microsoft.Extensions.Identity.Core (10.0.0)

### Community 93 - "LeagueTrades"
Cohesion: 0.28
Nodes (6): Guid, List, Task, TimeSpan, LeagueTrades, LeagueId

### Community 94 - "Aspire Server Bindings"
Cohesion: 0.29
Nodes (7): tcp, bindings, port, protocol, scheme, targetPort, transport

### Community 95 - "Draftsnapshot"
Cohesion: 0.22
Nodes (6): DateTime, Draftsnapshot, Draftstate, Draftteams, Leagueid, Tsupdated

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
Cohesion: 0.16
Nodes (14): TeamDraftBoard, Pick, TeamId, TeamName, Dictionary, DraftBoardTeams, IOptions, List (+6 more)

### Community 102 - "Chat Schema DDL"
Cohesion: 0.70
Nodes (4): chat.conversationparticipants, chat.messages, chat.rooms, nba.applicationuser

### Community 103 - "NbaFantasyRedis"
Cohesion: 0.22
Nodes (9): Lazy, IDatabase, NbaFantasyRedis, Auth, Draft, Game, Lock, Player (+1 more)

### Community 104 - "Aspire HTTP Bindings"
Cohesion: 0.50
Nodes (4): http, protocol, scheme, transport

### Community 105 - "LeaguePlayerSeedTests"
Cohesion: 0.08
Nodes (27): NBA.Service.League, NBA.Service.LeaguePlayer, Fact, IEndpointRouteBuilder, League, NbaFantasyContext, PagedResult, Task (+19 more)

### Community 116 - "Task"
Cohesion: 0.15
Nodes (10): CancellationToken, List, Player, Task, Teamplayer, Player, Playerid, Team (+2 more)

### Community 118 - ".RegisterHangFire"
Cohesion: 0.29
Nodes (5): NBA.Api, IConfiguration, HttpResponseMessage, IServiceCollection, Extentions

### Community 119 - ".Generate"
Cohesion: 0.32
Nodes (3): RefreshTokenGenerator, Fact, RefreshTokenGeneratorTests

### Community 120 - "Argon2idPasswordHasher"
Cohesion: 0.16
Nodes (10): Argon2Options, DegreeOfParallelism, Iterations, MemoryKib, IPasswordHasher, IOptions, Argon2idPasswordHasher, Fact (+2 more)

### Community 121 - "Userleague"
Cohesion: 0.29
Nodes (6): Userleague, League, Leagueid, User, Userid, Userleagueid

### Community 123 - "JwtOptions"
Cohesion: 0.29
Nodes (6): JwtOptions, AccessTokenMinutes, Audience, Issuer, RefreshTokenDays, SigningKey

### Community 124 - ".PerformCalculations"
Cohesion: 0.29
Nodes (5): NBA.Service.CalculateBoxScore, Dictionary, List, Task, BoxScoreCalculationService

### Community 125 - "Q: Tell me how individual players are stored in redis"
Cohesion: 0.40
Nodes (4): Answer, Outcome, Q: Tell me how individual players are stored in redis, Source Nodes

### Community 126 - "LockRedisOperations"
Cohesion: 0.33
Nodes (4): IDatabase, Task, TimeSpan, LockRedisOperations

### Community 127 - "LeagueScope"
Cohesion: 0.33
Nodes (5): LeagueScope, Draft, LeagueId, Players, Trades

### Community 128 - ".CreateToken"
Cohesion: 0.50
Nodes (3): DateTime, AuthToken, ITokenService

## Knowledge Gaps
- **704 isolated node(s):** `init.sh script`, `ErrorCodes`, `TradeStatuses`, `BoxScoreEvaluation`, `LeagueId` (+699 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **16 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `NbaFantasyContext` connect `NbaFantasyContext` to `.GetAllPlayers`, `NBAException`, `DraftSnapshotService`, `Playermemento`, `TradeHubFixture`, `Applicationuser`, `Team`, `DraftEndDraftTests`, `Playoff`, `Transaction`, `AuthTokenIssuer`, `.OnModelCreating`, `NBA.Data.Entities`, `Leagueplayer`, `DraftService`, `.ToggleFreeAgencyStatus`, `ApplicationHostedService`, `Draftsnapshot`, `DraftLifecycleService`, `LeaguePlayerSeedTests`, `Task`, `Userleague`, `.PerformCalculations`?**
  _High betweenness centrality (0.166) - this node is a cross-community bridge._
- **Why does `Player` connect `Player` to `EntityMappings`, `PlayerShort`, `Leagueplayer`, `NBAException`, `Playermemento`, `NBA.Data.Redis.Entities`, `NBA.Data.Context`, `Task`, `.ToggleFreeAgencyStatus`, `FreeAgencyEndpoints.cs`, `.ToPlayerDb`, `.PerformCalculations`, `NBA.Service.Player`?**
  _High betweenness centrality (0.082) - this node is a cross-community bridge._
- **Why does `NBA.Data.Redis.Entities` connect `NBA.Data.Redis.Entities` to `ExternalClients`, `GameShort`, `DraftBoardTeams`, `GameRedisOperations`, `TradeHubFixture`, `NBA.Data.Context`, `TradeRedisOperations`, `Trade`, `NBA.Api.SignalR.Clients`, `NBA.Api.Authentication`, `DraftEndDraftTests`, `NBA.Service.Player`, `TradeBetweenTeams`?**
  _High betweenness centrality (0.066) - this node is a cross-community bridge._
- **Are the 28 inferred relationships involving `NBAException` (e.g. with `.GetAsync()` and `.RefreshAsync()`) actually correct?**
  _`NBAException` has 28 INFERRED edges - model-reasoned connections that need verification._
- **What connects `init.sh script`, `ErrorCodes`, `TradeStatuses` to the rest of the system?**
  _704 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DraftRedisOperations` be split into smaller, more focused modules?**
  _Cohesion score 0.08816326530612245 - nodes in this community are weakly interconnected._
- **Should `NBAException` be split into smaller, more focused modules?**
  _Cohesion score 0.053613053613053616 - nodes in this community are weakly interconnected._