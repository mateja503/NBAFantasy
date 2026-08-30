# Graph Report - NBAFantasy  (2026-08-30)

## Corpus Check
- 187 files · ~53,658 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2037 nodes · 3726 edges · 140 communities (126 shown, 14 thin omitted)
- Extraction: 92% EXTRACTED · 8% INFERRED · 0% AMBIGUOUS · INFERRED: 282 edges (avg confidence: 0.82)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `0023388d`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- TradeBetweenTeams
- LeagueDraft
- .ProposeAsync
- PlayerShort
- Project Rules & Vendor Licenses
- .CreateClient
- Player
- PlayersFilterSearch
- .League
- ApplicationHostedService
- Player Service Search
- NBAException
- BoxScoreStatsBuilder
- .InitializeAsync
- DraftRedisOperations
- League & Stats Value Requests
- TradeHubFixture
- NbaFantasyContext
- Player
- Applicationuser
- NBA.Data.Entities
- PlayerDto
- GameDto
- .BuildHub
- Team
- League
- Program.cs
- NbaFantasyRedis
- NBA.Data.Redis.Entities
- ExternalClients.Response
- AuthTokenIssuer
- AppHost Launch Settings
- ShortenJobExpirationFilter
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
- DraftTimerProcessor
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
- .EnsureRehydratedAsync
- ServiceDefaults Extensions
- ChatHub
- TeamDraftBoard
- AppHost Packages
- TradeHub
- TeamInfoResponse
- DraftState
- ScheduledGames
- .BucketByDay
- Aspire HTTPS Bindings
- .TryHandleAsync
- MetaData
- Draft Type Enum
- Draft Status Enum
- TradeOutcome
- Draftsnapshot
- AppHost Hosting Packages
- GameRedisOperations
- Aspire Server Bindings
- DraftService
- ITradeOrchestrator
- LeagueTeamDto
- Aspire Password Parameters
- League Team Insert Request
- Draft Request DTO
- .DraftOrder
- Chat Schema DDL
- HangFireJobSchedulerHostedService
- Aspire HTTP Bindings
- PagedResult
- Infrastructure Init Entry
- Naming Rule
- League
- Task
- DraftEndDraftTests
- .GetDraftTeams
- Argon2idPasswordHasherTests
- Log
- IEndpointRouteBuilder
- Userleague
- IServiceCollection
- DraftBoardTeams
- List
- JsonOptions
- JsonSerializerOptions
- Fact
- IReadOnlyList
- Team
- GameTeamDto
- DraftHub
- BallDontLieClientOptions
- .PrepareDraftBoard
- NBA.Api.Requests.Team
- GetAllTeamsResponse
- adding-an-endpoint/SKILL.md
- first-time-setup/SKILL.md

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
- `Accepted` --references--> `TradeBetweenTeams`  [EXTRACTED]
  NBA.Service/Trade/TradeEvent.cs → NBA.Data/Redis/Entities/TradeBetweenTeams.cs
- `OfferedToLeague` --references--> `TradeBetweenTeams`  [EXTRACTED]
  NBA.Service/Trade/TradeEvent.cs → NBA.Data/Redis/Entities/TradeBetweenTeams.cs
- `Rejected` --references--> `TradeBetweenTeams`  [EXTRACTED]
  NBA.Service/Trade/TradeEvent.cs → NBA.Data/Redis/Entities/TradeBetweenTeams.cs
- `Superseded` --references--> `TradeBetweenTeams`  [EXTRACTED]
  NBA.Service/Trade/TradeEvent.cs → NBA.Data/Redis/Entities/TradeBetweenTeams.cs
- `DraftOrderManager` --references--> `NbaFantasyRedis`  [EXTRACTED]
  NBA.Service/Draft/DraftOrderManager.cs → NBA.Data/Context/NbaFantasyRedis.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Draft Pick Processing Flow** — claude_drafthub, claude_playermanager, claude_draftmanager, claude_draftstate [EXTRACTED 0.95]
- **Draft Timer & Deadline Coordination** — claude_drafttimerhostedservice, claude_draftredisoperations, claude_draftmanager, claude_draft_realtime [EXTRACTED 0.95]
- **External HTTP Resilience Strategy** — claude_resilience_pipeline_rule, claude_externalclients, claude_nba_servicedefaults [INFERRED 0.85]

## Communities (140 total, 14 thin omitted)

### Community 0 - "TradeBetweenTeams"
Cohesion: 0.06
Nodes (40): IHubCallerClients, Method, List, Task, ITradeHubClient, DateTimeOffset, Guid, List (+32 more)

### Community 1 - "LeagueDraft"
Cohesion: 0.16
Nodes (8): DateTimeOffset, Dictionary, List, Queue, Task, TimeSpan, LeagueDraft, LeagueId

### Community 2 - ".ProposeAsync"
Cohesion: 0.16
Nodes (12): IServiceCollection, TradeExtention, Guid, IOptions, List, Task, TradeManager, Guid (+4 more)

### Community 3 - "PlayerShort"
Cohesion: 0.07
Nodes (25): IEnumerable, List, PlayerShortMappings, PlayerShort, FullName, PlayerId, Position, RedisKeys (+17 more)

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
Nodes (38): NBA.Api.Requests.Player, DateTime, PlayersFilterSearch, allowdrop, gameready, irlteamid, irlteamname, islock (+30 more)

### Community 8 - ".League"
Cohesion: 0.19
Nodes (9): DraftState, Task, DraftOptions, DraftSnapshotService, DraftState, IOptions, NbaFantasyRedis, Task (+1 more)

### Community 9 - "ApplicationHostedService"
Cohesion: 0.27
Nodes (7): CancellationToken, ILogger, IOptions, IServiceProvider, Task, TimeSpan, ApplicationHostedService

### Community 10 - "Player Service Search"
Cohesion: 0.06
Nodes (36): PlayerSearchInput, Allowdrop, Gameready, Irlteamid, Irlteamname, Islock, LeagueId, MaxAssists (+28 more)

### Community 11 - "NBAException"
Cohesion: 0.05
Nodes (46): NBAException, ErrorCode, Created, Exception, IClassFixture, IQueryable, IRequestMessage, IResponseBuilder (+38 more)

### Community 12 - "BoxScoreStatsBuilder"
Cohesion: 0.07
Nodes (17): BoxScoreStatsBuilder, PlayerStats, ast, blk, fg3a, fg3m, fga, fgm (+9 more)

### Community 13 - ".InitializeAsync"
Cohesion: 0.25
Nodes (7): ApplicationOptions, CenterLimit, MaxPlayersPerTeam, ProposedTradeTtlMinutes, AuthenticationHandler, AuthenticationSchemeOptions, TestAuthHandler

### Community 14 - "DraftRedisOperations"
Cohesion: 0.19
Nodes (7): DateTimeOffset, IDatabase, JsonSerializerOptions, List, Task, TimeSpan, DraftRedisOperations

### Community 15 - "League & Stats Value Requests"
Cohesion: 0.07
Nodes (27): NBA.Api.Requests.League, NBA.Api.Requests.StatValue, LeagueRequest, Autostart, DraftStyle, LeagueName, LeagueType, ScoringSystem (+19 more)

### Community 16 - "TradeHubFixture"
Cohesion: 0.08
Nodes (39): AuthenticateResult, ClaimsPrincipal, NBA.Api.SignalR, HubConnection, HubException, HubInvocationContext, ICollectionFixture, IConnectionMultiplexer (+31 more)

### Community 17 - "NbaFantasyContext"
Cohesion: 0.09
Nodes (24): DbContext, DbSet, ModelBuilder, Player, Trade, NbaFantasyContext, Applicationusers, Draftsnapshots (+16 more)

### Community 18 - "Player"
Cohesion: 0.21
Nodes (5): NBA.Data.Context, NBA.Service.FreeAgency, NBA.Service.Builder, NBA.Service.Team, Player

### Community 19 - "Applicationuser"
Cohesion: 0.11
Nodes (16): ICollection, Applicationuser, Email, Managerlevel, Password, Teams, Userid, Userleagues (+8 more)

### Community 20 - "NBA.Data.Entities"
Cohesion: 0.14
Nodes (9): NBA.Data.Entities, NBA.Api.DTOs, NBA.Service.Authentication, NBA.Api.Mappings, NBA.Api.Authentication, NBA.Tests, NBA.Service, NBA.Api.Endpoints (+1 more)

### Community 21 - "PlayerDto"
Cohesion: 0.08
Nodes (24): DateTime, PlayerDto, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow, Gameready (+16 more)

### Community 22 - "GameDto"
Cohesion: 0.12
Nodes (16): DateTime, List, GameDto, Date, GameId, HomeTeam, Postponed, Postseason (+8 more)

### Community 23 - ".BuildHub"
Cohesion: 0.27
Nodes (11): Clients, Accepted, OfferedToLeague, Rejected, Superseded, TradeEvent, Fact, Hub (+3 more)

### Community 24 - "Team"
Cohesion: 0.11
Nodes (17): ICollection, Team, Approved, Categoryleaguepoints, Islock, Lastweekpoints, League, Leagueid (+9 more)

### Community 25 - "League"
Cohesion: 0.09
Nodes (21): ICollection, League, Autostart, Commissioner, Draftcompleted, Draftsnapshot, Draftstyle, Leagueid (+13 more)

### Community 26 - "Program.cs"
Cohesion: 0.12
Nodes (8): NBA.Api.Draft, NBA.Service.League, NBA.Api.HostedService, NBA.Api.SignalR.Clients, NBA.Tests.Fakes, NBA.Service.Draft, NBA.Api.SignalR.Hubs, NBA.Service.CalculateBoxScore

### Community 27 - "NbaFantasyRedis"
Cohesion: 0.06
Nodes (29): BackgroundService, Lazy, CancellationToken, ILogger, IServiceProvider, Task, TimeSpan, DraftTimerHostedService (+21 more)

### Community 28 - "NBA.Data.Redis.Entities"
Cohesion: 0.13
Nodes (11): NBA.Data.Redis.Operations, NBA.Data.Redis.Scopes, NBA.Data.Enumerations, NBA.Data.Redis.Enumerations, NBA.Data.Redis.Keys, NBA.Data.Redis.Entities, NBA.Data.Redis.Dtos, NBA.Data.Redis (+3 more)

### Community 29 - "ExternalClients.Response"
Cohesion: 0.22
Nodes (6): ApplicationDefaults.Time, ExternalClients.Response, ExternalClients, NBA.Service.Game, NBA.Service.Player, ExternalClients.Poco

### Community 30 - "AuthTokenIssuer"
Cohesion: 0.18
Nodes (10): DateTime, IOptions, Task, AuthTokenIssuer, TokenPair, RefreshTokenGenerator, IEndpointRouteBuilder, AuthenticationEndpoints (+2 more)

### Community 31 - "AppHost Launch Settings"
Cohesion: 0.13
Nodes (18): ASPIRE_DASHBOARD_OTLP_ENDPOINT_URL, ASPIRE_RESOURCE_SERVICE_ENDPOINT_URL, ASPNETCORE_ENVIRONMENT, DOTNET_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables (+10 more)

### Community 32 - "ShortenJobExpirationFilter"
Cohesion: 0.15
Nodes (11): ApplyStateContext, NBA.Api.HangFire, NBA.Api, IApplyStateFilter, IConfiguration, IWriteOnlyTransaction, JobFilterAttribute, HttpResponseMessage (+3 more)

### Community 33 - "create-objects-nba-schema.sql"
Cohesion: 0.22
Nodes (18): nba.applicationuser, nba.draftsnapshot, nba.league, nba.leagueplayer, nba.player, nba.playermemento, nba.playoff, nba.playoffbracket (+10 more)

### Community 34 - "LeagueDto"
Cohesion: 0.14
Nodes (13): LeagueDto, Autostart, Commissioner, CommissionersTeam, Draftstyle, Leagueid, Name, Seasonyear (+5 more)

### Community 35 - "Game Redis Shapes"
Cohesion: 0.12
Nodes (17): DateTime, GameShort, Date, GameId, HomeTeam, Postponed, Postseason, StartTime (+9 more)

### Community 36 - "ExternalClients Project Files"
Cohesion: 0.13
Nodes (16): ApplicationDefaults, net10.0, Microsoft.NET.Sdk, BoxScoreBuilder, net10.0, Microsoft.NET.Sdk, ExternalClients, net10.0 (+8 more)

### Community 37 - "BallDontLieWireMockFixture"
Cohesion: 0.18
Nodes (9): IAsyncLifetime, HttpResponseMessage, IOptions, Task, BallDontLieWireMockFixture, Client, Server, ServiceProvider (+1 more)

### Community 38 - "PlayerInfoResponse"
Cohesion: 0.14
Nodes (14): PlayerInfoResponse, college, country, draft_number, draft_round, draft_year, first_name, height (+6 more)

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

### Community 43 - "DraftTimerProcessor"
Cohesion: 0.20
Nodes (9): IEndpointRouteBuilder, IHubContext, DraftOptions, IDraftHubClient, IOptions, NbaFantasyRedis, Task, DraftTimerProcessor (+1 more)

### Community 44 - "PlayerStatsResponse"
Cohesion: 0.13
Nodes (14): PlayerStatsResponse, ast, blk, fg3a, fg3m, fga, fgm, fta (+6 more)

### Community 45 - "Per-League Stats Values"
Cohesion: 0.13
Nodes (14): Statsvalue, Assistsvalue, Blocksvalue, Fieldgoalvaluemade, Fieldgoalvaluemissed, Freethrowvaluemade, Freethrowvaluemissed, League (+6 more)

### Community 46 - "PlayerService"
Cohesion: 0.16
Nodes (11): AutomaticRetry, JobDisplayName, List, PlayerFilter, CancellationToken, DateTime, IReadOnlyList, List (+3 more)

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
Cohesion: 0.13
Nodes (9): ErrorCodes, ApplicationDefaults.LogDefaults, ApplicationDefaults.Exceptions, NBA.Service.Trade, NBA.Tests.Integration, NBA.Service.Roster, ApplicationDefaults.Options, NBA.Data.Constants (+1 more)

### Community 52 - "LoginDto"
Cohesion: 0.22
Nodes (8): List, LoginDto, Leagues, RefreshToken, Teams, Token, Userid, Username

### Community 53 - "JwtOptions"
Cohesion: 0.14
Nodes (15): JwtOptions, AccessTokenMinutes, Audience, Issuer, RefreshTokenDays, SigningKey, InvalidOperationException, DateTime (+7 more)

### Community 54 - "GameInfoResponse"
Cohesion: 0.15
Nodes (13): DateTime, GameInfoResponse, date, datetime, home_team, home_team_score, id, postponed (+5 more)

### Community 55 - "GameService"
Cohesion: 0.26
Nodes (8): IBackgroundJobClient, IEndpointRouteBuilder, GameEndpoints, CancellationToken, IOptions, List, Task, GameService

### Community 56 - "TeamDto"
Cohesion: 0.13
Nodes (13): List, DraftOrderDto, Round, Teams, TeamDto, Categoryleaguepoints, Competesinleague, Islock (+5 more)

### Community 57 - "Leagueplayer"
Cohesion: 0.17
Nodes (9): Leagueplayer, Isfreeagent, League, Leagueid, Leagueplayerid, Playerid, List, Task (+1 more)

### Community 58 - ".ToPlayerDb"
Cohesion: 0.20
Nodes (7): List, PlayerData, Adapter, Fact, InlineData, Theory, AdapterTests

### Community 59 - "DraftLifecycleService"
Cohesion: 0.17
Nodes (9): IEndpointRouteBuilder, TestingEndpoints, DraftOptions, DraftSnapshotService, IOptions, NbaFantasyContext, NbaFantasyRedis, Task (+1 more)

### Community 60 - "Trade"
Cohesion: 0.12
Nodes (15): DateTime, Guid, List, Trade, Fromteam, Fromteamid, League, Leagueid (+7 more)

### Community 61 - "UserTeamDto"
Cohesion: 0.15
Nodes (12): List, UserTeamDto, Categoryleaguepoints, Islock, Lastweekpoints, Leagueid, Leaguename, Name (+4 more)

### Community 62 - "Test Project Packages"
Cohesion: 0.17
Nodes (12): NBA.Tests, net10.0, Microsoft.NET.Sdk, coverlet.collector (6.0.2), Microsoft.AspNetCore.SignalR.Client (10.0.0), Microsoft.AspNetCore.TestHost (10.0.0), Microsoft.EntityFrameworkCore.InMemory (10.0.0), Microsoft.NET.Test.Sdk (17.12.0) (+4 more)

### Community 63 - "Argon2idPasswordHasher"
Cohesion: 0.18
Nodes (8): Argon2Options, DegreeOfParallelism, Iterations, MemoryKib, IPasswordHasher, IOptions, Argon2idPasswordHasher, PasswordVerificationResult

### Community 64 - "NBA Calendar Date Handling"
Cohesion: 0.24
Nodes (5): NbaCalendar, DateOnly, InlineData, Theory, TimeZoneInfo

### Community 65 - "TradeDto"
Cohesion: 0.15
Nodes (12): DateTime, Guid, List, TradeDto, Fromteamid, Leagueid, Playerids, Status (+4 more)

### Community 66 - "EntityMappings"
Cohesion: 0.22
Nodes (3): List, Team, EntityMappings

### Community 67 - "NBA.Api Package References"
Cohesion: 0.18
Nodes (10): net10.0, Aspire.StackExchange.Redis (13.1.2), Microsoft.Extensions.Http.Resilience (10.1.0), Aspire.Npgsql.EntityFrameworkCore.PostgreSQL (13.1.0), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.0), Microsoft.AspNetCore.OpenApi (10.0.0), Microsoft.AspNetCore.SignalR.StackExchangeRedis (10.0.5), Microsoft.OpenApi (2.7.5) (+2 more)

### Community 68 - "PlayerPositionEnum"
Cohesion: 0.22
Nodes (8): PlayerPositionEnum, C, CF, F, FG, G, GF, UNKOWN

### Community 69 - "BoxScoreCalculationBuilder"
Cohesion: 0.12
Nodes (5): BoxScoreCalculationBuilder, Dictionary, List, Task, BoxScoreCalculationService

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

### Community 75 - ".EnsureRehydratedAsync"
Cohesion: 0.15
Nodes (10): DraftOptions, DraftPickTime, Rounds, ShowTeamDraftBoardCount, Dictionary, IOptions, JsonSerializerOptions, Queue (+2 more)

### Community 76 - "ServiceDefaults Extensions"
Cohesion: 0.22
Nodes (3): Microsoft.Extensions.Hosting, Extensions, WebApplication

### Community 77 - "ChatHub"
Cohesion: 0.25
Nodes (5): Hub, Task, IChatHubClient, Task, ChatHub

### Community 78 - "TeamDraftBoard"
Cohesion: 0.16
Nodes (11): Task, IDraftHubClient, List, DraftBoardTeams, CurrentRound, DraftOrder, onTheClockTeam, TeamDraftBoard (+3 more)

### Community 79 - "AppHost Packages"
Cohesion: 0.22
Nodes (8): net10.0, Aspire.Hosting.Redis (13.1.2), Aspire.StackExchange.Redis (13.1.2), Microsoft.NET.Sdk, Aspire.Hosting.AppHost (13.1.0), Aspire.Hosting.PostgreSQL (13.1.0), CommunityToolkit.Aspire.Hosting.NodeJS.Extensions (9.9.0), OpenTelemetry.Api (1.16.0)

### Community 80 - "TradeHub"
Cohesion: 0.29
Nodes (6): Guid, ILogger, IReadOnlyList, List, Task, TradeHub

### Community 81 - "TeamInfoResponse"
Cohesion: 0.25
Nodes (8): TeamInfoResponse, abbreviation, city, conference, division, full_name, id, name

### Community 82 - "DraftState"
Cohesion: 0.14
Nodes (14): PlayerShortDto, FullName, PlayerId, Position, DateTime, Dictionary, List, DraftState (+6 more)

### Community 83 - "ScheduledGames"
Cohesion: 0.24
Nodes (9): List, ScheduledGames, RestOfWeek, Today, Tomorrow, DateOnly, Task, TimeSpan (+1 more)

### Community 84 - ".BucketByDay"
Cohesion: 0.50
Nodes (3): DateOnly, Fact, GameScheduleTests

### Community 85 - "Aspire HTTPS Bindings"
Cohesion: 0.25
Nodes (8): https, protocol, scheme, transport, bindings, path, type, nba-api

### Community 86 - ".TryHandleAsync"
Cohesion: 0.17
Nodes (10): ErrorResponse, ErrorCode, ErrorMessage, HttpContext, IExceptionHandler, GlobalExceptionHandler, CancellationToken, Exception (+2 more)

### Community 87 - "MetaData"
Cohesion: 0.13
Nodes (17): CancellationToken, DateOnly, List, Task, IBallDontLieClient, MetaData, Next_cursor, Per_page (+9 more)

### Community 88 - "Draft Type Enum"
Cohesion: 0.29
Nodes (6): DraftType, Auction, Linear, Offline, RRR, Snake

### Community 89 - "Draft Status Enum"
Cohesion: 0.29
Nodes (6): DraftStatus, DraftCompleted, DraftEnded, DraftStarted, Initial, Paused

### Community 90 - "TradeOutcome"
Cohesion: 0.33
Nodes (6): IReadOnlyList, TradeOutcome, Guid, List, TradeData, FakeTradeOrchestrator

### Community 91 - "Draftsnapshot"
Cohesion: 0.22
Nodes (6): DateTime, Draftsnapshot, Draftstate, Draftteams, Leagueid, Tsupdated

### Community 92 - "AppHost Hosting Packages"
Cohesion: 0.29
Nodes (7): NBA.Service, net10.0, Aspire.Hosting.Redis (13.1.2), Microsoft.Extensions.Options (10.0.3), Microsoft.NET.Sdk, Isopoh.Cryptography.Argon2 (1.1.10), Microsoft.Extensions.Identity.Core (10.0.0)

### Community 93 - "GameRedisOperations"
Cohesion: 0.32
Nodes (5): IDatabase, JsonSerializerOptions, Task, TimeSpan, GameRedisOperations

### Community 94 - "Aspire Server Bindings"
Cohesion: 0.29
Nodes (7): tcp, bindings, port, protocol, scheme, targetPort, transport

### Community 95 - "DraftService"
Cohesion: 0.12
Nodes (14): ApplicationOptions, IServiceCollection, JsonOptions, JsonSerializerOptions, DraftOrderManager, DraftSnapshotService, DraftExtention, DraftOptions (+6 more)

### Community 96 - "ITradeOrchestrator"
Cohesion: 0.43
Nodes (4): Guid, List, Task, ITradeOrchestrator

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

### Community 101 - ".DraftOrder"
Cohesion: 0.23
Nodes (8): Dictionary, Queue, Task, DraftOrderManager, Dictionary, Queue, Task, TeamDraftBoard

### Community 102 - "Chat Schema DDL"
Cohesion: 0.70
Nodes (4): chat.conversationparticipants, chat.messages, chat.rooms, nba.applicationuser

### Community 103 - "HangFireJobSchedulerHostedService"
Cohesion: 0.38
Nodes (5): IHostedService, IRecurringJobManager, CancellationToken, Task, HangFireJobSchedulerHostedService

### Community 104 - "Aspire HTTP Bindings"
Cohesion: 0.50
Nodes (4): http, protocol, scheme, transport

### Community 105 - "PagedResult"
Cohesion: 0.25
Nodes (5): IEndpointRouteBuilder, PlayerEndpoints, IReadOnlyList, PagedResult, TotalPages

### Community 116 - "League"
Cohesion: 0.19
Nodes (11): IEndpointRouteBuilder, LeagueEndpoints, League, PagedResult, Task, TeamData, CreateLeagueInput, JoinLeagueInput (+3 more)

### Community 117 - "Task"
Cohesion: 0.24
Nodes (5): CancellationToken, List, Player, Task, Trade

### Community 118 - "DraftEndDraftTests"
Cohesion: 0.37
Nodes (7): Fact, IReadOnlyList, List, NbaFantasyContext, Task, DraftEndDraftTests, TradeHubFixture

### Community 121 - "Log"
Cohesion: 0.33
Nodes (4): Log, message, request, response

### Community 123 - "Userleague"
Cohesion: 0.29
Nodes (6): Userleague, League, Leagueid, User, Userid, Userleagueid

### Community 131 - "Team"
Cohesion: 0.33
Nodes (5): Team, abbreviation, city, full_name, id

### Community 132 - "GameTeamDto"
Cohesion: 0.33
Nodes (6): GameTeamDto, Abbreviation, City, FullName, Score, TeamId

### Community 133 - "DraftHub"
Cohesion: 0.33
Nodes (6): DraftOptions, IDraftHubClient, IOptions, NbaFantasyRedis, DraftHub, PlayerManager

### Community 134 - "BallDontLieClientOptions"
Cohesion: 0.40
Nodes (4): BallDontLieClientOptions, ApiKey, BaseUrl, Per_Page

### Community 135 - ".PrepareDraftBoard"
Cohesion: 0.40
Nodes (4): DraftBoardTeams, Dictionary, Queue, TeamDraftBoard

### Community 136 - "NBA.Api.Requests.Team"
Cohesion: 0.50
Nodes (3): NBA.Api.Requests.Team, TeamRequest, teamName

### Community 137 - "GetAllTeamsResponse"
Cohesion: 0.50
Nodes (4): List, GetAllTeamsResponse, data, meta

## Knowledge Gaps
- **697 isolated node(s):** `Adding an HTTP endpoint`, `First-time secret setup`, `BoxScoreEvaluation`, `ErrorCodes`, `TradeStatuses` (+692 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **14 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `NbaFantasyContext` connect `NbaFantasyContext` to `Player`, `ApplicationHostedService`, `NBAException`, `.InitializeAsync`, `TradeHubFixture`, `Player`, `Applicationuser`, `Team`, `AuthTokenIssuer`, `Playoff Bracket Entities`, `Transaction Entities`, `PlayerService`, `Usertrophie`, `Leagueplayer`, `DraftLifecycleService`, `BoxScoreCalculationBuilder`, `PlayerManager`, `.EnsureRehydratedAsync`, `Draftsnapshot`, `League`, `Task`, `Userleague`?**
  _High betweenness centrality (0.118) - this node is a cross-community bridge._
- **Why does `NBAException` connect `NBAException` to `.ProposeAsync`, `.DraftOrder`, `.CreateClient`, `BallDontLieClient`, `.League`, `DraftTimerProcessor`, `.EnsureRehydratedAsync`, `PlayerService`, `TradeHubFixture`, `Applicationuser`, `League`, `DraftLifecycleService`, `AuthTokenIssuer`?**
  _High betweenness centrality (0.102) - this node is a cross-community bridge._
- **Why does `NbaFantasyRedis` connect `NbaFantasyRedis` to `TradeBetweenTeams`, `.ProposeAsync`, `PlayerShort`, `.DraftOrder`, `PlayerManager`, `.League`, `ApplicationHostedService`, `.EnsureRehydratedAsync`, `.InitializeAsync`, `DraftRedisOperations`, `TradeHubFixture`, `ScheduledGames`, `NBA.Data.Redis.Entities`, `GameRedisOperations`, `AuthTokenIssuer`?**
  _High betweenness centrality (0.073) - this node is a cross-community bridge._
- **Are the 31 inferred relationships involving `NBAException` (e.g. with `.GetAsync()` and `.RefreshAsync()`) actually correct?**
  _`NBAException` has 31 INFERRED edges - model-reasoned connections that need verification._
- **What connects `Adding an HTTP endpoint`, `First-time secret setup`, `BoxScoreEvaluation` to the rest of the system?**
  _697 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `TradeBetweenTeams` be split into smaller, more focused modules?**
  _Cohesion score 0.058823529411764705 - nodes in this community are weakly interconnected._
- **Should `PlayerShort` be split into smaller, more focused modules?**
  _Cohesion score 0.07393483709273183 - nodes in this community are weakly interconnected._