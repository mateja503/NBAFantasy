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
            services.AddScoped<DraftOrderManager>();
            // Shared by DraftManager and DraftService: the draft-board projection, the end-of-draft
            // flush to Postgres and the league/team lookups.
            services.AddScoped<DraftLifecycleService>();
            // Postgres durability mirror for the live Redis draft.
            services.AddScoped<DraftSnapshotService>();

            return services;
        }
    }
}
