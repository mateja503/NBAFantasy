namespace NBA.Service.Draft
{
    public class DraftState
    {
        public string LeagueName { get; set; }
        public DateTime PickEndTime { get; set; } = DateTime.UtcNow;
        public bool IsPaused { get; set; }
    }
}
