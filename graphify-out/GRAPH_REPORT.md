# Graph Report - NBAFantasy  (2026-08-29)

## Corpus Check
- 177 files · ~50,441 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 1903 nodes · 3498 edges · 116 communities (108 shown, 8 thin omitted)
- Extraction: 92% EXTRACTED · 8% INFERRED · 0% AMBIGUOUS · INFERRED: 285 edges (avg confidence: 0.82)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `67127ac1`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Trade Redis & TradeHub Tests
- Draft Redis Operations & Keys
- Trade & Team Services
- Player Redis Operations
- Project Rules & Vendor Licenses
- BallDontLie Client Tests
- Player Memento Stats
- Player Search Request DTOs
- Draft Orchestration & Hub
- Hosted Services & Exception Handling
- Player Service Search
- BallDontLie WireMock Tests
- BoxScore Stats Builder
- Redis Facade & Draft Options
- TradeHub Real-Time Trading
- League & Stats Value Requests
- TradeHub Test Fixture
- EF Core DbContext Model
- TeamPlayer & Draft Snapshot Persistence
- User Auth Persistence
- Team Endpoints & Auth Claims
- Player DTO
- Game Schedule DTOs
- Error Codes & Trade Statuses
- League Service & Endpoints
- League Entity
- Draft Endpoints Integration Tests
- End-Draft Integration Tests
- Redis Operations Integration Tests
- External Client Response Tests
- Game Redis Operations
- AppHost Launch Settings
- Hangfire Job Expiration Filter
- Core Database Schema DDL
- Team Entity
- Game Redis Shapes
- ExternalClients Project Files
- WireMock BallDontLie Fixture
- PlayerInfo Response Shape
- JWT Options & Token Tests
- API Launch Profiles
- Playoff Bracket Entities
- Transaction Entities
- BallDontLie Client & NBA Calendar
- Player Stats Response
- Per-League Stats Values
- BoxScore Calculation Jobs
- Trophy Entities
- NBA.Data Package References
- Aspire Manifest OTEL Config
- Auth Request DTOs
- Player Position Extensions
- IBallDontLieClient Contract
- BallDontLie Response Metadata
- Game Info Response
- Game Service & Endpoints
- League DTO
- Free Agency Service
- Adapter Mapping Tests
- Auth Token Issuance
- Trade DTO
- User Team DTO
- Test Project Packages
- Argon2id Password Hashing
- NBA Calendar Date Handling
- Draft Order DTO
- Entity to DTO Mappers
- NBA.Api Package References
- Player Position Enum
- BoxScore Calculation Builder
- Player Redis Manager
- ServiceDefaults Packages
- Entity Mapping Tests
- Aspire Postgres Connection
- Aspire Postgres Container
- Draft Timer Hosted Service
- ServiceDefaults Extensions
- Login DTO
- Team DTO
- AppHost Packages
- SignalR Exception Hub Filter
- Team Info Response
- Refresh Token Generator Tests
- Player Endpoints
- Game Schedule Bucketing Tests
- Aspire HTTPS Bindings
- Chat Hub
- UserLeague Join Entity
- Draft Type Enum
- Draft Status Enum
- Redis Lock Operations
- Redis Adapter Mappings
- AppHost Hosting Packages
- Argon2id Hasher Tests
- Aspire Server Bindings
- Hosted Service Namespaces
- JWT Token Service
- League Team DTO
- Aspire Password Parameters
- League Team Insert Request
- Draft Request DTO
- Game Team Response
- Chat Schema DDL
- Redis Serializer
- Aspire HTTP Bindings
- Testing Endpoints
- Infrastructure Init Entry
- Naming Rule

## God Nodes (most connected - your core abstractions)
1. `NbaFantasyContext` - 84 edges
2. `TradeBetweenTeams` - 52 edges
3. `NBAException` - 47 edges
4. `NBA.Data.Entities` - 42 edges
5. `PlayerSearchInput` - 40 edges
6. `PlayersFilterSearch` - 38 edges
7. `Applicationuser` - 35 edges
8. `Team` - 35 edges
9. `NBA.Data.Redis.Entities` - 33 edges
10. `Player` - 31 edges

## Surprising Connections (you probably didn't know these)
- `TradeHub` --references--> `DraftManager`  [EXTRACTED]
  NBA.Api/SignalR/Hubs/TradeHub.cs → NBA.Service/Draft/DraftManager.cs
- `DraftTimerProcessor` --references--> `DraftOptions`  [EXTRACTED]
  NBA.Api/Draft/DraftTimerProcessor.cs → ApplicationDefaults/Options/DraftOptions.cs
- `DraftHub` --references--> `DraftOptions`  [EXTRACTED]
  NBA.Api/SignalR/Hubs/DraftHub.cs → ApplicationDefaults/Options/DraftOptions.cs
- `DraftService` --references--> `DraftOptions`  [EXTRACTED]
  NBA.Service/Draft/DraftService.cs → ApplicationDefaults/Options/DraftOptions.cs
- `DraftTimerProcessor` --references--> `NbaFantasyRedis`  [EXTRACTED]
  NBA.Api/Draft/DraftTimerProcessor.cs → NBA.Data/Context/NbaFantasyRedis.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Draft Pick Processing Flow** — claude_drafthub, claude_playermanager, claude_draftmanager, claude_draftstate [EXTRACTED 0.95]
- **Draft Timer & Deadline Coordination** — claude_drafttimerhostedservice, claude_draftredisoperations, claude_draftmanager, claude_draft_realtime [EXTRACTED 0.95]
- **External HTTP Resilience Strategy** — claude_resilience_pipeline_rule, claude_externalclients, claude_nba_servicedefaults [INFERRED 0.85]

## Communities (116 total, 8 thin omitted)

### Community 0 - "Trade Redis & TradeHub Tests"
Cohesion: 0.06
Nodes (41): HubConnection, HubException, HubInvocationContext, IHubFilter, List, Task, ITradeHubClient, Func (+33 more)

### Community 1 - "Draft Redis Operations & Keys"
Cohesion: 0.06
Nodes (34): PlayerShortDto, FullName, PlayerId, Position, DateTime, Dictionary, List, DraftState (+26 more)

### Community 2 - "Trade & Team Services"
Cohesion: 0.17
Nodes (13): NBAException, ErrorCode, Created, Exception, Trade, DateTime, Guid, ILogger (+5 more)

### Community 3 - "Player Redis Operations"
Cohesion: 0.06
Nodes (29): IEnumerable, List, PlayerShortMappings, PlayerShort, FullName, PlayerId, Position, RedisKeys (+21 more)

### Community 4 - "Project Rules & Vendor Licenses"
Cohesion: 0.05
Nodes (51): Adapter (static mapper), Adding an HTTP Endpoint Flow, ApplicationDefaults, ApplicationOptions, Argon2Options, Aspire AppHost, Auth & Tests, Authenticate Everything Rule (+43 more)

### Community 5 - "BallDontLie Client Tests"
Cohesion: 0.11
Nodes (27): Action, NBA.Tests.Fakes, HttpMessageHandler, HttpRequestMessage, Fact, HttpResponseMessage, HttpStatusCode, InlineData (+19 more)

### Community 6 - "Player Memento Stats"
Cohesion: 0.04
Nodes (43): DateTime, ICollection, Player, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow (+35 more)

### Community 7 - "Player Search Request DTOs"
Cohesion: 0.05
Nodes (38): NBA.Api.Requests.Player, DateTime, PlayersFilterSearch, allowdrop, gameready, irlteamid, irlteamname, islock (+30 more)

### Community 8 - "Draft Orchestration & Hub"
Cohesion: 0.12
Nodes (18): DraftOptions, DraftService, DraftSnapshotService, DraftState, IOptions, IHubContext, IOptions, Task (+10 more)

### Community 9 - "Hosted Services & Exception Handling"
Cohesion: 0.07
Nodes (26): ErrorResponse, ErrorCode, ErrorMessage, Log, message, request, response, HttpContext (+18 more)

### Community 10 - "Player Service Search"
Cohesion: 0.06
Nodes (36): PlayerSearchInput, Allowdrop, Gameready, Irlteamid, Irlteamname, Islock, LeagueId, MaxAssists (+28 more)

### Community 11 - "BallDontLie WireMock Tests"
Cohesion: 0.18
Nodes (12): IClassFixture, IRequestMessage, IResponseBuilder, Fact, JsonException, OperationCanceledException, Task, BallDontLieClientWireMockTests (+4 more)

### Community 12 - "BoxScore Stats Builder"
Cohesion: 0.07
Nodes (17): BoxScoreStatsBuilder, PlayerStats, ast, blk, fg3a, fg3m, fga, fgm (+9 more)

### Community 13 - "Redis Facade & Draft Options"
Cohesion: 0.13
Nodes (14): Lazy, IDatabase, NbaFantasyRedis, Auth, Draft, Game, Lock, Player (+6 more)

### Community 14 - "TradeHub Real-Time Trading"
Cohesion: 0.14
Nodes (15): Guid, IHubContext, ILogger, IOptions, List, Task, TradeHub, Dictionary (+7 more)

### Community 15 - "League & Stats Value Requests"
Cohesion: 0.07
Nodes (27): NBA.Api.Requests.League, NBA.Api.Requests.StatValue, LeagueRequest, Autostart, DraftStyle, LeagueName, LeagueType, ScoringSystem (+19 more)

### Community 16 - "TradeHub Test Fixture"
Cohesion: 0.11
Nodes (15): AuthenticateResult, ClaimsPrincipal, ICollectionFixture, IConnectionMultiplexer, IHost, ClaimsPrincipalExtensions, IDatabase, Task (+7 more)

### Community 17 - "EF Core DbContext Model"
Cohesion: 0.07
Nodes (28): DbContext, DbSet, NbaFantasyContext, Applicationusers, Draftsnapshots, Leagueplayers, Leagues, Playermementos (+20 more)

### Community 18 - "TeamPlayer & Draft Snapshot Persistence"
Cohesion: 0.22
Nodes (6): DateTime, Draftsnapshot, Draftstate, Draftteams, Leagueid, Tsupdated

### Community 19 - "User Auth Persistence"
Cohesion: 0.13
Nodes (16): ICollection, Applicationuser, Email, Managerlevel, Password, Teams, Userid, Userleagues (+8 more)

### Community 20 - "Team Endpoints & Auth Claims"
Cohesion: 0.17
Nodes (7): NBA.Api.DTOs, NBA.Api.Mappings, NBA.Api.Authentication, NBA.Tests, NBA.Service, NBA.Api.Endpoints, BoxScoreEvaluation

### Community 21 - "Player DTO"
Cohesion: 0.08
Nodes (24): DateTime, PlayerDto, Allowdrop, Assists, Blocks, Fieldgoal, Freethrow, Gameready (+16 more)

### Community 22 - "Game Schedule DTOs"
Cohesion: 0.18
Nodes (11): DateTime, GameDto, Date, GameId, HomeTeam, Postponed, Postseason, StartTime (+3 more)

### Community 23 - "Error Codes & Trade Statuses"
Cohesion: 0.16
Nodes (10): ErrorCodes, NBA.Data.Entities, NBA.Data.Context, NBA.Service.Authentication, ApplicationDefaults.Exceptions, NBA.Service.CalculateBoxScore, NBA.Service.Builder, NBA.Data.Constants (+2 more)

### Community 24 - "League Service & Endpoints"
Cohesion: 0.20
Nodes (11): IEndpointRouteBuilder, LeagueEndpoints, League, PagedResult, Task, TeamData, CreateLeagueInput, JoinLeagueInput (+3 more)

### Community 25 - "League Entity"
Cohesion: 0.09
Nodes (21): ICollection, League, Autostart, Commissioner, Draftcompleted, Draftsnapshot, Draftstyle, Leagueid (+13 more)

### Community 26 - "Draft Endpoints Integration Tests"
Cohesion: 0.27
Nodes (7): NBA.Api.SignalR.Clients, NBA.Data.Redis.Enumerations, NBA.Service.Draft, NBA.Api.SignalR.Hubs, NBA.Service.Player, NBA.Service.Trade, ApplicationDefaults.Options

### Community 27 - "End-Draft Integration Tests"
Cohesion: 0.12
Nodes (15): DraftOptions, DraftPickTime, Rounds, ShowTeamDraftBoardCount, AuthenticationHandler, AuthenticationSchemeOptions, Dictionary, IOptions (+7 more)

### Community 28 - "Redis Operations Integration Tests"
Cohesion: 0.15
Nodes (8): NBA.Data.Redis.Operations, NBA.Data.Redis.Scopes, NBA.Data.Enumerations, NBA.Data.Redis.Keys, NBA.Data.Redis.Entities, NBA.Data.Redis.Dtos, NBA.Service.Roster, PlayerPositionExtensions

### Community 29 - "External Client Response Tests"
Cohesion: 0.17
Nodes (6): ApplicationDefaults.Time, ExternalClients.Response, ExternalClients, NBA.Service.Game, NBA.Tests.Integration, ExternalClients.Poco

### Community 30 - "Game Redis Operations"
Cohesion: 0.14
Nodes (14): List, ScheduledGames, RestOfWeek, Today, Tomorrow, IDatabase, JsonSerializerOptions, Task (+6 more)

### Community 31 - "AppHost Launch Settings"
Cohesion: 0.13
Nodes (18): ASPIRE_DASHBOARD_OTLP_ENDPOINT_URL, ASPIRE_RESOURCE_SERVICE_ENDPOINT_URL, ASPNETCORE_ENVIRONMENT, DOTNET_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables (+10 more)

### Community 32 - "Hangfire Job Expiration Filter"
Cohesion: 0.05
Nodes (36): JwtOptions, AccessTokenMinutes, Audience, Issuer, RefreshTokenDays, SigningKey, ApplyStateContext, NBA.Api.HangFire (+28 more)

### Community 33 - "Core Database Schema DDL"
Cohesion: 0.24
Nodes (17): nba.applicationuser, nba.draftsnapshot, nba.league, nba.leagueplayer, nba.player, nba.playermemento, nba.playoff, nba.playoffbracket (+9 more)

### Community 34 - "Team Entity"
Cohesion: 0.09
Nodes (24): IEndpointRouteBuilder, TeamEndpoints, ICollection, Team, Approved, Categoryleaguepoints, Islock, Lastweekpoints (+16 more)

### Community 35 - "Game Redis Shapes"
Cohesion: 0.12
Nodes (17): DateTime, GameShort, Date, GameId, HomeTeam, Postponed, Postseason, StartTime (+9 more)

### Community 36 - "ExternalClients Project Files"
Cohesion: 0.13
Nodes (16): ApplicationDefaults, net10.0, Microsoft.NET.Sdk, BoxScoreBuilder, net10.0, Microsoft.NET.Sdk, ExternalClients, net10.0 (+8 more)

### Community 37 - "WireMock BallDontLie Fixture"
Cohesion: 0.12
Nodes (13): BallDontLieClientOptions, ApiKey, BaseUrl, Per_Page, IAsyncLifetime, HttpResponseMessage, IOptions, Task (+5 more)

### Community 38 - "PlayerInfo Response Shape"
Cohesion: 0.12
Nodes (16): PlayerInfoResponse, college, country, draft_number, draft_round, draft_year, first_name, height (+8 more)

### Community 39 - "JWT Options & Token Tests"
Cohesion: 0.12
Nodes (15): DateTime, Guid, List, Trade, Fromteam, Fromteamid, League, Leagueid (+7 more)

### Community 40 - "API Launch Profiles"
Cohesion: 0.13
Nodes (15): ASPNETCORE_ENVIRONMENT, applicationUrl, commandName, dotnetRunMessages, environmentVariables, launchBrowser, applicationUrl, commandName (+7 more)

### Community 41 - "Playoff Bracket Entities"
Cohesion: 0.12
Nodes (14): ICollection, Playoff, League, Leagueid, Playoffbrackets, Playoffid, Totalrounds, Playoffbracket (+6 more)

### Community 42 - "Transaction Entities"
Cohesion: 0.12
Nodes (14): DateTime, ICollection, Transaction, Transactionid, Transactionleagues, Transactionstatus, Tscreated, Typetransaction (+6 more)

### Community 43 - "BallDontLie Client & NBA Calendar"
Cohesion: 0.21
Nodes (9): CancellationToken, DateOnly, HttpResponseMessage, List, Task, BallDontLieClient, HttpClient, ResiliencePipeline (+1 more)

### Community 44 - "Player Stats Response"
Cohesion: 0.13
Nodes (14): PlayerStatsResponse, ast, blk, fg3a, fg3m, fga, fgm, fta (+6 more)

### Community 45 - "Per-League Stats Values"
Cohesion: 0.13
Nodes (14): Statsvalue, Assistsvalue, Blocksvalue, Fieldgoalvaluemade, Fieldgoalvaluemissed, Freethrowvaluemade, Freethrowvaluemissed, League (+6 more)

### Community 46 - "BoxScore Calculation Jobs"
Cohesion: 0.19
Nodes (10): AutomaticRetry, JobDisplayName, Dictionary, List, Task, BoxScoreCalculationService, CancellationToken, List (+2 more)

### Community 47 - "Trophy Entities"
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

### Community 51 - "Player Position Extensions"
Cohesion: 0.16
Nodes (9): IQueryable, Teamplayer, Player, Playerid, Team, Teamid, Teamplayerid, IOptions (+1 more)

### Community 52 - "IBallDontLieClient Contract"
Cohesion: 0.24
Nodes (9): CancellationToken, DateOnly, List, Task, IBallDontLieClient, List, GetGamesResponse, data (+1 more)

### Community 53 - "BallDontLie Response Metadata"
Cohesion: 0.15
Nodes (12): MetaData, Next_cursor, Per_page, Prev_Cursor, List, GetAllPlayersResponse, data, meta (+4 more)

### Community 54 - "Game Info Response"
Cohesion: 0.15
Nodes (13): DateTime, GameInfoResponse, date, datetime, home_team, home_team_score, id, postponed (+5 more)

### Community 55 - "Game Service & Endpoints"
Cohesion: 0.23
Nodes (9): IBackgroundJobClient, IEndpointRouteBuilder, GameEndpoints, CancellationToken, DateOnly, IOptions, List, Task (+1 more)

### Community 56 - "League DTO"
Cohesion: 0.05
Nodes (34): List, DraftOrderDto, Round, Teams, LeagueDto, Autostart, Commissioner, CommissionersTeam (+26 more)

### Community 57 - "Free Agency Service"
Cohesion: 0.29
Nodes (6): Leagueplayer, Isfreeagent, League, Leagueid, Leagueplayerid, Playerid

### Community 58 - "Adapter Mapping Tests"
Cohesion: 0.26
Nodes (5): PlayerData, Fact, InlineData, Theory, AdapterTests

### Community 59 - "Auth Token Issuance"
Cohesion: 0.16
Nodes (11): Task, IDraftHubClient, List, DraftBoardTeams, CurrentRound, DraftOrder, onTheClockTeam, TeamDraftBoard (+3 more)

### Community 60 - "Trade DTO"
Cohesion: 0.15
Nodes (12): DateTime, Guid, List, TradeDto, Fromteamid, Leagueid, Playerids, Status (+4 more)

### Community 61 - "User Team DTO"
Cohesion: 0.15
Nodes (12): List, UserTeamDto, Categoryleaguepoints, Islock, Lastweekpoints, Leagueid, Leaguename, Name (+4 more)

### Community 62 - "Test Project Packages"
Cohesion: 0.17
Nodes (12): NBA.Tests, net10.0, Microsoft.NET.Sdk, coverlet.collector (6.0.2), Microsoft.AspNetCore.SignalR.Client (10.0.0), Microsoft.AspNetCore.TestHost (10.0.0), Microsoft.EntityFrameworkCore.InMemory (10.0.0), Microsoft.NET.Test.Sdk (17.12.0) (+4 more)

### Community 63 - "Argon2id Password Hashing"
Cohesion: 0.16
Nodes (10): Argon2Options, DegreeOfParallelism, Iterations, MemoryKib, IPasswordHasher, IOptions, Argon2idPasswordHasher, Fact (+2 more)

### Community 64 - "NBA Calendar Date Handling"
Cohesion: 0.24
Nodes (5): NbaCalendar, DateOnly, InlineData, Theory, TimeZoneInfo

### Community 65 - "Draft Order DTO"
Cohesion: 0.29
Nodes (6): GameTeamDto, Abbreviation, City, FullName, Score, TeamId

### Community 66 - "Entity to DTO Mappers"
Cohesion: 0.24
Nodes (3): List, Team, EntityMappings

### Community 67 - "NBA.Api Package References"
Cohesion: 0.18
Nodes (10): net10.0, Aspire.StackExchange.Redis (13.1.2), Microsoft.Extensions.Http.Resilience (10.1.0), Aspire.Npgsql.EntityFrameworkCore.PostgreSQL (13.1.0), Microsoft.AspNetCore.Authentication.JwtBearer (10.0.0), Microsoft.AspNetCore.OpenApi (10.0.0), Microsoft.AspNetCore.SignalR.StackExchangeRedis (10.0.5), Microsoft.OpenApi (2.7.5) (+2 more)

### Community 68 - "Player Position Enum"
Cohesion: 0.22
Nodes (8): PlayerPositionEnum, C, CF, F, FG, G, GF, UNKOWN

### Community 70 - "Player Redis Manager"
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

### Community 77 - "Login DTO"
Cohesion: 0.33
Nodes (5): List, ScheduledGamesDto, RestOfWeek, Today, Tomorrow

### Community 78 - "Team DTO"
Cohesion: 0.40
Nodes (4): ApplicationOptions, CenterLimit, MaxPlayersPerTeam, ProposedTradeTtlMinutes

### Community 79 - "AppHost Packages"
Cohesion: 0.22
Nodes (8): net10.0, Aspire.Hosting.Redis (13.1.2), Aspire.StackExchange.Redis (13.1.2), Microsoft.NET.Sdk, Aspire.Hosting.AppHost (13.1.0), Aspire.Hosting.PostgreSQL (13.1.0), CommunityToolkit.Aspire.Hosting.NodeJS.Extensions (9.9.0), OpenTelemetry.Api (1.16.0)

### Community 80 - "SignalR Exception Hub Filter"
Cohesion: 0.50
Nodes (3): NBA.Api.Requests.Team, TeamRequest, teamName

### Community 81 - "Team Info Response"
Cohesion: 0.25
Nodes (8): TeamInfoResponse, abbreviation, city, conference, division, full_name, id, name

### Community 82 - "Refresh Token Generator Tests"
Cohesion: 0.50
Nodes (3): HttpStatusCode, InlineData, Theory

### Community 83 - "Player Endpoints"
Cohesion: 0.15
Nodes (8): IEndpointRouteBuilder, PlayerEndpoints, IReadOnlyList, PagedResult, TotalPages, DateTime, IReadOnlyList, PagedResult

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

### Community 92 - "AppHost Hosting Packages"
Cohesion: 0.29
Nodes (7): NBA.Service, net10.0, Aspire.Hosting.Redis (13.1.2), Microsoft.Extensions.Options (10.0.3), Microsoft.NET.Sdk, Isopoh.Cryptography.Argon2 (1.1.10), Microsoft.Extensions.Identity.Core (10.0.0)

### Community 94 - "Aspire Server Bindings"
Cohesion: 0.29
Nodes (7): tcp, bindings, port, protocol, scheme, targetPort, transport

### Community 95 - "Hosted Service Namespaces"
Cohesion: 0.17
Nodes (7): NBA.Api.Draft, NBA.Service.League, NBA.Api.HostedService, NBA.Service.FreeAgency, ApplicationDefaults.LogDefaults, NBA.Api.SignalR, NBA.Service.Team

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

### Community 101 - "Game Team Response"
Cohesion: 0.40
Nodes (5): Team, abbreviation, city, full_name, id

### Community 102 - "Chat Schema DDL"
Cohesion: 0.70
Nodes (4): chat.conversationparticipants, chat.messages, chat.rooms, nba.applicationuser

### Community 103 - "Redis Serializer"
Cohesion: 0.50
Nodes (3): NBA.Data.Redis, JsonSerializerOptions, RedisSerializer

### Community 104 - "Aspire HTTP Bindings"
Cohesion: 0.50
Nodes (4): http, protocol, scheme, transport

### Community 105 - "Testing Endpoints"
Cohesion: 0.18
Nodes (10): IEndpointRouteBuilder, TestingEndpoints, Dictionary, DraftBoardTeams, IOptions, JsonOptions, JsonSerializerOptions, Queue (+2 more)

## Knowledge Gaps
- **682 isolated node(s):** `ErrorCodes`, `TradeStatuses`, `BoxScoreEvaluation`, `FromTeam`, `PlayersIds` (+677 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **8 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `NbaFantasyContext` connect `EF Core DbContext Model` to `Draft Redis Operations & Keys`, `Trade & Team Services`, `Player Memento Stats`, `Hosted Services & Exception Handling`, `TradeHub Test Fixture`, `TeamPlayer & Draft Snapshot Persistence`, `User Auth Persistence`, `Error Codes & Trade Statuses`, `League Service & Endpoints`, `End-Draft Integration Tests`, `Hangfire Job Expiration Filter`, `Team Entity`, `Playoff Bracket Entities`, `Transaction Entities`, `BoxScore Calculation Jobs`, `Trophy Entities`, `Player Position Extensions`, `Free Agency Service`, `Player Redis Manager`, `Testing Endpoints`?**
  _High betweenness centrality (0.117) - this node is a cross-community bridge._
- **Why does `NBAException` connect `Trade & Team Services` to `Hangfire Job Expiration Filter`, `Team Entity`, `BallDontLie Client Tests`, `Draft Orchestration & Hub`, `Testing Endpoints`, `BallDontLie Client & NBA Calendar`, `BallDontLie WireMock Tests`, `TradeHub Real-Time Trading`, `BoxScore Calculation Jobs`, `TradeHub Test Fixture`, `Refresh Token Generator Tests`, `User Auth Persistence`, `Player Position Extensions`, `Player Endpoints`, `League Service & Endpoints`, `End-Draft Integration Tests`?**
  _High betweenness centrality (0.117) - this node is a cross-community bridge._
- **Why does `Player` connect `Error Codes & Trade Statuses` to `Entity to DTO Mappers`, `Player Redis Operations`, `Draft Endpoints Integration Tests`, `BoxScore Calculation Builder`, `Player Memento Stats`, `Player Redis Manager`, `Team Entity`, `BoxScore Calculation Jobs`, `EF Core DbContext Model`, `Player Position Extensions`, `Player Endpoints`, `Free Agency Service`, `Adapter Mapping Tests`, `Redis Adapter Mappings`, `External Client Response Tests`?**
  _High betweenness centrality (0.085) - this node is a cross-community bridge._
- **Are the 3 inferred relationships involving `TradeBetweenTeams` (e.g. with `.RemoveProposedTrade_removes_only_the_matching_trade_and_returns_it()` and `.RemoveProposedTrade_returns_null_for_unknown_id()`) actually correct?**
  _`TradeBetweenTeams` has 3 INFERRED edges - model-reasoned connections that need verification._
- **Are the 33 inferred relationships involving `NBAException` (e.g. with `.GetAsync()` and `.RefreshAsync()`) actually correct?**
  _`NBAException` has 33 INFERRED edges - model-reasoned connections that need verification._
- **What connects `ErrorCodes`, `TradeStatuses`, `BoxScoreEvaluation` to the rest of the system?**
  _682 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Trade Redis & TradeHub Tests` be split into smaller, more focused modules?**
  _Cohesion score 0.06288568909785483 - nodes in this community are weakly interconnected._