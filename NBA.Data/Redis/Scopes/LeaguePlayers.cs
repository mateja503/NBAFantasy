using NBA.Data.Redis.Entities;
using NBA.Data.Redis.Operations;

namespace NBA.Data.Redis.Scopes
{
    // League-bound view over the league-scoped half of PlayerRedisOperations. The "Leagues" prefix on
    // the underlying names is dropped here — the scope already says which league this is.
    //
    // The team-scoped and global player operations (GetPlayer, GetAllPlayers, SetPlayersRange,
    // GetTeamsDraftedPlayers, AddTeamsDrafterPlayer, ReplaceTeamsDraftedPlayers) are not exposed:
    // they take a teamId or nothing at all, so a league scope adds nothing to them.
    public readonly struct LeaguePlayers(PlayerRedisOperations operations, long leagueId)
    {
        public long LeagueId => leagueId;

        public Task<HashSet<PlayerShort>> AddAvailableDraftPlayers(List<PlayerShort> players) =>
            operations.AddLeaguesAvailableDraftPlayers(leagueId, players);

        public Task<HashSet<PlayerShort>?> GetAvailableDraftPlayers() =>
            operations.GetLeaguesAvailableDraftPlayers(leagueId);

        public Task AddDraftedPlayer(long playerId, int pick) =>
            operations.AddLeaguesDraftedPlayer(leagueId, playerId, pick);

        public Task<HashSet<long>?> GetDraftedPlayers() => operations.GetLeaguesDrafterPlayers(leagueId);

        public Task DeleteDraftPlayers(IEnumerable<long> teamIds) =>
            operations.DeleteLeagueDraftPlayers(leagueId, teamIds);

        public Task<bool> IsPlayerDrafted(long playerId) => operations.IsPlayerDrafted(leagueId, playerId);
    }
}
