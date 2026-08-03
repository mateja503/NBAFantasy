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

        Task<GetTodaysGamesResponse> GetTodaysGames(CancellationToken cancellationToken);

        Task<List<PlayerStatsResponse>> GetPlayerStats(List<long> playerIds, long gameId, CancellationToken cancellationToken);
    }
}
