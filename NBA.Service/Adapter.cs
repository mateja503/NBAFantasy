

using ApplicationDefaults.Time;
using ExternalClients.Response;
using NBA.Data.Entities;
using NBA.Data.Enumerations;
using NBA.Data.Redis.Entities;
using PlayerData = NBA.Data.Entities.Player;
using Team = ExternalClients.Response.Team;

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
                Playerposition = playerInfo.position.ToUpper() switch
                {
                    "G" => (int)PlayerPositionEnum.G,
                    "F" => (int)PlayerPositionEnum.F,
                    "C" => (int)PlayerPositionEnum.C,
                    "G-F" => (int)PlayerPositionEnum.GF,
                    "C-F" => (int)PlayerPositionEnum.CF,
                    "F-G" => (int)PlayerPositionEnum.FG,
                    _ => (int)PlayerPositionEnum.UNKOWN
                },
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
                Position = p.position!
            }).ToList();
        }

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

        private static GameTeamShort? ToGameTeamRedis(Team? team, int score)
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
                Position = (long)player.Playerposition! switch
                {
                    (long)PlayerPositionEnum.G => "G",
                    (long)PlayerPositionEnum.F => "F",
                    (long)PlayerPositionEnum.C => "C",
                    (long)PlayerPositionEnum.GF => "GF",
                    (long)PlayerPositionEnum.CF => "CF",
                    (long)PlayerPositionEnum.FG => "FG",
                    _ => "UNKOWN"
                }
            }).ToList();
        }
    }
}
