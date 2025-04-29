using Ecommerce.Application.Common.Configs;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace Ecommerce.API.ExtensionMethods;

public static class RateLimitingExtensions
{
    public static IServiceCollection AddRateLimitingServices(this IServiceCollection services, IConfiguration configuration)
    {
        RateLimitConfiguration? rateLimitSettings = configuration.GetSection("RateLimit").Get<RateLimitConfiguration>();
        if (rateLimitSettings?.Enabled == true)
        {
            services.AddRateLimiter(rateLimiterOptions =>
            {
                rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
                {
                    options.PermitLimit = rateLimitSettings.PermitLimit;
                    options.Window = TimeSpan.FromSeconds(rateLimitSettings.WindowInSeconds);
                    options.QueueLimit = rateLimitSettings.QueueLimit;
                    options.QueueProcessingOrder = Enum.TryParse(rateLimitSettings.QueueProcessingOrder, out QueueProcessingOrder order)
                        ? order
                        : QueueProcessingOrder.OldestFirst;
                });
            });
        }
        return services;
    }
}
