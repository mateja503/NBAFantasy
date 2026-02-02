var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.NBA_Api>("nba-api")
    .WithUrlForEndpoint("https", url =>
    {
        url.DisplayText = $"{url.Url}/scalar";
        url.Url = "/scalar/v1";
    });

builder.Build().Run();
