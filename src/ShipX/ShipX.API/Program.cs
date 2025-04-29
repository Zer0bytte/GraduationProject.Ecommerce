using Carter;
using Ecommerce.API.ExtensionMethods;
using ShipX.Application;
using ShipX.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddIdentitiyServices();
builder.Services.AddCarter();

var app = builder.Build();


app.UseHttpsRedirection();

app.MapCarter();
app.UseAuthentication();
app.UseAuthorization();

app.Run();