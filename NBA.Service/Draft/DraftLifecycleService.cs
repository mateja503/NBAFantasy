using ApplicationDefaults.Exceptions;
using ApplicationDefaults.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NBA.Data.Context;
using NBA.Data.Entities;
using NBA.Data.Redis.Entities;

namespace NBA.Service.Draft
{
    // The two draft operations that both DraftManager (Redis-side coordinator) and DraftService
    // (Postgres-side logic) need. They used to live on DraftService, which forced DraftManager to
    // depend on the whole DraftService graph just to finalise a draft and rebuild the board.
    // Extracted here so neither type depends on the other; both take this as a dependency instead.
    //
    // Named *Service (rule 4) because EndDraft is the write that flushes the drafted rosters into
    // Postgres; PrepareDraftBoard is a pure projection over the in-memory draft order and touches
    // neither store.
    public class DraftLifecycleService(NbaFantasyContext context, IOptions<DraftOptions> draftOptions,
        NbaFantasyRedis redis, DraftSnapshotService snapshot)
    {
        private readonly NbaFantasyContext _context = context;
        private readonly DraftOptions _draftOptions = draftOptions.Value;
        private readonly NbaFantasyRedis _redis = redis;
        // The durable mirror of the live draft. EndDraft is the one place that drops it: once the
        // rosters are in Postgres there is nothing left to recover.
        private readonly DraftSnapshotService _snapshot = snapshot;

        // League name for the draft state. Returns null when the league is missing; the caller owns
        // the display fallback ("NO LEAGUE"), which is what DraftManager did before this moved here.
        public Task<Dictionary<long, Queue<TeamDraftBoard>>?> GetTeams(long leagueId) =>
          _redis.League(leagueId).Draft.GetTeams();

        public Task SetTeams(long leagueId, Dictionary<long, Queue<TeamDraftBoard>> teams) =>
            _redis.League(leagueId).Draft.SetTeams(teams);

        public Task<string?> GetLeagueName(long leagueId) =>
            _context.GetAllLeagues().Where(u => u.Leagueid == leagueId).Select(u => u.Name).SingleOrDefaultAsync();

        // Team ids of a league, used to scope the per-team Redis clean-up when a draft ends.
        public Task<List<long>> GetLeagueTeamIds(long leagueId) =>
            _context.GetAllTeams().Where(t => t.Leagueid == leagueId).Select(t => t.Teamid).ToListAsync();

        public DraftBoardTeams? PrepareDraftBoard(Dictionary<long, Queue<TeamDraftBoard>> teams)
        {
            var currentRound = teams.Keys.FirstOrDefault();
            if (currentRound == 0) return null;

            var onTheClockTeam = teams[currentRound].Select(t => new TeamDraftBoard { TeamId = t.TeamId, TeamName = t.TeamName!, Pick = t.Pick }).FirstOrDefault();
            var onTheClockTeams = teams[currentRound].Select(t => new TeamDraftBoard { TeamId = t.TeamId, TeamName = t.TeamName!, Pick = t.Pick }).Skip(1).Take(_draftOptions.ShowTeamDraftBoardCount).ToList();

            return new DraftBoardTeams
            {
                CurrentRound = currentRound,
                onTheClockTeam = onTheClockTeam,
                DraftOrder = onTheClockTeams
            };
        }

        // The whole end-of-draft sequence, in a load-bearing order: cancel the clock, flush the
        // rosters to Postgres, then tear the draft-time Redis/snapshot data down. This used to be
        // split across DraftManager.EndDraft (Redis clean-up) and this method (the Postgres flush);
        // both halves live here now so no caller can run one without the other.
        public async Task EndDraft(long leagueId)
        {
            var league = await _context.GetAllLeagues().SingleOrDefaultAsync(l => leagueId == l.Leagueid)
                    ?? throw new NBAException($"Missing league with leagueId {leagueId}", ErrorCodes.DataBaseRecordNotFound);

            var leagueRedis = _redis.League(leagueId);

            // (a) Remove any pending pick deadline from the timer sorted set.
            await leagueRedis.Draft.CancelTimer();

            // (b) Write the drafted rosters into Postgres — this reads DraftedPlayersPerTeam off
            // draft:state, so it has to run before the Redis clean-up below.
            //
            // The Draftcompleted guard deliberately skips only this flush, not the clean-up that
            // follows: re-inserting the Teamplayer rows would duplicate them, but the draft-time
            // Redis keys still have to go. This matches what the old split did — DraftManager.EndDraft
            // ran CancelTimer and the whole clean-up unconditionally and only the flush early-returned.
            if (league.Draftcompleted != true)
            {
                var draftedPerTeam = await leagueRedis.Draft.GetAllTeamsDraftedPlayers();
                var teamPlayers = draftedPerTeam?
                    .SelectMany(kvp => kvp.Value?.Select(p => new Teamplayer { Teamid = kvp.Key, Playerid = p.PlayerId ?? 0 }) ?? [])
                    .ToList() ?? [];

                var strategy = _context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    await using var tx = await _context.Database.BeginTransactionAsync();
                    try
                    {
                        if (teamPlayers.Count > 0)
                        {
                            await _context.AddTeamPlayerRange(teamPlayers);

                            // Same transaction as the roster flush: a committed roster whose players
                            // still read as free agents is a corrupt league. Only the drafted ids are
                            // touched - whoever nobody picked stays a free agent, which is what makes
                            // the post-draft pool meaningful.
                            //
                            // Distinct() because draftedPerTeam is keyed per team; a player id
                            // appearing under two teams would widen the Contains list for no reason.
                            var draftedPlayerIds = teamPlayers.Select(tp => tp.Playerid).Distinct().ToList();
                            _ = await _context.SetLeaguePlayersDrafted(leagueId, draftedPlayerIds);
                        }

                        league.Draftcompleted = true;
                        await _context.UpdateLeague(league);

                        await tx.CommitAsync();
                    }
                    catch
                    {
                        await tx.RollbackAsync();
                        throw;
                    }
                });
            }

            // (c) The draft board, the available player pool and the per-team roster sets are
            // draft-time scratch data; once the rosters are in Teamplayer there is nothing left to
            // draft from.
            var teamIds = await GetLeagueTeamIds(leagueId);

            await leagueRedis.Players.DeleteDraftPlayers(teamIds ?? []);

            // (d) Drop the live draft itself.
            _ = await leagueRedis.Draft.DeleteState();
            await leagueRedis.Draft.DeleteTeams();

            // (e) And finally the durable snapshot — nothing may rehydrate a finished draft.
            await _snapshot.DeleteAsync(leagueId);
        }
    }
}
