# Graph Report - NBAFantasy  (2026-08-30)

## Corpus Check
- 193 files · ~56,971 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2080 nodes · 3919 edges · 142 communities (122 shown, 20 thin omitted)
- Extraction: 92% EXTRACTED · 8% INFERRED · 0% AMBIGUOUS · INFERRED: 296 edges (avg confidence: 0.82)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `0e453313`
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
- .EnsureRehydratedAsync
- PlayerSearchInput
- NBAException
- BoxScoreStatsBuilder
- TradeBetweenTeams
- LeaguePlayerService
- League & Stats Value Requests
- TradeHubFixture
- NbaFantasyContext
- NBA.Data.Context
- Applicationuser
- Team
- PlayerDto
- GameDto
- BallDontLieWireMockFixture
- Player
- League
- NbaFantasyContext
- DraftTimerHostedService
- NBA.Data.Redis.Entities
- ExternalClients.Response
- GameService
- AppHost Launch Settings
- ApplicationDefaults.Exceptions
- create-objects-nba-schema.sql
- ShortenJobExpirationFilter
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
- Playermemento
- BallDontLieClient
- GameInfoResponse
- TradeDto
- LeagueDto
- FreeAgencyEndpoints.cs
- .ToPlayerDb
- AuthTokenIssuer
- Trade
- UserTeamDto
- Test Project Packages
- .GetUserTeamsWithPlayersAsync
- ScheduledGames
- .DraftOrder
- EntityMappings
- NBA.Api Package References
- .LoginAsync
- .AddProposedTrade
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
- .MapFreeAgencyEndpoints
- Aspire HTTPS Bindings
- ApplicationHostedService
- NBA.Service.FreeAgency
- NBA.Data.Entities
- Draft Status Enum
- .Generate
- PlayerPositionEnum
- AppHost Hosting Packages
- TeamEndpoints.cs
- Aspire Server Bindings
- Argon2idPasswordHasherTests
- LeaguePlayerSeedTests
- LeagueTeamDto
- Aspire Password Parameters
- League Team Insert Request
- Draft Request DTO
- DraftLifecycleService
- Chat Schema DDL
- NbaFantasyRedis
- Aspire HTTP Bindings
- League
- Infrastructure Init Entry
- Naming Rule
- Teamplayer
- Userleague
- Team
- ScheduledGamesDto
- Argon2Options
- LockRedisOperations
- .CreateToken
- Draftsnapshot
- NBA.Data.Redis
- Q: Tell me how individual players are stored in redis
- .MapPlayerEndpoints
- .MapTradeEndpoints
- .ToPlayerDb_maps_position_string_to_enum
- IEndpointRouteBuilder
- CancellationToken
- Player
- Trade
- Dictionary
- DraftBoardTeams
- IOptions
- Queue
- IServiceCollection
- adding-an-endpoint/SKILL.md
- first-time-setup/SKILL.md
- Fact
- IReadOnlyList

## God Nodes (most connected - your core abstractions)
1. `NbaFantasyContext` - 63 edges
2. `NBAException` - 49 edges
3. `TradeBetweenTeams` - 49 edges
4. `NbaFantasyContext` - 43 edges
5. `Trade` - 41 edges
6. `PlayerSearchInput` - 40 edges
7. `NBA.Data.Entities` - 40 edges
8. `PlayersFilterSearch` - 38 edges
9. `NBA.Data.Redis.Entities` - 36 edges
10. `Team` - 33 edges

## Surprising Connections (you probably didn't know these)
- `DraftLifecycleService` --references--> `NbaFantasyContext`  [EXTRACTED]
  NBA.Service/Draft/DraftLifecycleService.cs → NBA.Data/Context/NbaFantasyContextExt.cs
- `FreeAgencyService` --references--> `NbaFantasyContext`  [EXTRACTED]
  NBA.Service/FreeAgency/FreeAgencyService.cs → NBA.Data/Context/NbaFantasyContextExt.cs
- `DraftTimerProcessor` --references--> `DraftLifecycleService`  [EXTRACTED]
  NBA.Api/Draft/DraftTimerProcessor.cs → NBA.Service/Draft/DraftLifecycleService.cs
- `DraftHub` --references--> `DraftLifecycleService`  [EXTRACTED]
  NBA.Api/SignalR/Hubs/DraftHub.cs → NBA.Service/Draft/DraftLifecycleService.cs
- `AuthTokenIssuer` --references--> `JwtOptions`  [EXTRACTED]
  NBA.Api/Authentication/AuthTokenIssuer.cs → ApplicationDefaults/Options/JwtOptions.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Draft Pick Processing Flow** — claude_drafthub, claude_playermanager, claude_draftmanager, claude_draftstate [EXTRACTED 0.95]
- **Draft Timer & Deadline Coordination** — claude_drafttimerhostedservice, claude_draftredisoperations, claude_draftmanager, claude_draft_realtime [EXTRACTED 0.95]
- **External HTTP Resilience Strategy** — claude_resilience_pipeline_rule, claude_externalclients, claude_nba_servicedefaults [INFERRED 0.85]

## Communities (142 total, 20 thin omitted)

### Community 0 - "JwtOptions"
Cohesion: 0.17
Nodes (12): JwtOptions, AccessTokenMinutes, Audience, Issuer, RefreshTokenDays, SigningKey, InvalidOperationException, IOptions (+4 more)

### Community 1 - "DraftRedisOperations"
Cohesion: 0.06
Nodes (30): PlayerShortDto, FullName, PlayerId, Position, List, DraftBoardTeams, CurrentRound, DraftOrder (+22 more)

### Community 2 - ".ProposeAsync"
Cohesion: 0.16
Nodes (12): IServiceCollection, TradeExtention, Guid, IOptions, List, Task, TradeManager, Guid (+4 more)

### Community 3 - "PlayerShort"
Cohesion: 0.06
Nodes (29): IEnumerable, List, PlayerShortMappings, PlayerShort, FullName, PlayerId, Position, RedisKeys (+21 more)

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
Cohesion: 0.05
Nodes (38): NBA.Api.Requests.Player, DateTime, PlayersFilterSearch, allowdrop, gameready, irlteamid, irlteamname, islock (+30 more)

### Community 8 - ".League"
Cohesion: 0.21
Nodes (7): Task, IEndpointRouteBuilder, DraftEndpoints, Task, IOptions, Task, DraftManager

### Community 9 - ".EnsureRehydratedAsync"
Cohesion: 0.18
Nodes (8): IServiceCollection, DraftExtention, Dictionary, IOptions, JsonSerializerOptions, Queue, Task, DraftSnapshotService

### Community 10 - "PlayerSearchInput"
Cohesion: 0.06
Nodes (36): PlayerSearchInput, Allowdrop, Gameready, Irlteamid, Irlteamname, Islock, LeagueId, MaxAssists (+28 more)

### Community 11 - "NBAException"
Cohesion: 0.20
Nodes (9): NBAException, ErrorCode, Exception, IQueryable, Guid, ILogger, List, Task (+1 more)

### Community 12 - "BoxScoreStatsBuilder"
Cohesion: 0.07
Nodes (17): BoxScoreStatsBuilder, PlayerStats, ast, blk, fg3a, fg3m, fga, fgm (+9 more)

### Community 13 - "TradeBetweenTeams"
Cohesion: 0.05
Nodes (45): IHubCallerClients, Method, List, Task, ITradeHubClient, DateTimeOffset, Guid, List (+37 more)

### Community 14 - "LeaguePlayerService"
Cohesion: 0.40
Nodes (3): IServiceCollection, LeaguePlayerExtention, LeaguePlayerService

### Community 15 - "League & Stats Value Requests"
Cohesion: 0.07
Nodes (27): NBA.Api.Requests.League, NBA.Api.Requests.StatValue, LeagueRequest, Autostart, DraftStyle, LeagueName, LeagueType, ScoringSystem (+19 more)

### Community 16 - "TradeHubFixture"
Cohesion: 0.09
Nodes (36): NBA.Api.SignalR, HubConnection, HubException, HubInvocationContext, ICollectionFixture, IConnectionMultiplexer, IHost, IHubFilter (+28 more)

### Community 17 - "NbaFantasyContext"
Cohesion: 0.09
Nodes (24): DbContext, DbSet, ModelBuilder, Player, Trade, NbaFantasyContext, Applicationusers, Draftsnapshots (+16 more)

### Community 18 - "NBA.Data.Context"
Cohesion: 0.21
Nodes (7): NBA.Api.Draft, NBA.Api.SignalR.Clients, NBA.Data.Context, NBA.Data.Redis.Enumerations, NBA.Service.Draft, NBA.Api.SignalR.Hubs, NBA.Api.Endpoints

### Community 19 - "Applicationuser"
Cohesion: 0.12
Nodes (15): IPasswordHasher, ICollection, Applicationuser, Email, Managerlevel, Password, Teams, Userid (+7 more)

### Community 20 - "Team"
Cohesion: 0.11
Nodes (17): ICollection, Team, Approved, Categoryleaguepoints, Islock, Lastweekpoints, League, Leagueid (+9 more)

### Community 21 - "PlayerDto"
Cohesion: 0.08
Nodes (24): DateTime, PlayerDto, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow, Gameready (+16 more)

### Community 22 - "GameDto"
Cohesion: 0.18
Nodes (11): DateTime, GameDto, Date, GameId, HomeTeam, Postponed, Postseason, StartTime (+3 more)

### Community 23 - "BallDontLieWireMockFixture"
Cohesion: 0.12
Nodes (13): BallDontLieClientOptions, ApiKey, BaseUrl, Per_Page, IAsyncLifetime, HttpResponseMessage, IOptions, Task (+5 more)

### Community 24 - "Player"
Cohesion: 0.16
Nodes (3): NBA.Service.Builder, Player, BoxScoreCalculationBuilder

### Community 25 - "League"
Cohesion: 0.07
Nodes (27): ICollection, League, Autostart, Commissioner, Draftcompleted, Draftsnapshot, Draftstyle, Leagueid (+19 more)

### Community 26 - "NbaFantasyContext"
Cohesion: 0.09
Nodes (21): Applicationuser, CancellationToken, Dictionary, Draftsnapshot, Fact, IReadOnlyList, League, Leagueplayer (+13 more)

### Community 27 - "DraftTimerHostedService"
Cohesion: 0.31
Nodes (7): BackgroundService, CancellationToken, ILogger, IServiceProvider, Task, TimeSpan, DraftTimerHostedService

### Community 28 - "NBA.Data.Redis.Entities"
Cohesion: 0.13
Nodes (9): NBA.Data.Redis.Operations, NBA.Data.Redis.Scopes, NBA.Data.Enumerations, NBA.Tests.Fakes, NBA.Data.Redis.Keys, NBA.Data.Redis.Entities, NBA.Service.Trade, NBA.Data.Redis.Dtos (+1 more)

### Community 29 - "ExternalClients.Response"
Cohesion: 0.16
Nodes (7): ApplicationDefaults.Time, ExternalClients.Response, NBA.Api.HostedService, ExternalClients, NBA.Service.Game, NBA.Service.Player, ExternalClients.Poco

### Community 30 - "GameService"
Cohesion: 0.23
Nodes (9): IBackgroundJobClient, IEndpointRouteBuilder, GameEndpoints, CancellationToken, DateOnly, IOptions, List, Task (+1 more)

### Community 31 - "AppHost Launch Settings"
Cohesion: 0.13
Nodes (18): ASPIRE_DASHBOARD_OTLP_ENDPOINT_URL, ASPIRE_RESOURCE_SERVICE_ENDPOINT_URL, ASPNETCORE_ENVIRONMENT, DOTNET_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables (+10 more)

### Community 32 - "ApplicationDefaults.Exceptions"
Cohesion: 0.13
Nodes (9): ErrorCodes, ApplicationDefaults.LogDefaults, ApplicationDefaults.Exceptions, NBA.Tests.Integration, NBA.Service.Roster, NBA.Service.CalculateBoxScore, NBA.Service.LeaguePlayer, NBA.Data.Constants (+1 more)

### Community 33 - "create-objects-nba-schema.sql"
Cohesion: 0.22
Nodes (18): nba.applicationuser, nba.draftsnapshot, nba.league, nba.leagueplayer, nba.player, nba.playermemento, nba.playoff, nba.playoffbracket (+10 more)

### Community 34 - "ShortenJobExpirationFilter"
Cohesion: 0.15
Nodes (11): ApplyStateContext, NBA.Api.HangFire, NBA.Api, IApplyStateFilter, IConfiguration, IWriteOnlyTransaction, JobFilterAttribute, HttpResponseMessage (+3 more)

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
Cohesion: 0.12
Nodes (16): PlayerInfoResponse, college, country, draft_number, draft_round, draft_year, first_name, height (+8 more)

### Community 39 - "MetaData"
Cohesion: 0.13
Nodes (17): CancellationToken, DateOnly, List, Task, IBallDontLieClient, MetaData, Next_cursor, Per_page (+9 more)

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
Cohesion: 0.14
Nodes (12): ApplicationOptions, CenterLimit, MaxPlayersPerTeam, ProposedTradeTtlMinutes, AuthenticateResult, AuthenticationHandler, AuthenticationSchemeOptions, ClaimsPrincipal (+4 more)

### Community 44 - "PlayerStatsResponse"
Cohesion: 0.10
Nodes (18): PlayerStatsResponse, ast, blk, fg3a, fg3m, fga, fgm, fta (+10 more)

### Community 45 - "Per-League Stats Values"
Cohesion: 0.13
Nodes (14): Statsvalue, Assistsvalue, Blocksvalue, Fieldgoalvaluemade, Fieldgoalvaluemissed, Freethrowvaluemade, Freethrowvaluemissed, League (+6 more)

### Community 46 - "NbaCalendar"
Cohesion: 0.23
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
Cohesion: 0.20
Nodes (9): AutomaticRetry, JobDisplayName, CancellationToken, DateTime, IReadOnlyList, List, PagedResult, Task (+1 more)

### Community 52 - "Playermemento"
Cohesion: 0.12
Nodes (15): DateTime, Playermemento, Assists, Blocks, Fieldgoalperc, Freethrowperc, Player, Playermemontoid (+7 more)

### Community 53 - "BallDontLieClient"
Cohesion: 0.23
Nodes (9): CancellationToken, DateOnly, HttpResponseMessage, List, Task, BallDontLieClient, HttpClient, ResiliencePipeline (+1 more)

### Community 54 - "GameInfoResponse"
Cohesion: 0.15
Nodes (13): DateTime, GameInfoResponse, date, datetime, home_team, home_team_score, id, postponed (+5 more)

### Community 55 - "TradeDto"
Cohesion: 0.05
Nodes (44): Clients, Hub, DateTime, Guid, List, TradeDto, Fromteamid, Leagueid (+36 more)

### Community 56 - "LeagueDto"
Cohesion: 0.05
Nodes (34): List, DraftOrderDto, Round, Teams, LeagueDto, Autostart, Commissioner, CommissionersTeam (+26 more)

### Community 57 - "FreeAgencyEndpoints.cs"
Cohesion: 0.25
Nodes (6): NBA.Api.Requests.FreeAgency, FreeAgencyEndpoints, List, FreeAgencyPickUpRequest, leagueId, playerIds

### Community 58 - ".ToPlayerDb"
Cohesion: 0.26
Nodes (5): List, PlayerData, Adapter, Fact, AdapterTests

### Community 59 - "AuthTokenIssuer"
Cohesion: 0.29
Nodes (7): DateTime, IOptions, Task, AuthTokenIssuer, TokenPair, IEndpointRouteBuilder, AuthenticationEndpoints

### Community 60 - "Trade"
Cohesion: 0.12
Nodes (15): DateTime, Guid, List, Trade, Fromteam, Fromteamid, League, Leagueid (+7 more)

### Community 61 - "UserTeamDto"
Cohesion: 0.15
Nodes (12): List, UserTeamDto, Categoryleaguepoints, Islock, Lastweekpoints, Leagueid, Leaguename, Name (+4 more)

### Community 62 - "Test Project Packages"
Cohesion: 0.17
Nodes (12): NBA.Tests, net10.0, Microsoft.NET.Sdk, coverlet.collector (6.0.2), Microsoft.AspNetCore.SignalR.Client (10.0.0), Microsoft.AspNetCore.TestHost (10.0.0), Microsoft.EntityFrameworkCore.InMemory (10.0.0), Microsoft.NET.Test.Sdk (17.12.0) (+4 more)

### Community 63 - ".GetUserTeamsWithPlayersAsync"
Cohesion: 0.29
Nodes (7): IEndpointRouteBuilder, TeamEndpoints, Dictionary, List, Task, TeamData, TeamService

### Community 64 - "ScheduledGames"
Cohesion: 0.24
Nodes (9): List, ScheduledGames, RestOfWeek, Today, Tomorrow, DateOnly, Task, TimeSpan (+1 more)

### Community 65 - ".DraftOrder"
Cohesion: 0.25
Nodes (5): IEndpointRouteBuilder, TestingEndpoints, Dictionary, Queue, Task

### Community 66 - "EntityMappings"
Cohesion: 0.24
Nodes (3): List, Team, EntityMappings

### Community 67 - "NBA.Api Package References"
Cohesion: 0.18
Nodes (10): net10.0, Aspire.StackExchange.Redis (13.1.2), Microsoft.Extensions.Http.Resilience (10.1.0), Aspire.Npgsql.EntityFrameworkCore.PostgreSQL (13.1.0), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.0), Microsoft.AspNetCore.OpenApi (10.0.0), Microsoft.AspNetCore.SignalR.StackExchangeRedis (10.0.5), Microsoft.OpenApi (2.7.5) (+2 more)

### Community 68 - ".LoginAsync"
Cohesion: 0.33
Nodes (5): IPasswordHasher, List, Task, AuthService, LoginResult

### Community 69 - ".AddProposedTrade"
Cohesion: 0.29
Nodes (5): Created, DateTime, TradeData, Superseded, Trade

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
Cohesion: 0.18
Nodes (13): DraftOptions, DraftPickTime, Rounds, ShowTeamDraftBoardCount, IHubContext, IOptions, DraftTimerProcessor, IOptions (+5 more)

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
Cohesion: 0.15
Nodes (12): Task, IDraftHubClient, DateTime, Dictionary, List, DraftState, DraftBoardTeams, DraftedPlayersPerTeam (+4 more)

### Community 84 - ".MapFreeAgencyEndpoints"
Cohesion: 0.39
Nodes (5): IEndpointRouteBuilder, List, Task, FreeAgencyService, PlayerData

### Community 85 - "Aspire HTTPS Bindings"
Cohesion: 0.25
Nodes (8): https, protocol, scheme, transport, bindings, path, type, nba-api

### Community 86 - "ApplicationHostedService"
Cohesion: 0.07
Nodes (26): ErrorResponse, ErrorCode, ErrorMessage, Log, message, request, response, HttpContext (+18 more)

### Community 87 - "NBA.Service.FreeAgency"
Cohesion: 0.29
Nodes (3): NBA.Service.FreeAgency, IServiceCollection, FreeAgencyExtention

### Community 88 - "NBA.Data.Entities"
Cohesion: 0.14
Nodes (10): NBA.Service.League, NBA.Data.Entities, NBA.Api.DTOs, NBA.Service.Authentication, NBA.Api.Mappings, NBA.Api.Authentication, NBA.Tests, NBA.Service (+2 more)

### Community 89 - "Draft Status Enum"
Cohesion: 0.29
Nodes (6): DraftStatus, DraftCompleted, DraftEnded, DraftStarted, Initial, Paused

### Community 90 - ".Generate"
Cohesion: 0.38
Nodes (3): RefreshTokenGenerator, Fact, RefreshTokenGeneratorTests

### Community 91 - "PlayerPositionEnum"
Cohesion: 0.22
Nodes (8): PlayerPositionEnum, C, CF, F, FG, G, GF, UNKOWN

### Community 92 - "AppHost Hosting Packages"
Cohesion: 0.29
Nodes (7): NBA.Service, net10.0, Aspire.Hosting.Redis (13.1.2), Microsoft.Extensions.Options (10.0.3), Microsoft.NET.Sdk, Isopoh.Cryptography.Argon2 (1.1.10), Microsoft.Extensions.Identity.Core (10.0.0)

### Community 93 - "TeamEndpoints.cs"
Cohesion: 0.29
Nodes (4): NBA.Api.Requests.Team, NBA.Service.Team, TeamRequest, teamName

### Community 94 - "Aspire Server Bindings"
Cohesion: 0.29
Nodes (7): tcp, bindings, port, protocol, scheme, targetPort, transport

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
Cohesion: 0.20
Nodes (10): DraftBoardTeams, DraftOptions, DraftSnapshotService, IOptions, List, Task, DraftLifecycleService, NbaFantasyRedis (+2 more)

### Community 102 - "Chat Schema DDL"
Cohesion: 0.70
Nodes (4): chat.conversationparticipants, chat.messages, chat.rooms, nba.applicationuser

### Community 103 - "NbaFantasyRedis"
Cohesion: 0.22
Nodes (9): Lazy, IDatabase, NbaFantasyRedis, Auth, Draft, Game, Lock, Player (+1 more)

### Community 104 - "Aspire HTTP Bindings"
Cohesion: 0.50
Nodes (4): http, protocol, scheme, transport

### Community 105 - "League"
Cohesion: 0.15
Nodes (14): IEndpointRouteBuilder, LeagueEndpoints, League, PagedResult, Task, TeamData, CreateLeagueInput, JoinLeagueInput (+6 more)

### Community 116 - "Teamplayer"
Cohesion: 0.29
Nodes (6): Teamplayer, Player, Playerid, Team, Teamid, Teamplayerid

### Community 117 - "Userleague"
Cohesion: 0.29
Nodes (6): Userleague, League, Leagueid, User, Userid, Userleagueid

### Community 118 - "Team"
Cohesion: 0.33
Nodes (5): Team, abbreviation, city, full_name, id

### Community 119 - "ScheduledGamesDto"
Cohesion: 0.33
Nodes (5): List, ScheduledGamesDto, RestOfWeek, Today, Tomorrow

### Community 120 - "Argon2Options"
Cohesion: 0.40
Nodes (4): Argon2Options, DegreeOfParallelism, Iterations, MemoryKib

### Community 121 - "LockRedisOperations"
Cohesion: 0.33
Nodes (4): IDatabase, Task, TimeSpan, LockRedisOperations

### Community 122 - ".CreateToken"
Cohesion: 0.50
Nodes (3): DateTime, AuthToken, ITokenService

### Community 123 - "Draftsnapshot"
Cohesion: 0.29
Nodes (6): DateTime, Draftsnapshot, Draftstate, Draftteams, Leagueid, Tsupdated

### Community 124 - "NBA.Data.Redis"
Cohesion: 0.50
Nodes (3): NBA.Data.Redis, JsonSerializerOptions, RedisSerializer

### Community 125 - "Q: Tell me how individual players are stored in redis"
Cohesion: 0.40
Nodes (4): Answer, Outcome, Q: Tell me how individual players are stored in redis, Source Nodes

## Knowledge Gaps
- **703 isolated node(s):** `leagueId`, `playerIds`, `TradeStatuses`, `BoxScoreEvaluation`, `ErrorCodes` (+698 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **20 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `NbaFantasyContext` connect `NbaFantasyContext` to `.EnsureRehydratedAsync`, `NBAException`, `LeaguePlayerService`, `TradeHubFixture`, `Applicationuser`, `Team`, `League`, `Playoff`, `Transaction`, `.InitializeAsync`, `PlayerStatsResponse`, `Usertrophie`, `PlayerService`, `Playermemento`, `AuthTokenIssuer`, `.GetUserTeamsWithPlayersAsync`, `.LoginAsync`, `PlayerManager`, `DraftService`, `ApplicationHostedService`, `NBA.Data.Entities`, `LeaguePlayerSeedTests`, `League`, `Teamplayer`, `Userleague`, `Draftsnapshot`?**
  _High betweenness centrality (0.109) - this node is a cross-community bridge._
- **Why does `NBAException` connect `NBAException` to `LeaguePlayerSeedTests`, `.DraftOrder`, `.ProposeAsync`, `.LoginAsync`, `DraftLifecycleService`, `.AddProposedTrade`, `.CreateClient`, `.League`, `.EnsureRehydratedAsync`, `League`, `.InitializeAsync`, `BallDontLieClientWireMockTests`, `PlayerService`, `.MapFreeAgencyEndpoints`, `BallDontLieClient`, `NbaFantasyContext`, `AuthTokenIssuer`, `.GetUserTeamsWithPlayersAsync`?**
  _High betweenness centrality (0.101) - this node is a cross-community bridge._
- **Why does `NbaFantasyRedis` connect `NbaFantasyRedis` to `ScheduledGames`, `DraftRedisOperations`, `.ProposeAsync`, `PlayerShort`, `PlayerManager`, `.League`, `AuthTokenIssuer`, `.EnsureRehydratedAsync`, `DraftService`, `.InitializeAsync`, `GameRedisOperations`, `TradeBetweenTeams`, `TradeHubFixture`, `PlayerService`, `ApplicationHostedService`, `LockRedisOperations`, `DraftTimerHostedService`, `NBA.Data.Redis.Entities`?**
  _High betweenness centrality (0.095) - this node is a cross-community bridge._
- **Are the 34 inferred relationships involving `NBAException` (e.g. with `.GetAsync()` and `.RefreshAsync()`) actually correct?**
  _`NBAException` has 34 INFERRED edges - model-reasoned connections that need verification._
- **What connects `leagueId`, `playerIds`, `TradeStatuses` to the rest of the system?**
  _703 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DraftRedisOperations` be split into smaller, more focused modules?**
  _Cohesion score 0.06299603174603174 - nodes in this community are weakly interconnected._
- **Should `PlayerShort` be split into smaller, more focused modules?**
  _Cohesion score 0.06153846153846154 - nodes in this community are weakly interconnected._