namespace NBA.Api.DTOs
{
    public class DraftOrderDto
    {
        public long Round { get; set; }
        public List<TeamDto> Teams { get; set; } = new List<TeamDto>();
    }
}
