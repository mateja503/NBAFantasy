using System;
using System.Collections.Generic;
using System.Text;

namespace NBA.Service.Redis
{
    public static class RedisKeys
    {
        public static string GetStartDraftTimerJobIdKey(long leagueId) => $"draft_timer:{leagueId}";
        public static string GetDraftStateKey(long leagueId) => $"draft_state:{leagueId}";
    }
}
