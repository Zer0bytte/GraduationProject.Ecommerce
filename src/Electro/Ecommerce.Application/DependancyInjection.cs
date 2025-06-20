using Ecommerce.Application.Behaviours;
using Ecommerce.Application.Caching;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ecommerce.Application;
public static class DependancyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMassTransit(busConfig =>
        {
            busConfig.SetKebabCaseEndpointNameFormatter();
            busConfig.AddConsumers(Assembly.GetExecutingAssembly());
            busConfig.UsingInMemory((context, config) => config.ConfigureEndpoints(context));
        });

        services.AddScoped<DeleteCategoriesCache>();
        return services;
    }
}