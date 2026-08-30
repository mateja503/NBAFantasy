using Microsoft.Extensions.DependencyInjection;

namespace NBA.Service.FreeAgency
{
    // Single registration point for everything in NBA.Service/FreeAgency, so Program.cs does not have
    // to track each new free-agency type by hand. Mirrors DraftExtention, TradeExtention and
    // LeaguePlayerExtention: scoped to match the DbContext lifetime the rest of the request pipeline
    // uses.
    //
    // Registrations are listed rather than discovered by reflection, for the same reason as the other
    // three: the set is small, changes rarely, and a missing registration should surface here rather
    // than at first request.
    public static class FreeAgencyExtention
    {
        public static IServiceCollection RegisterFreeAgency(this IServiceCollection services)
        {
            // Postgres-side owner of the Isfreeagent flag on nba.leagueplayer — reads the pool and
            // toggles ownership (rule 4). Nothing here touches Redis.
            services.AddScoped<FreeAgencyService>();

            return services;
        }
    }
}
