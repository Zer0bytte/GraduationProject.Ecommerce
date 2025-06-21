using BuildingBlocks.MediaSecurity;
using Ecommerce.Application.Common.Configs;
using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Infrastructure.Common.Services;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure;
public static class DependancyInjection
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {


        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Database"));
        });
        services.AddScoped<MediaValidator>();

        services.AddScoped<IJwtService, JwtService>();

        DirectoryConfiguration directoryCofig = new();
        configuration.GetSection("DirectoryConfiguration").Bind(directoryCofig);
        services.AddSingleton(directoryCofig);


        JwtConfiguration jwtConfig = new();
        configuration.GetSection("JWT").Bind(jwtConfig);
        services.AddSingleton(jwtConfig);


        ClickPayConfig clickPayConfig = new();
        configuration.GetSection("ClickPay").Bind(clickPayConfig);
        services.AddSingleton(clickPayConfig);


        HostingConfig hostingConfig = new();
        configuration.GetSection("HostingData").Bind(hostingConfig);
        services.AddSingleton(hostingConfig);


        SMTPConfig sMTPConfig = new();
        configuration.GetSection("SMTPConfig").Bind(sMTPConfig);
        services.AddSingleton(sMTPConfig);

        ChatGptConfig chatGptConfig = new();
        configuration.GetSection("ChatGptConfig").Bind(chatGptConfig);
        services.AddSingleton(chatGptConfig);


        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<IShippingCalculatorService, ShippingCalculatorService>();
        return services;
    }
}
