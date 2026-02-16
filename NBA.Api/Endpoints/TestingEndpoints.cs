using ExternalClients;
using System.Runtime.CompilerServices;

namespace NBA.Api.Endpoints
{
    public static class TestingEndpoints
    {
        public static IEndpointRouteBuilder TestEndpoints(this IEndpointRouteBuilder builder) 
        {
            builder.MapGet("/todaysgame", async (BallDontLieClient client) => 
            {
                //await client.GetTodaysGame();

                await client.GetAllPlayers();
            });
            return builder;

        }
    }
}
