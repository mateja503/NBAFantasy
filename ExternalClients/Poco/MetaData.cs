namespace ExternalClients.Poco
{
    public record MetaData
    {
        public long? Prev_Cursor { get; init; }
        public long? Next_cursor { get; init; } 
        public long Per_page { get; init; } 
    }
}
