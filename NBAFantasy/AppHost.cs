var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis-cache")
                   //.WithDataBindMount("../infrastructure/redis-data") // Persists data locally
                   .WithHostPort(6379)
                   .WithRedisInsight(); 

// Secret parameters are resolved from configuration (user-secrets in dev, env vars in prod),
// never hardcoded. Run once: dotnet user-secrets set "Parameters:password" "<value>"
//                             dotnet user-secrets set "Parameters:balldontlie-apikey" "<value>"
var password = builder.AddParameter("postgress-password", secret: true);
var ballDontLieApiKey = builder.AddParameter("balldontlie-apikey", secret: true);

var postgres = builder.AddPostgres("nbafantasy-server", password: password)
    .WithHostPort(6382)
    .WithBindMount("../infrastructure", "/docker-entrypoint-initdb.d")
    .WithBindMount("../infrastructure/db/create", "/scripts/create")
    .WithBindMount("../infrastructure/db/seed", "/scripts/seed");

var db = postgres.AddDatabase("nbafantasydb");

var backend = builder.AddProject<Projects.NBA_Api>("nba-api")
    .WithReference(db)
    .WithReference(redis)
    // Injected as a configuration value; binds to ExternalClients:BallDontLie:ApiKey in the API.
    .WithEnvironment("ExternalClients__BallDontLie__ApiKey", ballDontLieApiKey)
    .WaitFor(db)
    .WaitFor(redis)
    .WithUrlForEndpoint("https", url =>
    {
        url.DisplayText = $"{url.Url}/scalar";
        url.Url = "/scalar/v1";
    });


builder.Build().Run();
