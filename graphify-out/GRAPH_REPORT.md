# Graph Report - NBAFantasy  (2026-08-30)

## Corpus Check
- 193 files · ~56,986 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2055 nodes · 3964 edges · 116 communities (111 shown, 5 thin omitted)
- Extraction: 92% EXTRACTED · 8% INFERRED · 0% AMBIGUOUS · INFERRED: 307 edges (avg confidence: 0.82)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `f8a47794`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- JwtOptions
- DraftRedisOperations
- .ProposeAsync
- PlayerShort
- Project Rules & Vendor Licenses
- .CreateClient
- Player
- PlayersFilterSearch
- .League
- DraftSnapshotService
- PlayerSearchInput
- NBAException
- BoxScoreStatsBuilder
- TradeBetweenTeams
- Leagueplayer
- League & Stats Value Requests
- TradeHubFixture
- NbaFantasyContext
- NBA.Data.Redis.Entities
- Applicationuser
- Team
- PlayerDto
- GameDto
- TradeHub
- BoxScoreCalculationBuilder
- League
- DraftEndDraftTests
- DraftTimerHostedService
- NBA.Data.Enumerations
- Player
- GameService
- AppHost Launch Settings
- NBA.Data.Context
- create-objects-nba-schema.sql
- LeagueDto
- GameShort
- ExternalClients Project Files
- BallDontLieClientWireMockTests
- PlayerInfoResponse
- MetaData
- API Launch Profiles
- Playoff
- Transaction
- .InitializeAsync
- PlayerStatsResponse
- Per-League Stats Values
- NbaCalendar
- Usertrophie
- NBA.Data Package References
- Aspire Manifest OTEL Config
- NBA.Api.Requests.Authentication
- PlayerService
- LoginDto
- ITradeOrchestrator
- GameInfoResponse
- .BuildHub
- TeamDto
- FreeAgencyPickUpRequest
- .ToPlayerDb
- .PerformCalculations
- Trade
- UserTeamDto
- Test Project Packages
- TradeDto
- EntityMappings
- NBA.Api Package References
- PlayerManager
- ServiceDefaults Packages
- Entity Mapping Tests
- Aspire Postgres Connection
- Aspire Postgres Container
- DraftService
- ServiceDefaults Extensions
- ScheduledGames
- DraftType
- AppHost Packages
- GameTeamDto
- TeamInfoResponse
- DraftState
- .ToggleFreeAgencyStatus
- Aspire HTTPS Bindings
- ApplicationHostedService
- NBA.Data.Entities
- Draft Status Enum
- TradeOutcome
- PlayerPositionEnum
- AppHost Hosting Packages
- ApplicationDefaults.Exceptions
- Aspire Server Bindings
- Argon2idPasswordHasher
- LeaguePlayerSeedTests
- LeagueTeamDto
- Aspire Password Parameters
- League Team Insert Request
- Draft Request DTO
- DraftLifecycleService
- Chat Schema DDL
- NbaFantasyRedis
- Aspire HTTP Bindings
- PagedResult
- Infrastructure Init Entry
- Naming Rule
- ChatHub
- LockRedisOperations
- .OnModelCreating
- Q: Tell me how individual players are stored in redis
- adding-an-endpoint/SKILL.md
- first-time-setup/SKILL.md

## God Nodes (most connected - your core abstractions)
1. `NbaFantasyContext` - 106 edges
2. `NBAException` - 49 edges
3. `TradeBetweenTeams` - 49 edges
4. `NBA.Data.Entities` - 45 edges
5. `Trade` - 41 edges
6. `PlayerSearchInput` - 40 edges
7. `PlayersFilterSearch` - 38 edges
8. `NBA.Data.Redis.Entities` - 38 edges
9. `Team` - 36 edges
10. `Applicationuser` - 35 edges

## Surprising Connections (you probably didn't know these)
- `DraftService` --references--> `ApplicationOptions`  [EXTRACTED]
  NBA.Service/Draft/DraftService.cs → ApplicationDefaults/Options/ApplicationOptions.cs
- `RosterValidator` --references--> `ApplicationOptions`  [EXTRACTED]
  NBA.Service/Roster/RosterValidator.cs → ApplicationDefaults/Options/ApplicationOptions.cs
- `TradeManager` --references--> `ApplicationOptions`  [EXTRACTED]
  NBA.Service/Trade/TradeManager.cs → ApplicationDefaults/Options/ApplicationOptions.cs
- `TradeOrchestrator` --references--> `ApplicationOptions`  [EXTRACTED]
  NBA.Service/Trade/TradeOrchestrator.cs → ApplicationDefaults/Options/ApplicationOptions.cs
- `GameService` --references--> `BallDontLieClientOptions`  [EXTRACTED]
  NBA.Service/Game/GameService.cs → ApplicationDefaults/Options/BallDontLieClientOptions.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Draft Pick Processing Flow** — claude_drafthub, claude_playermanager, claude_draftmanager, claude_draftstate [EXTRACTED 0.95]
- **Draft Timer & Deadline Coordination** — claude_drafttimerhostedservice, claude_draftredisoperations, claude_draftmanager, claude_draft_realtime [EXTRACTED 0.95]
- **External HTTP Resilience Strategy** — claude_resilience_pipeline_rule, claude_externalclients, claude_nba_servicedefaults [INFERRED 0.85]

## Communities (116 total, 5 thin omitted)

### Community 0 - "JwtOptions"
Cohesion: 0.05
Nodes (36): JwtOptions, AccessTokenMinutes, Audience, Issuer, RefreshTokenDays, SigningKey, ApplyStateContext, NBA.Api.HangFire (+28 more)

### Community 1 - "DraftRedisOperations"
Cohesion: 0.09
Nodes (17): DateTimeOffset, Dictionary, IDatabase, JsonSerializerOptions, List, Queue, Task, TimeSpan (+9 more)

### Community 2 - ".ProposeAsync"
Cohesion: 0.16
Nodes (12): IServiceCollection, TradeExtention, Guid, IOptions, List, Task, TradeManager, Guid (+4 more)

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
Cohesion: 0.04
Nodes (43): DateTime, ICollection, Player, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow (+35 more)

### Community 7 - "PlayersFilterSearch"
Cohesion: 0.05
Nodes (38): NBA.Api.Requests.Player, DateTime, PlayersFilterSearch, allowdrop, gameready, irlteamid, irlteamname, islock (+30 more)

### Community 8 - ".League"
Cohesion: 0.20
Nodes (7): Task, IEndpointRouteBuilder, DraftEndpoints, Task, IOptions, Task, DraftManager

### Community 9 - "DraftSnapshotService"
Cohesion: 0.15
Nodes (8): IServiceCollection, DraftExtention, Dictionary, IOptions, JsonSerializerOptions, Queue, Task, DraftSnapshotService

### Community 10 - "PlayerSearchInput"
Cohesion: 0.06
Nodes (36): PlayerSearchInput, Allowdrop, Gameready, Irlteamid, Irlteamname, Islock, LeagueId, MaxAssists (+28 more)

### Community 11 - "NBAException"
Cohesion: 0.06
Nodes (35): NBAException, ErrorCode, AuthenticateResult, ClaimsPrincipal, Created, Exception, IQueryable, ClaimsPrincipalExtensions (+27 more)

### Community 12 - "BoxScoreStatsBuilder"
Cohesion: 0.07
Nodes (17): BoxScoreStatsBuilder, PlayerStats, ast, blk, fg3a, fg3m, fga, fgm (+9 more)

### Community 13 - "TradeBetweenTeams"
Cohesion: 0.06
Nodes (40): IHubCallerClients, Method, List, Task, ITradeHubClient, DateTimeOffset, Guid, List (+32 more)

### Community 14 - "Leagueplayer"
Cohesion: 0.12
Nodes (13): LeaguePlayerData, Leagueplayer, Isfreeagent, League, Leagueid, Leagueplayerid, Playerid, LeagueService (+5 more)

### Community 15 - "League & Stats Value Requests"
Cohesion: 0.07
Nodes (27): NBA.Api.Requests.League, NBA.Api.Requests.StatValue, LeagueRequest, Autostart, DraftStyle, LeagueName, LeagueType, ScoringSystem (+19 more)

### Community 16 - "TradeHubFixture"
Cohesion: 0.10
Nodes (35): HubConnection, HubException, HubInvocationContext, ICollectionFixture, IConnectionMultiplexer, IHost, IHubFilter, Func (+27 more)

### Community 17 - "NbaFantasyContext"
Cohesion: 0.09
Nodes (25): DbContext, DbSet, NbaFantasyContext, Applicationusers, Draftsnapshots, Leagueplayers, Leagues, Playermementos (+17 more)

### Community 18 - "NBA.Data.Redis.Entities"
Cohesion: 0.17
Nodes (9): NBA.Api.SignalR.Clients, NBA.Tests.Fakes, NBA.Data.Redis.Enumerations, NBA.Service.Draft, NBA.Api.SignalR.Hubs, NBA.Data.Redis.Entities, NBA.Service.Trade, NBA.Service.Roster (+1 more)

### Community 19 - "Applicationuser"
Cohesion: 0.11
Nodes (16): ICollection, Applicationuser, Email, Managerlevel, Password, Teams, Userid, Userleagues (+8 more)

### Community 20 - "Team"
Cohesion: 0.07
Nodes (29): IEndpointRouteBuilder, LeagueEndpoints, IEndpointRouteBuilder, TestingEndpoints, League, ICollection, Team, Approved (+21 more)

### Community 21 - "PlayerDto"
Cohesion: 0.08
Nodes (24): DateTime, PlayerDto, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow, Gameready (+16 more)

### Community 22 - "GameDto"
Cohesion: 0.12
Nodes (16): DateTime, List, GameDto, Date, GameId, HomeTeam, Postponed, Postseason (+8 more)

### Community 23 - "TradeHub"
Cohesion: 0.29
Nodes (6): Guid, ILogger, IReadOnlyList, List, Task, TradeHub

### Community 25 - "League"
Cohesion: 0.09
Nodes (21): ICollection, League, Autostart, Commissioner, Draftcompleted, Draftsnapshot, Draftstyle, Leagueid (+13 more)

### Community 26 - "DraftEndDraftTests"
Cohesion: 0.23
Nodes (11): IEnumerable, List, PlayerShortDto, FullName, PlayerId, Position, PlayerShortMappings, Fact (+3 more)

### Community 27 - "DraftTimerHostedService"
Cohesion: 0.31
Nodes (7): BackgroundService, CancellationToken, ILogger, IServiceProvider, Task, TimeSpan, DraftTimerHostedService

### Community 28 - "NBA.Data.Enumerations"
Cohesion: 0.12
Nodes (9): NBA.Data.Redis.Operations, NBA.Data.Redis.Scopes, NBA.Data.Enumerations, NBA.Data.Redis.Keys, NBA.Data.Redis.Dtos, NBA.Data.Redis, PlayerPositionExtensions, JsonSerializerOptions (+1 more)

### Community 29 - "Player"
Cohesion: 0.13
Nodes (10): ApplicationDefaults.Time, ExternalClients.Response, ExternalClients, NBA.Service.Game, NBA.Service.Player, NBA.Tests.Integration, NBA.Service.CalculateBoxScore, ExternalClients.Poco (+2 more)

### Community 30 - "GameService"
Cohesion: 0.18
Nodes (12): IBackgroundJobClient, IEndpointRouteBuilder, GameEndpoints, DateOnly, Task, TimeSpan, GameManager, CancellationToken (+4 more)

### Community 31 - "AppHost Launch Settings"
Cohesion: 0.13
Nodes (18): ASPIRE_DASHBOARD_OTLP_ENDPOINT_URL, ASPIRE_RESOURCE_SERVICE_ENDPOINT_URL, ASPNETCORE_ENVIRONMENT, DOTNET_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables (+10 more)

### Community 32 - "NBA.Data.Context"
Cohesion: 0.15
Nodes (7): NBA.Data.Context, ApplicationDefaults.LogDefaults, NBA.Data.Constants, IExceptionHandler, GlobalExceptionHandler, ILogger, TradeStatuses

### Community 33 - "create-objects-nba-schema.sql"
Cohesion: 0.22
Nodes (18): nba.applicationuser, nba.draftsnapshot, nba.league, nba.leagueplayer, nba.player, nba.playermemento, nba.playoff, nba.playoffbracket (+10 more)

### Community 34 - "LeagueDto"
Cohesion: 0.14
Nodes (13): LeagueDto, Autostart, Commissioner, CommissionersTeam, Draftstyle, Leagueid, Name, Seasonyear (+5 more)

### Community 35 - "GameShort"
Cohesion: 0.11
Nodes (20): DateTime, GameShort, Date, GameId, HomeTeam, Postponed, Postseason, StartTime (+12 more)

### Community 36 - "ExternalClients Project Files"
Cohesion: 0.13
Nodes (16): ApplicationDefaults, net10.0, Microsoft.NET.Sdk, BoxScoreBuilder, net10.0, Microsoft.NET.Sdk, ExternalClients, net10.0 (+8 more)

### Community 37 - "BallDontLieClientWireMockTests"
Cohesion: 0.10
Nodes (24): IAsyncLifetime, IClassFixture, IRequestMessage, IResponseBuilder, Fact, HttpStatusCode, InlineData, JsonException (+16 more)

### Community 38 - "PlayerInfoResponse"
Cohesion: 0.09
Nodes (21): List, GetAllPlayersResponse, data, meta, PlayerInfoResponse, college, country, draft_number (+13 more)

### Community 39 - "MetaData"
Cohesion: 0.11
Nodes (22): CancellationToken, DateOnly, HttpResponseMessage, List, Task, BallDontLieClient, CancellationToken, DateOnly (+14 more)

### Community 40 - "API Launch Profiles"
Cohesion: 0.13
Nodes (15): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, applicationUrl, commandName (+7 more)

### Community 41 - "Playoff"
Cohesion: 0.12
Nodes (14): ICollection, Playoff, League, Leagueid, Playoffbrackets, Playoffid, Totalrounds, Playoffbracket (+6 more)

### Community 42 - "Transaction"
Cohesion: 0.12
Nodes (14): DateTime, ICollection, Transaction, Transactionid, Transactionleagues, Transactionstatus, Tscreated, Typetransaction (+6 more)

### Community 43 - ".InitializeAsync"
Cohesion: 0.25
Nodes (7): ApplicationOptions, CenterLimit, MaxPlayersPerTeam, ProposedTradeTtlMinutes, AuthenticationHandler, AuthenticationSchemeOptions, TestAuthHandler

### Community 44 - "PlayerStatsResponse"
Cohesion: 0.13
Nodes (14): PlayerStatsResponse, ast, blk, fg3a, fg3m, fga, fgm, fta (+6 more)

### Community 45 - "Per-League Stats Values"
Cohesion: 0.13
Nodes (14): Statsvalue, Assistsvalue, Blocksvalue, Fieldgoalvaluemade, Fieldgoalvaluemissed, Freethrowvaluemade, Freethrowvaluemissed, League (+6 more)

### Community 46 - "NbaCalendar"
Cohesion: 0.24
Nodes (5): NbaCalendar, DateOnly, InlineData, Theory, TimeZoneInfo

### Community 47 - "Usertrophie"
Cohesion: 0.14
Nodes (12): ICollection, Trophie, Trophieid, Typetrophie, Usertrophies, Xp, Usertrophie, Trophie (+4 more)

### Community 48 - "NBA.Data Package References"
Cohesion: 0.14
Nodes (13): net10.0, Aspire.Hosting.Redis (13.1.2), Microsoft.Extensions.Configuration.Abstractions (10.0.0), Microsoft.NET.Sdk, MessagePack (2.5.302), Microsoft.EntityFrameworkCore (10.0.0), Microsoft.EntityFrameworkCore.Design (10.0.0), Microsoft.Extensions.Configuration (10.0.0) (+5 more)

### Community 49 - "Aspire Manifest OTEL Config"
Cohesion: 0.14
Nodes (14): ASPNETCORE_FORWARDEDHEADERS_ENABLED, ConnectionStrings__nbafantasydb, HTTP_PORTS, NBAFANTASYDB_DATABASENAME, NBAFANTASYDB_HOST, NBAFANTASYDB_JDBCCONNECTIONSTRING, NBAFANTASYDB_PASSWORD, NBAFANTASYDB_PORT (+6 more)

### Community 50 - "NBA.Api.Requests.Authentication"
Cohesion: 0.15
Nodes (10): NBA.Api.Requests.Authentication, LoginRequestNBA, Password, Username, RefreshRequest, RefreshToken, SignUpRequest, Email (+2 more)

### Community 51 - "PlayerService"
Cohesion: 0.23
Nodes (8): AutomaticRetry, JobDisplayName, DateTime, IReadOnlyList, List, PagedResult, Task, PlayerService

### Community 52 - "LoginDto"
Cohesion: 0.22
Nodes (8): List, LoginDto, Leagues, RefreshToken, Teams, Token, Userid, Username

### Community 53 - "ITradeOrchestrator"
Cohesion: 0.43
Nodes (4): Guid, List, Task, ITradeOrchestrator

### Community 54 - "GameInfoResponse"
Cohesion: 0.11
Nodes (18): DateTime, GameInfoResponse, date, datetime, home_team, home_team_score, id, postponed (+10 more)

### Community 55 - ".BuildHub"
Cohesion: 0.27
Nodes (11): Clients, Accepted, OfferedToLeague, Rejected, Superseded, TradeEvent, Fact, Hub (+3 more)

### Community 56 - "TeamDto"
Cohesion: 0.13
Nodes (13): List, DraftOrderDto, Round, Teams, TeamDto, Categoryleaguepoints, Competesinleague, Islock (+5 more)

### Community 57 - "FreeAgencyPickUpRequest"
Cohesion: 0.33
Nodes (5): NBA.Api.Requests.FreeAgency, List, FreeAgencyPickUpRequest, leagueId, playerIds

### Community 58 - ".ToPlayerDb"
Cohesion: 0.20
Nodes (7): List, PlayerData, Adapter, Fact, InlineData, Theory, AdapterTests

### Community 59 - ".PerformCalculations"
Cohesion: 0.40
Nodes (4): Dictionary, List, Task, BoxScoreCalculationService

### Community 60 - "Trade"
Cohesion: 0.12
Nodes (15): DateTime, Guid, List, Trade, Fromteam, Fromteamid, League, Leagueid (+7 more)

### Community 61 - "UserTeamDto"
Cohesion: 0.15
Nodes (12): List, UserTeamDto, Categoryleaguepoints, Islock, Lastweekpoints, Leagueid, Leaguename, Name (+4 more)

### Community 62 - "Test Project Packages"
Cohesion: 0.17
Nodes (12): NBA.Tests, net10.0, Microsoft.NET.Sdk, coverlet.collector (6.0.2), Microsoft.AspNetCore.SignalR.Client (10.0.0), Microsoft.AspNetCore.TestHost (10.0.0), Microsoft.EntityFrameworkCore.InMemory (10.0.0), Microsoft.NET.Test.Sdk (17.12.0) (+4 more)

### Community 64 - "TradeDto"
Cohesion: 0.15
Nodes (12): DateTime, Guid, List, TradeDto, Fromteamid, Leagueid, Playerids, Status (+4 more)

### Community 66 - "EntityMappings"
Cohesion: 0.22
Nodes (3): List, Team, EntityMappings

### Community 67 - "NBA.Api Package References"
Cohesion: 0.18
Nodes (10): net10.0, Aspire.StackExchange.Redis (13.1.2), Microsoft.Extensions.Http.Resilience (10.1.0), Aspire.Npgsql.EntityFrameworkCore.PostgreSQL (13.1.0), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.0), Microsoft.AspNetCore.OpenApi (10.0.0), Microsoft.AspNetCore.SignalR.StackExchangeRedis (10.0.5), Microsoft.OpenApi (2.7.5) (+2 more)

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

### Community 75 - "DraftService"
Cohesion: 0.14
Nodes (15): DraftOptions, DraftPickTime, Rounds, ShowTeamDraftBoardCount, IHubContext, IOptions, DraftTimerProcessor, Task (+7 more)

### Community 76 - "ServiceDefaults Extensions"
Cohesion: 0.22
Nodes (3): Microsoft.Extensions.Hosting, Extensions, WebApplication

### Community 77 - "ScheduledGames"
Cohesion: 0.19
Nodes (10): List, ScheduledGames, RestOfWeek, Today, Tomorrow, IDatabase, JsonSerializerOptions, Task (+2 more)

### Community 78 - "DraftType"
Cohesion: 0.29
Nodes (6): DraftType, Auction, Linear, Offline, RRR, Snake

### Community 79 - "AppHost Packages"
Cohesion: 0.22
Nodes (8): net10.0, Aspire.Hosting.Redis (13.1.2), Aspire.StackExchange.Redis (13.1.2), Microsoft.NET.Sdk, Aspire.Hosting.AppHost (13.1.0), Aspire.Hosting.PostgreSQL (13.1.0), CommunityToolkit.Aspire.Hosting.NodeJS.Extensions (9.9.0), OpenTelemetry.Api (1.16.0)

### Community 80 - "GameTeamDto"
Cohesion: 0.33
Nodes (6): GameTeamDto, Abbreviation, City, FullName, Score, TeamId

### Community 81 - "TeamInfoResponse"
Cohesion: 0.17
Nodes (12): List, GetAllTeamsResponse, data, meta, TeamInfoResponse, abbreviation, city, conference (+4 more)

### Community 82 - "DraftState"
Cohesion: 0.20
Nodes (10): DateTime, Dictionary, List, DraftState, DraftBoardTeams, DraftedPlayersPerTeam, DraftPlayers, DraftStatus (+2 more)

### Community 84 - ".ToggleFreeAgencyStatus"
Cohesion: 0.21
Nodes (7): IEndpointRouteBuilder, FreeAgencyEndpoints, IServiceCollection, FreeAgencyExtention, List, Task, FreeAgencyService

### Community 85 - "Aspire HTTPS Bindings"
Cohesion: 0.25
Nodes (8): https, protocol, scheme, transport, bindings, path, type, nba-api

### Community 86 - "ApplicationHostedService"
Cohesion: 0.07
Nodes (27): ErrorResponse, ErrorCode, ErrorMessage, Log, message, request, response, BallDontLieClientOptions (+19 more)

### Community 88 - "NBA.Data.Entities"
Cohesion: 0.12
Nodes (10): NBA.Data.Entities, NBA.Api.DTOs, NBA.Service.FreeAgency, NBA.Service.Authentication, NBA.Api.Mappings, NBA.Api.Authentication, NBA.Tests, NBA.Service (+2 more)

### Community 89 - "Draft Status Enum"
Cohesion: 0.29
Nodes (6): DraftStatus, DraftCompleted, DraftEnded, DraftStarted, Initial, Paused

### Community 90 - "TradeOutcome"
Cohesion: 0.33
Nodes (6): IReadOnlyList, TradeOutcome, Guid, List, TradeData, FakeTradeOrchestrator

### Community 91 - "PlayerPositionEnum"
Cohesion: 0.22
Nodes (8): PlayerPositionEnum, C, CF, F, FG, G, GF, UNKOWN

### Community 92 - "AppHost Hosting Packages"
Cohesion: 0.29
Nodes (7): NBA.Service, net10.0, Aspire.Hosting.Redis (13.1.2), Microsoft.Extensions.Options (10.0.3), Microsoft.NET.Sdk, Isopoh.Cryptography.Argon2 (1.1.10), Microsoft.Extensions.Identity.Core (10.0.0)

### Community 93 - "ApplicationDefaults.Exceptions"
Cohesion: 0.13
Nodes (11): ErrorCodes, NBA.Api.Draft, NBA.Service.League, NBA.Api.HostedService, ApplicationDefaults.Exceptions, NBA.Api.Requests.Team, NBA.Api.SignalR, NBA.Service.LeaguePlayer (+3 more)

### Community 94 - "Aspire Server Bindings"
Cohesion: 0.29
Nodes (7): tcp, bindings, port, protocol, scheme, targetPort, transport

### Community 95 - "Argon2idPasswordHasher"
Cohesion: 0.16
Nodes (10): Argon2Options, DegreeOfParallelism, Iterations, MemoryKib, IPasswordHasher, IOptions, Argon2idPasswordHasher, Fact (+2 more)

### Community 96 - "LeaguePlayerSeedTests"
Cohesion: 0.42
Nodes (3): Fact, Task, LeaguePlayerSeedTests

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
Cohesion: 0.12
Nodes (18): List, DraftBoardTeams, CurrentRound, DraftOrder, onTheClockTeam, TeamDraftBoard, Pick, TeamId (+10 more)

### Community 102 - "Chat Schema DDL"
Cohesion: 0.70
Nodes (4): chat.conversationparticipants, chat.messages, chat.rooms, nba.applicationuser

### Community 103 - "NbaFantasyRedis"
Cohesion: 0.14
Nodes (14): Lazy, IDatabase, NbaFantasyRedis, Auth, Draft, Game, Lock, Player (+6 more)

### Community 104 - "Aspire HTTP Bindings"
Cohesion: 0.50
Nodes (4): http, protocol, scheme, transport

### Community 105 - "PagedResult"
Cohesion: 0.25
Nodes (5): IEndpointRouteBuilder, PlayerEndpoints, IReadOnlyList, PagedResult, TotalPages

### Community 116 - "ChatHub"
Cohesion: 0.25
Nodes (5): Hub, Task, IChatHubClient, Task, ChatHub

### Community 121 - "LockRedisOperations"
Cohesion: 0.33
Nodes (4): IDatabase, Task, TimeSpan, LockRedisOperations

### Community 123 - ".OnModelCreating"
Cohesion: 0.11
Nodes (15): ModelBuilder, Player, Trade, DateTime, Draftsnapshot, Draftstate, Draftteams, Leagueid (+7 more)

### Community 125 - "Q: Tell me how individual players are stored in redis"
Cohesion: 0.40
Nodes (4): Answer, Outcome, Q: Tell me how individual players are stored in redis, Source Nodes

## Knowledge Gaps
- **701 isolated node(s):** `net10.0`, `Microsoft.NET.Sdk`, `ErrorCodes`, `ErrorMessage`, `ErrorCode` (+696 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **5 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `NbaFantasyContext` connect `NbaFantasyContext` to `JwtOptions`, `Player`, `DraftSnapshotService`, `NBAException`, `Leagueplayer`, `TradeHubFixture`, `Applicationuser`, `Team`, `DraftEndDraftTests`, `NBA.Data.Context`, `Playoff`, `Transaction`, `.InitializeAsync`, `Usertrophie`, `PlayerService`, `.PerformCalculations`, `PlayerManager`, `DraftService`, `.ToggleFreeAgencyStatus`, `ApplicationHostedService`, `LeaguePlayerSeedTests`, `DraftLifecycleService`, `.OnModelCreating`?**
  _High betweenness centrality (0.176) - this node is a cross-community bridge._
- **Why does `NBAException` connect `NBAException` to `JwtOptions`, `LeaguePlayerSeedTests`, `.ProposeAsync`, `DraftLifecycleService`, `.CreateClient`, `MetaData`, `.League`, `DraftSnapshotService`, `BallDontLieClientWireMockTests`, `Leagueplayer`, `Applicationuser`, `.ToggleFreeAgencyStatus`, `Team`, `PlayerService`?**
  _High betweenness centrality (0.092) - this node is a cross-community bridge._
- **Why does `NbaFantasyRedis` connect `NbaFantasyRedis` to `JwtOptions`, `DraftRedisOperations`, `.ProposeAsync`, `PlayerShort`, `.League`, `DraftSnapshotService`, `TradeBetweenTeams`, `TradeHubFixture`, `DraftTimerHostedService`, `NBA.Data.Enumerations`, `GameService`, `.InitializeAsync`, `PlayerService`, `PlayerManager`, `DraftService`, `ScheduledGames`, `ApplicationHostedService`, `DraftLifecycleService`, `LockRedisOperations`?**
  _High betweenness centrality (0.089) - this node is a cross-community bridge._
- **Are the 34 inferred relationships involving `NBAException` (e.g. with `.GetAsync()` and `.RefreshAsync()`) actually correct?**
  _`NBAException` has 34 INFERRED edges - model-reasoned connections that need verification._
- **What connects `net10.0`, `Microsoft.NET.Sdk`, `ErrorCodes` to the rest of the system?**
  _701 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `JwtOptions` be split into smaller, more focused modules?**
  _Cohesion score 0.05202661826981246 - nodes in this community are weakly interconnected._
- **Should `DraftRedisOperations` be split into smaller, more focused modules?**
  _Cohesion score 0.08816326530612245 - nodes in this community are weakly interconnected._