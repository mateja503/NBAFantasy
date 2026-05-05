using System;
using System.Collections.Generic;
using System.Text;

namespace NBA.Data.Redis.Entities
{
    public static class RedisKeys
    {
        public static string GetStartDraftTimerJobIdKey(long leagueId) => $"draft_timer:{leagueId}";
        public static string GetDraftStateKey(long leagueId) => $"draft_state:{leagueId}";
        public static string GetDraftTeamsKey(long leagueId) => $"draft_teams:{leagueId}";
        public static string GetStartPickJobIdKey(long leagueId) => $"start_pick_job:{leagueId}";
    }
}
