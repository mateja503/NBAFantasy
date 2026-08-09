using ExternalClients.Poco;
using ExternalClients.Response;

namespace ExternalClients
{
    /// <summary>
    /// The balldontlie surface the rest of the app is allowed to depend on. Consumers
    /// (<c>PlayerService</c>, <c>GameService</c>) take this instead of the concrete
    /// <see cref="BallDontLieClient"/> so they can be unit tested without an HttpClient.
    /// </summary>
    public interface IBallDontLieClient
    {
        Task<GetAllPlayersResponse> GetAllPlayers(MetaData metaData, CancellationToken cancellationToken);

        Task<GetGamesResponse> GetTodaysGames(CancellationToken cancellationToken);

        /// <summary>
        /// One page of the schedule between two dates, inclusive, in the NBA timezone.
        /// Paging is the caller's job (follow <c>meta.next_cursor</c>), matching <see cref="GetAllPlayers"/>.
        /// </summary>
        Task<GetGamesResponse> GetGames(DateOnly startDate, DateOnly endDate, MetaData metaData, CancellationToken cancellationToken);

        Task<List<PlayerStatsResponse>> GetPlayerStats(List<long> playerIds, long gameId, CancellationToken cancellationToken);
    }
}
