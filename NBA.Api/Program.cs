using ExternalClients;
using ExternalClients.Options;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using NBA.Api;
using NBA.Api.Endpoints;
using NBA.Data.Context;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

#region Options
builder.Services.Configure<BallDontLieClientOptions>(builder.Configuration.GetSection("ExternalClients:BallDontLie"));
#endregion

builder.AddNpgsqlDbContext<NbaFantasyContext>("nbafantasydb");

builder.Services.AddPostgreSQLHangFire(builder.Configuration);

builder.Services.AddHttpContextAccessor();

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


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    // Expose the JSON endpoint
    app.MapOpenApi();

    // Map the Scalar UI (This replaces builder.Services.AddScalar)
    app.MapScalarApiReference();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


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
