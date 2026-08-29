# Graph Report - NBAFantasy  (2026-08-29)

## Corpus Check
- 180 files · ~51,001 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 1917 nodes · 3561 edges · 119 communities (111 shown, 8 thin omitted)
- Extraction: 92% EXTRACTED · 8% INFERRED · 0% AMBIGUOUS · INFERRED: 294 edges (avg confidence: 0.82)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `159d042d`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- TradeBetweenTeams
- DraftRedisOperations
- TradeService
- PlayerShort
- Project Rules & Vendor Licenses
- .CreateClient
- Player Memento Stats
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
- Player
- League
- League Entity
- NBA.Data.Context
- NbaFantasyRedis
- NBA.Data.Redis.Entities
- ApplicationDefaults.Exceptions
- DraftService
- AppHost Launch Settings
- JwtOptions
- Core Database Schema DDL
- Team
- Game Redis Shapes
- ExternalClients Project Files
- AuthTokenIssuer
- PlayerInfo Response Shape
- MetaData
- API Launch Profiles
- Playoff Bracket Entities
- Transaction Entities
- NBAException
- PlayerStatsResponse
- Per-League Stats Values
- PlayerService
- Usertrophie
- NBA.Data Package References
- Aspire Manifest OTEL Config
- Auth Request DTOs
- ShortenJobExpirationFilter
- NBA.Data.Entities
- TradeManager
- GameInfoResponse
- GameService
- TeamDto
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
- BoxScoreCalculationBuilder
- PlayerManager
- ServiceDefaults Packages
- Entity Mapping Tests
- Aspire Postgres Connection
- Aspire Postgres Container
- Draft Timer Hosted Service
- ServiceDefaults Extensions
- LeagueDto
- ScheduledGames
- AppHost Packages
- Draftsnapshot
- TeamInfoResponse
- .InvokeMethodAsync
- LoginDto
- .BucketByDay
- Aspire HTTPS Bindings
- Chat Hub
- Argon2idPasswordHasherTests
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
- JwtTokenServiceTests
- Chat Schema DDL
- .PerformCalculations
- Aspire HTTP Bindings
- NBA.Data.Redis
- Infrastructure Init Entry
- Naming Rule
- .MapPlayerEndpoints
- .GetUserId
- .MapTradeEndpoints

## God Nodes (most connected - your core abstractions)
1. `NbaFantasyContext` - 85 edges
2. `TradeBetweenTeams` - 52 edges
3. `NBAException` - 47 edges
4. `NBA.Data.Entities` - 43 edges
5. `PlayerSearchInput` - 40 edges
6. `PlayersFilterSearch` - 38 edges
7. `NBA.Data.Redis.Entities` - 36 edges
8. `Applicationuser` - 35 edges
9. `Team` - 35 edges
10. `DraftState` - 34 edges

## Surprising Connections (you probably didn't know these)
- `TradeHub` --references--> `ApplicationOptions`  [EXTRACTED]
  NBA.Api/SignalR/Hubs/TradeHub.cs → ApplicationDefaults/Options/ApplicationOptions.cs
- `DraftService` --references--> `ApplicationOptions`  [EXTRACTED]
  NBA.Service/Draft/DraftService.cs → ApplicationDefaults/Options/ApplicationOptions.cs
- `TradeManager` --references--> `ApplicationOptions`  [EXTRACTED]
  NBA.Service/Trade/TradeManager.cs → ApplicationDefaults/Options/ApplicationOptions.cs
- `ApplicationHostedService` --references--> `BallDontLieClientOptions`  [EXTRACTED]
  NBA.Api/HostedService/ApplicationHostedService.cs → ApplicationDefaults/Options/BallDontLieClientOptions.cs
- `GameService` --references--> `BallDontLieClientOptions`  [EXTRACTED]
  NBA.Service/Game/GameService.cs → ApplicationDefaults/Options/BallDontLieClientOptions.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Draft Pick Processing Flow** — claude_drafthub, claude_playermanager, claude_draftmanager, claude_draftstate [EXTRACTED 0.95]
- **Draft Timer & Deadline Coordination** — claude_drafttimerhostedservice, claude_draftredisoperations, claude_draftmanager, claude_draft_realtime [EXTRACTED 0.95]
- **External HTTP Resilience Strategy** — claude_resilience_pipeline_rule, claude_externalclients, claude_nba_servicedefaults [INFERRED 0.85]

## Communities (119 total, 8 thin omitted)

### Community 0 - "TradeBetweenTeams"
Cohesion: 0.06
Nodes (41): HubConnection, HubException, List, Task, ITradeHubClient, DateTimeOffset, Guid, List (+33 more)

### Community 1 - "DraftRedisOperations"
Cohesion: 0.09
Nodes (17): DateTimeOffset, Dictionary, IDatabase, JsonSerializerOptions, List, Queue, Task, TimeSpan (+9 more)

### Community 2 - "TradeService"
Cohesion: 0.19
Nodes (10): Created, IQueryable, DateTime, Guid, ILogger, List, Task, TradeData (+2 more)

### Community 3 - "PlayerShort"
Cohesion: 0.06
Nodes (29): IEnumerable, List, PlayerShortMappings, PlayerShort, FullName, PlayerId, Position, RedisKeys (+21 more)

### Community 4 - "Project Rules & Vendor Licenses"
Cohesion: 0.05
Nodes (51): Adapter (static mapper), Adding an HTTP Endpoint Flow, ApplicationDefaults, ApplicationOptions, Argon2Options, Aspire AppHost, Auth & Tests, Authenticate Everything Rule (+43 more)

### Community 5 - ".CreateClient"
Cohesion: 0.11
Nodes (27): Action, NBA.Tests.Fakes, HttpMessageHandler, HttpRequestMessage, Fact, HttpResponseMessage, HttpStatusCode, InlineData (+19 more)

### Community 6 - "Player Memento Stats"
Cohesion: 0.04
Nodes (43): DateTime, ICollection, Player, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow (+35 more)

### Community 7 - "Player Search Request DTOs"
Cohesion: 0.05
Nodes (38): NBA.Api.Requests.Player, DateTime, PlayersFilterSearch, allowdrop, gameready, irlteamid, irlteamname, islock (+30 more)

### Community 8 - ".League"
Cohesion: 0.18
Nodes (9): Task, IEndpointRouteBuilder, DraftEndpoints, Task, IOptions, Task, DraftManager, Dictionary (+1 more)

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
Nodes (17): PlayerShortDto, FullName, PlayerId, Position, DateTime, Dictionary, List, DraftState (+9 more)

### Community 14 - "TradeHub"
Cohesion: 0.24
Nodes (7): Guid, IHubContext, ILogger, IOptions, List, Task, TradeHub

### Community 15 - "League & Stats Value Requests"
Cohesion: 0.07
Nodes (27): NBA.Api.Requests.League, NBA.Api.Requests.StatValue, LeagueRequest, Autostart, DraftStyle, LeagueName, LeagueType, ScoringSystem (+19 more)

### Community 16 - "TradeHubFixture"
Cohesion: 0.11
Nodes (18): AuthenticateResult, AuthenticationHandler, AuthenticationSchemeOptions, ICollectionFixture, IConnectionMultiplexer, IHost, IDatabase, IOptions (+10 more)

### Community 17 - "NbaFantasyContext"
Cohesion: 0.09
Nodes (24): DbContext, DbSet, ModelBuilder, Player, Trade, NbaFantasyContext, Applicationusers, Draftsnapshots (+16 more)

### Community 18 - "Task"
Cohesion: 0.14
Nodes (11): CancellationToken, List, Player, Task, Trade, Teamplayer, Player, Playerid (+3 more)

### Community 19 - "Applicationuser"
Cohesion: 0.11
Nodes (16): ICollection, Applicationuser, Email, Managerlevel, Password, Teams, Userid, Userleagues (+8 more)

### Community 20 - "NBA.Api.DTOs"
Cohesion: 0.13
Nodes (10): NBA.Api.DTOs, NBA.Api.Mappings, NBA.Api.Requests.Team, NBA.Api.Authentication, NBA.Tests, NBA.Service, NBA.Api.Endpoints, TeamRequest (+2 more)

### Community 21 - "Player DTO"
Cohesion: 0.08
Nodes (24): DateTime, PlayerDto, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow, Gameready (+16 more)

### Community 22 - "GameDto"
Cohesion: 0.12
Nodes (16): DateTime, List, GameDto, Date, GameId, HomeTeam, Postponed, Postseason (+8 more)

### Community 23 - "Player"
Cohesion: 0.15
Nodes (7): NBA.Data.Enumerations, ApplicationDefaults.LogDefaults, NBA.Service.Roster, NBA.Data.Constants, TradeStatuses, Player, PlayerPositionExtensions

### Community 24 - "League"
Cohesion: 0.14
Nodes (14): IEndpointRouteBuilder, LeagueEndpoints, League, PagedResult, Task, TeamData, CreateLeagueInput, JoinLeagueInput (+6 more)

### Community 25 - "League Entity"
Cohesion: 0.09
Nodes (21): ICollection, League, Autostart, Commissioner, Draftcompleted, Draftsnapshot, Draftstyle, Leagueid (+13 more)

### Community 26 - "NBA.Data.Context"
Cohesion: 0.20
Nodes (10): NBA.Api.Draft, NBA.Api.HostedService, NBA.Api.SignalR.Clients, NBA.Data.Context, NBA.Data.Redis.Enumerations, NBA.Service.Draft, NBA.Api.SignalR.Hubs, NBA.Service.Player (+2 more)

### Community 27 - "NbaFantasyRedis"
Cohesion: 0.13
Nodes (16): Lazy, IHubContext, IOptions, DraftTimerProcessor, Task, IDraftHubClient, IOptions, DraftHub (+8 more)

### Community 28 - "NBA.Data.Redis.Entities"
Cohesion: 0.25
Nodes (5): NBA.Data.Redis.Operations, NBA.Data.Redis.Scopes, NBA.Data.Redis.Keys, NBA.Data.Redis.Entities, NBA.Data.Redis.Dtos

### Community 29 - "ApplicationDefaults.Exceptions"
Cohesion: 0.14
Nodes (9): ErrorCodes, ApplicationDefaults.Time, ExternalClients.Response, ExternalClients, NBA.Service.Game, ApplicationDefaults.Exceptions, NBA.Tests.Integration, NBA.Service.CalculateBoxScore (+1 more)

### Community 30 - "DraftService"
Cohesion: 0.12
Nodes (14): DraftOptions, DraftPickTime, Rounds, ShowTeamDraftBoardCount, IServiceCollection, DraftExtention, IOptions, JsonOptions (+6 more)

### Community 31 - "AppHost Launch Settings"
Cohesion: 0.13
Nodes (18): ASPIRE_DASHBOARD_OTLP_ENDPOINT_URL, ASPIRE_RESOURCE_SERVICE_ENDPOINT_URL, ASPNETCORE_ENVIRONMENT, DOTNET_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables (+10 more)

### Community 32 - "JwtOptions"
Cohesion: 0.17
Nodes (11): JwtOptions, AccessTokenMinutes, Audience, Issuer, RefreshTokenDays, SigningKey, DateTime, AuthToken (+3 more)

### Community 33 - "Core Database Schema DDL"
Cohesion: 0.24
Nodes (17): nba.applicationuser, nba.draftsnapshot, nba.league, nba.leagueplayer, nba.player, nba.playermemento, nba.playoff, nba.playoffbracket (+9 more)

### Community 34 - "Team"
Cohesion: 0.11
Nodes (17): ICollection, Team, Approved, Categoryleaguepoints, Islock, Lastweekpoints, League, Leagueid (+9 more)

### Community 35 - "Game Redis Shapes"
Cohesion: 0.12
Nodes (17): DateTime, GameShort, Date, GameId, HomeTeam, Postponed, Postseason, StartTime (+9 more)

### Community 36 - "ExternalClients Project Files"
Cohesion: 0.13
Nodes (16): ApplicationDefaults, net10.0, Microsoft.NET.Sdk, BoxScoreBuilder, net10.0, Microsoft.NET.Sdk, ExternalClients, net10.0 (+8 more)

### Community 37 - "AuthTokenIssuer"
Cohesion: 0.18
Nodes (10): DateTime, IOptions, Task, AuthTokenIssuer, TokenPair, RefreshTokenGenerator, IEndpointRouteBuilder, AuthenticationEndpoints (+2 more)

### Community 38 - "PlayerInfo Response Shape"
Cohesion: 0.12
Nodes (16): PlayerInfoResponse, college, country, draft_number, draft_round, draft_year, first_name, height (+8 more)

### Community 39 - "MetaData"
Cohesion: 0.05
Nodes (43): BallDontLieClientOptions, ApiKey, BaseUrl, Per_Page, CancellationToken, DateOnly, HttpResponseMessage, List (+35 more)

### Community 40 - "API Launch Profiles"
Cohesion: 0.13
Nodes (15): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, applicationUrl, commandName (+7 more)

### Community 41 - "Playoff Bracket Entities"
Cohesion: 0.12
Nodes (14): ICollection, Playoff, League, Leagueid, Playoffbrackets, Playoffid, Totalrounds, Playoffbracket (+6 more)

### Community 42 - "Transaction Entities"
Cohesion: 0.12
Nodes (14): DateTime, ICollection, Transaction, Transactionid, Transactionleagues, Transactionstatus, Tscreated, Typetransaction (+6 more)

### Community 43 - "NBAException"
Cohesion: 0.22
Nodes (10): NBAException, ErrorCode, Exception, IEndpointRouteBuilder, TeamEndpoints, Dictionary, List, Task (+2 more)

### Community 44 - "PlayerStatsResponse"
Cohesion: 0.13
Nodes (14): PlayerStatsResponse, ast, blk, fg3a, fg3m, fga, fgm, fta (+6 more)

### Community 45 - "Per-League Stats Values"
Cohesion: 0.13
Nodes (14): Statsvalue, Assistsvalue, Blocksvalue, Fieldgoalvaluemade, Fieldgoalvaluemissed, Freethrowvaluemade, Freethrowvaluemissed, League (+6 more)

### Community 46 - "PlayerService"
Cohesion: 0.23
Nodes (8): AutomaticRetry, JobDisplayName, CancellationToken, DateTime, List, PagedResult, Task, PlayerService

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

### Community 51 - "ShortenJobExpirationFilter"
Cohesion: 0.14
Nodes (12): ApplyStateContext, NBA.Api.HangFire, NBA.Api, IApplyStateFilter, IConfiguration, InvalidOperationException, IWriteOnlyTransaction, JobFilterAttribute (+4 more)

### Community 52 - "NBA.Data.Entities"
Cohesion: 0.15
Nodes (5): NBA.Service.League, NBA.Data.Entities, NBA.Service.FreeAgency, NBA.Service.Authentication, NBA.Service.Team

### Community 53 - "TradeManager"
Cohesion: 0.25
Nodes (8): Dictionary, Guid, IOptions, List, Task, TradeManager, newFromPlayers, newToPlayers

### Community 54 - "GameInfoResponse"
Cohesion: 0.11
Nodes (18): DateTime, GameInfoResponse, date, datetime, home_team, home_team_score, id, postponed (+10 more)

### Community 55 - "GameService"
Cohesion: 0.16
Nodes (13): IBackgroundJobClient, IEndpointRouteBuilder, GameEndpoints, DateOnly, Task, TimeSpan, GameManager, CancellationToken (+5 more)

### Community 56 - "TeamDto"
Cohesion: 0.13
Nodes (13): List, DraftOrderDto, Round, Teams, TeamDto, Categoryleaguepoints, Competesinleague, Islock (+5 more)

### Community 57 - "Leagueplayer"
Cohesion: 0.17
Nodes (9): Leagueplayer, Isfreeagent, League, Leagueid, Leagueplayerid, Playerid, List, Task (+1 more)

### Community 58 - ".ToPlayerDb"
Cohesion: 0.20
Nodes (7): List, PlayerData, Adapter, Fact, InlineData, Theory, AdapterTests

### Community 59 - "TeamDraftBoard"
Cohesion: 0.12
Nodes (18): List, DraftBoardTeams, CurrentRound, DraftOrder, onTheClockTeam, TeamDraftBoard, Pick, TeamId (+10 more)

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
Cohesion: 0.18
Nodes (9): PlayerPositionEnum, C, CF, F, FG, G, GF, UNKOWN (+1 more)

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

### Community 77 - "LeagueDto"
Cohesion: 0.14
Nodes (13): LeagueDto, Autostart, Commissioner, CommissionersTeam, Draftstyle, Leagueid, Name, Seasonyear (+5 more)

### Community 78 - "ScheduledGames"
Cohesion: 0.19
Nodes (10): List, ScheduledGames, RestOfWeek, Today, Tomorrow, IDatabase, JsonSerializerOptions, Task (+2 more)

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

### Community 83 - "LoginDto"
Cohesion: 0.22
Nodes (8): List, LoginDto, Leagues, RefreshToken, Teams, Token, Userid, Username

### Community 85 - "Aspire HTTPS Bindings"
Cohesion: 0.25
Nodes (8): https, protocol, scheme, transport, bindings, path, type, nba-api

### Community 86 - "Chat Hub"
Cohesion: 0.25
Nodes (5): Hub, Task, IChatHubClient, Task, ChatHub

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
Cohesion: 0.25
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
Cohesion: 0.33
Nodes (6): GameTeamDto, Abbreviation, City, FullName, Score, TeamId

### Community 96 - "DraftLifecycleService"
Cohesion: 0.18
Nodes (7): IEndpointRouteBuilder, TestingEndpoints, IOptions, List, Task, DraftLifecycleService, Task

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

### Community 101 - "JwtTokenServiceTests"
Cohesion: 0.53
Nodes (3): Fact, Task, JwtTokenServiceTests

### Community 102 - "Chat Schema DDL"
Cohesion: 0.70
Nodes (4): chat.conversationparticipants, chat.messages, chat.rooms, nba.applicationuser

### Community 103 - ".PerformCalculations"
Cohesion: 0.40
Nodes (4): Dictionary, List, Task, BoxScoreCalculationService

### Community 104 - "Aspire HTTP Bindings"
Cohesion: 0.50
Nodes (4): http, protocol, scheme, transport

### Community 105 - "NBA.Data.Redis"
Cohesion: 0.50
Nodes (3): NBA.Data.Redis, JsonSerializerOptions, RedisSerializer

## Knowledge Gaps
- **682 isolated node(s):** `net10.0`, `Microsoft.NET.Sdk`, `ErrorCodes`, `ErrorMessage`, `ErrorCode` (+677 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **8 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `NbaFantasyContext` connect `NbaFantasyContext` to `TradeService`, `Player Memento Stats`, `ApplicationHostedService`, `DraftState`, `TradeHubFixture`, `Task`, `Applicationuser`, `League`, `DraftService`, `Team`, `AuthTokenIssuer`, `Playoff Bracket Entities`, `Transaction Entities`, `NBAException`, `PlayerService`, `Usertrophie`, `NBA.Data.Entities`, `Leagueplayer`, `PlayerManager`, `Draftsnapshot`, `Userleague`, `DraftLifecycleService`, `.PerformCalculations`?**
  _High betweenness centrality (0.129) - this node is a cross-community bridge._
- **Why does `Player` connect `Player` to `EntityMappings`, `PlayerShort`, `NBA.Data.Context`, `BoxScoreCalculationBuilder`, `Player Memento Stats`, `.PerformCalculations`, `PlayerManager`, `NBAException`, `PlayerService`, `Task`, `NBA.Data.Entities`, `Leagueplayer`, `.ToPlayerDb`, `ApplicationDefaults.Exceptions`?**
  _High betweenness centrality (0.102) - this node is a cross-community bridge._
- **Why does `NBAException` connect `NBAException` to `DraftLifecycleService`, `TradeService`, `PlayerPositionEnum`, `AuthTokenIssuer`, `.CreateClient`, `MetaData`, `.League`, `ApplicationOptions`, `BallDontLieClientWireMockTests`, `TradeHub`, `PlayerService`, `Task`, `Applicationuser`, `.GetUserId`, `TradeManager`, `League`, `TeamDraftBoard`?**
  _High betweenness centrality (0.093) - this node is a cross-community bridge._
- **Are the 3 inferred relationships involving `TradeBetweenTeams` (e.g. with `.RemoveProposedTrade_removes_only_the_matching_trade_and_returns_it()` and `.RemoveProposedTrade_returns_null_for_unknown_id()`) actually correct?**
  _`TradeBetweenTeams` has 3 INFERRED edges - model-reasoned connections that need verification._
- **Are the 33 inferred relationships involving `NBAException` (e.g. with `.GetAsync()` and `.RefreshAsync()`) actually correct?**
  _`NBAException` has 33 INFERRED edges - model-reasoned connections that need verification._
- **What connects `net10.0`, `Microsoft.NET.Sdk`, `ErrorCodes` to the rest of the system?**
  _682 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `TradeBetweenTeams` be split into smaller, more focused modules?**
  _Cohesion score 0.06288568909785483 - nodes in this community are weakly interconnected._