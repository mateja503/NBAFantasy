using ApplicationDefaults.Exceptions;
using ExternalClients;
using ExternalClients.Poco;
using ExternalClients.Response;
using Hangfire;
using Hangfire.Storage;
using Microsoft.EntityFrameworkCore;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Data.Enumerations;
using NBA.Data.Redis.Entities;
using NBA.Service.CalculateBoxScore;
using PlayerData = NBA.Data.Entities.Player;

namespace NBA.Service.Player
{
    // Input contract is owned by the service layer so it never depends on the API's request types
    // (dependencies point inward: Api -> Service -> Data). Every member is optional; an input with
    // nothing set means "every player". Numeric columns come as ranges, strings as partial matches.
    public record PlayerSearchInput
    {
        public string? Name { get; init; }
        public string? Surname { get; init; }
        public string? Irlteamname { get; init; }
        public long? Irlteamid { get; init; }

        // Position labels ("G", "GF", ...) as the client sends them; parsed to PlayerPositionEnum here.
        public IReadOnlyList<string>? Positions { get; init; }

        public decimal? MinPoints { get; init; }
        public decimal? MaxPoints { get; init; }
        public decimal? MinAssists { get; init; }
        public decimal? MaxAssists { get; init; }
        public decimal? MinRebounds { get; init; }
        public decimal? MaxRebounds { get; init; }
        public decimal? MinBlocks { get; init; }
        public decimal? MaxBlocks { get; init; }
        public decimal? MinSteals { get; init; }
        public decimal? MaxSteals { get; init; }
        public decimal? MinThreepointers { get; init; }
        public decimal? MaxThreepointers { get; init; }
        public decimal? MinTurnovers { get; init; }
        public decimal? MaxTurnovers { get; init; }
        public decimal? MinFreethrow { get; init; }
        public decimal? MaxFreethrow { get; init; }
        public decimal? MinFieldgoal { get; init; }
        public decimal? MaxFieldgoal { get; init; }

        public bool? Allowdrop { get; init; }
        public bool? Islock { get; init; }

        public int? Rosterrole { get; init; }
        public int? Gameready { get; init; }
        public long? Playermemontoid { get; init; }

        public DateTime? TscreatedFrom { get; init; }
        public DateTime? TscreatedTo { get; init; }
        public DateTime? TsupdatedFrom { get; init; }
        public DateTime? TsupdatedTo { get; init; }

        // Scopes which fantasy team name is projected onto each player rather than narrowing the
        // results — a player can be on one team per league.
        public long? LeagueId { get; init; }

        public int? Page { get; init; }
        public int? PageSize { get; init; }
    }

    //use hangfire so this service to be executaed at a spefici time
    public class PlayerService(IBallDontLieClient ballClient, NbaFantasyContext nbaContext, BoxScoreCalculationService boxScoreCalculationService)
    {
        private readonly IBallDontLieClient _ballDontLieClient = ballClient;
        private readonly NbaFantasyContext _nbaContext = nbaContext;
        private readonly BoxScoreCalculationService _boxScoreCalculationService = boxScoreCalculationService;

        private const int MaxPageSize = 100;
        public async Task<GetAllPlayersResponse> AddPlayersToDb(MetaData metadata, CancellationToken cancellationToken) 
        {
            var externalPlayers = await _ballDontLieClient.GetAllPlayers(metadata, cancellationToken);
            var filteredPlayers = PlayerFilter.FilterNonActivePlayers(externalPlayers.data);
            var players = Adapter.ToPlayerDb(filteredPlayers);
            await _nbaContext.AddPlayers(players, cancellationToken);
            return externalPlayers;
        }

        public async Task<PlayerData?> GetSpecificPlayerById(long playerId) 
        {
            return await _nbaContext.GetAllPlayers().FirstOrDefaultAsync(u => u.Playerid == playerId) ?? throw new NBAException($"For playerId {playerId}",ErrorCodes.DataBaseRecordNotFound);
        }

        // Paged so the list endpoint never loads the whole table. Every filter is applied only when
        // the caller supplied it, so an empty input degrades to "first page of all players".
        public async Task<PagedResult<PlayerData>> GetPagedAsync(PlayerSearchInput input)
        {
            var page = input.Page is null or < 1 ? 1 : input.Page.Value;
            var pageSize = input.PageSize is null or < 1
                ? 20
                : (input.PageSize.Value > MaxPageSize ? MaxPageSize : input.PageSize.Value);

            var query = _nbaContext.GetAllPlayers()
                    .Include(p => p.Teamplayers)
                    .ThenInclude(tp => tp.Team)
                    .AsNoTracking();

            // ILike is Postgres' case-insensitive LIKE, so a partial name matches whatever the
            // client typed regardless of casing.
            if (!string.IsNullOrWhiteSpace(input.Name))
                query = query.Where(p => EF.Functions.ILike(p.Name, $"%{input.Name}%"));

            if (!string.IsNullOrWhiteSpace(input.Surname))
                query = query.Where(p => EF.Functions.ILike(p.Surname, $"%{input.Surname}%"));

            if (!string.IsNullOrWhiteSpace(input.Irlteamname))
                query = query.Where(p => p.Irlteamname != null && EF.Functions.ILike(p.Irlteamname, $"%{input.Irlteamname}%"));

            if (input.Irlteamid is not null)
                query = query.Where(p => p.Irlteamid == input.Irlteamid);

            if (input.Positions is { Count: > 0 })
            {
                var positionCodes = ToPositionCodes(input.Positions);
                query = query.Where(p => p.Playerposition != null && positionCodes.Contains(p.Playerposition.Value));
            }

            if (input.MinPoints is not null) query = query.Where(p => p.Points >= input.MinPoints);
            if (input.MaxPoints is not null) query = query.Where(p => p.Points <= input.MaxPoints);
            if (input.MinAssists is not null) query = query.Where(p => p.Assists >= input.MinAssists);
            if (input.MaxAssists is not null) query = query.Where(p => p.Assists <= input.MaxAssists);
            if (input.MinRebounds is not null) query = query.Where(p => p.Rebounds >= input.MinRebounds);
            if (input.MaxRebounds is not null) query = query.Where(p => p.Rebounds <= input.MaxRebounds);
            if (input.MinBlocks is not null) query = query.Where(p => p.Blocks >= input.MinBlocks);
            if (input.MaxBlocks is not null) query = query.Where(p => p.Blocks <= input.MaxBlocks);
            if (input.MinSteals is not null) query = query.Where(p => p.Steals >= input.MinSteals);
            if (input.MaxSteals is not null) query = query.Where(p => p.Steals <= input.MaxSteals);
            if (input.MinThreepointers is not null) query = query.Where(p => p.Threepointers >= input.MinThreepointers);
            if (input.MaxThreepointers is not null) query = query.Where(p => p.Threepointers <= input.MaxThreepointers);
            if (input.MinTurnovers is not null) query = query.Where(p => p.Turnovers >= input.MinTurnovers);
            if (input.MaxTurnovers is not null) query = query.Where(p => p.Turnovers <= input.MaxTurnovers);
            if (input.MinFreethrow is not null) query = query.Where(p => p.Freethrow >= input.MinFreethrow);
            if (input.MaxFreethrow is not null) query = query.Where(p => p.Freethrow <= input.MaxFreethrow);
            if (input.MinFieldgoal is not null) query = query.Where(p => p.Fieldgoal >= input.MinFieldgoal);
            if (input.MaxFieldgoal is not null) query = query.Where(p => p.Fieldgoal <= input.MaxFieldgoal);

            if (input.Allowdrop is not null) query = query.Where(p => p.Allowdrop == input.Allowdrop);
            if (input.Islock is not null) query = query.Where(p => p.Islock == input.Islock);

            if (input.Rosterrole is not null) query = query.Where(p => p.Rosterrole == input.Rosterrole);
            if (input.Gameready is not null) query = query.Where(p => p.Gameready == input.Gameready);
            if (input.Playermemontoid is not null) query = query.Where(p => p.Playermemontoid == input.Playermemontoid);

            // tscreated/tsupdated are 'timestamp with time zone', and Npgsql rejects any DateTime
            // that is not Kind=Utc. A date parsed out of the query string is Kind=Unspecified, so
            // every bound comparison has to be normalised first.
            var tscreatedFrom = ToUtc(input.TscreatedFrom);
            var tscreatedTo = ToUtc(input.TscreatedTo);
            var tsupdatedFrom = ToUtc(input.TsupdatedFrom);
            var tsupdatedTo = ToUtc(input.TsupdatedTo);

            if (tscreatedFrom is not null) query = query.Where(p => p.Tscreated >= tscreatedFrom);
            if (tscreatedTo is not null) query = query.Where(p => p.Tscreated <= tscreatedTo);
            if (tsupdatedFrom is not null) query = query.Where(p => p.Tsupdated >= tsupdatedFrom);
            if (tsupdatedTo is not null) query = query.Where(p => p.Tsupdated <= tsupdatedTo);

            // Alphabetical, and tie-broken by the primary key so the sort is total - skip/take paging
            // returns the wrong rows without a stable order.
            var ordered = query.OrderBy(p => p.Surname).ThenBy(p => p.Name).ThenBy(p => p.Playerid);

            var totalCount = await ordered.CountAsync();
            var items = await ordered
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<PlayerData>(items, page, pageSize, totalCount);
        }

        // A bare date from the query string carries no zone, so it is read as UTC rather than as
        // server-local time - that keeps the same query returning the same rows on any machine.
        private static DateTime? ToUtc(DateTime? value) => value?.Kind switch
        {
            null => null,
            DateTimeKind.Utc => value,
            DateTimeKind.Local => value.Value.ToUniversalTime(),
            _ => DateTime.SpecifyKind(value.Value, DateTimeKind.Utc),
        };

        private static int[] ToPositionCodes(IReadOnlyList<string> positions)
        {
            var codes = new int[positions.Count];

            for (var i = 0; i < positions.Count; i++)
            {
                if (!Enum.TryParse<PlayerPositionEnum>(positions[i], ignoreCase: true, out var parsed))
                    throw new NBAException($"'{positions[i]}' is not a known player position", ErrorCodes.InvalidFilterValue);

                codes[i] = (int)parsed;
            }

            return codes;
        }

        public async Task<List<PlayerData>> GetPlayersForTeams(List<long> teamIds)
        {
            return await _nbaContext.GetAllPlayers()
                .AsNoTracking()
                .Where(u => teamIds.Contains(u.Irlteamid ?? 0))
                .ToListAsync();
        }

        [JobDisplayName("Stats Check: {3} vs {4} (ID: {0})")]
        [AutomaticRetry(Attempts = 3, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        public async Task<List<PlayerStatsResponse>> GetPlayersGameStats(long gameId, long hometeamId, long awayteamId, string homeTeam, string awayTeam, CancellationToken cancellationToken)
        {
            //TODO implment observer where the calculated points are stored in database and the fantasy teams are updated with the new points
            //
            var players = await GetPlayersForTeams([hometeamId, awayteamId]);

            var playerIds = players.Select(p => p.Playerid).ToList();

            var playersStats = await _ballDontLieClient.GetPlayerStats(playerIds, gameId, cancellationToken);

            using (var connection = JobStorage.Current.GetConnection()) 
            {
                using (connection.AcquireDistributedLock("fantasy-update-player-lock", TimeSpan.FromSeconds(100)))
                {
                    await _boxScoreCalculationService.PerformCalculations(playersStats, players.ToDictionary(p=>p.Playerid));
                }
            }
            return playersStats;
        }
    }
}
