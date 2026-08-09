using ApplicationDefaults.Options;
using ApplicationDefaults.Time;
using ExternalClients;
using ExternalClients.Poco;
using ExternalClients.Response;
using Hangfire;
using Microsoft.Extensions.Options;
using NBA.Data.Redis.Entities;
using NBA.Service.Player;

namespace NBA.Service.Game
{
    public class GameService(IBallDontLieClient ballDontLieClient, IBackgroundJobClient jobClient,
        GameManager gameManager, IOptions<BallDontLieClientOptions> ballDontLieOptions)
    {
        private readonly IBallDontLieClient _ballDontLieClient = ballDontLieClient;
        private readonly GameManager _gameManager = gameManager;
        private readonly BallDontLieClientOptions _ballDontLieOptions = ballDontLieOptions.Value;

        public IBackgroundJobClient _jobClient = jobClient;

        // A week of games is ~70 rows, so at the configured page size this loop normally runs once.
        // The bound only exists so a misbehaving cursor cannot spin forever.
        private const int MaxSchedulePages = 10;
        private const int FallbackPerPage = 100;

        public async Task<List<GameInfoResponse>> TodaysGames(CancellationToken cancellationToken)
        {
            var games = await _ballDontLieClient.GetTodaysGames(cancellationToken);

            foreach (var game in games.data)
            {
                DateTimeOffset gameFinishes = new DateTimeOffset(game.datetime).AddHours(4);//for each game, schedule a job to fetch the stats after the game is finished (4 hours after the game starts)

                _jobClient.Schedule<PlayerService>(
                    playerService => playerService.GetPlayersGameStats(game.id,game.home_team.id,
                    game.visitor_team.id, game.home_team.full_name,game.visitor_team.full_name, CancellationToken.None),
                    gameFinishes);
            }
            return games.data;
        }

        /// <summary>
        /// The schedule served by GET /v1/games, split into today / tomorrow / the rest of the
        /// calendar week. Read-only by design: unlike <see cref="TodaysGames"/> it schedules no
        /// Hangfire work, because it is reachable anonymously and every page load would otherwise
        /// enqueue a duplicate set of stat jobs.
        /// </summary>
        public async Task<ScheduledGames> GetScheduledGamesAsync(CancellationToken cancellationToken)
        {
            var today = NbaCalendar.Today();

            var cached = await _gameManager.GetScheduledGames(today);
            if (cached is not null)
            {
                return cached;
            }

            var tomorrow = today.AddDays(1);
            var endOfWeek = NbaCalendar.EndOfWeek(today);

            // Tomorrow keeps its own bucket even when it falls into the next calendar week (i.e.
            // today is Sunday), which is why the window ends at the later of the two dates.
            var rangeEnd = endOfWeek > tomorrow ? endOfWeek : tomorrow;

            var games = await FetchGamesAsync(today, rangeEnd, cancellationToken);
            var scheduled = BucketByDay(games, today, tomorrow);

            await _gameManager.SetScheduledGames(today, scheduled);

            return scheduled;
        }

        /// <summary>
        /// Splits a flat window of games into the three buckets. Public and static so the day-boundary
        /// rules can be unit tested without standing up Redis or an HttpClient.
        /// </summary>
        public static ScheduledGames BucketByDay(List<GameShort> games, DateOnly today, DateOnly tomorrow)
        {
            var todayKey = today.ToApiDate();
            var tomorrowKey = tomorrow.ToApiDate();

            // Ordinal string comparison is safe here: the dates are already normalised to
            // yyyy-MM-dd by the Adapter, a format that sorts the same lexically and chronologically.
            return new ScheduledGames
            {
                Today = games.Where(g => g.Date == todayKey).ToList(),
                Tomorrow = games.Where(g => g.Date == tomorrowKey).ToList(),
                // Strictly after tomorrow, so the three buckets never overlap. Empty when the week
                // has no days left past tomorrow (today is Saturday or Sunday).
                RestOfWeek = games
                    .Where(g => !string.IsNullOrEmpty(g.Date) && string.CompareOrdinal(g.Date, tomorrowKey) > 0)
                    .ToList(),
            };
        }

        private async Task<List<GameShort>> FetchGamesAsync(DateOnly start, DateOnly end, CancellationToken cancellationToken)
        {
            var perPage = _ballDontLieOptions.Per_Page > 0 ? _ballDontLieOptions.Per_Page : FallbackPerPage;
            var metaData = new MetaData { Per_page = perPage };

            var all = new List<GameShort>();

            // Sequential cursor paging on purpose: external-api-shield permits one concurrent call.
            for (var page = 0; page < MaxSchedulePages; page++)
            {
                var response = await _ballDontLieClient.GetGames(start, end, metaData, cancellationToken);

                var batch = response?.data;
                if (batch is not null && batch.Count > 0)
                {
                    all.AddRange(Adapter.ToGameRedis(batch));
                }

                var nextCursor = response?.meta?.Next_cursor;
                if (nextCursor is null)
                {
                    break;
                }

                metaData = metaData with { Next_cursor = nextCursor };
            }

            return all;
        }
    }
}
