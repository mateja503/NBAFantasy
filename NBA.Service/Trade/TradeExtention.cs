using Microsoft.Extensions.DependencyInjection;

namespace NBA.Service.Trade
{
    // Single registration point for everything in NBA.Service/Trade, so Program.cs does not have to
    // track each new trade type by hand. Mirrors DraftExtention: scoped to match the DbContext/Redis
    // facade lifetime the rest of the request pipeline uses.
    //
    // Registrations are listed rather than discovered by reflection. The set is small and changes
    // rarely, and naming each one keeps the interface-to-implementation mapping below explicit — a
    // convention-based scan would have to be told about it anyway, and would resolve a missing
    // registration at first request instead of at compile time.
    public static class TradeExtention
    {
        public static IServiceCollection RegisterTrade(this IServiceCollection services)
        {
            // Postgres-side trade logic — the durable nba.trades row and the roster swap (rule 4).
            services.AddScoped<TradeService>();
            // Redis-only: the hot copies of live proposals that drive the real-time push (rule 4).
            services.AddScoped<TradeManager>();
            // Coordinates the two above and returns TradeEvents describing what happened. TradeHub
            // depends on the interface, not on the two concrete types: the orchestrator is the seam
            // that lets the hub be constructed without a live Postgres and Redis behind it.
            services.AddScoped<ITradeOrchestrator, TradeOrchestrator>();

            return services;
        }
    }
}
