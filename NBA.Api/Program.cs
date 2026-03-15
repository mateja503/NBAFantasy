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
using NBA.Api.HostedService;
using NBA.Data.Context;
using NBA.Service.CalculateBoxScore;
using NBA.Service.Game;
using NBA.Service.League.Draft;
using NBA.Service.League.FreeAgency;
using NBA.Service.League.Trade;
using NBA.Service.Observer;
using NBA.Service.Observer.HubSignalR;
using NBA.Service.Observer.Listeners;
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

builder.Services.AddSingleton<EventManager>();
builder.Services.AddSingleton<AuctionListener>();
builder.Services.AddSingleton<AuctionHub>();


builder.Services.AddSignalR().AddStackExchangeRedis(options =>
{
    options.ConnectionFactory = async writer =>
    {
        var multiplexer = builder.Services.BuildServiceProvider()//this has to be fixed
                                     .GetRequiredService<IConnectionMultiplexer>();
        return multiplexer;
    };
    options.Configuration.ChannelPrefix = RedisChannel.Literal("NBA");
    options.Configuration.AbortOnConnectFail = false;
    options.Configuration.ConnectRetry = 3;
    options.Configuration.ConfigurationChannel = "nba-fantasy-channel";
    options.Configuration.ClientName = "redis-nba-fantasy";
    options.Configuration.SyncTimeout = 1000; // 1 second timeout

});

//builder.Services.AddSignalR().AddStackExchangeRedis();

//// 2. Use AddOptions to "inject" the Multiplexer into the RedisOptions
//builder.Services.AddOptions<RedisOptions>("StackExchangeRedis")
//    .Configure<IConnectionMultiplexer>((options, multiplexer) =>
//    {
//        // Simply return the multiplexer that Aspire already created
//        options.ConnectionFactory = _ => Task.FromResult(multiplexer);

//        // Apply your specific configurations
//        options.Configuration.ChannelPrefix = RedisChannel.Literal("NBA");
//        options.Configuration.AbortOnConnectFail = false;
//        options.Configuration.ConnectRetry = 3;
//        options.Configuration.ConfigurationChannel = "nba-fantasy-channel";
//        options.Configuration.ClientName = "redis-nba-fantasy";
//        options.Configuration.SyncTimeout = 1000;
//    });


builder.AddNpgsqlDbContext<NbaFantasyContext>("nbafantasydb");

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
        policy.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod();
    });
});

#region Services
builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<PlayerService>();
builder.Services.AddScoped<BoxScoreCalculationService>();
builder.Services.AddScoped<DraftService>();
builder.Services.AddScoped<TradeService>();
builder.Services.AddScoped<FreeAgencyService>();

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

app.MapHub<AuctionHub>("/auctionHub");

//app.UseExceptionHandler();



//if (app.Environment.IsDevelopment())
//{
   
//}

// Expose the JSON endpoint
app.MapOpenApi();

// Map the Scalar UI (This replaces builder.Services.AddScalar)
app.MapScalarApiReference();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}


app.UseHttpsRedirection();
app.UseRouting();


app.UseCors();
app.UseAuthorization();

app.MapStaticAssets();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}")
//    .WithStaticAssets();

app.UseHangfireDashboard();

var v1 = app.MapGroup("/v1");

v1.TestEndpoints();


app.Run();

