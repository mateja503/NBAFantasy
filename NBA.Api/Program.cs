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
using NBA.Service.CalculateBoxScore;
using NBA.Service.Draft;
using NBA.Service.Game;
using NBA.Service.League.Draft;
using NBA.Service.League.FreeAgency;
using NBA.Service.League.Trade;
using NBA.Service.Player;
using Polly;
using Scalar.AspNetCore;
using StackExchange.Redis;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);


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

builder.Services.AddSignalR()
    .AddStackExchangeRedis(options =>
    {
        // This ensures SignalR doesn't try to create its own connection 
        // but waits for the one registered by builder.AddRedisClient
        options.ConnectionFactory = async (writer) =>
        {
            var multiplexer = builder.Services.BuildServiceProvider().GetRequiredService<IConnectionMultiplexer>();
            return multiplexer;
        };
    });

//var multiplexer = ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis"));
//builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);

//builder.Services.AddSignalR().AddStackExchangeRedis();

//// 3. FIX THE WARNING: Configure SignalR Redis Options using DI
//// This tells SignalR: "When you need the Redis connection, grab it from the DI container"
//builder.Services.AddOptions<RedisOptions>("StackExchangeRedis")
//    .Configure<IConnectionMultiplexer>((options, multiplexer) =>
//    {
//        // Link the shared ConnectionMultiplexer to SignalR
//        options.ConnectionFactory = _ => Task.FromResult(multiplexer);
//        //options.Configuration.ChannelPrefix = RedisChannel.Literal("NBA_FANTASY");

//        //var config = ConfigurationOptions.Parse(multiplexer.Configuration);
//        //config.ChannelPrefix = RedisChannel.Literal("NBA_FANTASY");
//        //config.AbortOnConnectFail = false;
//        //options.Configuration = config;

//        //// Apply your specific configurations
//        //options.Configuration.ChannelPrefix = RedisChannel.Literal("NBA");
//        //options.Configuration.AbortOnConnectFail = false;
//        //options.Configuration.ConnectRetry = 3;
//        //options.Configuration.ConfigurationChannel = "nba-fantasy-channel";
//        //options.Configuration.ClientName = "redis-nba-fantasy";
//        //options.Configuration.SyncTimeout = 1000;
//    });


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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

#region Services
builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<PlayerService>();
builder.Services.AddScoped<BoxScoreCalculationService>();
builder.Services.AddScoped<DraftService>();
builder.Services.AddSingleton<DraftManager>();
builder.Services.AddScoped<TradeService>();
builder.Services.AddScoped<FreeAgencyService>();

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


//app.UseExceptionHandler();



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
v1.MapLeagueTeamEndpoints();
v1.MapTeamEndpoints();
v1.MapDraftEndpoints();


v1.MapGet("/redis-check", (IConnectionMultiplexer redis) =>
{
    var status = redis.GetStatus();
    return Results.Ok(new { IsConnected = redis.IsConnected, Status = status });
});

app.Run();

