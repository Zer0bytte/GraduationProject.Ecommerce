using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ecommerce.API.ExtensionMethods;
public static class HttpExtensions
{
    public static IServiceCollection AddHttpServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());

        });

        return services;
    }
}
