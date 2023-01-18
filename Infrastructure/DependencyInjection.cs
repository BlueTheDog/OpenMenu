using Application.Services;
using Domain.Client;
using Domain.ClientType;
using Domain.Location;
using Domain.MenuItem;
using Domain.MenuItemType;
using Domain.ResourceParameters;
using Infrastructure.DbContexts;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // DbContext
        services.AddScoped<IOpenMenuContext, OpenMenuContext>();
        // Repositories
        services.AddScoped<IEntityRepository<ClientEntity, ClientResourceParameters>, ClientRepository>();
        services.AddScoped<IEntityRepository<ClientTypeEntity, ClientTypeResourceParameters>, ClientTypeRepository>();
        services.AddScoped<IEntityRepository<LocationEntity, LocationResourceParameters>, LocationRepository>();
        services.AddScoped<IEntityRepository<MenuItemEntity, MenuItemResourceParameters>, MenuItemRepository>();
        services.AddScoped<IEntityRepository<MenuItemTypeEntity, MenuItemTypeResourceParameters>, MenuItemTypeRepository>();
        // Custom property mapping service
        services.AddTransient<IPropertyMappingService, PropertyMappingService>();
        // Property checker service
        services.AddTransient<IPropertyCheckerService, PropertyCheckerService>();
        return services;
    }
}
