using Microsoft.Extensions.DependencyInjection;

namespace NBA.Service.Draft
{
    // Single registration point for everything in NBA.Service/Draft, so Program.cs does not have to
    // track each new draft type by hand. Scoped to match the DbContext/Redis facade lifetime the
    // rest of the request pipeline uses.
    public static class DraftExtention
    {
        public static IServiceCollection RegisterDraft(this IServiceCollection services)
        {
            // Postgres-side draft logic (rule 4).
            services.AddScoped<DraftService>();
            // Redis-only coordinators (rule 4).
            services.AddScoped<DraftManager>();
         
            // Owns the end-of-draft sequence outright (Postgres flush + Redis/snapshot tear-down) and
            // the draft-board projection; DraftManager and the API callers take it as a dependency.
            services.AddScoped<DraftLifecycleService>();
            // Postgres durability mirror for the live Redis draft.
            services.AddScoped<DraftSnapshotService>();

            return services;
        }
    }
}
