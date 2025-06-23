using Ecommerce.Application.Features.Wheel.Commands.SpinWheel;

namespace Ecommerce.API.Endpoints.Wheel;

public class SpinWheelEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/wheel/spin", async (ISender sender) =>
        {
            SpinWheelResult result = await sender.Send(new SpinWheelCommand());
            return Results.Ok(ApiResponse<SpinWheelResult>.Success(result));
        })
            .RequireAuthorization("User")
            .WithTags("Wheel")
            .WithSummary("Spin Wheel");

    }
}
