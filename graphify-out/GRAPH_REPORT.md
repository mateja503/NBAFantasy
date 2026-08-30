# Graph Report - NBAFantasy  (2026-08-30)

## Corpus Check
- 195 files · ~57,137 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2063 nodes · 3972 edges · 128 communities (119 shown, 9 thin omitted)
- Extraction: 92% EXTRACTED · 8% INFERRED · 0% AMBIGUOUS · INFERRED: 306 edges (avg confidence: 0.82)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `b850127c`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- .GetAllTeams
- DraftRedisOperations
- .ProposeAsync
- PlayerShort
- Project Rules & Vendor Licenses
- .CreateClient
- Player
- PlayersFilterSearch
- .League
- .EnsureRehydratedAsync
- PlayerSearchInput
- TradeService
- BoxScoreStatsBuilder
- TradeBetweenTeams
- LeagueDto
- League & Stats Value Requests
- TradeHubFixture
- NbaFantasyContext
- NBA.Data.Context
- Applicationuser
- Team
- PlayerDto
- GameDto
- NBA.Data.Enumerations
- Player
- League
- DraftEndDraftTests
- DraftTimerHostedService
- NBA.Data.Redis.Operations
- ExternalClients.Response
- GameService
- AppHost Launch Settings
- NBA.Data.Redis.Entities
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
- TradeHub
- PlayerStatsResponse
- Per-League Stats Values
- NbaCalendar
- .OnModelCreating
- NBA.Data Package References
- Aspire Manifest OTEL Config
- NBA.Api.Requests.Authentication
- PlayerService
- TradeDto
- BallDontLieClient
- GameInfoResponse
- .BuildHub
- TeamDto
- FreeAgencyPickUpRequest
- .ToPlayerDb
- JwtOptions
- Trade
- UserTeamDto
- Test Project Packages
- TradeOutcome
- ScheduledGames
- DraftLifecycleService
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
- DraftType
- AppHost Packages
- GameTeamDto
- TeamInfoResponse
- DraftState
- .BucketByDay
- .ToggleFreeAgencyStatus
- Aspire HTTPS Bindings
- ApplicationHostedService
- ChatHub
- NBA.Data.Entities
- Draft Status Enum
- ITradeOrchestrator
- PlayerPositionEnum
- AppHost Hosting Packages
- NBA.Api.Requests.Team
- Aspire Server Bindings
- Draftsnapshot
- LeaguePlayerSeedTests
- LeagueTeamDto
- Aspire Password Parameters
- League Team Insert Request
- Draft Request DTO
- TeamDraftBoard
- Chat Schema DDL
- NbaFantasyRedis
- Aspire HTTP Bindings
- League
- Infrastructure Init Entry
- Naming Rule
- Teamplayer
- .RegisterPlayer
- Team
- NBAException
- Argon2idPasswordHasher
- Userleague
- .MapTradeEndpoints
- Q: Tell me how individual players are stored in redis
- .MapPlayerEndpoints
- .ToPlayerDb_maps_position_string_to_enum
- adding-an-endpoint/SKILL.md
- first-time-setup/SKILL.md

## God Nodes (most connected - your core abstractions)
1. `NbaFantasyContext` - 106 edges
2. `NBAException` - 49 edges
3. `TradeBetweenTeams` - 49 edges
4. `NBA.Data.Entities` - 44 edges
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
- `GameService` --references--> `BallDontLieClientOptions`  [EXTRACTED]
  NBA.Service/Game/GameService.cs → ApplicationDefaults/Options/BallDontLieClientOptions.cs
- `DraftHub` --references--> `DraftOptions`  [EXTRACTED]
  NBA.Api/SignalR/Hubs/DraftHub.cs → ApplicationDefaults/Options/DraftOptions.cs
- `DraftLifecycleService` --references--> `DraftOptions`  [EXTRACTED]
  NBA.Service/Draft/DraftLifecycleService.cs → ApplicationDefaults/Options/DraftOptions.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Draft Pick Processing Flow** — claude_drafthub, claude_playermanager, claude_draftmanager, claude_draftstate [EXTRACTED 0.95]
- **Draft Timer & Deadline Coordination** — claude_drafttimerhostedservice, claude_draftredisoperations, claude_draftmanager, claude_draft_realtime [EXTRACTED 0.95]
- **External HTTP Resilience Strategy** — claude_resilience_pipeline_rule, claude_externalclients, claude_nba_servicedefaults [INFERRED 0.85]

## Communities (128 total, 9 thin omitted)

### Community 0 - ".GetAllTeams"
Cohesion: 0.21
Nodes (8): IQueryable, IEndpointRouteBuilder, TeamEndpoints, Dictionary, List, Task, TeamData, TeamService

### Community 1 - "DraftRedisOperations"
Cohesion: 0.09
Nodes (17): DateTimeOffset, Dictionary, IDatabase, JsonSerializerOptions, List, Queue, Task, TimeSpan (+9 more)

### Community 2 - ".ProposeAsync"
Cohesion: 0.12
Nodes (17): ApplicationOptions, CenterLimit, MaxPlayersPerTeam, ProposedTradeTtlMinutes, Rejected, IServiceCollection, TradeExtention, Guid (+9 more)

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
Cohesion: 0.24
Nodes (6): Task, IEndpointRouteBuilder, DraftEndpoints, IOptions, Task, DraftManager

### Community 9 - ".EnsureRehydratedAsync"
Cohesion: 0.29
Nodes (3): Dictionary, Queue, Task

### Community 10 - "PlayerSearchInput"
Cohesion: 0.06
Nodes (36): PlayerSearchInput, Allowdrop, Gameready, Irlteamid, Irlteamname, Islock, LeagueId, MaxAssists (+28 more)

### Community 11 - "TradeService"
Cohesion: 0.19
Nodes (10): Created, Trade, DateTime, Guid, ILogger, List, Task, TradeData (+2 more)

### Community 12 - "BoxScoreStatsBuilder"
Cohesion: 0.07
Nodes (17): BoxScoreStatsBuilder, PlayerStats, ast, blk, fg3a, fg3m, fga, fgm (+9 more)

### Community 13 - "TradeBetweenTeams"
Cohesion: 0.06
Nodes (40): IHubCallerClients, Method, List, Task, ITradeHubClient, DateTimeOffset, Guid, List (+32 more)

### Community 14 - "LeagueDto"
Cohesion: 0.14
Nodes (13): LeagueDto, Autostart, Commissioner, CommissionersTeam, Draftstyle, Leagueid, Name, Seasonyear (+5 more)

### Community 15 - "League & Stats Value Requests"
Cohesion: 0.07
Nodes (27): NBA.Api.Requests.League, NBA.Api.Requests.StatValue, LeagueRequest, Autostart, DraftStyle, LeagueName, LeagueType, ScoringSystem (+19 more)

### Community 16 - "TradeHubFixture"
Cohesion: 0.08
Nodes (42): AuthenticateResult, AuthenticationHandler, AuthenticationSchemeOptions, ClaimsPrincipal, NBA.Api.SignalR, HubConnection, HubException, HubInvocationContext (+34 more)

### Community 17 - "NbaFantasyContext"
Cohesion: 0.08
Nodes (25): DbContext, DbSet, NbaFantasyContext, Applicationusers, Draftsnapshots, Leagueplayers, Leagues, Playermementos (+17 more)

### Community 18 - "NBA.Data.Context"
Cohesion: 0.17
Nodes (13): ErrorCodes, NBA.Api.Draft, NBA.Service.League, NBA.Api.HostedService, NBA.Data.Context, NBA.Data.Redis.Enumerations, NBA.Service.Draft, NBA.Api.SignalR.Hubs (+5 more)

### Community 19 - "Applicationuser"
Cohesion: 0.13
Nodes (16): ICollection, Applicationuser, Email, Managerlevel, Password, Teams, Userid, Userleagues (+8 more)

### Community 20 - "Team"
Cohesion: 0.11
Nodes (17): ICollection, Team, Approved, Categoryleaguepoints, Islock, Lastweekpoints, League, Leagueid (+9 more)

### Community 21 - "PlayerDto"
Cohesion: 0.08
Nodes (24): DateTime, PlayerDto, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow, Gameready (+16 more)

### Community 22 - "GameDto"
Cohesion: 0.12
Nodes (16): DateTime, List, GameDto, Date, GameId, HomeTeam, Postponed, Postseason (+8 more)

### Community 23 - "NBA.Data.Enumerations"
Cohesion: 0.18
Nodes (5): NBA.Data.Enumerations, NBA.Service, NBA.Data.Redis.Dtos, PlayerPositionExtensions, BoxScoreEvaluation

### Community 24 - "Player"
Cohesion: 0.12
Nodes (6): NBA.Service.FreeAgency, NBA.Service.CalculateBoxScore, NBA.Service.Builder, NBA.Service.Team, Player, BoxScoreCalculationBuilder

### Community 25 - "League"
Cohesion: 0.09
Nodes (21): ICollection, League, Autostart, Commissioner, Draftcompleted, Draftsnapshot, Draftstyle, Leagueid (+13 more)

### Community 26 - "DraftEndDraftTests"
Cohesion: 0.23
Nodes (11): IEnumerable, List, PlayerShortDto, FullName, PlayerId, Position, PlayerShortMappings, Fact (+3 more)

### Community 27 - "DraftTimerHostedService"
Cohesion: 0.31
Nodes (7): BackgroundService, CancellationToken, ILogger, IServiceProvider, Task, TimeSpan, DraftTimerHostedService

### Community 28 - "NBA.Data.Redis.Operations"
Cohesion: 0.17
Nodes (6): NBA.Data.Redis.Operations, NBA.Data.Redis.Scopes, NBA.Data.Redis.Keys, NBA.Data.Redis, JsonSerializerOptions, RedisSerializer

### Community 29 - "ExternalClients.Response"
Cohesion: 0.17
Nodes (6): ApplicationDefaults.Time, ExternalClients.Response, ExternalClients, NBA.Service.Game, NBA.Tests.Integration, ExternalClients.Poco

### Community 30 - "GameService"
Cohesion: 0.23
Nodes (9): IBackgroundJobClient, IEndpointRouteBuilder, GameEndpoints, CancellationToken, DateOnly, IOptions, List, Task (+1 more)

### Community 31 - "AppHost Launch Settings"
Cohesion: 0.13
Nodes (18): ASPIRE_DASHBOARD_OTLP_ENDPOINT_URL, ASPIRE_RESOURCE_SERVICE_ENDPOINT_URL, ASPNETCORE_ENVIRONMENT, DOTNET_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables (+10 more)

### Community 32 - "NBA.Data.Redis.Entities"
Cohesion: 0.15
Nodes (7): NBA.Api.SignalR.Clients, NBA.Tests.Fakes, ApplicationDefaults.LogDefaults, NBA.Data.Redis.Entities, NBA.Service.Trade, NBA.Data.Constants, TradeStatuses

### Community 33 - "create-objects-nba-schema.sql"
Cohesion: 0.22
Nodes (18): nba.applicationuser, nba.draftsnapshot, nba.league, nba.leagueplayer, nba.player, nba.playermemento, nba.playoff, nba.playoffbracket (+10 more)

### Community 34 - "MetaData"
Cohesion: 0.22
Nodes (8): MetaData, Next_cursor, Per_page, Prev_Cursor, List, GetAllPlayersResponse, data, meta

### Community 35 - "GameShort"
Cohesion: 0.12
Nodes (17): DateTime, GameShort, Date, GameId, HomeTeam, Postponed, Postseason, StartTime (+9 more)

### Community 36 - "ExternalClients Project Files"
Cohesion: 0.13
Nodes (16): ApplicationDefaults, net10.0, Microsoft.NET.Sdk, BoxScoreBuilder, net10.0, Microsoft.NET.Sdk, ExternalClients, net10.0 (+8 more)

### Community 37 - "BallDontLieClientWireMockTests"
Cohesion: 0.11
Nodes (22): IAsyncLifetime, IClassFixture, IRequestMessage, IResponseBuilder, Fact, HttpStatusCode, InlineData, JsonException (+14 more)

### Community 38 - "PlayerInfoResponse"
Cohesion: 0.12
Nodes (16): PlayerInfoResponse, college, country, draft_number, draft_round, draft_year, first_name, height (+8 more)

### Community 39 - "IBallDontLieClient"
Cohesion: 0.18
Nodes (11): CancellationToken, DateOnly, List, Task, IBallDontLieClient, List, GetGamesResponse, data (+3 more)

### Community 40 - "API Launch Profiles"
Cohesion: 0.13
Nodes (15): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, applicationUrl, commandName (+7 more)

### Community 41 - "Playoff"
Cohesion: 0.12
Nodes (14): ICollection, Playoff, League, Leagueid, Playoffbrackets, Playoffid, Totalrounds, Playoffbracket (+6 more)

### Community 42 - "Transaction"
Cohesion: 0.12
Nodes (14): DateTime, ICollection, Transaction, Transactionid, Transactionleagues, Transactionstatus, Tscreated, Typetransaction (+6 more)

### Community 43 - "TradeHub"
Cohesion: 0.29
Nodes (6): Guid, ILogger, IReadOnlyList, List, Task, TradeHub

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

### Community 51 - "PlayerService"
Cohesion: 0.15
Nodes (13): AutomaticRetry, JobDisplayName, Dictionary, List, Task, BoxScoreCalculationService, CancellationToken, DateTime (+5 more)

### Community 52 - "TradeDto"
Cohesion: 0.15
Nodes (12): DateTime, Guid, List, TradeDto, Fromteamid, Leagueid, Playerids, Status (+4 more)

### Community 53 - "BallDontLieClient"
Cohesion: 0.23
Nodes (9): CancellationToken, DateOnly, HttpResponseMessage, List, Task, BallDontLieClient, HttpClient, ResiliencePipeline (+1 more)

### Community 54 - "GameInfoResponse"
Cohesion: 0.15
Nodes (13): DateTime, GameInfoResponse, date, datetime, home_team, home_team_score, id, postponed (+5 more)

### Community 55 - ".BuildHub"
Cohesion: 0.33
Nodes (8): Clients, OfferedToLeague, Superseded, Fact, Hub, Task, TradeHubPublishTests, LeagueGroup

### Community 56 - "TeamDto"
Cohesion: 0.13
Nodes (13): List, DraftOrderDto, Round, Teams, TeamDto, Categoryleaguepoints, Competesinleague, Islock (+5 more)

### Community 57 - "FreeAgencyPickUpRequest"
Cohesion: 0.33
Nodes (5): NBA.Api.Requests.FreeAgency, List, FreeAgencyPickUpRequest, leagueId, playerIds

### Community 58 - ".ToPlayerDb"
Cohesion: 0.26
Nodes (5): List, PlayerData, Adapter, Fact, AdapterTests

### Community 59 - "JwtOptions"
Cohesion: 0.05
Nodes (36): JwtOptions, AccessTokenMinutes, Audience, Issuer, RefreshTokenDays, SigningKey, ApplyStateContext, NBA.Api.HangFire (+28 more)

### Community 60 - "Trade"
Cohesion: 0.12
Nodes (15): DateTime, Guid, List, Trade, Fromteam, Fromteamid, League, Leagueid (+7 more)

### Community 61 - "UserTeamDto"
Cohesion: 0.15
Nodes (12): List, UserTeamDto, Categoryleaguepoints, Islock, Lastweekpoints, Leagueid, Leaguename, Name (+4 more)

### Community 62 - "Test Project Packages"
Cohesion: 0.17
Nodes (12): NBA.Tests, net10.0, Microsoft.NET.Sdk, coverlet.collector (6.0.2), Microsoft.AspNetCore.SignalR.Client (10.0.0), Microsoft.AspNetCore.TestHost (10.0.0), Microsoft.EntityFrameworkCore.InMemory (10.0.0), Microsoft.NET.Test.Sdk (17.12.0) (+4 more)

### Community 63 - "TradeOutcome"
Cohesion: 0.27
Nodes (8): IReadOnlyList, Accepted, TradeEvent, TradeOutcome, Guid, List, TradeData, FakeTradeOrchestrator

### Community 64 - "ScheduledGames"
Cohesion: 0.24
Nodes (9): List, ScheduledGames, RestOfWeek, Today, Tomorrow, DateOnly, Task, TimeSpan (+1 more)

### Community 65 - "DraftLifecycleService"
Cohesion: 0.25
Nodes (6): IEndpointRouteBuilder, TestingEndpoints, IOptions, List, Task, DraftLifecycleService

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
Cohesion: 0.12
Nodes (16): DraftOptions, DraftPickTime, Rounds, ShowTeamDraftBoardCount, IHubContext, IOptions, DraftTimerProcessor, IServiceCollection (+8 more)

### Community 76 - "ServiceDefaults Extensions"
Cohesion: 0.22
Nodes (3): Microsoft.Extensions.Hosting, Extensions, WebApplication

### Community 77 - "GameRedisOperations"
Cohesion: 0.32
Nodes (5): IDatabase, JsonSerializerOptions, Task, TimeSpan, GameRedisOperations

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
Cohesion: 0.12
Nodes (15): Task, IDraftHubClient, IOptions, Task, DraftHub, DateTime, Dictionary, List (+7 more)

### Community 84 - ".ToggleFreeAgencyStatus"
Cohesion: 0.21
Nodes (7): IEndpointRouteBuilder, FreeAgencyEndpoints, IServiceCollection, FreeAgencyExtention, List, Task, FreeAgencyService

### Community 85 - "Aspire HTTPS Bindings"
Cohesion: 0.25
Nodes (8): https, protocol, scheme, transport, bindings, path, type, nba-api

### Community 86 - "ApplicationHostedService"
Cohesion: 0.06
Nodes (30): ErrorResponse, ErrorCode, ErrorMessage, Log, message, request, response, BallDontLieClientOptions (+22 more)

### Community 87 - "ChatHub"
Cohesion: 0.25
Nodes (5): Hub, Task, IChatHubClient, Task, ChatHub

### Community 88 - "NBA.Data.Entities"
Cohesion: 0.15
Nodes (7): NBA.Data.Entities, NBA.Api.DTOs, NBA.Service.Authentication, NBA.Api.Mappings, NBA.Api.Authentication, NBA.Tests, NBA.Api.Endpoints

### Community 89 - "Draft Status Enum"
Cohesion: 0.29
Nodes (6): DraftStatus, DraftCompleted, DraftEnded, DraftStarted, Initial, Paused

### Community 90 - "ITradeOrchestrator"
Cohesion: 0.43
Nodes (4): Guid, List, Task, ITradeOrchestrator

### Community 91 - "PlayerPositionEnum"
Cohesion: 0.22
Nodes (8): PlayerPositionEnum, C, CF, F, FG, G, GF, UNKOWN

### Community 92 - "AppHost Hosting Packages"
Cohesion: 0.29
Nodes (7): NBA.Service, net10.0, Aspire.Hosting.Redis (13.1.2), Microsoft.Extensions.Options (10.0.3), Microsoft.NET.Sdk, Isopoh.Cryptography.Argon2 (1.1.10), Microsoft.Extensions.Identity.Core (10.0.0)

### Community 93 - "NBA.Api.Requests.Team"
Cohesion: 0.50
Nodes (3): NBA.Api.Requests.Team, TeamRequest, teamName

### Community 94 - "Aspire Server Bindings"
Cohesion: 0.29
Nodes (7): tcp, bindings, port, protocol, scheme, targetPort, transport

### Community 95 - "Draftsnapshot"
Cohesion: 0.25
Nodes (6): DateTime, Draftsnapshot, Draftstate, Draftteams, Leagueid, Tsupdated

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

### Community 101 - "TeamDraftBoard"
Cohesion: 0.14
Nodes (15): List, DraftBoardTeams, CurrentRound, DraftOrder, onTheClockTeam, TeamDraftBoard, Pick, TeamId (+7 more)

### Community 102 - "Chat Schema DDL"
Cohesion: 0.70
Nodes (4): chat.conversationparticipants, chat.messages, chat.rooms, nba.applicationuser

### Community 103 - "NbaFantasyRedis"
Cohesion: 0.10
Nodes (18): Lazy, IDatabase, NbaFantasyRedis, Auth, Draft, Game, Lock, Player (+10 more)

### Community 104 - "Aspire HTTP Bindings"
Cohesion: 0.50
Nodes (4): http, protocol, scheme, transport

### Community 105 - "League"
Cohesion: 0.09
Nodes (20): IEndpointRouteBuilder, LeagueEndpoints, League, PagedResult, Task, TeamData, CreateLeagueInput, JoinLeagueInput (+12 more)

### Community 116 - "Teamplayer"
Cohesion: 0.24
Nodes (6): Teamplayer, Player, Playerid, Team, Teamid, Teamplayerid

### Community 118 - "Team"
Cohesion: 0.33
Nodes (5): Team, abbreviation, city, full_name, id

### Community 119 - "NBAException"
Cohesion: 0.29
Nodes (5): NBAException, ErrorCode, Exception, IOptions, RosterValidator

### Community 120 - "Argon2idPasswordHasher"
Cohesion: 0.16
Nodes (10): Argon2Options, DegreeOfParallelism, Iterations, MemoryKib, IPasswordHasher, IOptions, Argon2idPasswordHasher, Fact (+2 more)

### Community 121 - "Userleague"
Cohesion: 0.29
Nodes (6): Userleague, League, Leagueid, User, Userid, Userleagueid

### Community 125 - "Q: Tell me how individual players are stored in redis"
Cohesion: 0.40
Nodes (4): Answer, Outcome, Q: Tell me how individual players are stored in redis, Source Nodes

## Knowledge Gaps
- **701 isolated node(s):** `net10.0`, `Microsoft.NET.Sdk`, `ErrorCodes`, `ErrorMessage`, `ErrorCode` (+696 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **9 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `NbaFantasyContext` connect `NbaFantasyContext` to `.GetAllTeams`, `Player`, `.EnsureRehydratedAsync`, `TradeService`, `TradeHubFixture`, `Applicationuser`, `Team`, `DraftEndDraftTests`, `Playoff`, `Transaction`, `.OnModelCreating`, `PlayerService`, `JwtOptions`, `DraftLifecycleService`, `Leagueplayer`, `DraftService`, `.ToggleFreeAgencyStatus`, `ApplicationHostedService`, `NBA.Data.Entities`, `Draftsnapshot`, `LeaguePlayerSeedTests`, `League`, `Teamplayer`, `Userleague`?**
  _High betweenness centrality (0.166) - this node is a cross-community bridge._
- **Why does `NBAException` connect `NBAException` to `.GetAllTeams`, `.ProposeAsync`, `.CreateClient`, `.League`, `.EnsureRehydratedAsync`, `TradeService`, `TradeHubFixture`, `Applicationuser`, `BallDontLieClientWireMockTests`, `PlayerService`, `BallDontLieClient`, `JwtOptions`, `DraftLifecycleService`, `Leagueplayer`, `DraftState`, `.ToggleFreeAgencyStatus`, `LeaguePlayerSeedTests`, `TeamDraftBoard`, `League`, `Teamplayer`?**
  _High betweenness centrality (0.091) - this node is a cross-community bridge._
- **Why does `Trade` connect `Trade` to `NBA.Data.Redis.Entities`, `EntityMappings`, `.ProposeAsync`, `League`, `TradeService`, `TradeHubFixture`, `TradeDto`, `Team`, `League`, `ITradeOrchestrator`, `TradeOutcome`?**
  _High betweenness centrality (0.089) - this node is a cross-community bridge._
- **Are the 34 inferred relationships involving `NBAException` (e.g. with `.GetAsync()` and `.RefreshAsync()`) actually correct?**
  _`NBAException` has 34 INFERRED edges - model-reasoned connections that need verification._
- **What connects `net10.0`, `Microsoft.NET.Sdk`, `ErrorCodes` to the rest of the system?**
  _701 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DraftRedisOperations` be split into smaller, more focused modules?**
  _Cohesion score 0.08816326530612245 - nodes in this community are weakly interconnected._
- **Should `.ProposeAsync` be split into smaller, more focused modules?**
  _Cohesion score 0.11904761904761904 - nodes in this community are weakly interconnected._