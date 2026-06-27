using System.Text;
using ApplicationDefaults.Exceptions;
using ApplicationDefaults.Options;
using ExternalClients;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using NBA.Api;
using NBA.Api.Authentication;
using NBA.Api.Draft;
using NBA.Api.Endpoints;
using NBA.Api.HostedService;
using NBA.Api.SignalR.Hubs;
using NBA.Data.Context;
using NBA.Data.Entities;
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
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<Argon2Options>(builder.Configuration.GetSection("Argon2"));

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

#region Authentication
var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>() ?? new JwtOptions();

// Argon2id (memory-hard) password hashing and JWT issuance.
builder.Services.AddSingleton<IPasswordHasher<Applicationuser>, Argon2idPasswordHasher>();
builder.Services.AddSingleton<ITokenService, JwtTokenService>();
builder.Services.AddScoped<AuthTokenIssuer>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Keep claim names as-issued ("sub", "unique_name") instead of remapping to legacy URIs.
        options.MapInboundClaims = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
            ClockSkew = TimeSpan.FromSeconds(30),
        };

        // Browsers can't set the Authorization header on a WebSocket, so SignalR clients pass the
        // token as ?access_token=... — lift it into the auth pipeline for the hub paths.
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    (path.StartsWithSegments("/draftHub") || path.StartsWithSegments("/chatHub")
                        || path.StartsWithSegments("/tradeHub")))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
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
builder.Services.AddScoped<DraftSnapshotService>();
builder.Services.AddScoped<TradeService>();
builder.Services.AddScoped<TradeManager>();
builder.Services.AddScoped<FreeAgencyService>();
builder.Services.AddScoped<LeagueService>();
builder.Services.AddScoped<TeamService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<DraftTimerProcessor>();

#endregion


#region ExceptionHandlers
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
#endregion

#region HostedServices
builder.Services.AddHostedService<ApplicationHostedService>();
builder.Services.AddHostedService<HangFireJobSchedulerHostedService>();
// Polls Redis for due draft pick deadlines (replaces the per-pick Hangfire timer jobs).
builder.Services.AddHostedService<DraftTimerHostedService>();
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
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.UseHangfireDashboard();

#region HUBS
app.MapHub<ChatHub>("/chatHub");
app.MapHub<DraftHub>("/draftHub");
app.MapHub<TradeHub>("/tradeHub");

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

