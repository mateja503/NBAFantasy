using Microsoft.Extensions.DependencyInjection;

namespace NBA.Service.Player
{
    // Single registration point for everything in NBA.Service/Player, so Program.cs does not have to
    // track each new player type by hand. Mirrors DraftExtention: scoped to match the DbContext/Redis
    // facade lifetime the rest of the request pipeline uses.
    //
    // Registrations are listed rather than discovered by reflection - the set is small, changes rarely,
    // and a missing registration should surface here rather than at first request.
    public static class PlayerExtention
    {
        public static IServiceCollection RegisterPlayer(this IServiceCollection services)
        {
            // Postgres-side player logic (rule 4): the balldontlie import, paged search, game stats.
            services.AddScoped<PlayerService>();
            // Redis-only coordinator (rule 4).
            services.AddScoped<PlayerManager>();
            // Spans both stores on purpose - see PlayerCoordinator.
            services.AddScoped<PlayerCoordinator>();

            return services;
        }
    }
}
