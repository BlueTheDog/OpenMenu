using Application.Location;
using Application.MenuType;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Application.Services;
using Domain.Location.Dto;
using Domain.Location;
using Domain.ResourceParameters;

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

        services.AddTransient<IHateoasHelper, HateoasHelper>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<ILocationService ,LocationService>();
        //services.AddScoped<IMenuTypeService, MenuTypeService>();
        return services;
    }
}
