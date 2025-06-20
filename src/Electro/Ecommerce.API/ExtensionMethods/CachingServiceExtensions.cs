using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

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

        services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect("localhost"));


        return services;
    }
}
