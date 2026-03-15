using ApplicationDefaults.Exceptions;
using ApplicationDefaults.Options;
using ExternalClients;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.SignalR;
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
using NBA.Service.Player;
using Polly;
using Scalar.AspNetCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);


#region Options
builder.Services.Configure<BallDontLieClientOptions>(builder.Configuration.GetSection("ExternalClients:BallDontLie"));
builder.Services.Configure<DraftOptions>(builder.Configuration.GetSection("ApplicationSettings:Draft"));
builder.Services.Configure<ApplicationOptions>(builder.Configuration.GetSection("ApplicationSettings"));
#endregion

builder.AddRedisClient("redis-cache");
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


//app.UseExceptionHandler();



if (app.Environment.IsDevelopment())
{
    // Expose the JSON endpoint
    app.MapOpenApi();

    // Map the Scalar UI (This replaces builder.Services.AddScalar)
    app.MapScalarApiReference();
}

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
