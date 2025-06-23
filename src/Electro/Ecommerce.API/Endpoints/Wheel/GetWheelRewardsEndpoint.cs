using Ecommerce.Application.Features.Wheel.Commands.SpinWheel;
using Ecommerce.Application.Features.Wheel.Queries.GetWheelRewards;

namespace Ecommerce.API.Endpoints.Wheel;

public class GetWheelRewardsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/wheel/rewards", async (ISender sender) =>
        {
            GetWheelRewardsResult result = await sender.Send(new GetWheelRewardsQuery());
            return Results.Ok(ApiResponse<GetWheelRewardsResult>.Success(result));
        })
            .RequireAuthorization("User")
            .WithTags("Wheel")
            .WithSummary("Get Wheel Rewards");

    }

}
