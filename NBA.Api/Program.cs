using ApplicationDefaults.Exceptions;
using ApplicationDefaults.Options;
using ExternalClients;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;
using NBA.Api;
using NBA.Api.Endpoints;
using NBA.Api.HangFire;
using NBA.Api.HostedService;
using NBA.Api.SignalR.Hubs;
using NBA.Data.Context;
using NBA.Service.Authentication;
using NBA.Service.CalculateBoxScore;
using NBA.Service.Game;
using NBA.Service.League;
using NBA.Service.League.Draft;
using NBA.Service.League.FreeAgency;
using NBA.Service.League.Trade;
using NBA.Service.Player;
using Polly;
using Scalar.AspNetCore;
using StackExchange.Redis;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Health checks, OpenTelemetry (traces/metrics/logs), service discovery and
// default HTTP resilience. Must run before other registrations.
builder.AddServiceDefaults();


#region Options
builder.Services.Configure<BallDontLieClientOptions>(builder.Configuration.GetSection("ExternalClients:BallDontLie"));
builder.Services.Configure<DraftOptions>(builder.Configuration.GetSection("ApplicationSettings:Draft"));
builder.Services.Configure<ApplicationOptions>(builder.Configuration.GetSection("ApplicationSettings"));

builder.Services.Configure<JsonOptions>(options =>
{
    // This will keep track of objects it has already seen and use a reference ID instead of recursing
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.WriteIndented = true;
});
#endregion

builder.AddRedisClient("redis-cache");

var connectionString = builder.Configuration.GetConnectionString("redis-cache");

builder.Services.AddSignalR()
    .AddStackExchangeRedis(connectionString!, options =>
    {
        // Optional: Add a prefix if you share this Redis with other apps
        //options.Configuration.ChannelPrefix = "YourAppName";
    });

builder.AddNpgsqlDbContext<NbaFantasyContext>("nbafantasydb");
builder.Services.AddSingleton<NbaFantasyRedis>();

builder.Services.RegisterHangFire(builder.Configuration);


builder.Services.AddHttpContextAccessor();

builder.Services.CreateResiliencePipeline();

#region HttpClients
builder.Services.AddHttpClient<BallDontLieClient>((serviceProvider, client) =>
{
    var _options = serviceProvider.GetRequiredService<IOptions<BallDontLieClientOptions>>().Value;

    client.BaseAddress = new Uri(_options.BaseUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("Authorization", _options.ApiKey);
});
#endregion

builder.Services.AddAuthorization();

builder.Services.AddOpenApi();

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
    ?? [];

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        // Origins come from configuration per environment. AllowCredentials requires
        // explicit origins (no wildcard), which is why we bind a concrete list.
        policy.WithOrigins(allowedOrigins)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

#region Services
builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<PlayerService>();
builder.Services.AddScoped<PlayerManager>();
builder.Services.AddScoped<BoxScoreCalculationService>();
builder.Services.AddScoped<DraftService>();
builder.Services.AddScoped<DraftManager>();
builder.Services.AddScoped<TradeService>();
builder.Services.AddScoped<FreeAgencyService>();
builder.Services.AddScoped<LeagueService>();
builder.Services.AddScoped<TeamService>();
builder.Services.AddScoped<AuthService>();

#endregion

#region HangFire
builder.Services.AddTransient<DraftJobs>();
#endregion


#region ExceptionHandlers
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
#endregion

#region HostedServices
builder.Services.AddHostedService<ApplicationHostedService>();
builder.Services.AddHostedService<HangFireJobSchedulerHostedService>();
#endregion



var app = builder.Build();

// Maps /health and /alive (development only by default — see ServiceDefaults).
app.MapDefaultEndpoints();

app.UseExceptionHandler();



if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

// Expose the JSON endpoint




app.UseHttpsRedirection();
app.UseRouting();


app.UseCors();
app.UseAuthorization();

app.MapStaticAssets();

app.UseHangfireDashboard();

#region HUBS
app.MapHub<ChatHub>("/chatHub");
app.MapHub<DraftHub>("/draftHub");

#endregion


var v1 = app.MapGroup("/v1");

v1.TestEndpoints();
v1.MapLeaguEndpoints();
v1.MapTeamEndpoints();
v1.MapDraftEndpoints();
v1.MapAuthenticationEndpoints();


v1.MapGet("/redis-check", (IConnectionMultiplexer redis) =>
{
    var status = redis.GetStatus();
    return Results.Ok(new { IsConnected = redis.IsConnected, Status = status });
});

app.Run();

