using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Application.Services;
using Application.Location;
using Domain.Location.Dto;
using Domain.Location;
using Domain.ResourceParameters;
using Domain.ClientType.Dto;
using Domain.ClientType;
using Application.ClientType;
using Domain.MenuItemType;
using Domain.MenuItemType.Dto;
using Application.MenuItemType;
using Domain.Client;
using Domain.Client.Dto;
using Application.Client;
using Domain.MenuItem;
using Application.MenuItem;
using Domain.MenuItem.Dto;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddScoped<IUrlHelper>(x => {
            var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
            var factory = x.GetRequiredService<IUrlHelperFactory>();
            return factory.GetUrlHelper(actionContext);
        });
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddTransient<IHateoasHelper, HateoasHelper>();

        services.AddScoped<IEntityService<ClientEntity, ClientResourceParameters, ClientDto,
            ClientForCreationDto, ClientForUpdateDto>, ClientService>();
        services.AddScoped<IEntityService<ClientTypeEntity, ClientTypeResourceParameters, ClientTypeDto,
            ClientTypeForCreationDto, ClientTypeForUpdateDto>, ClientTypeService>();
        services.AddScoped<IEntityService<LocationEntity, LocationResourceParameters, LocationDto,
            LocationForCreationDto, LocationForUpdateDto>, LocationService>();
        services.AddScoped<IEntityService<MenuItemEntity, MenuItemResourceParameters, MenuItemDto,
            MenuItemForCreationDto, MenuItemForUpdateDto>, MenuItemService>();
        services.AddScoped<IEntityService<MenuItemTypeEntity, MenuItemTypeResourceParameters, MenuItemTypeDto,
            MenuItemTypeForCreationDto, MenuItemTypeForUpdateDto>, MenuItemTypeService>();
        return services;
    }
}
