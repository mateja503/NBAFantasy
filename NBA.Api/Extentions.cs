using Hangfire;
using Hangfire.PostgreSql;
using Npgsql;
namespace NBA.Api
{
    public static class Extentions
    {
        public static IServiceCollection AddPostgreSQLHangFire(this IServiceCollection services,IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("nbafantasydb")
                        ?? throw new InvalidOperationException("Connection string 'nbafantasydb' not found.");

            services.AddHangfire(configuration =>
            {
                configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                     .UseSimpleAssemblyNameTypeSerializer()
                     .UseRecommendedSerializerSettings()
                     .UsePostgreSqlStorage(boostrapperOptions =>
                         {
                             boostrapperOptions.UseNpgsqlConnection(connectionString);
                         },
                         new PostgreSqlStorageOptions 
                         {
                             QueuePollInterval = TimeSpan.FromSeconds(3),
                             PrepareSchemaIfNecessary = true,
                             SchemaName = "hangfire"
                         }
                     )

                     .WithJobExpirationTimeout(TimeSpan.FromHours(1000));
            }).AddHangfireServer(option =>
            {
                option.SchedulePollingInterval = TimeSpan.FromSeconds(1);
            });

            return services;
        }
    }
}
