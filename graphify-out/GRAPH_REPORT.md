# Graph Report - NBAFantasy  (2026-08-30)

## Corpus Check
- 185 files · ~53,629 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2033 nodes · 3724 edges · 131 communities (118 shown, 13 thin omitted)
- Extraction: 92% EXTRACTED · 8% INFERRED · 0% AMBIGUOUS · INFERRED: 282 edges (avg confidence: 0.82)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `f585dfed`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- TradeBetweenTeams
- DraftState
- .ProposeAsync
- PlayerShort
- Project Rules & Vendor Licenses
- .CreateClient
- Player
- PlayersFilterSearch
- DraftManager
- ApplicationHostedService
- Player Service Search
- BallDontLieClientWireMockTests
- BoxScoreStatsBuilder
- .InitializeAsync
- TradeService
- League & Stats Value Requests
- TradeHubFixture
- NbaFantasyContext
- NBA.Data.Entities
- Applicationuser
- NBA.Api.Mappings
- PlayerDto
- GameDto
- TradeDto
- Team
- League
- NBA.Service.Draft
- NbaFantasyRedis
- NBA.Data.Redis.Operations
- ExternalClients.Response
- NBAException
- AppHost Launch Settings
- TradeRedisOperations
- create-objects-nba-schema.sql
- LeagueDto
- Game Redis Shapes
- ExternalClients Project Files
- BallDontLieWireMockFixture
- PlayerInfoResponse
- BallDontLieClient
- API Launch Profiles
- Playoff Bracket Entities
- Transaction Entities
- DraftHub
- PlayerStatsResponse
- Per-League Stats Values
- PlayerService
- Usertrophie
- NBA.Data Package References
- Aspire Manifest OTEL Config
- Auth Request DTOs
- ApplicationDefaults.Exceptions
- LoginDto
- JwtOptions
- GameInfoResponse
- GameService
- TeamDto
- Leagueplayer
- .ToPlayerDb
- DraftLifecycleService
- Trade
- UserTeamDto
- Test Project Packages
- Argon2idPasswordHasher
- NBA Calendar Date Handling
- NBA.Api.DTOs
- EntityMappings
- NBA.Api Package References
- PlayerPositionEnum
- Player
- PlayerManager
- ServiceDefaults Packages
- Entity Mapping Tests
- Aspire Postgres Connection
- Aspire Postgres Container
- DraftSnapshotService
- ServiceDefaults Extensions
- NBA.Data.Redis.Entities
- LeagueTrades
- AppHost Packages
- .League
- TeamInfoResponse
- AuthRedisOperations
- ScheduledGames
- .BucketByDay
- Aspire HTTPS Bindings
- .RegisterDraft
- MetaData
- Draft Type Enum
- Draft Status Enum
- Redis Lock Operations
- Draftsnapshot
- AppHost Hosting Packages
- LeagueService.cs
- Aspire Server Bindings
- DraftService
- RedisSerializer.cs
- LeagueTeamDto
- Aspire Password Parameters
- League Team Insert Request
- Draft Request DTO
- .GetTeams
- Chat Schema DDL
- DraftTimerHostedService
- Aspire HTTP Bindings
- PagedResult
- Infrastructure Init Entry
- Naming Rule
- League
- Task
- PlayerShortDto
- LeagueScope
- ExternalClients.Poco
- .RegisterTrade
- IEndpointRouteBuilder
- Userleague
- IServiceCollection
- DraftBoardTeams
- List
- JsonOptions
- JsonSerializerOptions
- Fact
- IReadOnlyList

## God Nodes (most connected - your core abstractions)
1. `NbaFantasyContext` - 86 edges
2. `TradeBetweenTeams` - 49 edges
3. `NBAException` - 45 edges
4. `Trade` - 41 edges
5. `NBA.Data.Entities` - 41 edges
6. `PlayerSearchInput` - 40 edges
7. `PlayersFilterSearch` - 38 edges
8. `Team` - 36 edges
9. `Applicationuser` - 35 edges
10. `NBA.Data.Redis.Entities` - 33 edges

## Surprising Connections (you probably didn't know these)
- `DraftTimerProcessor` --references--> `DraftLifecycleService`  [EXTRACTED]
  NBA.Api/Draft/DraftTimerProcessor.cs → NBA.Service/Draft/DraftLifecycleService.cs
- `DraftTimerProcessor` --references--> `DraftManager`  [EXTRACTED]
  NBA.Api/Draft/DraftTimerProcessor.cs → NBA.Service/Draft/DraftManager.cs
- `DraftTimerProcessor` --references--> `DraftService`  [EXTRACTED]
  NBA.Api/Draft/DraftTimerProcessor.cs → NBA.Service/Draft/DraftService.cs
- `DraftHub` --references--> `DraftLifecycleService`  [EXTRACTED]
  NBA.Api/SignalR/Hubs/DraftHub.cs → NBA.Service/Draft/DraftLifecycleService.cs
- `DraftHub` --references--> `DraftManager`  [EXTRACTED]
  NBA.Api/SignalR/Hubs/DraftHub.cs → NBA.Service/Draft/DraftManager.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Draft Pick Processing Flow** — claude_drafthub, claude_playermanager, claude_draftmanager, claude_draftstate [EXTRACTED 0.95]
- **Draft Timer & Deadline Coordination** — claude_drafttimerhostedservice, claude_draftredisoperations, claude_draftmanager, claude_draft_realtime [EXTRACTED 0.95]
- **External HTTP Resilience Strategy** — claude_resilience_pipeline_rule, claude_externalclients, claude_nba_servicedefaults [INFERRED 0.85]

## Communities (131 total, 13 thin omitted)

### Community 0 - "TradeBetweenTeams"
Cohesion: 0.10
Nodes (25): IHubCallerClients, Method, List, Task, ITradeHubClient, DateTimeOffset, Guid, List (+17 more)

### Community 1 - "DraftState"
Cohesion: 0.06
Nodes (36): List, DraftBoardTeams, CurrentRound, DraftOrder, onTheClockTeam, TeamDraftBoard, Pick, TeamId (+28 more)

### Community 2 - ".ProposeAsync"
Cohesion: 0.20
Nodes (10): Guid, IOptions, List, Task, TradeManager, Guid, IOptions, List (+2 more)

### Community 3 - "PlayerShort"
Cohesion: 0.09
Nodes (22): PlayerShort, FullName, PlayerId, Position, RedisKeys, HashSet, IDatabase, IEnumerable (+14 more)

### Community 4 - "Project Rules & Vendor Licenses"
Cohesion: 0.05
Nodes (51): Adapter (static mapper), Adding an HTTP Endpoint Flow, ApplicationDefaults, ApplicationOptions, Argon2Options, Aspire AppHost, Auth & Tests, Authenticate Everything Rule (+43 more)

### Community 5 - ".CreateClient"
Cohesion: 0.11
Nodes (26): Action, HttpMessageHandler, HttpRequestMessage, Fact, HttpResponseMessage, HttpStatusCode, InlineData, JsonException (+18 more)

### Community 6 - "Player"
Cohesion: 0.05
Nodes (43): DateTime, ICollection, Player, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow (+35 more)

### Community 7 - "PlayersFilterSearch"
Cohesion: 0.05
Nodes (37): DateTime, PlayersFilterSearch, allowdrop, gameready, irlteamid, irlteamname, islock, leagueId (+29 more)

### Community 8 - "DraftManager"
Cohesion: 0.26
Nodes (7): DraftOptions, DraftSnapshotService, DraftState, IOptions, NbaFantasyRedis, Task, DraftManager

### Community 9 - "ApplicationHostedService"
Cohesion: 0.08
Nodes (23): ErrorResponse, ErrorCode, ErrorMessage, Log, message, request, response, HttpContext (+15 more)

### Community 10 - "Player Service Search"
Cohesion: 0.06
Nodes (36): PlayerSearchInput, Allowdrop, Gameready, Irlteamid, Irlteamname, Islock, LeagueId, MaxAssists (+28 more)

### Community 11 - "BallDontLieClientWireMockTests"
Cohesion: 0.16
Nodes (15): IClassFixture, IRequestMessage, IResponseBuilder, Fact, HttpStatusCode, InlineData, JsonException, OperationCanceledException (+7 more)

### Community 12 - "BoxScoreStatsBuilder"
Cohesion: 0.07
Nodes (15): BoxScoreStatsBuilder, PlayerStats, ast, blk, fg3a, fg3m, fga, fgm (+7 more)

### Community 13 - ".InitializeAsync"
Cohesion: 0.17
Nodes (9): ApplicationOptions, CenterLimit, MaxPlayersPerTeam, ProposedTradeTtlMinutes, AuthenticateResult, AuthenticationHandler, AuthenticationSchemeOptions, ClaimsPrincipal (+1 more)

### Community 14 - "TradeService"
Cohesion: 0.19
Nodes (10): Created, Trade, DateTime, Guid, ILogger, List, Task, TradeData (+2 more)

### Community 15 - "League & Stats Value Requests"
Cohesion: 0.07
Nodes (27): NBA.Api.Requests.League, NBA.Api.Requests.StatValue, LeagueRequest, Autostart, DraftStyle, LeagueName, LeagueType, ScoringSystem (+19 more)

### Community 16 - "TradeHubFixture"
Cohesion: 0.10
Nodes (33): HubConnection, HubException, HubInvocationContext, IConnectionMultiplexer, IHost, IHubFilter, Func, ValueTask (+25 more)

### Community 17 - "NbaFantasyContext"
Cohesion: 0.09
Nodes (24): DbContext, DbSet, ModelBuilder, Player, Trade, NbaFantasyContext, Applicationusers, Draftsnapshots (+16 more)

### Community 18 - "NBA.Data.Entities"
Cohesion: 0.12
Nodes (11): NBA.Data.Entities, NBA.Api.HostedService, NBA.Data.Context, NBA.Service.FreeAgency, NBA.Service.Authentication, NBA.Api.Authentication, ApplicationDefaults.Options, NBA.Service.Team (+3 more)

### Community 19 - "Applicationuser"
Cohesion: 0.14
Nodes (16): ICollection, Applicationuser, Email, Managerlevel, Password, Teams, Userid, Userleagues (+8 more)

### Community 20 - "NBA.Api.Mappings"
Cohesion: 0.09
Nodes (16): NBA.Service.League, NBA.Api.Mappings, NBA.Api.Requests.Team, NBA.Service, NBA.Api.Requests.Player, NBA.Api.Endpoints, AuthenticationEndpoints, IEndpointRouteBuilder (+8 more)

### Community 21 - "PlayerDto"
Cohesion: 0.08
Nodes (24): DateTime, PlayerDto, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow, Gameready (+16 more)

### Community 22 - "GameDto"
Cohesion: 0.09
Nodes (22): DateTime, List, GameDto, Date, GameId, HomeTeam, Postponed, Postseason (+14 more)

### Community 23 - "TradeDto"
Cohesion: 0.07
Nodes (39): Clients, DateTime, Guid, List, TradeDto, Fromteamid, Leagueid, Playerids (+31 more)

### Community 24 - "Team"
Cohesion: 0.12
Nodes (17): ICollection, Team, Approved, Categoryleaguepoints, Islock, Lastweekpoints, League, Leagueid (+9 more)

### Community 25 - "League"
Cohesion: 0.10
Nodes (21): ICollection, League, Autostart, Commissioner, Draftcompleted, Draftsnapshot, Draftstyle, Leagueid (+13 more)

### Community 26 - "NBA.Service.Draft"
Cohesion: 0.17
Nodes (6): NBA.Api.Draft, NBA.Service.Draft, NBA.Api.SignalR.Hubs, ICollectionFixture, DraftEndpoints, TradeIntegrationCollection

### Community 27 - "NbaFantasyRedis"
Cohesion: 0.17
Nodes (12): Lazy, IDatabase, NbaFantasyRedis, Auth, Draft, Game, Lock, Player (+4 more)

### Community 28 - "NBA.Data.Redis.Operations"
Cohesion: 0.21
Nodes (4): NBA.Data.Redis.Operations, NBA.Data.Redis.Scopes, NBA.Data.Redis.Keys, NBA.Data.Redis

### Community 29 - "ExternalClients.Response"
Cohesion: 0.11
Nodes (12): ApplicationDefaults.Time, ExternalClients.Response, NBA.Data.Enumerations, NBA.Service.Game, NBA.Service.Player, NBA.Tests, NBA.Data.Redis.Dtos, PlayerPositionExtensions (+4 more)

### Community 30 - "NBAException"
Cohesion: 0.17
Nodes (11): NBAException, ErrorCode, Exception, IQueryable, IEndpointRouteBuilder, Dictionary, List, Task (+3 more)

### Community 31 - "AppHost Launch Settings"
Cohesion: 0.13
Nodes (18): ASPIRE_DASHBOARD_OTLP_ENDPOINT_URL, ASPIRE_RESOURCE_SERVICE_ENDPOINT_URL, ASPNETCORE_ENVIRONMENT, DOTNET_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables (+10 more)

### Community 32 - "TradeRedisOperations"
Cohesion: 0.22
Nodes (9): Guid, IDatabase, JsonSerializerOptions, List, Task, TimeSpan, TradeRedisOperations, RedisKey (+1 more)

### Community 33 - "create-objects-nba-schema.sql"
Cohesion: 0.22
Nodes (18): nba.applicationuser, nba.draftsnapshot, nba.league, nba.leagueplayer, nba.player, nba.playermemento, nba.playoff, nba.playoffbracket (+10 more)

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

### Community 38 - "PlayerInfoResponse"
Cohesion: 0.11
Nodes (18): List, GetAllPlayersResponse, data, meta, PlayerInfoResponse, college, country, draft_number (+10 more)

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

### Community 43 - "DraftHub"
Cohesion: 0.17
Nodes (12): IHubContext, DraftOptions, IDraftHubClient, IOptions, NbaFantasyRedis, DraftTimerProcessor, DraftOptions, IDraftHubClient (+4 more)

### Community 44 - "PlayerStatsResponse"
Cohesion: 0.14
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

### Community 51 - "ApplicationDefaults.Exceptions"
Cohesion: 0.10
Nodes (13): ErrorCodes, ApplicationDefaults.LogDefaults, ApplicationDefaults.Exceptions, NBA.Api.SignalR, NBA.Service.Roster, NBA.Data.Constants, IExceptionHandler, ClaimsPrincipalExtensions (+5 more)

### Community 52 - "LoginDto"
Cohesion: 0.22
Nodes (8): List, LoginDto, Leagues, RefreshToken, Teams, Token, Userid, Username

### Community 53 - "JwtOptions"
Cohesion: 0.05
Nodes (33): JwtOptions, AccessTokenMinutes, Audience, Issuer, RefreshTokenDays, SigningKey, ApplyStateContext, NBA.Api.HangFire (+25 more)

### Community 54 - "GameInfoResponse"
Cohesion: 0.11
Nodes (18): DateTime, GameInfoResponse, date, datetime, home_team, home_team_score, id, postponed (+10 more)

### Community 55 - "GameService"
Cohesion: 0.23
Nodes (9): IBackgroundJobClient, DateOnly, Task, CancellationToken, DateOnly, IOptions, List, Task (+1 more)

### Community 56 - "TeamDto"
Cohesion: 0.22
Nodes (9): TeamDto, Categoryleaguepoints, Competesinleague, Islock, Lastweekpoints, Name, Seed, Teamid (+1 more)

### Community 57 - "Leagueplayer"
Cohesion: 0.18
Nodes (8): Leagueplayer, Isfreeagent, League, Leagueid, Leagueplayerid, Playerid, List, Task

### Community 58 - ".ToPlayerDb"
Cohesion: 0.19
Nodes (7): List, PlayerData, Adapter, Fact, InlineData, Theory, AdapterTests

### Community 59 - "DraftLifecycleService"
Cohesion: 0.17
Nodes (10): DraftBoardTeams, Dictionary, DraftOptions, DraftSnapshotService, IOptions, NbaFantasyContext, NbaFantasyRedis, Queue (+2 more)

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

### Community 65 - "NBA.Api.DTOs"
Cohesion: 0.18
Nodes (5): NBA.Api.DTOs, List, DraftOrderDto, Round, Teams

### Community 66 - "EntityMappings"
Cohesion: 0.22
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
Cohesion: 0.33
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
Cohesion: 0.17
Nodes (10): DraftOptions, DraftPickTime, Rounds, ShowTeamDraftBoardCount, Dictionary, IOptions, JsonSerializerOptions, Queue (+2 more)

### Community 76 - "ServiceDefaults Extensions"
Cohesion: 0.22
Nodes (3): Microsoft.Extensions.Hosting, Extensions, WebApplication

### Community 77 - "NBA.Data.Redis.Entities"
Cohesion: 0.09
Nodes (12): NBA.Api.SignalR.Clients, NBA.Tests.Fakes, NBA.Data.Redis.Enumerations, NBA.Data.Redis.Entities, NBA.Service.Trade, Hub, Task, IChatHubClient (+4 more)

### Community 78 - "LeagueTrades"
Cohesion: 0.24
Nodes (6): Guid, List, Task, TimeSpan, LeagueTrades, LeagueId

### Community 79 - "AppHost Packages"
Cohesion: 0.22
Nodes (8): net10.0, Aspire.Hosting.Redis (13.1.2), Aspire.StackExchange.Redis (13.1.2), Microsoft.NET.Sdk, Aspire.Hosting.AppHost (13.1.0), Aspire.Hosting.PostgreSQL (13.1.0), CommunityToolkit.Aspire.Hosting.NodeJS.Extensions (9.9.0), OpenTelemetry.Api (1.16.0)

### Community 80 - ".League"
Cohesion: 0.23
Nodes (5): IEndpointRouteBuilder, Task, DraftState, Task, Task

### Community 81 - "TeamInfoResponse"
Cohesion: 0.17
Nodes (12): List, GetAllTeamsResponse, data, meta, TeamInfoResponse, abbreviation, city, conference (+4 more)

### Community 82 - "AuthRedisOperations"
Cohesion: 0.38
Nodes (4): IDatabase, Task, TimeSpan, AuthRedisOperations

### Community 83 - "ScheduledGames"
Cohesion: 0.24
Nodes (7): List, ScheduledGames, RestOfWeek, Today, Tomorrow, Task, TimeSpan

### Community 85 - "Aspire HTTPS Bindings"
Cohesion: 0.25
Nodes (8): https, protocol, scheme, transport, bindings, path, type, nba-api

### Community 86 - ".RegisterDraft"
Cohesion: 0.33
Nodes (4): IServiceCollection, DraftOrderManager, DraftSnapshotService, DraftExtention

### Community 87 - "MetaData"
Cohesion: 0.16
Nodes (13): CancellationToken, DateOnly, List, Task, IBallDontLieClient, MetaData, Next_cursor, Per_page (+5 more)

### Community 88 - "Draft Type Enum"
Cohesion: 0.29
Nodes (6): DraftType, Auction, Linear, Offline, RRR, Snake

### Community 89 - "Draft Status Enum"
Cohesion: 0.29
Nodes (6): DraftStatus, DraftCompleted, DraftEnded, DraftStarted, Initial, Paused

### Community 90 - "Redis Lock Operations"
Cohesion: 0.33
Nodes (4): IDatabase, Task, TimeSpan, LockRedisOperations

### Community 91 - "Draftsnapshot"
Cohesion: 0.22
Nodes (6): DateTime, Draftsnapshot, Draftstate, Draftteams, Leagueid, Tsupdated

### Community 92 - "AppHost Hosting Packages"
Cohesion: 0.29
Nodes (7): NBA.Service, net10.0, Aspire.Hosting.Redis (13.1.2), Microsoft.Extensions.Options (10.0.3), Microsoft.NET.Sdk, Isopoh.Cryptography.Argon2 (1.1.10), Microsoft.Extensions.Identity.Core (10.0.0)

### Community 93 - "LeagueService.cs"
Cohesion: 0.53
Nodes (4): IEndpointRouteBuilder, CreateLeagueInput, JoinLeagueInput, StatsValueInput

### Community 94 - "Aspire Server Bindings"
Cohesion: 0.29
Nodes (7): tcp, bindings, port, protocol, scheme, targetPort, transport

### Community 95 - "DraftService"
Cohesion: 0.13
Nodes (14): ApplicationOptions, JsonOptions, JsonSerializerOptions, Dictionary, DraftOptions, DraftOrderManager, DraftSnapshotService, IOptions (+6 more)

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

### Community 101 - ".GetTeams"
Cohesion: 0.53
Nodes (4): Dictionary, Queue, Task, DraftOrderManager

### Community 102 - "Chat Schema DDL"
Cohesion: 0.70
Nodes (4): chat.conversationparticipants, chat.messages, chat.rooms, nba.applicationuser

### Community 103 - "DraftTimerHostedService"
Cohesion: 0.31
Nodes (7): BackgroundService, CancellationToken, ILogger, IServiceProvider, Task, TimeSpan, DraftTimerHostedService

### Community 104 - "Aspire HTTP Bindings"
Cohesion: 0.50
Nodes (4): http, protocol, scheme, transport

### Community 105 - "PagedResult"
Cohesion: 0.25
Nodes (5): IEndpointRouteBuilder, PlayerEndpoints, IReadOnlyList, PagedResult, TotalPages

### Community 116 - "League"
Cohesion: 0.24
Nodes (6): League, PagedResult, Task, TeamData, JoinLeagueResult, LeagueService

### Community 117 - "Task"
Cohesion: 0.13
Nodes (10): CancellationToken, List, Player, Task, Teamplayer, Player, Playerid, Team (+2 more)

### Community 118 - "PlayerShortDto"
Cohesion: 0.18
Nodes (14): Fact, IReadOnlyList, List, IEnumerable, List, PlayerShortDto, FullName, PlayerId (+6 more)

### Community 119 - "LeagueScope"
Cohesion: 0.40
Nodes (5): LeagueScope, Draft, LeagueId, Players, Trades

### Community 120 - "ExternalClients.Poco"
Cohesion: 0.15
Nodes (7): BoxScoreBuilder, ExternalClients, BoxScoreBuilder.Model, NBA.Tests.Integration, ExternalClients.Poco, IEndpointRouteBuilder, TestingEndpoints

### Community 123 - "Userleague"
Cohesion: 0.29
Nodes (6): Userleague, League, Leagueid, User, Userid, Userleagueid

## Knowledge Gaps
- **695 isolated node(s):** `BoxScoreEvaluation`, `ErrorCodes`, `TradeStatuses`, `FromTeam`, `PlayersIds` (+690 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **13 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `NbaFantasyContext` connect `NbaFantasyContext` to `Player`, `ApplicationHostedService`, `.InitializeAsync`, `TradeService`, `TradeHubFixture`, `NBA.Data.Entities`, `Applicationuser`, `Team`, `NBAException`, `Playoff Bracket Entities`, `Transaction Entities`, `PlayerService`, `Usertrophie`, `JwtOptions`, `Leagueplayer`, `Player`, `PlayerManager`, `DraftSnapshotService`, `Draftsnapshot`, `DraftService`, `League`, `Task`, `Userleague`?**
  _High betweenness centrality (0.156) - this node is a cross-community bridge._
- **Why does `NBAException` connect `NBAException` to `.ProposeAsync`, `.CreateClient`, `BallDontLieClient`, `DraftManager`, `DraftSnapshotService`, `BallDontLieClientWireMockTests`, `.InitializeAsync`, `PlayerService`, `TradeService`, `.League`, `ApplicationDefaults.Exceptions`, `Applicationuser`, `JwtOptions`, `League`, `DraftService`?**
  _High betweenness centrality (0.089) - this node is a cross-community bridge._
- **Why does `Trade` connect `Trade` to `NBA.Api.DTOs`, `EntityMappings`, `.ProposeAsync`, `NBA.Data.Redis.Entities`, `TradeService`, `TradeHubFixture`, `NBA.Data.Entities`, `ApplicationDefaults.Exceptions`, `League`, `TradeDto`, `Team`, `League`?**
  _High betweenness centrality (0.073) - this node is a cross-community bridge._
- **Are the 31 inferred relationships involving `NBAException` (e.g. with `.GetAsync()` and `.RefreshAsync()`) actually correct?**
  _`NBAException` has 31 INFERRED edges - model-reasoned connections that need verification._
- **What connects `BoxScoreEvaluation`, `ErrorCodes`, `TradeStatuses` to the rest of the system?**
  _695 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `TradeBetweenTeams` be split into smaller, more focused modules?**
  _Cohesion score 0.09898989898989899 - nodes in this community are weakly interconnected._
- **Should `DraftState` be split into smaller, more focused modules?**
  _Cohesion score 0.05714285714285714 - nodes in this community are weakly interconnected._