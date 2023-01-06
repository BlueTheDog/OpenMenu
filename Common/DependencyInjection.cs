using Microsoft.Extensions.DependencyInjection;

namespace Common;
public static class DependencyInjection
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        //services.AddScoped<IServiceName, Service>();

        return services;
    }
}
