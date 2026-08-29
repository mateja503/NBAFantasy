

using ApplicationDefaults.Time;
using ExternalClients.Response;
using NBA.Data.Entities;
using NBA.Data.Enumerations;
using NBA.Data.Redis.Entities;
using PlayerData = NBA.Data.Entities.Player;
using ApiTeam = ExternalClients.Response.Team;

namespace NBA.Service
{
    public static class Adapter
    {
        public static List<PlayerData> ToPlayerDb(List<PlayerInfoResponse> playersInfo)
        {
            return playersInfo.Select(playerInfo => new PlayerData
            {
                Playerid = playerInfo.id,
                Name = playerInfo.first_name,
                Surname = playerInfo.last_name,
                Tscreated = DateTime.UtcNow,
                Playerposition = ToPositionCode(playerInfo.position),
                Irlteamname = playerInfo.team?.full_name,
                Irlteamid = playerInfo.team?.id
            }).ToList();
        }

        public static List<PlayerShort> ToPlayerRedis(List<PlayerInfoResponse> playersInfo)
        {
            return playersInfo.Select(p => new PlayerShort
            {
                PlayerId = p.id,
                FullName = $"{p.first_name} {p.last_name}",
                // Previously stored the raw balldontlie string; PlayerShort.Position is now the same
                // int code as Player.Playerposition, so the Redis and Postgres shapes agree.
                Position = ToPositionCode(p.position)
            }).ToList();
        }

        // balldontlie sends the position as a free-text abbreviation ("G", "C-F"), and it can be absent
        // entirely — an unrecognised or missing value maps to UNKOWN rather than throwing, because one
        // odd player record should not fail a whole page import.
        public static int ToPositionCode(string? position) => position?.ToUpper() switch
        {
            "G" => (int)PlayerPositionEnum.G,
            "F" => (int)PlayerPositionEnum.F,
            "C" => (int)PlayerPositionEnum.C,
            "G-F" => (int)PlayerPositionEnum.GF,
            "C-F" => (int)PlayerPositionEnum.CF,
            "F-G" => (int)PlayerPositionEnum.FG,
            _ => (int)PlayerPositionEnum.UNKOWN
        };

        public static List<GameShort> ToGameRedis(List<GameInfoResponse> games)
        {
            return games.Select(game => new GameShort
            {
                GameId = game.id,
                // Normalised here so every consumer downstream can compare it to a yyyy-MM-dd bucket key.
                Date = NbaCalendar.ToApiDatePart(game.date),
                Status = game.status,
                Time = game.time,
                // default(DateTime) means balldontlie omitted it; null travels better than 0001-01-01.
                StartTime = game.datetime == default ? null : game.datetime,
                Postseason = game.postseason,
                Postponed = game.postponed,
                HomeTeam = ToGameTeamRedis(game.home_team, game.home_team_score),
                VisitorTeam = ToGameTeamRedis(game.visitor_team, game.visitor_team_score),
            }).ToList();
        }

        private static GameTeamShort? ToGameTeamRedis(ApiTeam? team, int score)
        {
            if (team is null)
            {
                return null;
            }

            return new GameTeamShort
            {
                TeamId = team.id,
                FullName = team.full_name,
                Abbreviation = team.abbreviation,
                City = team.city,
                Score = score,
            };
        }

        public static List<PlayerShort> ToPlayerRedisFromDB(List<PlayerData> players)
        {
            return players.Select(player => new PlayerShort
            {
                PlayerId = player.Playerid,
                FullName = $"{player.Name} {player.Surname}",
                // Both sides now hold the same int code, so this is a copy rather than a conversion.
                Position = player.Playerposition
            }).ToList();
        }
    }
}
