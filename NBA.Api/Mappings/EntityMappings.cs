using NBA.Api.DTOs;
using NBA.Data.Entities;
using NBA.Data.Enumerations;
using NBA.Data.Redis.Entities;

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

        public static PlayerDto ToPlayerDto(this Player e, long? leagueId = null) => new()
        {
            Playerid = e.Playerid,
            Name = e.Name,
            Surname = e.Surname,
            Irlteamname = e.Irlteamname,
            Position = e.Playerposition.ToPositionLabel(),
            Points = e.Points,
            Rebounds = e.Rebounds,
            Assists = e.Assists,
            Steals = e.Steals,
            Blocks = e.Blocks,
            Threepointers = e.Threepointers,
            Turnovers = e.Turnovers,
            Fieldgoal = e.Fieldgoal,
            Freethrow = e.Freethrow,
            Irlteamid = e.Irlteamid,
            Allowdrop = e.Allowdrop,
            Islock = e.Islock,
            Rosterrole = e.Rosterrole,
            Gameready = e.Gameready,
            Playermemontoid = e.Playermemontoid,
            Tsupdated = e.Tsupdated,
            // Teamplayers is only populated when the caller asked for a league (see
            // PlayerService.GetPagedAsync). Lazy loading is off, so an unloaded collection is
            // simply empty here rather than triggering a query — callers that don't need the
            // fantasy team, like the roster endpoint, get null and pay nothing.
            Team = leagueId is not null ? e.Teamplayers.Where(tp=> tp.Team.Leagueid == leagueId).Select(tp => tp.Team.Name).FirstOrDefault() : null,
        };

        // The roster is passed in rather than read off e.Teamplayers so the caller decides how the
        // players were loaded (and an untouched navigation collection can't silently produce an
        // empty roster).
        public static UserTeamDto ToUserTeamDto(this Team e, List<Player> players) => new()
        {
            Teamid = e.Teamid,
            Name = e.Name,
            Seed = e.Seed,
            Waiverpriority = e.Waiverpriority,
            Lastweekpoints = e.Lastweekpoints,
            Categoryleaguepoints = e.Categoryleaguepoints,
            Islock = e.Islock,
            Leagueid = e.Leagueid,
            Leaguename = e.League?.Name,
            Players = players.Select(p => p.ToPlayerDto()).ToList(),
        };

        // The schedule never touches Postgres: GameShort is the cached Redis shape produced by
        // Adapter.ToGameRedis, and this is still the only place it becomes an API contract (rule 5).
        public static ScheduledGamesDto ToScheduledGamesDto(this ScheduledGames e) => new()
        {
            Today = e.Today.Select(g => g.ToGameDto()).ToList(),
            Tomorrow = e.Tomorrow.Select(g => g.ToGameDto()).ToList(),
            RestOfWeek = e.RestOfWeek.Select(g => g.ToGameDto()).ToList(),
        };

        public static GameDto ToGameDto(this GameShort e) => new()
        {
            GameId = e.GameId,
            Date = e.Date,
            Status = e.Status,
            Time = e.Time,
            StartTime = e.StartTime,
            Postseason = e.Postseason,
            Postponed = e.Postponed,
            HomeTeam = e.HomeTeam.ToGameTeamDto(),
            VisitorTeam = e.VisitorTeam.ToGameTeamDto(),
        };

        // Null in, null out: a game missing a side is not worth failing the whole schedule over.
        private static GameTeamDto? ToGameTeamDto(this GameTeamShort? e) => e is null ? null : new GameTeamDto
        {
            TeamId = e.TeamId,
            FullName = e.FullName,
            Abbreviation = e.Abbreviation,
            City = e.City,
            Score = e.Score,
        };

        // Mirrors the PlayerShort conversion in NBA.Service/Adapter.cs. It is duplicated rather than
        // shared because Adapter belongs to the balldontlie translation layer while this belongs to
        // the entity -> DTO layer; they are free to diverge.
        private static string ToPositionLabel(this int? playerPosition) => playerPosition switch
        {
            (int)PlayerPositionEnum.G => "G",
            (int)PlayerPositionEnum.F => "F",
            (int)PlayerPositionEnum.C => "C",
            (int)PlayerPositionEnum.GF => "GF",
            (int)PlayerPositionEnum.CF => "CF",
            (int)PlayerPositionEnum.FG => "FG",
            _ => nameof(PlayerPositionEnum.UNKOWN),
        };
    }
}
