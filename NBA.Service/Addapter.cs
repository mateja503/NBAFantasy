

using ExternalClients.Response;
using NBA.Data.Enumerations;
using PlayerData = NBA.Data.Entities.Player;

namespace NBA.Service
{
    public static class Addapter
    {
        public static List<PlayerData> ToPlayer(List<PlayerInfoResponse> playersInfo)
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
    }
}
