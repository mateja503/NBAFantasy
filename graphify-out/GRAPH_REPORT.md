# Graph Report - NBAFantasy  (2026-08-29)

## Corpus Check
- 184 files · ~52,635 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2026 nodes · 3670 edges · 120 communities (108 shown, 12 thin omitted)
- Extraction: 92% EXTRACTED · 8% INFERRED · 0% AMBIGUOUS · INFERRED: 284 edges (avg confidence: 0.82)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `d0665615`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- RecordingTradeHubClients
- DraftRedisOperations
- TradeService
- PlayerShort
- Project Rules & Vendor Licenses
- NBAException
- Player
- Player Search Request DTOs
- DraftState
- ApplicationHostedService
- Player Service Search
- BallDontLieClientWireMockTests
- BoxScore Stats Builder
- PlayerShortDto
- TradeRedisOperations
- League & Stats Value Requests
- TradeHubFixture
- NbaFantasyContext
- JwtOptions
- Applicationuser
- NBA.Data.Enumerations
- PlayerDto
- GameDto
- NBA.Data.Entities
- Team
- League Entity
- NBA.Data.Redis.Entities
- NbaFantasyRedis
- NBA.Data.Redis.Operations
- ApplicationDefaults.Exceptions
- TradeHub
- AppHost Launch Settings
- TradeBetweenTeams
- Core Database Schema DDL
- LeagueDto
- Game Redis Shapes
- ExternalClients Project Files
- BallDontLieWireMockFixture
- PlayerInfo Response Shape
- BallDontLieClient
- API Launch Profiles
- Playoff Bracket Entities
- Transaction Entities
- ApplicationDefaults.LogDefaults
- PlayerStatsResponse
- Per-League Stats Values
- PlayerService
- .OnModelCreating
- NBA.Data Package References
- Aspire Manifest OTEL Config
- Auth Request DTOs
- MetaData
- LoginDto
- TradeStatuses.cs
- GameInfoResponse
- GameService
- TeamDto
- Leagueplayer
- .ToPlayerDb
- TeamDraftBoard
- Trade
- UserTeamDto
- Test Project Packages
- Argon2idPasswordHasher
- NBA Calendar Date Handling
- TradeDto
- EntityMappings
- NBA.Api Package References
- PlayerPositionEnum
- BoxScoreCalculationBuilder
- PlayerManager
- ServiceDefaults Packages
- Entity Mapping Tests
- Aspire Postgres Connection
- Aspire Postgres Container
- JsonSerializerOptions
- ServiceDefaults Extensions
- Dictionary
- GameRedisOperations
- AppHost Packages
- DateTime
- TeamInfoResponse
- Trade
- ScheduledGames
- .BucketByDay
- Aspire HTTPS Bindings
- IServiceProvider
- IBallDontLieClient
- Draft Type Enum
- Draft Status Enum
- Redis Lock Operations
- Player
- AppHost Hosting Packages
- Aspire Server Bindings
- DraftLifecycleService
- NBA.Api.DTOs
- Aspire Password Parameters
- League Team Insert Request
- Draft Request DTO
- NBA.Service.Trade
- Chat Schema DDL
- Aspire HTTP Bindings
- .MapPlayerEndpoints
- Infrastructure Init Entry
- Naming Rule
- League
- Task
- DraftTimerProcessor
- DraftService
- .ToGameRedis
- Team
- Player

## God Nodes (most connected - your core abstractions)
1. `NbaFantasyContext` - 81 edges
2. `NBAException` - 45 edges
3. `PlayerSearchInput` - 40 edges
4. `NBA.Data.Entities` - 40 edges
5. `PlayersFilterSearch` - 38 edges
6. `Applicationuser` - 35 edges
7. `Team` - 35 edges
8. `DraftState` - 31 edges
9. `Player` - 31 edges
10. `League` - 30 edges

## Surprising Connections (you probably didn't know these)
- `BallDontLieWireMockFixture` --references--> `IBallDontLieClient`  [EXTRACTED]
  NBA.Tests/Integration/BallDontLieWireMockFixture.cs → ExternalClients/IBallDontLieClient.cs
- `JoinLeagueResult` --references--> `Team`  [EXTRACTED]
  NBA.Service/League/LeagueService.cs → NBA.Data/Entities/Team.cs
- `LeagueService` --references--> `NbaFantasyContext`  [EXTRACTED]
  NBA.Service/League/LeagueService.cs → NBA.Data/Context/NbaFantasyContext.cs
- `DraftTimerProcessor` --references--> `DraftService`  [EXTRACTED]
  NBA.Api/Draft/DraftTimerProcessor.cs → NBA.Service/Draft/DraftService.cs
- `DraftHub` --references--> `DraftService`  [EXTRACTED]
  NBA.Api/SignalR/Hubs/DraftHub.cs → NBA.Service/Draft/DraftService.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Draft Pick Processing Flow** — claude_drafthub, claude_playermanager, claude_draftmanager, claude_draftstate [EXTRACTED 0.95]
- **Draft Timer & Deadline Coordination** — claude_drafttimerhostedservice, claude_draftredisoperations, claude_draftmanager, claude_draft_realtime [EXTRACTED 0.95]
- **External HTTP Resilience Strategy** — claude_resilience_pipeline_rule, claude_externalclients, claude_nba_servicedefaults [INFERRED 0.85]

## Communities (120 total, 12 thin omitted)

### Community 0 - "RecordingTradeHubClients"
Cohesion: 0.16
Nodes (15): IHubCallerClients, Method, IReadOnlyList, ITradeHubClient, List, Task, TradeBetweenTeams, Recorder (+7 more)

### Community 1 - "DraftRedisOperations"
Cohesion: 0.09
Nodes (17): DateTimeOffset, Dictionary, IDatabase, JsonSerializerOptions, List, Queue, Task, TimeSpan (+9 more)

### Community 2 - "TradeService"
Cohesion: 0.07
Nodes (33): Created, DateTime, IQueryable, IEndpointRouteBuilder, TradeEndpoints, Trade, ApplicationOptions, Guid (+25 more)

### Community 3 - "PlayerShort"
Cohesion: 0.06
Nodes (29): IEnumerable, List, PlayerShortMappings, PlayerShort, FullName, PlayerId, Position, RedisKeys (+21 more)

### Community 4 - "Project Rules & Vendor Licenses"
Cohesion: 0.05
Nodes (51): Adapter (static mapper), Adding an HTTP Endpoint Flow, ApplicationDefaults, ApplicationOptions, Argon2Options, Aspire AppHost, Auth & Tests, Authenticate Everything Rule (+43 more)

### Community 5 - "NBAException"
Cohesion: 0.09
Nodes (31): Action, NBAException, ErrorCode, ClaimsPrincipal, Exception, HttpMessageHandler, HttpRequestMessage, ClaimsPrincipalExtensions (+23 more)

### Community 6 - "Player"
Cohesion: 0.05
Nodes (43): DateTime, ICollection, Player, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow (+35 more)

### Community 7 - "Player Search Request DTOs"
Cohesion: 0.05
Nodes (38): NBA.Api.Requests.Player, DateTime, PlayersFilterSearch, allowdrop, gameready, irlteamid, irlteamname, islock (+30 more)

### Community 8 - "DraftState"
Cohesion: 0.14
Nodes (18): IOptions, Task, DraftHub, DateTime, Dictionary, List, DraftState, DraftBoardTeams (+10 more)

### Community 9 - "ApplicationHostedService"
Cohesion: 0.07
Nodes (26): ErrorResponse, ErrorCode, ErrorMessage, Log, message, request, response, HttpContext (+18 more)

### Community 10 - "Player Service Search"
Cohesion: 0.06
Nodes (36): PlayerSearchInput, Allowdrop, Gameready, Irlteamid, Irlteamname, Islock, LeagueId, MaxAssists (+28 more)

### Community 11 - "BallDontLieClientWireMockTests"
Cohesion: 0.16
Nodes (15): IClassFixture, IRequestMessage, IResponseBuilder, Fact, HttpStatusCode, InlineData, JsonException, OperationCanceledException (+7 more)

### Community 12 - "BoxScore Stats Builder"
Cohesion: 0.07
Nodes (17): BoxScoreStatsBuilder, PlayerStats, ast, blk, fg3a, fg3m, fga, fgm (+9 more)

### Community 13 - "PlayerShortDto"
Cohesion: 0.15
Nodes (13): ApplicationOptions, CenterLimit, MaxPlayersPerTeam, ProposedTradeTtlMinutes, PlayerShortDto, FullName, PlayerId, Position (+5 more)

### Community 14 - "TradeRedisOperations"
Cohesion: 0.10
Nodes (22): JsonSerializerOptions, Guid, IDatabase, List, Task, TimeSpan, TradeBetweenTeams, TradeRedisOperations (+14 more)

### Community 15 - "League & Stats Value Requests"
Cohesion: 0.07
Nodes (27): NBA.Api.Requests.League, NBA.Api.Requests.StatValue, LeagueRequest, Autostart, DraftStyle, LeagueName, LeagueType, ScoringSystem (+19 more)

### Community 16 - "TradeHubFixture"
Cohesion: 0.07
Nodes (50): AuthenticateResult, AuthenticationHandler, AuthenticationSchemeOptions, NBA.Api.SignalR, HubConnection, HubException, HubInvocationContext, ICollectionFixture (+42 more)

### Community 17 - "NbaFantasyContext"
Cohesion: 0.07
Nodes (27): DbContext, DbSet, NbaFantasyContext, Applicationusers, Draftsnapshots, Leagueplayers, Leagues, Playermementos (+19 more)

### Community 18 - "JwtOptions"
Cohesion: 0.05
Nodes (34): JwtOptions, AccessTokenMinutes, Audience, Issuer, RefreshTokenDays, SigningKey, ApplyStateContext, NBA.Api.HangFire (+26 more)

### Community 19 - "Applicationuser"
Cohesion: 0.10
Nodes (18): IEndpointRouteBuilder, AuthenticationEndpoints, ICollection, Applicationuser, Email, Managerlevel, Password, Teams (+10 more)

### Community 20 - "NBA.Data.Enumerations"
Cohesion: 0.10
Nodes (10): ApplicationDefaults.Time, NBA.Service.League, NBA.Data.Enumerations, NBA.Service.Game, NBA.Api.Mappings, NBA.Tests, NBA.Service, NBA.Api.Endpoints (+2 more)

### Community 21 - "PlayerDto"
Cohesion: 0.08
Nodes (24): DateTime, PlayerDto, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow, Gameready (+16 more)

### Community 22 - "GameDto"
Cohesion: 0.09
Nodes (22): DateTime, List, GameDto, Date, GameId, HomeTeam, Postponed, Postseason (+14 more)

### Community 23 - "NBA.Data.Entities"
Cohesion: 0.19
Nodes (7): NBA.Data.Entities, NBA.Api.HostedService, NBA.Data.Context, NBA.Service.Authentication, NBA.Api.Authentication, NBA.Service.Roster, ApplicationDefaults.Options

### Community 24 - "Team"
Cohesion: 0.09
Nodes (24): IEndpointRouteBuilder, TeamEndpoints, ICollection, Team, Approved, Categoryleaguepoints, Islock, Lastweekpoints (+16 more)

### Community 25 - "League Entity"
Cohesion: 0.09
Nodes (21): ICollection, League, Autostart, Commissioner, Draftcompleted, Draftsnapshot, Draftstyle, Leagueid (+13 more)

### Community 26 - "NBA.Data.Redis.Entities"
Cohesion: 0.26
Nodes (6): NBA.Api.Draft, NBA.Api.SignalR.Clients, NBA.Data.Redis.Enumerations, NBA.Service.Draft, NBA.Api.SignalR.Hubs, NBA.Data.Redis.Entities

### Community 27 - "NbaFantasyRedis"
Cohesion: 0.13
Nodes (16): BackgroundService, Lazy, CancellationToken, ILogger, IServiceProvider, Task, TimeSpan, DraftTimerHostedService (+8 more)

### Community 28 - "NBA.Data.Redis.Operations"
Cohesion: 0.14
Nodes (7): NBA.Data.Redis.Operations, NBA.Data.Redis.Scopes, NBA.Data.Redis.Keys, NBA.Data.Redis.Dtos, NBA.Data.Redis, JsonSerializerOptions, RedisSerializer

### Community 29 - "ApplicationDefaults.Exceptions"
Cohesion: 0.20
Nodes (6): ErrorCodes, ExternalClients.Response, ExternalClients, NBA.Service.Player, ApplicationDefaults.Exceptions, ExternalClients.Poco

### Community 30 - "TradeHub"
Cohesion: 0.07
Nodes (38): Clients, Hub, Task, IChatHubClient, Task, ChatHub, Guid, ILogger (+30 more)

### Community 31 - "AppHost Launch Settings"
Cohesion: 0.13
Nodes (18): ASPIRE_DASHBOARD_OTLP_ENDPOINT_URL, ASPIRE_RESOURCE_SERVICE_ENDPOINT_URL, ASPNETCORE_ENVIRONMENT, DOTNET_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables (+10 more)

### Community 32 - "TradeBetweenTeams"
Cohesion: 0.16
Nodes (12): List, Task, ITradeHubClient, DateTimeOffset, Guid, List, TradeBetweenTeams, FromTeam (+4 more)

### Community 33 - "Core Database Schema DDL"
Cohesion: 0.24
Nodes (17): nba.applicationuser, nba.draftsnapshot, nba.league, nba.leagueplayer, nba.player, nba.playermemento, nba.playoff, nba.playoffbracket (+9 more)

### Community 34 - "LeagueDto"
Cohesion: 0.15
Nodes (13): LeagueDto, Autostart, Commissioner, CommissionersTeam, Draftstyle, Leagueid, Name, Seasonyear (+5 more)

### Community 35 - "Game Redis Shapes"
Cohesion: 0.12
Nodes (17): DateTime, GameShort, Date, GameId, HomeTeam, Postponed, Postseason, StartTime (+9 more)

### Community 36 - "ExternalClients Project Files"
Cohesion: 0.13
Nodes (16): ApplicationDefaults, net10.0, Microsoft.NET.Sdk, BoxScoreBuilder, net10.0, Microsoft.NET.Sdk, ExternalClients, net10.0 (+8 more)

### Community 37 - "BallDontLieWireMockFixture"
Cohesion: 0.12
Nodes (13): BallDontLieClientOptions, ApiKey, BaseUrl, Per_Page, IAsyncLifetime, HttpResponseMessage, IOptions, Task (+5 more)

### Community 38 - "PlayerInfo Response Shape"
Cohesion: 0.12
Nodes (16): PlayerInfoResponse, college, country, draft_number, draft_round, draft_year, first_name, height (+8 more)

### Community 39 - "BallDontLieClient"
Cohesion: 0.21
Nodes (9): CancellationToken, DateOnly, HttpResponseMessage, List, Task, BallDontLieClient, HttpClient, ResiliencePipeline (+1 more)

### Community 40 - "API Launch Profiles"
Cohesion: 0.13
Nodes (15): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, applicationUrl, commandName (+7 more)

### Community 41 - "Playoff Bracket Entities"
Cohesion: 0.12
Nodes (14): ICollection, Playoff, League, Leagueid, Playoffbrackets, Playoffid, Totalrounds, Playoffbracket (+6 more)

### Community 42 - "Transaction Entities"
Cohesion: 0.12
Nodes (14): DateTime, ICollection, Transaction, Transactionid, Transactionleagues, Transactionstatus, Tscreated, Typetransaction (+6 more)

### Community 44 - "PlayerStatsResponse"
Cohesion: 0.13
Nodes (14): PlayerStatsResponse, ast, blk, fg3a, fg3m, fga, fgm, fta (+6 more)

### Community 45 - "Per-League Stats Values"
Cohesion: 0.13
Nodes (14): Statsvalue, Assistsvalue, Blocksvalue, Fieldgoalvaluemade, Fieldgoalvaluemissed, Freethrowvaluemade, Freethrowvaluemissed, League (+6 more)

### Community 46 - "PlayerService"
Cohesion: 0.20
Nodes (9): AutomaticRetry, JobDisplayName, CancellationToken, DateTime, IReadOnlyList, List, PagedResult, Task (+1 more)

### Community 47 - ".OnModelCreating"
Cohesion: 0.08
Nodes (21): ModelBuilder, Player, Trade, ICollection, Trophie, Trophieid, Typetrophie, Usertrophies (+13 more)

### Community 48 - "NBA.Data Package References"
Cohesion: 0.14
Nodes (13): net10.0, Aspire.Hosting.Redis (13.1.2), Microsoft.Extensions.Configuration.Abstractions (10.0.0), Microsoft.NET.Sdk, MessagePack (2.5.302), Microsoft.EntityFrameworkCore (10.0.0), Microsoft.EntityFrameworkCore.Design (10.0.0), Microsoft.Extensions.Configuration (10.0.0) (+5 more)

### Community 49 - "Aspire Manifest OTEL Config"
Cohesion: 0.14
Nodes (14): ASPNETCORE_FORWARDEDHEADERS_ENABLED, ConnectionStrings__nbafantasydb, HTTP_PORTS, NBAFANTASYDB_DATABASENAME, NBAFANTASYDB_HOST, NBAFANTASYDB_JDBCCONNECTIONSTRING, NBAFANTASYDB_PASSWORD, NBAFANTASYDB_PORT (+6 more)

### Community 50 - "Auth Request DTOs"
Cohesion: 0.15
Nodes (10): NBA.Api.Requests.Authentication, LoginRequestNBA, Password, Username, RefreshRequest, RefreshToken, SignUpRequest, Email (+2 more)

### Community 51 - "MetaData"
Cohesion: 0.15
Nodes (12): MetaData, Next_cursor, Per_page, Prev_Cursor, List, GetAllPlayersResponse, data, meta (+4 more)

### Community 52 - "LoginDto"
Cohesion: 0.22
Nodes (8): List, LoginDto, Leagues, RefreshToken, Teams, Token, Userid, Username

### Community 54 - "GameInfoResponse"
Cohesion: 0.15
Nodes (13): DateTime, GameInfoResponse, date, datetime, home_team, home_team_score, id, postponed (+5 more)

### Community 55 - "GameService"
Cohesion: 0.26
Nodes (8): IBackgroundJobClient, IEndpointRouteBuilder, GameEndpoints, CancellationToken, IOptions, List, Task, GameService

### Community 56 - "TeamDto"
Cohesion: 0.14
Nodes (13): List, DraftOrderDto, Round, Teams, TeamDto, Categoryleaguepoints, Competesinleague, Islock (+5 more)

### Community 57 - "Leagueplayer"
Cohesion: 0.17
Nodes (9): Leagueplayer, Isfreeagent, League, Leagueid, Leagueplayerid, Playerid, List, Task (+1 more)

### Community 58 - ".ToPlayerDb"
Cohesion: 0.26
Nodes (5): PlayerData, Fact, InlineData, Theory, AdapterTests

### Community 59 - "TeamDraftBoard"
Cohesion: 0.12
Nodes (18): List, DraftBoardTeams, CurrentRound, DraftOrder, onTheClockTeam, TeamDraftBoard, Pick, TeamId (+10 more)

### Community 60 - "Trade"
Cohesion: 0.13
Nodes (15): DateTime, Guid, List, Trade, Fromteam, Fromteamid, League, Leagueid (+7 more)

### Community 61 - "UserTeamDto"
Cohesion: 0.17
Nodes (12): List, UserTeamDto, Categoryleaguepoints, Islock, Lastweekpoints, Leagueid, Leaguename, Name (+4 more)

### Community 62 - "Test Project Packages"
Cohesion: 0.17
Nodes (12): NBA.Tests, net10.0, Microsoft.NET.Sdk, coverlet.collector (6.0.2), Microsoft.AspNetCore.SignalR.Client (10.0.0), Microsoft.AspNetCore.TestHost (10.0.0), Microsoft.EntityFrameworkCore.InMemory (10.0.0), Microsoft.NET.Test.Sdk (17.12.0) (+4 more)

### Community 63 - "Argon2idPasswordHasher"
Cohesion: 0.16
Nodes (10): Argon2Options, DegreeOfParallelism, Iterations, MemoryKib, IPasswordHasher, IOptions, Argon2idPasswordHasher, Fact (+2 more)

### Community 64 - "NBA Calendar Date Handling"
Cohesion: 0.24
Nodes (5): NbaCalendar, DateOnly, InlineData, Theory, TimeZoneInfo

### Community 65 - "TradeDto"
Cohesion: 0.17
Nodes (12): DateTime, Guid, List, TradeDto, Fromteamid, Leagueid, Playerids, Status (+4 more)

### Community 66 - "EntityMappings"
Cohesion: 0.24
Nodes (3): List, Team, EntityMappings

### Community 67 - "NBA.Api Package References"
Cohesion: 0.18
Nodes (10): net10.0, Aspire.StackExchange.Redis (13.1.2), Microsoft.Extensions.Http.Resilience (10.1.0), Aspire.Npgsql.EntityFrameworkCore.PostgreSQL (13.1.0), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.0), Microsoft.AspNetCore.OpenApi (10.0.0), Microsoft.AspNetCore.SignalR.StackExchangeRedis (10.0.5), Microsoft.OpenApi (2.7.5) (+2 more)

### Community 68 - "PlayerPositionEnum"
Cohesion: 0.22
Nodes (8): PlayerPositionEnum, C, CF, F, FG, G, GF, UNKOWN

### Community 69 - "BoxScoreCalculationBuilder"
Cohesion: 0.10
Nodes (9): NBA.Service.Builder, Dictionary, BoxScoreCalculationBuilder, List, NbaFantasyContext, Task, BoxScoreCalculationService, PlayerData (+1 more)

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

### Community 76 - "ServiceDefaults Extensions"
Cohesion: 0.22
Nodes (3): Microsoft.Extensions.Hosting, Extensions, WebApplication

### Community 78 - "GameRedisOperations"
Cohesion: 0.32
Nodes (5): IDatabase, JsonSerializerOptions, Task, TimeSpan, GameRedisOperations

### Community 79 - "AppHost Packages"
Cohesion: 0.22
Nodes (8): net10.0, Aspire.Hosting.Redis (13.1.2), Aspire.StackExchange.Redis (13.1.2), Microsoft.NET.Sdk, Aspire.Hosting.AppHost (13.1.0), Aspire.Hosting.PostgreSQL (13.1.0), CommunityToolkit.Aspire.Hosting.NodeJS.Extensions (9.9.0), OpenTelemetry.Api (1.16.0)

### Community 81 - "TeamInfoResponse"
Cohesion: 0.25
Nodes (8): TeamInfoResponse, abbreviation, city, conference, division, full_name, id, name

### Community 83 - "ScheduledGames"
Cohesion: 0.24
Nodes (9): List, ScheduledGames, RestOfWeek, Today, Tomorrow, DateOnly, Task, TimeSpan (+1 more)

### Community 84 - ".BucketByDay"
Cohesion: 0.50
Nodes (3): DateOnly, Fact, GameScheduleTests

### Community 85 - "Aspire HTTPS Bindings"
Cohesion: 0.25
Nodes (8): https, protocol, scheme, transport, bindings, path, type, nba-api

### Community 87 - "IBallDontLieClient"
Cohesion: 0.24
Nodes (9): CancellationToken, DateOnly, List, Task, IBallDontLieClient, List, GetGamesResponse, data (+1 more)

### Community 88 - "Draft Type Enum"
Cohesion: 0.29
Nodes (6): DraftType, Auction, Linear, Offline, RRR, Snake

### Community 89 - "Draft Status Enum"
Cohesion: 0.29
Nodes (6): DraftStatus, DraftCompleted, DraftEnded, DraftStarted, Initial, Paused

### Community 90 - "Redis Lock Operations"
Cohesion: 0.33
Nodes (4): IDatabase, Task, TimeSpan, LockRedisOperations

### Community 92 - "AppHost Hosting Packages"
Cohesion: 0.29
Nodes (7): NBA.Service, net10.0, Aspire.Hosting.Redis (13.1.2), Microsoft.Extensions.Options (10.0.3), Microsoft.NET.Sdk, Isopoh.Cryptography.Argon2 (1.1.10), Microsoft.Extensions.Identity.Core (10.0.0)

### Community 94 - "Aspire Server Bindings"
Cohesion: 0.29
Nodes (7): tcp, bindings, port, protocol, scheme, targetPort, transport

### Community 96 - "DraftLifecycleService"
Cohesion: 0.18
Nodes (7): IEndpointRouteBuilder, TestingEndpoints, IOptions, List, Task, DraftLifecycleService, Task

### Community 97 - "NBA.Api.DTOs"
Cohesion: 0.17
Nodes (6): NBA.Api.DTOs, LeagueTeamDto, Approved, Leagueid, Leagueteamid, Teamid

### Community 98 - "Aspire Password Parameters"
Cohesion: 0.33
Nodes (6): value, inputs, type, value, password, type

### Community 99 - "League Team Insert Request"
Cohesion: 0.40
Nodes (4): NBA.Api.Requests.LeagueTeam, LeagueTeamInsertRequest, LeagueId, TeamName

### Community 100 - "Draft Request DTO"
Cohesion: 0.40
Nodes (4): NBA.Api.Requests.Draft, DraftRequest, LeagueId, StartDraft

### Community 101 - "NBA.Service.Trade"
Cohesion: 0.15
Nodes (4): NBA.Tests.Fakes, NBA.Service.Trade, NBA.Tests.Integration, NBA.Service.CalculateBoxScore

### Community 102 - "Chat Schema DDL"
Cohesion: 0.70
Nodes (4): chat.conversationparticipants, chat.messages, chat.rooms, nba.applicationuser

### Community 104 - "Aspire HTTP Bindings"
Cohesion: 0.50
Nodes (4): http, protocol, scheme, transport

### Community 116 - "League"
Cohesion: 0.14
Nodes (14): IEndpointRouteBuilder, LeagueEndpoints, League, PagedResult, Task, TeamData, CreateLeagueInput, JoinLeagueInput (+6 more)

### Community 117 - "Task"
Cohesion: 0.15
Nodes (10): CancellationToken, List, Player, Task, Teamplayer, Player, Playerid, Team (+2 more)

### Community 118 - "DraftTimerProcessor"
Cohesion: 0.19
Nodes (8): IHubContext, IOptions, Task, DraftTimerProcessor, IEndpointRouteBuilder, DraftEndpoints, Task, IDraftHubClient

### Community 120 - "DraftService"
Cohesion: 0.12
Nodes (14): DraftOptions, DraftPickTime, Rounds, ShowTeamDraftBoardCount, IServiceCollection, DraftExtention, IOptions, JsonOptions (+6 more)

### Community 126 - "Team"
Cohesion: 0.40
Nodes (5): Team, abbreviation, city, full_name, id

### Community 127 - "Player"
Cohesion: 0.18
Nodes (6): NBA.Service.FreeAgency, NBA.Api.Requests.Team, NBA.Service.Team, TeamRequest, teamName, Player

## Knowledge Gaps
- **700 isolated node(s):** `LeagueId`, `Sends`, `Caller`, `All`, `Others` (+695 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **12 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `NbaFantasyContext` connect `NbaFantasyContext` to `DraftLifecycleService`, `TradeService`, `Player`, `PlayerManager`, `ApplicationHostedService`, `Playoff Bracket Entities`, `Transaction Entities`, `PlayerShortDto`, `PlayerService`, `.OnModelCreating`, `JwtOptions`, `Applicationuser`, `League`, `Task`, `NBA.Data.Entities`, `Team`, `Leagueplayer`, `DraftService`?**
  _High betweenness centrality (0.108) - this node is a cross-community bridge._
- **Why does `NbaFantasyRedis` connect `NbaFantasyRedis` to `DraftLifecycleService`, `DraftRedisOperations`, `PlayerShort`, `PlayerManager`, `DraftState`, `ApplicationHostedService`, `GameRedisOperations`, `TradeRedisOperations`, `JwtOptions`, `ScheduledGames`, `DraftTimerProcessor`, `DraftService`, `Redis Lock Operations`, `TeamDraftBoard`, `NBA.Data.Redis.Operations`?**
  _High betweenness centrality (0.091) - this node is a cross-community bridge._
- **Why does `NBAException` connect `NBAException` to `DraftLifecycleService`, `TradeService`, `BallDontLieClient`, `DraftState`, `BallDontLieClientWireMockTests`, `PlayerShortDto`, `PlayerService`, `JwtOptions`, `Applicationuser`, `League`, `Task`, `DraftTimerProcessor`, `Team`, `TeamDraftBoard`?**
  _High betweenness centrality (0.087) - this node is a cross-community bridge._
- **Are the 31 inferred relationships involving `NBAException` (e.g. with `.GetAsync()` and `.RefreshAsync()`) actually correct?**
  _`NBAException` has 31 INFERRED edges - model-reasoned connections that need verification._
- **What connects `LeagueId`, `Sends`, `Caller` to the rest of the system?**
  _700 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DraftRedisOperations` be split into smaller, more focused modules?**
  _Cohesion score 0.08816326530612245 - nodes in this community are weakly interconnected._
- **Should `TradeService` be split into smaller, more focused modules?**
  _Cohesion score 0.07467532467532467 - nodes in this community are weakly interconnected._