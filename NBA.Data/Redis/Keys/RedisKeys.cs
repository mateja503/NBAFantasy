using System;
using System.Collections.Generic;
using System.Text;

namespace NBA.Data.Redis.Keys
{
    public static class RedisKeys
    {
        #region Draft
        public static string GetStartDraftTimerJobIdKey(long leagueId) => $"draft_timer:{leagueId}";
        public static string GetDraftStateKey(long leagueId) => $"draft_state:{leagueId}";
        public static string GetDraftTeamsKey(long leagueId) => $"draft_teams:{leagueId}";
        public static string GetStartPickJobIdKey(long leagueId) => $"start_pick_job:{leagueId}";

        #endregion


        #region Players

        public static string GetPlayerKey(long playerid) => $"nba:player:{playerid}";

        public static string GetLeaguesDraftedPlayersKey(long leagueid) => $"nba:players:league:{leagueid}";

        public static string GetTeamsDrafterPlayersKey(long teamId) => $"nba:players:league:team:{teamId}"; 

        #endregion
    }
}
