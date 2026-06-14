namespace NBA.Service
{
    // Generic page of results plus the metadata a client needs to page through the rest.
    // Lives in the service layer so both the query side and the API can share it.
    public record PagedResult<T>(IReadOnlyList<T> Items, int Page, int PageSize, int TotalCount)
    {
        public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}
