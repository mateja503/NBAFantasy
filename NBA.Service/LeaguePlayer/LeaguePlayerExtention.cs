using Microsoft.Extensions.DependencyInjection;

namespace NBA.Service.LeaguePlayer
{
    // Single registration point for everything in NBA.Service/LeaguePlayer, so Program.cs does not
    // have to track each new league-player type by hand. Mirrors DraftExtention and TradeExtention:
    // scoped to match the DbContext lifetime the rest of the request pipeline uses.
    //
    // Registrations are listed rather than discovered by reflection, for the same reason as the other
    // two: the set is small, changes rarely, and a missing registration should surface here rather
    // than at first request.
    public static class LeaguePlayerExtention
    {
        public static IServiceCollection RegisterLeaguePlayer(this IServiceCollection services)
        {
            // Postgres-side owner of nba.leagueplayer — the per-league pool seeded at league creation
            // (rule 4). It takes the resolved player ids as a parameter, so nothing here needs Redis.
            services.AddScoped<LeaguePlayerService>();

            return services;
        }
    }
}
