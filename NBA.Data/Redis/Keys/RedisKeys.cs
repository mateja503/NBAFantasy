using System;
using System.Collections.Generic;
using System.Text;

namespace NBA.Data.Redis.Keys
{
    public static class RedisKeys
    {
        #region Draft
        public static string GetDraftStateKey(long leagueId) => $"draft:state:{leagueId}";
        public static string GetDraftTeamsKey(long leagueId) => $"draft:teams:{leagueId}";
        public static string GetDraftCycleLockKey(long leagueId) => $"draft:lock:{leagueId}";

        // Single sorted set holding every league's pending pick deadline: member = leagueId,
        // score = unix-ms when the current pick expires. Replaces per-pick Hangfire jobs.
        public static string GetDraftTimersKey() => "draft:timers";

        #endregion

        #region Authentication
        // Refresh tokens are stored by their SHA-256 hash, never in clear text.
        public static string GetRefreshTokenKey(string tokenHash) => $"auth:refresh:{tokenHash}";

        #endregion

        #region Startup
        // Ensures only one replica performs the player back-fill / Redis load on boot.
        public static string GetStartupSeedLockKey() => "startup:player-seed:lock";

        #endregion


        #region Players

        public static string GetPlayerKey(long playerid) => $"nba:player:{playerid}";
        public static string GetMasterPlayerKey() => $"nba:master:players";
        public static string GetLeaguesDraftedPlayersKey(long leagueid) => $"nba:drafted:players:league:{leagueid}";
        public static string GetLeaguesAvailablePlayersKey(long leagueid) => $"nba:available:players:league:{leagueid}";
        public static string GetTeamsDraftedPlayersKey(long teamId) => $"nba:players:league:team:{teamId}";

        #endregion

        #region Games

        // Scoped to the NBA date the snapshot was built for, so yesterday's schedule can never be
        // served after the date rolls over even if its TTL has not elapsed yet.
        public static string GetScheduledGamesKey(string nbaDate) => $"nba:games:schedule:{nbaDate}";

        #endregion

        #region Trade

        public static string GetProposedDraftTradesKey(long leagueId) => $"draft:trade:proposed:{leagueId}";
        public static string GetAcceptedDraftTradeKey(long leagueId) => $"draft:trade:accepted:{leagueId}";

        #endregion
    }
}
