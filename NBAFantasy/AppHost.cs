var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.NBA_Api>("nba-api");

builder.Build().Run();
