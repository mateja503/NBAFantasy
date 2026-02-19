
namespace ExternalClients.Response
{
    public record MetaResponse
    {
        public long Next_cursor { get; init; } 
        public long Per_page { get; init; } 
    }
}
