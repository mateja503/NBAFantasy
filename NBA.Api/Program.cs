using NBA.Api.Endpoints;
using NBA.Data.Context;
using Scalar.AspNetCore;
using System.Runtime.Intrinsics.X86;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddNpgsql<NbaFantasyContext>(connectionString);


builder.Services.AddAuthorization();

builder.Services.AddOpenApi();


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

app.UseAuthorization();

app.MapStaticAssets();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}")
//    .WithStaticAssets();

var v1 = app.MapGroup("/v1");

v1.MapLeagueEnpoints();




app.Run();
