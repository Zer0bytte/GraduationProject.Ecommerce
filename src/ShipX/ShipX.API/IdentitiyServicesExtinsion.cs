﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ShipX.Domain.Entities;
using ShipX.Infrastructure.Persistence;
using System.Text;

namespace Ecommerce.API.ExtensionMethods;
public static class IdentitiyServicesExtinsion
{
    public static IServiceCollection AddIdentitiyServices(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireUppercase = true;
            options.Password.RequireDigit = true;
            options.SignIn.RequireConfirmedEmail = true;
            options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            byte[] key = Encoding.UTF8.GetBytes("282ce8d74008a99d25fd361eb1f0034a0b611852a254faed849e14bf926559e2");
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "https://shipx.zerobytetools.com",
                ValidAudience = "https://shipx.zerobytetools.com",
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    Microsoft.Extensions.Primitives.StringValues accessToken = context.Request.Query["access_token"];
                    PathString path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) &&
                        path.StartsWithSegments("/hubs"))
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };

        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("User", policy =>
            {
                policy.RequireClaim("UserType", "User");
            });
            options.AddPolicy("Admin", policy =>
            {
                policy.RequireClaim("UserType", "Admin");
            });
            options.AddPolicy("Supplier", policy =>
            {
                policy.RequireClaim("UserType", "Supplier");
            });
        });
        return services;
    }

}