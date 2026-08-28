using NBA.Data.Redis.Dtos;
using NBA.Data.Redis.Enumerations;

namespace NBA.Data.Redis.Entities
{
    public class DraftState
    {
        public string LeagueName { get; set; } = string.Empty;
        public DateTime PickEndTime { get; set; } = DateTime.UtcNow;
        //public string? TeamName { get; set; } = string.Empty;
        //public long? TeamId { get; set; } = null;
        //public long? Round { get; set; } = null;    
        public int DraftStatus { get; set; }
        //public bool IsPaused { get; set; }
        //public bool IsDraftStarted { get; set; } = false;
        //public bool? IsDraftEnded { get; set; } = false;
        //public bool? IsDraftCompleted { get; set; } = false;
        public DraftBoardTeams? DraftBoardTeams { get; set; }
        // PlayerShortDto, not the PlayerShort entity: this object is serialized straight to clients over
        // /draftHub, so the positions in it are labels rather than PlayerPositionEnum codes.
        public List<PlayerShortDto>? DraftPlayers { get; set; }
        public Dictionary<long, List<PlayerShortDto>>? DraftedPlayersPerTeam { get; set; } = new Dictionary<long, List<PlayerShortDto>>();
    }
}
