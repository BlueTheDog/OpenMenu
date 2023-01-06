using Application;
using Common;
using Domain;
using Hellang.Middleware.ProblemDetails;
using Infrastructure;
using Infrastructure.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Presentation;
using Serilog;

namespace Web;

// Here we configure services and pipeline
public static class StartupHelper
{
    // Add services to the container
    public static WebApplication ConfigureServices(
        this WebApplicationBuilder builder)
    {
        var presentationAssembly = typeof(AssemblyReference).Assembly;
        // configure Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/OpenMenu.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        // use Serilog
        builder.Host.UseSerilog();

        builder.Services.AddControllers(options =>
        {
            //options.ReturnHttpNotAcceptable = true; /// add problem json to accepted headers and uncomment
            options.CacheProfiles.Add("120SecondsProfile",
                new() { Duration = 120 });
        })
            .AddApplicationPart(presentationAssembly)
            .AddNewtonsoftJson()
            .AddXmlDataContractSerializerFormatters() // content negociacion;
            .ConfigureApiBehaviorOptions(setupAction =>
            {
            //setupAction.InvalidModelStateResponseFactory = context =>
            //{
            //    // create a validation problem details object more details on
            //    // ASP.NET Core 6 Web API Deep Dive / Customazing validation error responses
            //    //
            //    // needs more understanding :)
            //    var problemDetailsFactory = context.HttpContext.RequestServices
            //        .GetRequiredService<ProblemDetailsFactory>();
            //    var validationProblemDetails = problemDetailsFactory.CreateValidationProblemDetails(
            //            context.HttpContext,
            //            context.ModelState);
            //    // add aditional info not added by default
            //    validationProblemDetails.Detail = Labels.seeTheErrorForDetails;
            //    validationProblemDetails.Instance =
            //        context.HttpContext.Request.Path;
            //    // report invalid model state response as validation issues
            //    //validationProblemDetails.Type =
            //    //    "https://urlThatSpecifiesTheProblem";
            //    validationProblemDetails.Status =
            //        StatusCodes.Status422UnprocessableEntity;
            //    validationProblemDetails.Title = Labels.oneOrMoreValidationErrors;

            //    return new UnprocessableEntityObjectResult(
            //        validationProblemDetails)
            //    {
            //        ContentTypes = { "application/problem+json" }
            //    };
            //};
            });
        // add formetters support for HATEOAS type
        builder.Services.Configure<MvcOptions>(config =>
        {
            var newtonsoftJsonOutputFormatter = config.OutputFormatters.
                OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();
            newtonsoftJsonOutputFormatter?.SupportedMediaTypes.Add("application/hateoas+json");
        });

        // Learn more about configuring Swagger/OpenAPI
        // at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        // Add ProblemDetails
        builder.Services.AddDbContext<OpenMenuContext>(
            dbContextOptions => dbContextOptions.UseSqlite(
                builder.Configuration["ConnectionStrings:OpenMenuDBConnectionString"],
                b => b.MigrationsAssembly("Web"))
            );

        builder.Services.AddResponseCaching();
        //builder.Services.AddHttpCacheHeaders();

        // From Class Projects        //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());// scan for profiles
        builder.Services.AddAplication();
        builder.Services.AddInfrastructure();
        builder.Services.AddCommon();
        builder.Services.AddPresentation();
        return builder.Build();
    }

    // Configure the request/response pipeline
    public static WebApplication ConfigurePipeline(
        this WebApplication app)
    {
        if (app.Environment.IsDevelopment())// Only in development
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }
        else // Do not throw errors to the consumer while in production,
        {
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync(Labels.unexpectedFault);
                });
            });
        }
        // ProblemDetails
        app.UseProblemDetails();

        app.UseHttpsRedirection();
        app.UseResponseCaching();
        //app.UseHttpCacheHeaders();
        app.UseAuthorization();

        app.MapControllers();
        return app;
    }
    public static async Task ResetDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        try
        {
            var context = scope.ServiceProvider.GetService<OpenMenuContext>();
            if (context != null)
            {
                var del = await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, Labels.errorMigratingDb);
        }
    }
}
