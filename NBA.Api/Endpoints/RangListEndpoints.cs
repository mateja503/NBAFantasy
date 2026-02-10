namespace NBA.Api.Endpoints
{
    public static class RangListEndpoints
    {
        public static IEndpointRouteBuilder MapRangListEndpoints(this IEndpointRouteBuilder builder)
        {
            var ranglist = builder.MapGroup("/ranglist");
            ranglist.MapGet("/", () =>
            {
                return Results.Ok("This is the ranglist endpoint.");
            }).WithTags("ranglist");
            return ranglist;
        }
    }
}
