# Graph Report - NBAFantasy  (2026-08-30)

## Corpus Check
- 190 files · ~55,740 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2100 nodes · 3816 edges · 146 communities (124 shown, 22 thin omitted)
- Extraction: 93% EXTRACTED · 7% INFERRED · 0% AMBIGUOUS · INFERRED: 286 edges (avg confidence: 0.82)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `3bc555d0`
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
- NBA.Data.Redis.Entities
- Applicationuser
- ApplicationDefaults.Exceptions
- PlayerDto
- GameDto
- .BuildHub
- Team
- League
- PlayerShort
- DraftTimerHostedService
- NBA.Data.Redis.Operations
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
- List
- PlayerStatsResponse
- Per-League Stats Values
- Task
- Usertrophie
- NBA.Data Package References
- Aspire Manifest OTEL Config
- AuthenticationEndpoints.cs
- .GetPagedAsync
- LoginDto
- Draftsnapshot
- GameInfoResponse
- NBA.Api.Authentication
- TeamDto
- .DraftPlayer
- .ToPlayerDb
- TradeDto
- Trade
- UserTeamDto
- Test Project Packages
- Argon2idPasswordHasherTests
- .EnsureRehydratedAsync
- ITradeOrchestrator
- EntityMappings
- NBA.Api Package References
- BallDontLieWireMockFixture
- .DeleteAsync
- PlayerManager
- ServiceDefaults Packages
- Entity Mapping Tests
- Aspire Postgres Connection
- Aspire Postgres Container
- DraftSnapshotService
- ServiceDefaults Extensions
- ChatHub
- Dictionary
- AppHost Packages
- TradeHub
- TeamInfoResponse
- DraftState
- LeaguePlayerService.cs
- .ToGameRedis
- Aspire HTTPS Bindings
- ApplicationHostedService
- DraftTimerProcessor
- NBA.Data.Enumerations
- Draft Status Enum
- TradeOutcome
- NBA.Service.Player
- AppHost Hosting Packages
- ExternalClients
- Aspire Server Bindings
- Argon2Options
- LeaguePlayerSeedTests
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
- PagedResult
- IBallDontLieClient
- .Generate
- MetaData
- BallDontLieClientOptions
- LockRedisOperations
- .CreateToken
- Userleague
- League
- Q: Tell me how individual players are stored in redis
- Playoffbracket
- .GetPlayersGameStats
- Transactionleague
- ScheduledGamesDto
- Team
- PlayerService
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
1. `NbaFantasyContext` - 97 edges
2. `TradeBetweenTeams` - 49 edges
3. `Trade` - 41 edges
4. `PlayerSearchInput` - 40 edges
5. `PlayersFilterSearch` - 38 edges
6. `NBAException` - 37 edges
7. `NBA.Data.Entities` - 36 edges
8. `NBA.Data.Redis.Entities` - 34 edges
9. `DraftState` - 32 edges
10. `Player` - 31 edges

## Surprising Connections (you probably didn't know these)
- `DraftTimerProcessor` --references--> `DraftLifecycleService`  [EXTRACTED]
  NBA.Api/Draft/DraftTimerProcessor.cs → NBA.Service/Draft/DraftLifecycleService.cs
- `DraftHub` --references--> `DraftLifecycleService`  [EXTRACTED]
  NBA.Api/SignalR/Hubs/DraftHub.cs → NBA.Service/Draft/DraftLifecycleService.cs
- `DraftLifecycleService` --references--> `NbaFantasyContext`  [EXTRACTED]
  NBA.Service/Draft/DraftLifecycleService.cs → NBA.Data/Context/NbaFantasyContext.cs
- `AuthTokenIssuer` --references--> `NbaFantasyRedis`  [EXTRACTED]
  NBA.Api/Authentication/AuthTokenIssuer.cs → NBA.Data/Context/NbaFantasyRedis.cs
- `DraftTimerProcessor` --references--> `NbaFantasyRedis`  [EXTRACTED]
  NBA.Api/Draft/DraftTimerProcessor.cs → NBA.Data/Context/NbaFantasyRedis.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Draft Pick Processing Flow** — claude_drafthub, claude_playermanager, claude_draftmanager, claude_draftstate [EXTRACTED 0.95]
- **Draft Timer & Deadline Coordination** — claude_drafttimerhostedservice, claude_draftredisoperations, claude_draftmanager, claude_draft_realtime [EXTRACTED 0.95]
- **External HTTP Resilience Strategy** — claude_resilience_pipeline_rule, claude_externalclients, claude_nba_servicedefaults [INFERRED 0.85]

## Communities (146 total, 22 thin omitted)

### Community 0 - "ShortenJobExpirationFilter"
Cohesion: 0.15
Nodes (11): ApplyStateContext, NBA.Api.HangFire, NBA.Api, IApplyStateFilter, IConfiguration, IWriteOnlyTransaction, JobFilterAttribute, HttpResponseMessage (+3 more)

### Community 1 - "DraftRedisOperations"
Cohesion: 0.07
Nodes (26): List, DraftBoardTeams, CurrentRound, DraftOrder, onTheClockTeam, TeamDraftBoard, Pick, TeamId (+18 more)

### Community 2 - ".ProposeAsync"
Cohesion: 0.14
Nodes (14): ApplicationOptions, CenterLimit, MaxPlayersPerTeam, ProposedTradeTtlMinutes, Guid, IOptions, List, Task (+6 more)

### Community 3 - "PlayerRedisOperations"
Cohesion: 0.09
Nodes (20): HashSet, IDatabase, IEnumerable, RedisKeys, IDatabase, Task, TimeSpan, AuthRedisOperations (+12 more)

### Community 4 - "Project Rules & Vendor Licenses"
Cohesion: 0.05
Nodes (51): Adapter (static mapper), Adding an HTTP Endpoint Flow, ApplicationDefaults, ApplicationOptions, Argon2Options, Aspire AppHost, Auth & Tests, Authenticate Everything Rule (+43 more)

### Community 5 - "NBAException"
Cohesion: 0.10
Nodes (29): Action, NBAException, ErrorCode, Exception, HttpMessageHandler, HttpRequestMessage, Fact, HttpResponseMessage (+21 more)

### Community 6 - "Player"
Cohesion: 0.05
Nodes (43): DateTime, ICollection, Player, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow (+35 more)

### Community 7 - "PlayersFilterSearch"
Cohesion: 0.04
Nodes (42): IEndpointRouteBuilder, PlayerEndpoints, DateTime, PlayersFilterSearch, allowdrop, gameready, irlteamid, irlteamname (+34 more)

### Community 8 - ".League"
Cohesion: 0.23
Nodes (6): IOptions, Task, DraftHub, IOptions, Task, DraftManager

### Community 9 - "DraftService"
Cohesion: 0.14
Nodes (12): ApplicationOptions, DraftManager, JsonOptions, DraftSnapshotService, IServiceCollection, DraftExtention, DraftOptions, DraftSnapshotService (+4 more)

### Community 10 - "PlayerSearchInput"
Cohesion: 0.06
Nodes (36): PlayerSearchInput, Allowdrop, Gameready, Irlteamid, Irlteamname, Islock, LeagueId, MaxAssists (+28 more)

### Community 11 - "TradeService"
Cohesion: 0.15
Nodes (12): Created, IQueryable, Trade, List, DateTime, Guid, ILogger, List (+4 more)

### Community 12 - "BoxScoreStatsBuilder"
Cohesion: 0.07
Nodes (16): BoxScoreStatsBuilder, PlayerStats, ast, blk, fg3a, fg3m, fga, fgm (+8 more)

### Community 13 - "TradeBetweenTeams"
Cohesion: 0.05
Nodes (45): IHubCallerClients, Method, List, Task, ITradeHubClient, DateTimeOffset, Guid, List (+37 more)

### Community 14 - "AuthTokenIssuer"
Cohesion: 0.30
Nodes (6): DateTime, IOptions, Task, AuthTokenIssuer, TokenPair, IEndpointRouteBuilder

### Community 15 - "League & Stats Value Requests"
Cohesion: 0.07
Nodes (27): NBA.Api.Requests.League, NBA.Api.Requests.StatValue, LeagueRequest, Autostart, DraftStyle, LeagueName, LeagueType, ScoringSystem (+19 more)

### Community 16 - "TradeHubFixture"
Cohesion: 0.06
Nodes (53): AuthenticateResult, AuthenticationHandler, AuthenticationSchemeOptions, ClaimsPrincipal, NBA.Api.SignalR, HubConnection, HubException, HubInvocationContext (+45 more)

### Community 17 - "NbaFantasyContext"
Cohesion: 0.07
Nodes (38): DbContext, DbSet, ModelBuilder, Applicationuser, Draftsnapshot, League, Leagueplayer, Player (+30 more)

### Community 18 - "NBA.Data.Redis.Entities"
Cohesion: 0.14
Nodes (13): NBA.Api.Draft, NBA.Api.SignalR.Clients, NBA.Data.Context, NBA.Data.Redis.Enumerations, NBA.Service.Draft, NBA.Api.SignalR.Hubs, NBA.Data.Redis.Entities, NBA.Service.Trade (+5 more)

### Community 19 - "Applicationuser"
Cohesion: 0.12
Nodes (15): IPasswordHasher, ICollection, Applicationuser, Email, Managerlevel, Password, Teams, Userid (+7 more)

### Community 20 - "ApplicationDefaults.Exceptions"
Cohesion: 0.10
Nodes (11): ErrorCodes, ApplicationDefaults.LogDefaults, ApplicationDefaults.Exceptions, NBA.Service.Roster, NBA.Data.Constants, IExceptionHandler, GlobalExceptionHandler, ILogger (+3 more)

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
Cohesion: 0.05
Nodes (32): NBA.Service.CalculateBoxScore, NBA.Service.Builder, IEndpointRouteBuilder, TeamEndpoints, Player, ICollection, Team, Approved (+24 more)

### Community 25 - "League"
Cohesion: 0.09
Nodes (22): ICollection, League, Autostart, Commissioner, Draftcompleted, Draftsnapshot, Draftstyle, Leagueid (+14 more)

### Community 26 - "PlayerShort"
Cohesion: 0.11
Nodes (19): IEnumerable, List, PlayerShortDto, FullName, PlayerId, Position, PlayerShortMappings, PlayerShort (+11 more)

### Community 27 - "DraftTimerHostedService"
Cohesion: 0.31
Nodes (7): BackgroundService, CancellationToken, ILogger, IServiceProvider, Task, TimeSpan, DraftTimerHostedService

### Community 28 - "NBA.Data.Redis.Operations"
Cohesion: 0.17
Nodes (6): NBA.Data.Redis.Operations, NBA.Data.Redis.Scopes, NBA.Data.Redis.Keys, NBA.Data.Redis, JsonSerializerOptions, RedisSerializer

### Community 29 - "ExternalClients.Response"
Cohesion: 0.11
Nodes (11): ApplicationDefaults.Time, ExternalClients.Response, BoxScoreBuilder, NBA.Service.Game, NBA.Service, ExternalClients.Poco, IEndpointRouteBuilder, GameEndpoints (+3 more)

### Community 30 - "NBA.Api.DTOs"
Cohesion: 0.18
Nodes (5): NBA.Api.DTOs, List, DraftOrderDto, Round, Teams

### Community 31 - "AppHost Launch Settings"
Cohesion: 0.13
Nodes (18): ASPIRE_DASHBOARD_OTLP_ENDPOINT_URL, ASPIRE_RESOURCE_SERVICE_ENDPOINT_URL, ASPNETCORE_ENVIRONMENT, DOTNET_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables (+10 more)

### Community 32 - "JwtOptions"
Cohesion: 0.16
Nodes (12): JwtOptions, AccessTokenMinutes, Audience, Issuer, RefreshTokenDays, SigningKey, InvalidOperationException, IOptions (+4 more)

### Community 33 - "create-objects-nba-schema.sql"
Cohesion: 0.22
Nodes (18): nba.applicationuser, nba.draftsnapshot, nba.league, nba.leagueplayer, nba.player, nba.playermemento, nba.playoff, nba.playoffbracket (+10 more)

### Community 34 - "LeagueDto"
Cohesion: 0.15
Nodes (13): LeagueDto, Autostart, Commissioner, CommissionersTeam, Draftstyle, Leagueid, Name, Seasonyear (+5 more)

### Community 35 - "GameShort"
Cohesion: 0.05
Nodes (43): NbaCalendar, DateOnly, IBackgroundJobClient, DateTime, List, GameShort, Date, GameId (+35 more)

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

### Community 43 - "List"
Cohesion: 0.14
Nodes (9): NBA.Service.FreeAgency, CancellationToken, Leagueplayer, List, Player, Teamplayer, List, Task (+1 more)

### Community 44 - "PlayerStatsResponse"
Cohesion: 0.14
Nodes (14): PlayerStatsResponse, ast, blk, fg3a, fg3m, fga, fgm, fta (+6 more)

### Community 45 - "Per-League Stats Values"
Cohesion: 0.13
Nodes (14): Statsvalue, Assistsvalue, Blocksvalue, Fieldgoalvaluemade, Fieldgoalvaluemissed, Freethrowvaluemade, Freethrowvaluemissed, League (+6 more)

### Community 46 - "Task"
Cohesion: 0.16
Nodes (7): Applicationuser, Draftsnapshot, Task, Team, IPasswordHasher, Task, AuthService

### Community 47 - "Usertrophie"
Cohesion: 0.14
Nodes (12): ICollection, Trophie, Trophieid, Typetrophie, Usertrophies, Xp, Usertrophie, Trophie (+4 more)

### Community 48 - "NBA.Data Package References"
Cohesion: 0.14
Nodes (13): net10.0, Aspire.Hosting.Redis (13.1.2), Microsoft.Extensions.Configuration.Abstractions (10.0.0), Microsoft.NET.Sdk, MessagePack (2.5.302), Microsoft.EntityFrameworkCore (10.0.0), Microsoft.EntityFrameworkCore.Design (10.0.0), Microsoft.Extensions.Configuration (10.0.0) (+5 more)

### Community 49 - "Aspire Manifest OTEL Config"
Cohesion: 0.14
Nodes (14): ASPNETCORE_FORWARDEDHEADERS_ENABLED, ConnectionStrings__nbafantasydb, HTTP_PORTS, NBAFANTASYDB_DATABASENAME, NBAFANTASYDB_HOST, NBAFANTASYDB_JDBCCONNECTIONSTRING, NBAFANTASYDB_PASSWORD, NBAFANTASYDB_PORT (+6 more)

### Community 50 - "AuthenticationEndpoints.cs"
Cohesion: 0.13
Nodes (11): NBA.Api.Requests.Authentication, AuthenticationEndpoints, LoginRequestNBA, Password, Username, RefreshRequest, RefreshToken, SignUpRequest (+3 more)

### Community 51 - ".GetPagedAsync"
Cohesion: 0.39
Nodes (4): List, PagedResult, PlayerData, Task

### Community 52 - "LoginDto"
Cohesion: 0.22
Nodes (8): List, LoginDto, Leagues, RefreshToken, Teams, Token, Userid, Username

### Community 53 - "Draftsnapshot"
Cohesion: 0.29
Nodes (6): DateTime, Draftsnapshot, Draftstate, Draftteams, Leagueid, Tsupdated

### Community 54 - "GameInfoResponse"
Cohesion: 0.15
Nodes (13): DateTime, GameInfoResponse, date, datetime, home_team, home_team_score, id, postponed (+5 more)

### Community 55 - "NBA.Api.Authentication"
Cohesion: 0.20
Nodes (4): NBA.Tests.Fakes, NBA.Api.Authentication, NBA.Tests, ClaimsPrincipalExtensions

### Community 56 - "TeamDto"
Cohesion: 0.22
Nodes (9): TeamDto, Categoryleaguepoints, Competesinleague, Islock, Lastweekpoints, Name, Seed, Teamid (+1 more)

### Community 58 - ".ToPlayerDb"
Cohesion: 0.26
Nodes (5): PlayerData, Fact, InlineData, Theory, AdapterTests

### Community 59 - "TradeDto"
Cohesion: 0.17
Nodes (12): DateTime, Guid, List, TradeDto, Fromteamid, Leagueid, Playerids, Status (+4 more)

### Community 60 - "Trade"
Cohesion: 0.13
Nodes (15): DateTime, Guid, List, Trade, Fromteam, Fromteamid, League, Leagueid (+7 more)

### Community 61 - "UserTeamDto"
Cohesion: 0.17
Nodes (12): List, UserTeamDto, Categoryleaguepoints, Islock, Lastweekpoints, Leagueid, Leaguename, Name (+4 more)

### Community 62 - "Test Project Packages"
Cohesion: 0.17
Nodes (12): NBA.Tests, net10.0, Microsoft.NET.Sdk, coverlet.collector (6.0.2), Microsoft.AspNetCore.SignalR.Client (10.0.0), Microsoft.AspNetCore.TestHost (10.0.0), Microsoft.EntityFrameworkCore.InMemory (10.0.0), Microsoft.NET.Test.Sdk (17.12.0) (+4 more)

### Community 64 - ".EnsureRehydratedAsync"
Cohesion: 0.24
Nodes (6): Dictionary, Queue, Task, TeamDraftBoard, Dictionary, Queue

### Community 65 - "ITradeOrchestrator"
Cohesion: 0.24
Nodes (6): Guid, List, Task, ITradeOrchestrator, IServiceCollection, TradeExtention

### Community 66 - "EntityMappings"
Cohesion: 0.28
Nodes (3): List, Team, EntityMappings

### Community 67 - "NBA.Api Package References"
Cohesion: 0.18
Nodes (10): net10.0, Aspire.StackExchange.Redis (13.1.2), Microsoft.Extensions.Http.Resilience (10.1.0), Aspire.Npgsql.EntityFrameworkCore.PostgreSQL (13.1.0), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.0), Microsoft.AspNetCore.OpenApi (10.0.0), Microsoft.AspNetCore.SignalR.StackExchangeRedis (10.0.5), Microsoft.OpenApi (2.7.5) (+2 more)

### Community 68 - "BallDontLieWireMockFixture"
Cohesion: 0.18
Nodes (9): IAsyncLifetime, HttpResponseMessage, IOptions, Task, BallDontLieWireMockFixture, Client, Server, ServiceProvider (+1 more)

### Community 70 - "PlayerManager"
Cohesion: 0.32
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

### Community 75 - "DraftSnapshotService"
Cohesion: 0.20
Nodes (8): DraftOptions, DraftPickTime, Rounds, ShowTeamDraftBoardCount, IOptions, JsonSerializerOptions, Task, DraftSnapshotService

### Community 76 - "ServiceDefaults Extensions"
Cohesion: 0.22
Nodes (3): Microsoft.Extensions.Hosting, Extensions, WebApplication

### Community 77 - "ChatHub"
Cohesion: 0.25
Nodes (5): Hub, Task, IChatHubClient, Task, ChatHub

### Community 79 - "AppHost Packages"
Cohesion: 0.22
Nodes (8): net10.0, Aspire.Hosting.Redis (13.1.2), Aspire.StackExchange.Redis (13.1.2), Microsoft.NET.Sdk, Aspire.Hosting.AppHost (13.1.0), Aspire.Hosting.PostgreSQL (13.1.0), CommunityToolkit.Aspire.Hosting.NodeJS.Extensions (9.9.0), OpenTelemetry.Api (1.16.0)

### Community 80 - "TradeHub"
Cohesion: 0.29
Nodes (6): Guid, ILogger, IReadOnlyList, List, Task, TradeHub

### Community 81 - "TeamInfoResponse"
Cohesion: 0.17
Nodes (12): List, GetAllTeamsResponse, data, meta, TeamInfoResponse, abbreviation, city, conference (+4 more)

### Community 82 - "DraftState"
Cohesion: 0.15
Nodes (12): Task, IDraftHubClient, DateTime, Dictionary, List, DraftState, DraftBoardTeams, DraftedPlayersPerTeam (+4 more)

### Community 83 - "LeaguePlayerService.cs"
Cohesion: 0.29
Nodes (4): NBA.Service.LeaguePlayer, IServiceCollection, LeaguePlayerExtention, LeaguePlayerService

### Community 85 - "Aspire HTTPS Bindings"
Cohesion: 0.25
Nodes (8): https, protocol, scheme, transport, bindings, path, type, nba-api

### Community 86 - "ApplicationHostedService"
Cohesion: 0.07
Nodes (24): ErrorResponse, ErrorCode, ErrorMessage, Log, message, request, response, NBA.Api.HostedService (+16 more)

### Community 87 - "DraftTimerProcessor"
Cohesion: 0.32
Nodes (5): IHubContext, IOptions, Task, DraftTimerProcessor, IEndpointRouteBuilder

### Community 88 - "NBA.Data.Enumerations"
Cohesion: 0.11
Nodes (13): NBA.Data.Enumerations, NBA.Api.Mappings, NBA.Api.Requests.Team, NBA.Service.Team, TeamRequest, teamName, DraftType, Auction (+5 more)

### Community 89 - "Draft Status Enum"
Cohesion: 0.29
Nodes (6): DraftStatus, DraftCompleted, DraftEnded, DraftStarted, Initial, Paused

### Community 90 - "TradeOutcome"
Cohesion: 0.23
Nodes (9): IReadOnlyList, Accepted, Rejected, TradeEvent, TradeOutcome, Guid, List, TradeData (+1 more)

### Community 91 - "NBA.Service.Player"
Cohesion: 0.13
Nodes (9): NBA.Service.League, NBA.Service.Player, NBA.Api.Requests.Player, NBA.Api.Endpoints, LeagueEndpoints, IEndpointRouteBuilder, TradeEndpoints, List (+1 more)

### Community 92 - "AppHost Hosting Packages"
Cohesion: 0.29
Nodes (7): NBA.Service, net10.0, Aspire.Hosting.Redis (13.1.2), Microsoft.Extensions.Options (10.0.3), Microsoft.NET.Sdk, Isopoh.Cryptography.Argon2 (1.1.10), Microsoft.Extensions.Identity.Core (10.0.0)

### Community 93 - "ExternalClients"
Cohesion: 0.22
Nodes (4): ExternalClients, NBA.Tests.Integration, IEndpointRouteBuilder, TestingEndpoints

### Community 94 - "Aspire Server Bindings"
Cohesion: 0.29
Nodes (7): tcp, bindings, port, protocol, scheme, targetPort, transport

### Community 95 - "Argon2Options"
Cohesion: 0.40
Nodes (4): Argon2Options, DegreeOfParallelism, Iterations, MemoryKib

### Community 96 - "LeaguePlayerSeedTests"
Cohesion: 0.10
Nodes (24): Fact, IEndpointRouteBuilder, LeaguePlayerData, LeaguePlayerService, League, NbaFantasyContext, PlayerService, Task (+16 more)

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
Cohesion: 0.23
Nodes (10): DraftBoardTeams, Dictionary, DraftOptions, DraftSnapshotService, IOptions, NbaFantasyRedis, Queue, Task (+2 more)

### Community 102 - "Chat Schema DDL"
Cohesion: 0.70
Nodes (4): chat.conversationparticipants, chat.messages, chat.rooms, nba.applicationuser

### Community 103 - "NbaFantasyRedis"
Cohesion: 0.22
Nodes (9): Lazy, IDatabase, NbaFantasyRedis, Auth, Draft, Game, Lock, Player (+1 more)

### Community 104 - "Aspire HTTP Bindings"
Cohesion: 0.50
Nodes (4): http, protocol, scheme, transport

### Community 105 - "NBA.Data.Entities"
Cohesion: 0.28
Nodes (4): NBA.Data.Entities, NBA.Service.Authentication, List, LoginResult

### Community 117 - "IBallDontLieClient"
Cohesion: 0.33
Nodes (5): CancellationToken, DateOnly, List, Task, IBallDontLieClient

### Community 118 - ".Generate"
Cohesion: 0.47
Nodes (3): RefreshTokenGenerator, Fact, RefreshTokenGeneratorTests

### Community 119 - "MetaData"
Cohesion: 0.15
Nodes (12): MetaData, Next_cursor, Per_page, Prev_Cursor, List, GetGamesResponse, data, meta (+4 more)

### Community 120 - "BallDontLieClientOptions"
Cohesion: 0.40
Nodes (4): BallDontLieClientOptions, ApiKey, BaseUrl, Per_Page

### Community 121 - "LockRedisOperations"
Cohesion: 0.33
Nodes (4): IDatabase, Task, TimeSpan, LockRedisOperations

### Community 122 - ".CreateToken"
Cohesion: 0.50
Nodes (3): DateTime, AuthToken, ITokenService

### Community 123 - "Userleague"
Cohesion: 0.29
Nodes (6): Userleague, League, Leagueid, User, Userid, Userleagueid

### Community 124 - "League"
Cohesion: 0.25
Nodes (7): League, Leagueplayer, Isfreeagent, League, Leagueid, Leagueplayerid, Playerid

### Community 125 - "Q: Tell me how individual players are stored in redis"
Cohesion: 0.40
Nodes (4): Answer, Outcome, Q: Tell me how individual players are stored in redis, Source Nodes

### Community 127 - "Playoffbracket"
Cohesion: 0.25
Nodes (7): Playoffbracket, Playoff, Playoffbracketid, Playoffid, Playoffround, Team1, Team2

### Community 129 - ".GetPlayersGameStats"
Cohesion: 0.25
Nodes (6): AutomaticRetry, GetAllPlayersResponse, JobDisplayName, MetaData, CancellationToken, PlayerStatsResponse

### Community 130 - "Transactionleague"
Cohesion: 0.29
Nodes (6): Transactionleague, League, Leagueid, Transaction, Transactionid, Transactionleagueid

### Community 131 - "ScheduledGamesDto"
Cohesion: 0.33
Nodes (5): List, ScheduledGamesDto, RestOfWeek, Today, Tomorrow

### Community 132 - "Team"
Cohesion: 0.40
Nodes (5): Team, abbreviation, city, full_name, id

### Community 134 - "PlayerService"
Cohesion: 0.22
Nodes (7): BoxScoreCalculationService, DateTime, IBallDontLieClient, IReadOnlyList, NbaFantasyRedis, PlayerService, PlayerPositionEnum

## Knowledge Gaps
- **705 isolated node(s):** `BoxScoreEvaluation`, `ErrorCodes`, `TradeStatuses`, `NBA.Api`, `CurrentRound` (+700 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **22 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `NbaFantasyContext` connect `NbaFantasyContext` to `PlayerService`, `DraftService`, `TradeService`, `AuthTokenIssuer`, `TradeHubFixture`, `NBA.Data.Redis.Entities`, `Team`, `PlayerShort`, `List`, `Task`, `.GetPagedAsync`, `.DraftPlayer`, `.EnsureRehydratedAsync`, `.DeleteAsync`, `PlayerManager`, `DraftSnapshotService`, `LeaguePlayerService.cs`, `ApplicationHostedService`, `DraftLifecycleService`?**
  _High betweenness centrality (0.115) - this node is a cross-community bridge._
- **Why does `Player` connect `Team` to `EntityMappings`, `Player`, `PlayerManager`, `List`, `TradeHubFixture`, `NBA.Data.Redis.Entities`, `.ToGameRedis`, `ApplicationDefaults.Exceptions`, `.ToPlayerDb`, `League`, `ExternalClients.Response`?**
  _High betweenness centrality (0.078) - this node is a cross-community bridge._
- **Why does `NBAException` connect `NBAException` to `.EnsureRehydratedAsync`, `.ProposeAsync`, `BallDontLieClientWireMockTests`, `BallDontLieClient`, `.League`, `TradeService`, `AuthTokenIssuer`, `Task`, `ApplicationDefaults.Exceptions`, `NBA.Api.Authentication`, `DraftTimerProcessor`, `Team`, `.DraftPlayer`?**
  _High betweenness centrality (0.074) - this node is a cross-community bridge._
- **What connects `BoxScoreEvaluation`, `ErrorCodes`, `TradeStatuses` to the rest of the system?**
  _705 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `ShortenJobExpirationFilter` be split into smaller, more focused modules?**
  _Cohesion score 0.14705882352941177 - nodes in this community are weakly interconnected._
- **Should `DraftRedisOperations` be split into smaller, more focused modules?**
  _Cohesion score 0.06892655367231638 - nodes in this community are weakly interconnected._
- **Should `.ProposeAsync` be split into smaller, more focused modules?**
  _Cohesion score 0.14492753623188406 - nodes in this community are weakly interconnected._