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
        NbaFantasyRedis redis)
    {
        private readonly NbaFantasyContext _context = context;
        private readonly DraftOptions _draftOptions = draftOptions.Value;
        private readonly NbaFantasyRedis _redis = redis;

        // League name for the draft state. Returns null when the league is missing; the caller owns
        // the display fallback ("NO LEAGUE"), which is what DraftManager did before this moved here.
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

        public async Task EndDraft(long leagueId)
        {
            var league = await _context.GetAllLeagues().SingleOrDefaultAsync(l => leagueId == l.Leagueid)
                    ?? throw new NBAException($"Missing league with leagueId {leagueId}", ErrorCodes.DataBaseRecordNotFound);

            if (league.Draftcompleted == true) return;

            var draftedPerTeam = await _redis.League(leagueId).Draft.GetAllTeamsDraftedPlayers();
            var teamPlayers = draftedPerTeam
                .SelectMany(kvp => kvp.Value.Select(p => new Teamplayer { Teamid = kvp.Key, Playerid = p.PlayerId ?? 0 }))
                .ToList();


            var strategy = _context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var tx = await _context.Database.BeginTransactionAsync();
                try
                {
                    if (teamPlayers.Count > 0)
                        await _context.AddTeamPlayerRange(teamPlayers);

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
    }
}
