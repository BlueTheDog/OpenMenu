

using Web;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

// Delete and migrate database on startup so we start clean
#if DEBUG
await app.ResetDatabaseAsync();
#endif
app.Run();
