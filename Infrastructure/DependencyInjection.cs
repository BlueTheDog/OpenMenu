using Application.Location;
using Application.MenuType;
using Application.Services;
using Infrastructure.DbContexts;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // DbContext
        services.AddScoped<IOpenMenuContext, OpenMenuContext>();
        // Repositories
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IMenuTypeRepository,MenuTypeRepository>();
        // Custom property mapping service
        services.AddTransient<IPropertyMappingService, PropertyMappingService>();
        // Property checker service
        services.AddTransient<IPropertyCheckerService, PropertyCheckerService>();
        return services;
    }
}
