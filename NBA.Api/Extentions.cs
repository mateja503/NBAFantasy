using Hangfire;
using Hangfire.PostgreSql;
using Npgsql;
using Polly;
using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using System.Threading.RateLimiting;
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
