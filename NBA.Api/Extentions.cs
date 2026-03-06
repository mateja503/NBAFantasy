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

                configuration.UseFilter(new ShortenJobExpirationFilter());

            }).AddHangfireServer(options =>
            {
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
