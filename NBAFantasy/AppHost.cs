var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis-cache")
                   //.WithDataBindMount("../infrastructure/redis-data") // Persists data locally
                   .WithRedisInsight(); 

var password = builder.AddParameter("password", "postgres");

var postgres = builder.AddPostgres("nbafantasy-server", password: password)
    .WithHostPort(6382)
    .WithBindMount("../infrastructure", "/docker-entrypoint-initdb.d")
    .WithBindMount("../infrastructure/db/create", "/scripts/create")
    .WithBindMount("../infrastructure/db/seed", "/scripts/seed");

var db = postgres.AddDatabase("nbafantasydb");

var backend = builder.AddProject<Projects.NBA_Api>("nba-api")
    .WithReference(db)
    .WithReference(redis)
    .WaitFor(db)
    .WaitFor(redis)
    .WithUrlForEndpoint("https", url =>
    {
        url.DisplayText = $"{url.Url}/scalar";
        url.Url = "/scalar/v1";
    });


builder.Build().Run();
