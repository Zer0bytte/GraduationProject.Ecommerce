using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.ExtensionMethods;

public static class CachingServiceExtensions
{
    public static IServiceCollection AddCachingServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "graduation.ecommerce";
        });

        return services;
    }
}
