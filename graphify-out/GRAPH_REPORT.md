# Graph Report - NBAFantasy  (2026-08-29)

## Corpus Check
- 180 files · ~51,001 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 1951 nodes · 3497 edges · 134 communities (123 shown, 11 thin omitted)
- Extraction: 92% EXTRACTED · 8% INFERRED · 0% AMBIGUOUS · INFERRED: 287 edges (avg confidence: 0.82)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `9d5397ae`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- TradeBetweenTeams
- LeagueDraft
- .AddProposedTrade
- PlayerShort
- Project Rules & Vendor Licenses
- .CreateClient
- Player
- Player Search Request DTOs
- .League
- ApplicationHostedService
- Player Service Search
- BallDontLieClientWireMockTests
- BoxScore Stats Builder
- DraftState
- TradeHub
- League & Stats Value Requests
- TradeHubFixture
- NbaFantasyContext
- Task
- Applicationuser
- NBA.Api.DTOs
- Player DTO
- GameDto
- ApplicationDefaults.Exceptions
- Team
- League Entity
- NBA.Data.Context
- NbaFantasyRedis
- NBA.Data.Redis.Entities
- ExternalClients.Response
- DraftService
- AppHost Launch Settings
- DraftRedisOperations
- Core Database Schema DDL
- .GetAllLeagues
- Game Redis Shapes
- ExternalClients Project Files
- JwtOptions
- PlayerInfo Response Shape
- BallDontLieClient
- API Launch Profiles
- Playoff Bracket Entities
- Transaction Entities
- .GetAllTeams
- PlayerStatsResponse
- Per-League Stats Values
- PlayerService
- Usertrophie
- NBA.Data Package References
- Aspire Manifest OTEL Config
- Auth Request DTOs
- MetaData
- NBA.Data.Entities
- TradeManager
- GameInfoResponse
- GameService
- LeagueDto
- Leagueplayer
- .ToPlayerDb
- TeamDraftBoard
- Trade
- User Team DTO
- Test Project Packages
- Argon2idPasswordHasher
- NBA Calendar Date Handling
- TradeDto
- EntityMappings
- NBA.Api Package References
- PlayerPositionEnum
- Player
- PlayerManager
- ServiceDefaults Packages
- Entity Mapping Tests
- Aspire Postgres Connection
- Aspire Postgres Container
- Draft Timer Hosted Service
- ServiceDefaults Extensions
- BallDontLieWireMockFixture
- GameRedisOperations
- AppHost Packages
- Draftsnapshot
- TeamInfoResponse
- .InvokeMethodAsync
- ScheduledGames
- .BucketByDay
- Aspire HTTPS Bindings
- Chat Hub
- IBallDontLieClient
- Draft Type Enum
- Draft Status Enum
- Redis Lock Operations
- ApplicationOptions
- AppHost Hosting Packages
- Userleague
- Aspire Server Bindings
- GameTeamDto
- DraftLifecycleService
- League Team DTO
- Aspire Password Parameters
- League Team Insert Request
- Draft Request DTO
- DraftOrderManager
- Chat Schema DDL
- DraftTimerProcessor
- Aspire HTTP Bindings
- NBA.Data.Redis
- Infrastructure Init Entry
- Naming Rule
- PagedResult
- NBAException
- .MapTradeEndpoints
- .BuildService
- .EnsureRehydratedAsync
- .GetDraftTeams
- .ToGameRedis
- ScheduledGamesDto
- BallDontLieClientOptions
- NBA.Tests.Integration
- Team
- NBA.Api.Requests.Team
- GetAllPlayersResponse
- IServiceCollection
- List
- JsonSerializerOptions
- Fact
- IDatabase

## God Nodes (most connected - your core abstractions)
1. `NbaFantasyContext` - 79 edges
2. `TradeBetweenTeams` - 52 edges
3. `NBAException` - 47 edges
4. `PlayerSearchInput` - 40 edges
5. `PlayersFilterSearch` - 38 edges
6. `NBA.Data.Entities` - 38 edges
7. `Applicationuser` - 35 edges
8. `Team` - 35 edges
9. `Player` - 31 edges
10. `NBA.Data.Redis.Entities` - 31 edges

## Surprising Connections (you probably didn't know these)
- `DraftTimerProcessor` --references--> `DraftManager`  [EXTRACTED]
  NBA.Api/Draft/DraftTimerProcessor.cs → NBA.Service/Draft/DraftManager.cs
- `TradeHub` --references--> `DraftManager`  [EXTRACTED]
  NBA.Api/SignalR/Hubs/TradeHub.cs → NBA.Service/Draft/DraftManager.cs
- `DraftTimerProcessor` --references--> `DraftService`  [EXTRACTED]
  NBA.Api/Draft/DraftTimerProcessor.cs → NBA.Service/Draft/DraftService.cs
- `DraftHub` --references--> `DraftService`  [EXTRACTED]
  NBA.Api/SignalR/Hubs/DraftHub.cs → NBA.Service/Draft/DraftService.cs
- `BoxScoreCalculationService` --references--> `NbaFantasyContext`  [EXTRACTED]
  NBA.Service/CalculateBoxScore/BoxScoreCalculationService.cs → NBA.Data/Context/NbaFantasyContext.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Draft Pick Processing Flow** — claude_drafthub, claude_playermanager, claude_draftmanager, claude_draftstate [EXTRACTED 0.95]
- **Draft Timer & Deadline Coordination** — claude_drafttimerhostedservice, claude_draftredisoperations, claude_draftmanager, claude_draft_realtime [EXTRACTED 0.95]
- **External HTTP Resilience Strategy** — claude_resilience_pipeline_rule, claude_externalclients, claude_nba_servicedefaults [INFERRED 0.85]

## Communities (134 total, 11 thin omitted)

### Community 0 - "TradeBetweenTeams"
Cohesion: 0.07
Nodes (36): HubConnection, HubException, List, Task, ITradeHubClient, DateTimeOffset, Guid, List (+28 more)

### Community 1 - "LeagueDraft"
Cohesion: 0.16
Nodes (8): DateTimeOffset, Dictionary, List, Queue, Task, TimeSpan, LeagueDraft, LeagueId

### Community 2 - ".AddProposedTrade"
Cohesion: 0.18
Nodes (11): Created, IQueryable, Trade, DateTime, Guid, ILogger, List, Task (+3 more)

### Community 3 - "PlayerShort"
Cohesion: 0.06
Nodes (29): IEnumerable, List, PlayerShortMappings, PlayerShort, FullName, PlayerId, Position, RedisKeys (+21 more)

### Community 4 - "Project Rules & Vendor Licenses"
Cohesion: 0.05
Nodes (51): Adapter (static mapper), Adding an HTTP Endpoint Flow, ApplicationDefaults, ApplicationOptions, Argon2Options, Aspire AppHost, Auth & Tests, Authenticate Everything Rule (+43 more)

### Community 5 - ".CreateClient"
Cohesion: 0.11
Nodes (27): Action, NBA.Tests.Fakes, HttpMessageHandler, HttpRequestMessage, Fact, HttpResponseMessage, HttpStatusCode, InlineData (+19 more)

### Community 6 - "Player"
Cohesion: 0.05
Nodes (43): DateTime, ICollection, Player, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow (+35 more)

### Community 7 - "Player Search Request DTOs"
Cohesion: 0.05
Nodes (38): NBA.Api.Requests.Player, DateTime, PlayersFilterSearch, allowdrop, gameready, irlteamid, irlteamname, islock (+30 more)

### Community 8 - ".League"
Cohesion: 0.17
Nodes (12): DraftState, IEndpointRouteBuilder, DraftEndpoints, IOptions, Task, DraftHub, DraftOptions, DraftSnapshotService (+4 more)

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

### Community 13 - "DraftState"
Cohesion: 0.14
Nodes (14): PlayerShortDto, FullName, PlayerId, Position, DateTime, Dictionary, List, DraftState (+6 more)

### Community 14 - "TradeHub"
Cohesion: 0.26
Nodes (7): Guid, IHubContext, ILogger, IOptions, List, Task, TradeHub

### Community 15 - "League & Stats Value Requests"
Cohesion: 0.07
Nodes (27): NBA.Api.Requests.League, NBA.Api.Requests.StatValue, LeagueRequest, Autostart, DraftStyle, LeagueName, LeagueType, ScoringSystem (+19 more)

### Community 16 - "TradeHubFixture"
Cohesion: 0.08
Nodes (27): AuthenticateResult, AuthenticationHandler, AuthenticationSchemeOptions, ICollectionFixture, IConnectionMultiplexer, IDatabase, IHost, ApplicationOptions (+19 more)

### Community 17 - "NbaFantasyContext"
Cohesion: 0.09
Nodes (24): DbContext, DbSet, ModelBuilder, Player, Trade, NbaFantasyContext, Applicationusers, Draftsnapshots (+16 more)

### Community 18 - "Task"
Cohesion: 0.14
Nodes (10): CancellationToken, List, Player, Task, Teamplayer, Player, Playerid, Team (+2 more)

### Community 19 - "Applicationuser"
Cohesion: 0.13
Nodes (16): ICollection, Applicationuser, Email, Managerlevel, Password, Teams, Userid, Userleagues (+8 more)

### Community 20 - "NBA.Api.DTOs"
Cohesion: 0.17
Nodes (8): NBA.Api.DTOs, NBA.Api.Mappings, NBA.Service.Trade, NBA.Api.Authentication, NBA.Tests, NBA.Service, NBA.Api.Endpoints, BoxScoreEvaluation

### Community 21 - "Player DTO"
Cohesion: 0.08
Nodes (24): DateTime, PlayerDto, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow, Gameready (+16 more)

### Community 22 - "GameDto"
Cohesion: 0.18
Nodes (11): DateTime, GameDto, Date, GameId, HomeTeam, Postponed, Postseason, StartTime (+3 more)

### Community 23 - "ApplicationDefaults.Exceptions"
Cohesion: 0.20
Nodes (6): ErrorCodes, ApplicationDefaults.LogDefaults, ApplicationDefaults.Exceptions, NBA.Service.Roster, NBA.Data.Constants, TradeStatuses

### Community 24 - "Team"
Cohesion: 0.08
Nodes (29): NBA.Service.League, IEndpointRouteBuilder, LeagueEndpoints, League, ICollection, Team, Approved, Categoryleaguepoints (+21 more)

### Community 25 - "League Entity"
Cohesion: 0.09
Nodes (21): ICollection, League, Autostart, Commissioner, Draftcompleted, Draftsnapshot, Draftstyle, Leagueid (+13 more)

### Community 26 - "NBA.Data.Context"
Cohesion: 0.22
Nodes (7): NBA.Api.Draft, NBA.Api.SignalR.Clients, NBA.Data.Context, NBA.Data.Redis.Enumerations, NBA.Service.Draft, NBA.Api.SignalR.Hubs, ApplicationDefaults.Options

### Community 27 - "NbaFantasyRedis"
Cohesion: 0.14
Nodes (14): Lazy, IDatabase, NbaFantasyRedis, Auth, Draft, Game, Lock, Player (+6 more)

### Community 28 - "NBA.Data.Redis.Entities"
Cohesion: 0.17
Nodes (7): NBA.Data.Redis.Operations, NBA.Data.Redis.Scopes, NBA.Data.Enumerations, NBA.Data.Redis.Keys, NBA.Data.Redis.Entities, NBA.Data.Redis.Dtos, PlayerPositionExtensions

### Community 29 - "ExternalClients.Response"
Cohesion: 0.19
Nodes (7): ApplicationDefaults.Time, ExternalClients.Response, NBA.Api.HostedService, ExternalClients, NBA.Service.Game, NBA.Service.Player, ExternalClients.Poco

### Community 30 - "DraftService"
Cohesion: 0.14
Nodes (12): IServiceCollection, JsonSerializerOptions, DraftSnapshotService, DraftExtention, ApplicationOptions, DraftOptions, DraftSnapshotService, IOptions (+4 more)

### Community 31 - "AppHost Launch Settings"
Cohesion: 0.13
Nodes (18): ASPIRE_DASHBOARD_OTLP_ENDPOINT_URL, ASPIRE_RESOURCE_SERVICE_ENDPOINT_URL, ASPNETCORE_ENVIRONMENT, DOTNET_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables (+10 more)

### Community 32 - "DraftRedisOperations"
Cohesion: 0.19
Nodes (7): DateTimeOffset, IDatabase, JsonSerializerOptions, List, Task, TimeSpan, DraftRedisOperations

### Community 33 - "Core Database Schema DDL"
Cohesion: 0.24
Nodes (17): nba.applicationuser, nba.draftsnapshot, nba.league, nba.leagueplayer, nba.player, nba.playermemento, nba.playoff, nba.playoffbracket (+9 more)

### Community 34 - ".GetAllLeagues"
Cohesion: 0.19
Nodes (8): Task, IEndpointRouteBuilder, TestingEndpoints, Dictionary, DraftBoardTeams, Queue, Task, TeamDraftBoard

### Community 35 - "Game Redis Shapes"
Cohesion: 0.12
Nodes (17): DateTime, GameShort, Date, GameId, HomeTeam, Postponed, Postseason, StartTime (+9 more)

### Community 36 - "ExternalClients Project Files"
Cohesion: 0.13
Nodes (16): ApplicationDefaults, net10.0, Microsoft.NET.Sdk, BoxScoreBuilder, net10.0, Microsoft.NET.Sdk, ExternalClients, net10.0 (+8 more)

### Community 37 - "JwtOptions"
Cohesion: 0.05
Nodes (36): JwtOptions, AccessTokenMinutes, Audience, Issuer, RefreshTokenDays, SigningKey, ApplyStateContext, NBA.Api.HangFire (+28 more)

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

### Community 43 - ".GetAllTeams"
Cohesion: 0.28
Nodes (7): IEndpointRouteBuilder, TeamEndpoints, Dictionary, List, Task, TeamData, TeamService

### Community 44 - "PlayerStatsResponse"
Cohesion: 0.13
Nodes (14): PlayerStatsResponse, ast, blk, fg3a, fg3m, fga, fgm, fta (+6 more)

### Community 45 - "Per-League Stats Values"
Cohesion: 0.13
Nodes (14): Statsvalue, Assistsvalue, Blocksvalue, Fieldgoalvaluemade, Fieldgoalvaluemissed, Freethrowvaluemade, Freethrowvaluemissed, League (+6 more)

### Community 46 - "PlayerService"
Cohesion: 0.21
Nodes (9): AutomaticRetry, JobDisplayName, CancellationToken, DateTime, IReadOnlyList, List, PagedResult, Task (+1 more)

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

### Community 51 - "MetaData"
Cohesion: 0.15
Nodes (12): MetaData, Next_cursor, Per_page, Prev_Cursor, List, GetGamesResponse, data, meta (+4 more)

### Community 52 - "NBA.Data.Entities"
Cohesion: 0.20
Nodes (3): NBA.Data.Entities, NBA.Service.Authentication, NBA.Service.Team

### Community 53 - "TradeManager"
Cohesion: 0.22
Nodes (8): Dictionary, Guid, IOptions, List, Task, TradeManager, newFromPlayers, newToPlayers

### Community 54 - "GameInfoResponse"
Cohesion: 0.15
Nodes (13): DateTime, GameInfoResponse, date, datetime, home_team, home_team_score, id, postponed (+5 more)

### Community 55 - "GameService"
Cohesion: 0.26
Nodes (8): IBackgroundJobClient, IEndpointRouteBuilder, GameEndpoints, CancellationToken, IOptions, List, Task, GameService

### Community 56 - "LeagueDto"
Cohesion: 0.05
Nodes (34): List, DraftOrderDto, Round, Teams, LeagueDto, Autostart, Commissioner, CommissionersTeam (+26 more)

### Community 57 - "Leagueplayer"
Cohesion: 0.14
Nodes (10): NBA.Service.FreeAgency, Leagueplayer, Isfreeagent, League, Leagueid, Leagueplayerid, Playerid, List (+2 more)

### Community 58 - ".ToPlayerDb"
Cohesion: 0.26
Nodes (5): PlayerData, Fact, InlineData, Theory, AdapterTests

### Community 59 - "TeamDraftBoard"
Cohesion: 0.16
Nodes (11): Task, IDraftHubClient, List, DraftBoardTeams, CurrentRound, DraftOrder, onTheClockTeam, TeamDraftBoard (+3 more)

### Community 60 - "Trade"
Cohesion: 0.12
Nodes (15): DateTime, Guid, List, Trade, Fromteam, Fromteamid, League, Leagueid (+7 more)

### Community 61 - "User Team DTO"
Cohesion: 0.15
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
Cohesion: 0.15
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

### Community 69 - "Player"
Cohesion: 0.12
Nodes (8): NBA.Service.CalculateBoxScore, NBA.Service.Builder, Player, BoxScoreCalculationBuilder, Dictionary, List, Task, BoxScoreCalculationService

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

### Community 75 - "Draft Timer Hosted Service"
Cohesion: 0.31
Nodes (7): BackgroundService, CancellationToken, ILogger, IServiceProvider, Task, TimeSpan, DraftTimerHostedService

### Community 76 - "ServiceDefaults Extensions"
Cohesion: 0.22
Nodes (3): Microsoft.Extensions.Hosting, Extensions, WebApplication

### Community 77 - "BallDontLieWireMockFixture"
Cohesion: 0.18
Nodes (9): IAsyncLifetime, HttpResponseMessage, IOptions, Task, BallDontLieWireMockFixture, Client, Server, ServiceProvider (+1 more)

### Community 78 - "GameRedisOperations"
Cohesion: 0.32
Nodes (5): IDatabase, JsonSerializerOptions, Task, TimeSpan, GameRedisOperations

### Community 79 - "AppHost Packages"
Cohesion: 0.22
Nodes (8): net10.0, Aspire.Hosting.Redis (13.1.2), Aspire.StackExchange.Redis (13.1.2), Microsoft.NET.Sdk, Aspire.Hosting.AppHost (13.1.0), Aspire.Hosting.PostgreSQL (13.1.0), CommunityToolkit.Aspire.Hosting.NodeJS.Extensions (9.9.0), OpenTelemetry.Api (1.16.0)

### Community 80 - "Draftsnapshot"
Cohesion: 0.22
Nodes (6): DateTime, Draftsnapshot, Draftstate, Draftteams, Leagueid, Tsupdated

### Community 81 - "TeamInfoResponse"
Cohesion: 0.25
Nodes (8): TeamInfoResponse, abbreviation, city, conference, division, full_name, id, name

### Community 82 - ".InvokeMethodAsync"
Cohesion: 0.25
Nodes (6): NBA.Api.SignalR, HubInvocationContext, IHubFilter, Func, ValueTask, NBAExceptionHubFilter

### Community 83 - "ScheduledGames"
Cohesion: 0.24
Nodes (9): List, ScheduledGames, RestOfWeek, Today, Tomorrow, DateOnly, Task, TimeSpan (+1 more)

### Community 84 - ".BucketByDay"
Cohesion: 0.50
Nodes (3): DateOnly, Fact, GameScheduleTests

### Community 85 - "Aspire HTTPS Bindings"
Cohesion: 0.25
Nodes (8): https, protocol, scheme, transport, bindings, path, type, nba-api

### Community 86 - "Chat Hub"
Cohesion: 0.25
Nodes (5): Hub, Task, IChatHubClient, Task, ChatHub

### Community 87 - "IBallDontLieClient"
Cohesion: 0.39
Nodes (5): CancellationToken, DateOnly, List, Task, IBallDontLieClient

### Community 88 - "Draft Type Enum"
Cohesion: 0.29
Nodes (6): DraftType, Auction, Linear, Offline, RRR, Snake

### Community 89 - "Draft Status Enum"
Cohesion: 0.29
Nodes (6): DraftStatus, DraftCompleted, DraftEnded, DraftStarted, Initial, Paused

### Community 90 - "Redis Lock Operations"
Cohesion: 0.33
Nodes (4): IDatabase, Task, TimeSpan, LockRedisOperations

### Community 91 - "ApplicationOptions"
Cohesion: 0.29
Nodes (6): ApplicationOptions, CenterLimit, MaxPlayersPerTeam, ProposedTradeTtlMinutes, IOptions, RosterValidator

### Community 92 - "AppHost Hosting Packages"
Cohesion: 0.29
Nodes (7): NBA.Service, net10.0, Aspire.Hosting.Redis (13.1.2), Microsoft.Extensions.Options (10.0.3), Microsoft.NET.Sdk, Isopoh.Cryptography.Argon2 (1.1.10), Microsoft.Extensions.Identity.Core (10.0.0)

### Community 93 - "Userleague"
Cohesion: 0.29
Nodes (6): Userleague, League, Leagueid, User, Userid, Userleagueid

### Community 94 - "Aspire Server Bindings"
Cohesion: 0.29
Nodes (7): tcp, bindings, port, protocol, scheme, targetPort, transport

### Community 95 - "GameTeamDto"
Cohesion: 0.29
Nodes (6): GameTeamDto, Abbreviation, City, FullName, Score, TeamId

### Community 96 - "DraftLifecycleService"
Cohesion: 0.13
Nodes (11): List, Dictionary, DraftBoardTeams, DraftOptions, IOptions, NbaFantasyContext, NbaFantasyRedis, Queue (+3 more)

### Community 97 - "League Team DTO"
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

### Community 101 - "DraftOrderManager"
Cohesion: 0.33
Nodes (6): Dictionary, NbaFantasyRedis, Queue, Task, TeamDraftBoard, DraftOrderManager

### Community 102 - "Chat Schema DDL"
Cohesion: 0.70
Nodes (4): chat.conversationparticipants, chat.messages, chat.rooms, nba.applicationuser

### Community 103 - "DraftTimerProcessor"
Cohesion: 0.25
Nodes (7): DraftOptions, DraftPickTime, Rounds, ShowTeamDraftBoardCount, IHubContext, IOptions, DraftTimerProcessor

### Community 104 - "Aspire HTTP Bindings"
Cohesion: 0.50
Nodes (4): http, protocol, scheme, transport

### Community 105 - "NBA.Data.Redis"
Cohesion: 0.50
Nodes (3): NBA.Data.Redis, JsonSerializerOptions, RedisSerializer

### Community 116 - "PagedResult"
Cohesion: 0.25
Nodes (5): IEndpointRouteBuilder, PlayerEndpoints, IReadOnlyList, PagedResult, TotalPages

### Community 117 - "NBAException"
Cohesion: 0.20
Nodes (6): NBAException, ErrorCode, ClaimsPrincipal, Exception, ClaimsPrincipalExtensions, PlayerData

### Community 119 - ".BuildService"
Cohesion: 0.50
Nodes (4): Fact, NbaFantasyContext, Task, DraftEndDraftTests

### Community 120 - ".EnsureRehydratedAsync"
Cohesion: 0.29
Nodes (6): Dictionary, IOptions, JsonSerializerOptions, Queue, Task, DraftSnapshotService

### Community 123 - "ScheduledGamesDto"
Cohesion: 0.33
Nodes (5): List, ScheduledGamesDto, RestOfWeek, Today, Tomorrow

### Community 124 - "BallDontLieClientOptions"
Cohesion: 0.40
Nodes (4): BallDontLieClientOptions, ApiKey, BaseUrl, Per_Page

### Community 126 - "Team"
Cohesion: 0.40
Nodes (5): Team, abbreviation, city, full_name, id

### Community 127 - "NBA.Api.Requests.Team"
Cohesion: 0.50
Nodes (3): NBA.Api.Requests.Team, TeamRequest, teamName

### Community 128 - "GetAllPlayersResponse"
Cohesion: 0.50
Nodes (4): List, GetAllPlayersResponse, data, meta

## Knowledge Gaps
- **686 isolated node(s):** `Server`, `Database`, `Redis`, `BoxScoreEvaluation`, `TradeStatuses` (+681 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **11 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `NbaFantasyContext` connect `NbaFantasyContext` to `.AddProposedTrade`, `Player`, `ApplicationHostedService`, `Task`, `Applicationuser`, `Team`, `.GetAllLeagues`, `JwtOptions`, `Playoff Bracket Entities`, `Transaction Entities`, `.GetAllTeams`, `PlayerService`, `Usertrophie`, `NBA.Data.Entities`, `Leagueplayer`, `Player`, `PlayerManager`, `Draftsnapshot`, `Userleague`, `NBAException`, `.EnsureRehydratedAsync`?**
  _High betweenness centrality (0.139) - this node is a cross-community bridge._
- **Why does `Player` connect `Player` to `EntityMappings`, `PlayerShort`, `NBA.Data.Context`, `Player`, `.ToGameRedis`, `PlayerManager`, `.GetAllTeams`, `PlayerService`, `Task`, `NBA.Api.DTOs`, `NBAException`, `NBA.Data.Entities`, `ApplicationDefaults.Exceptions`, `Leagueplayer`, `.ToPlayerDb`, `ExternalClients.Response`?**
  _High betweenness centrality (0.096) - this node is a cross-community bridge._
- **Why does `NbaFantasyRedis` connect `NbaFantasyRedis` to `DraftRedisOperations`, `TradeBetweenTeams`, `PlayerShort`, `JwtOptions`, `PlayerManager`, `DraftTimerProcessor`, `.League`, `ApplicationHostedService`, `Draft Timer Hosted Service`, `GameRedisOperations`, `ScheduledGames`, `TradeManager`, `.EnsureRehydratedAsync`, `Redis Lock Operations`, `NBA.Data.Redis.Entities`?**
  _High betweenness centrality (0.087) - this node is a cross-community bridge._
- **Are the 3 inferred relationships involving `TradeBetweenTeams` (e.g. with `.RemoveProposedTrade_removes_only_the_matching_trade_and_returns_it()` and `.RemoveProposedTrade_returns_null_for_unknown_id()`) actually correct?**
  _`TradeBetweenTeams` has 3 INFERRED edges - model-reasoned connections that need verification._
- **Are the 33 inferred relationships involving `NBAException` (e.g. with `.GetAsync()` and `.RefreshAsync()`) actually correct?**
  _`NBAException` has 33 INFERRED edges - model-reasoned connections that need verification._
- **What connects `Server`, `Database`, `Redis` to the rest of the system?**
  _686 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `TradeBetweenTeams` be split into smaller, more focused modules?**
  _Cohesion score 0.0710868079289132 - nodes in this community are weakly interconnected._