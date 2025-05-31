
using BuildingBlocks.Exceptions.Handler;
using Carter;
using Ecommerce.API.Data;
using Ecommerce.API.ExtensionMethods;
using Ecommerce.API.Hubs;
using Ecommerce.API.Seeds;
using Ecommerce.API.Transformers;
using Ecommerce.Application;
using Ecommerce.Application.BackgroundServices;
using Ecommerce.Application.Common.Configs;
using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.Common.Services;
using Ecommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;
using Serilog;
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting web application");
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSerilog((services, lc) => lc
        .ReadFrom.Configuration(builder.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console());

    builder.Services
           .AddApplicationServices()
           .AddInfrastructureServices(builder.Configuration);

    builder.Services.AddRateLimitingServices(builder.Configuration);
    builder.Services.AddCachingServices(builder.Configuration);
    builder.Services.AddHttpServices();
    builder.Services.AddIdentitiyServices();

    builder.Services.AddScoped<ICurrentUser, CurrentUser>();
    builder.Services.AddScoped<IJwtService, JwtService>();
    builder.Services.AddScoped<IEmailSender, EmailSender>();
    builder.Services.AddSingleton<IClickPayService, ClickPayService>();
    builder.Services.AddExceptionHandler<CustomExceptionHandler>();
    builder.Services.AddSignalR();


    builder.Services.AddOpenApi("v1", options =>
    {
        options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
    });
    builder.Services.AddCarter();

    string[] origins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
    builder.Services.AddCors(o => o.AddPolicy("web", options =>
    {
        options
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
        .WithOrigins(origins);
    }));

    builder.Services.AddHostedService<OrderPaymentTimoutCheckerService>();

    WebApplication app = builder.Build();
    app.UseCors("web");


    using (IServiceScope scope = app.Services.CreateScope())
    {
        RoleManager<IdentityRole<Guid>> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        UserManager<AppUser> usermanager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        //dbContext.Database.MigrateAsync().GetAwaiter().GetResult();
        await SeedRoles.Seed(roleManager);
        await SeedAdmins.Seed(usermanager);

        DirectoryConfiguration config = scope.ServiceProvider.GetRequiredService<DirectoryConfiguration>();
        if (!Directory.Exists(config.MediaDirectory))
            Directory.CreateDirectory(config.MediaDirectory);

        app.UseStaticFiles(new StaticFileOptions()
        {

            FileProvider = new PhysicalFileProvider(config.MediaDirectory),
            RequestPath = new PathString("/media")
        });


    }

    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.MapFallbackToFile("index.html");


    app.MapPost("/checkout-success", (HttpRequest request, HttpResponse response) =>
    {
        return Results.Redirect("https://electroo.vercel.app/checkout-success");
    });

    app.UseAuthentication();
    app.UseAuthorization();
    app.MapOpenApi();
    app.UseHttpsRedirection();
    app.UseExceptionHandler(options => { });
    app.MapScalarApiReference(options =>
    {
        options.Title = "Ecommerce";
        options.Theme = ScalarTheme.DeepSpace;
        options.WithPreferredScheme("Bearer")
        .WithHttpBearerAuthentication(bearer =>
        {
            bearer.Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI1NGVlZTQ5YS1jZjlhLTQ1MzUtZTE5Ni0wOGRkNzA0MDMyOTEiLCJGdWxsTmFtZSI6Ik1hcmtvIE1lZGhhdCIsImVtYWlsIjoiemVyb2J5dGU5OTU1QGdtYWlsLmNvbSIsIlVzZXJUeXBlIjoiVXNlciIsIm5iZiI6MTc0MzQxNzU2MSwiZXhwIjoxODA4MjE3NTYxLCJpYXQiOjE3NDM0MTc1NjF9.1H-l-rzFGUrj64IxlL78N5Zd0zhW10cAvShxQJbeQN8";
        });
        options.HideModels = true;

    });
    app.MapCarter();
    RateLimitConfiguration? rateLimitSettings = builder.Configuration.GetSection("RateLimit").Get<RateLimitConfiguration>();
    if (rateLimitSettings.Enabled)
        app.UseRateLimiter();
    app.MapHub<ChatHub>("/hubs/chat");
    app.MapHub<ChatBotHub>("/hubs/aibot");
    app.Run();

}
catch (Exception ex)
{

    Console.WriteLine(ex.Message);
}
