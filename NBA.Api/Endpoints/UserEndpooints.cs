namespace NBA.Api.Endpoints
{
    public static class UserEndpooints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder builder) 
        {
            var user = builder.MapGroup("/user");

            user.MapGet("/", () =>
            {
                return Results.Ok("This is the user endpoint.");
            }).WithTags("user");

            return user;
        }
    }
}
