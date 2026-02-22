

using ExternalClients.Response;
using NBA.Data.Entities;
using NBA.Data.Enumerations;

namespace NBA.Service
{
    public static class Addapter
    {
        public static List<Player> ToPlayer(List<PlayerInfoResponse> playersInfo)
        {
            return playersInfo.Select(playerInfo => new Player
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
                    _ => (int)PlayerPositionEnum.UNKOWN
                },
                Irlteamname = playerInfo.team?.full_name,
                Irlteamid = playerInfo.team?.id
            }).ToList();
        }
    }
}
