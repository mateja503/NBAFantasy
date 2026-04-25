namespace NBA.Api.Requests.Draft
{
    public class DraftRequest
    {
        public long? LeagueId { get; set; } = null;
        public bool? StartDraft { get; set; } = true;
    }
}
