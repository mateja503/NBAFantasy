using ApplicationDefaults.Exceptions;
using ApplicationDefaults.Options;
using Microsoft.Extensions.Options;

namespace NBA.Service.League.Roster
{
    // League roster composition rules (squad size, center cap) in one place, so the draft, trade and
    // free-agency paths cannot drift apart — they already had: the trade path threw the generic
    // TradeIsNotValid where the draft path threw the specific TeamMaxPlayersReached /
    // MaxCenterLimitReached for the very same two limits.
    //
    // Deliberately not a *Service or *Manager (rule 4): it touches neither Postgres nor Redis. It is
    // pure policy over two counts, which is what lets every caller share it regardless of whether its
    // roster came from Postgres (Player) or Redis (PlayerShort).
    public class RosterValidator(IOptions<ApplicationOptions> applicationOptions)
    {
        private readonly ApplicationOptions _options = applicationOptions.Value;

        // Callers pass the roster as it WOULD look after their change: a draft pick passes count + 1,
        // a trade passes the length of the recomputed roster.
        public void Validate(int playerCount, int centerCount)
        {
            if (playerCount > _options.MaxPlayersPerTeam)
                throw new NBAException("Team has reached maximum number of players", ErrorCodes.TeamMaxPlayersReached);

            if (centerCount > _options.CenterLimit)
                throw new NBAException("Team has reached maximum number of centers", ErrorCodes.MaxCenterLimitReached);
        }
    }
}
