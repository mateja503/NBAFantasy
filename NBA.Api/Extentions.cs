using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;
using NBA.Api.HangFire;
using Npgsql;
using Polly;
using System;
using System.Net.Http;
using System.Threading.RateLimiting;
namespace NBA.Api
{
    public static class Extentions
    {
        public static IServiceCollection RegisterHangFire(this IServiceCollection services,IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("nbafantasydb")
                        ?? throw new InvalidOperationException("Connection string 'nbafantasydb' not found.");

            services.AddHangfire(hangfireConfig =>
            {
                hangfireConfig.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                     .UseSimpleAssemblyNameTypeSerializer()
                     .UseRecommendedSerializerSettings()
                     .UsePostgreSqlStorage(boostrapperOptions =>
                         {
                             boostrapperOptions.UseNpgsqlConnection(connectionString);
                         },
                         new PostgreSqlStorageOptions
                         {
                             // Immediate ("enqueued") jobs tolerate a few seconds of latency;
                             // a longer interval reduces constant polling load on Postgres.
                             QueuePollInterval = TimeSpan.FromSeconds(5),
                             PrepareSchemaIfNecessary = true,
                             SchemaName = "hangfire"
                         }
                     );
                // Job retention is owned solely by ShortenJobExpirationFilter (1 day). The previous
                // 1000-hour global default contradicted the filter and is intentionally removed.
                hangfireConfig.UseFilter(new ShortenJobExpirationFilter());

            }).AddHangfireServer(options =>
            {
                // The draft pick timer relies on scheduled (delayed) jobs, so this interval bounds
                // timer precision. 1s keeps picks responsive but means each server polls Postgres
                // every second — the main reason to migrate the timer to a Redis delayed queue
                // (see REFACTOR_NOTES.md) as concurrent drafts grow.
                options.SchedulePollingInterval = TimeSpan.FromSeconds(1);
                options.ServerName = "NBA-FANTASY";
            });

            return services;
        }

        public static IServiceCollection CreateResiliencePipeline(this IServiceCollection services)
        {
            return services.AddResiliencePipeline<string, HttpResponseMessage>("external-api-shield", pipelineBuilder =>
            {
                pipelineBuilder
                    .AddRetry(new HttpRetryStrategyOptions
                    {
                        BackoffType = DelayBackoffType.Exponential,
                        MaxRetryAttempts = 3,
                        UseJitter = true,
                        Delay = TimeSpan.FromSeconds(2)
                    })
                    .AddRateLimiter(new HttpRateLimiterStrategyOptions
                    {
                        DefaultRateLimiterOptions = new ConcurrencyLimiterOptions
                        {
                            PermitLimit = 1, 
                            //QueueLimit = 2
                        }
                    });
            });
        }
    }
}
