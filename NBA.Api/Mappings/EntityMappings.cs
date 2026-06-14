using NBA.Api.DTOs;
using NBA.Data.Entities;

namespace NBA.Api.Mappings
{
    // Single source of truth for entity -> API DTO mapping. Previously these blocks were
    // copy-pasted across LeagueEndpoints, TeamEndpoints and AuthenticationEndpoints, which
    // meant a new column had to be added in several places. Nested relationships
    // (CommissionersTeam, Competesinleague) are composed by the caller so this stays simple.
    public static class EntityMappings
    {
        public static LeagueDto ToLeagueDto(this League e) => new()
        {
            Leagueid = e.Leagueid,
            Name = e.Name,
            Commissioner = e.Commissioner,
            Seasonyear = e.Seasonyear,
            Weeksforseason = e.Weeksforseason,
            Transactionlimit = e.Transactionlimit,
            Autostart = e.Autostart,
            Typetransactionlimits = e.Typetransactionlimits,
            Typeleague = e.Typeleague,
            Draftstyle = e.Draftstyle,
            Statsvalueid = e.Statsvalueid,
        };

        public static TeamDto ToTeamDto(this Team e) => new()
        {
            Teamid = e.Teamid,
            Name = e.Name,
            Seed = e.Seed,
            Waiverpriority = e.Waiverpriority,
            Lastweekpoints = e.Lastweekpoints,
            Categoryleaguepoints = e.Categoryleaguepoints,
            Islock = e.Islock,
        };
    }
}
