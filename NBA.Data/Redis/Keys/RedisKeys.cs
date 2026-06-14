using System;
using System.Collections.Generic;
using System.Text;

namespace NBA.Data.Redis.Keys
{
    public static class RedisKeys
    {
        #region Draft
        public static string GetStartDraftTimerJobIdKey(long leagueId) => $"draft:timer:{leagueId}";
        public static string GetDraftStateKey(long leagueId) => $"draft:state:{leagueId}";
        public static string GetDraftTeamsKey(long leagueId) => $"draft:teams:{leagueId}";
        public static string GetStartPickJobIdKey(long leagueId) => $"start:pick_job:{leagueId}";
        public static string GetDraftCycleLockKey(long leagueId) => $"draft:lock:{leagueId}";

        #endregion

        #region Authentication
        // Refresh tokens are stored by their SHA-256 hash, never in clear text.
        public static string GetRefreshTokenKey(string tokenHash) => $"auth:refresh:{tokenHash}";

        #endregion


        #region Players

        public static string GetPlayerKey(long playerid) => $"nba:player:{playerid}";
        public static string GetMasterPlayerKey() => $"nba:master:players";
        public static string GetLeaguesDraftedPlayersKey(long leagueid) => $"nba:drafted:players:league:{leagueid}";
        public static string GetLeaguesAvailablePlayersKey(long leagueid) => $"nba:available:players:league:{leagueid}";
        public static string GetTeamsDraftedPlayersKey(long teamId) => $"nba:players:league:team:{teamId}"; 

        #endregion
    }
}
