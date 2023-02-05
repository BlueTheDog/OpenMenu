using Serilog;

namespace IDP;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // uncomment if you want to add a UI
        builder.Services.AddRazorPages();

        builder.Services.AddIdentityServer(options =>
            {
                // https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryApiResources(Config.GetApiResources())
            .AddInMemoryClients(Config.Clients)
            .AddTestUsers(TestUsers.Users)
            .AddDeveloperSigningCredential(); //not something we want to use in a production environment;
        /*
         * Here, we register in-memory identity resources, test users and clients. 
         * Additionally, we use the AddDeveloperSigningCredential method to set temporary signing credentials. 
         * It’s quite enough for the development environment. For the production environment, you should use 
         * the AddSigningCredentials method and provide a valid certificate.
         */
        return builder.Build();
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    { 
        app.UseSerilogRequestLogging();
    
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // uncomment if you want to add a UI
        app.UseStaticFiles();
        app.UseRouting();

        app.UseIdentityServer();

        // uncomment if you want to add a UI
        app.UseAuthorization();
        app.MapRazorPages().RequireAuthorization();

        return app;
    }
}
