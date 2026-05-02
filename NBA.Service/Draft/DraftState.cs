namespace NBA.Service.Draft
{
    public class DraftState
    {
        public string LeagueName { get; set; }
        public DateTime PickEndTime { get; set; } = DateTime.UtcNow;
        public string? TeamName { get; set; } = string.Empty;
        public long? TeamId { get; set; } = null;
        public long? Round { get; set; } = null;    
        public bool IsPaused { get; set; }
        public bool IsDraftStarted { get; set; } = false;
    }
}
