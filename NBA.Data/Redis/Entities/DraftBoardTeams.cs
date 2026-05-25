
namespace NBA.Data.Redis.Entities
{
    public class TeamDraftBoard
    {
        public long TeamId { get; set; }
        public string TeamName { get; set; }
        public int Pick { get; set; }
    }

    public class DraftBoardTeams
    {
        public long CurrentRound { get; set; }
        public TeamDraftBoard? onTheClockTeam { get; set; }
        public List<TeamDraftBoard> DraftOrder { get; set; } = new List<TeamDraftBoard>();
    }
}
