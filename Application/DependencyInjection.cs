using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Domain.ResourceParameters;
using Application.Helpers;
using Application.Services.Entity;
using Application.Services.Location;
using Application.Services.MenuItemType;
using Application.Services.MenuItem;
using Application.Services.ClientType;
using Application.Services.Client;
using Domain.Entities.MenuItemType.Dto;
using Domain.Entities.MenuItemType;
using Domain.Entities.MenuItem.Dto;
using Domain.Entities.MenuItem;
using Domain.Entities.Location.Dto;
using Domain.Entities.Location;
using Domain.Entities.ClientType.Dto;
using Domain.Entities.ClientType;
using Domain.Entities.Client.Dto;
using Domain.Entities.Client;

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
