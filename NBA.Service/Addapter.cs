

using ExternalClients.Response;
using NBA.Data.Entities;
using NBA.Data.Enumerations;
using NBA.Data.Redis.Entities;
using PlayerData = NBA.Data.Entities.Player;

namespace NBA.Service
{
    public static class Addapter
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
