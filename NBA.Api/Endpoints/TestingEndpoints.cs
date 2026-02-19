using ExternalClients;
using System.Runtime.CompilerServices;

namespace NBA.Api.Endpoints
{
    public static class TestingEndpoints
    {
        public static IEndpointRouteBuilder TestEndpoints(this IEndpointRouteBuilder builder) 
        {
            builder.MapGet("/activePlayers", async (BallDontLieClient client) => 
            {
                //await client.GetTodaysGame();

                await client.GetAllActivePlayers();
            });
            return builder;

        }
    }
}
