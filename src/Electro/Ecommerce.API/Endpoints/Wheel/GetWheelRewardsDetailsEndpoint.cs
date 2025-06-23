using Ecommerce.Application.Features.Wheel.Queries.GetWheelRewards;
using Ecommerce.Application.Features.Wheel.Queries.GetWheelRewardsDetails;

namespace Ecommerce.API.Endpoints.Wheel;

public class GetWheelRewardsDetailsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/wheel/details", async (ISender sender) =>
        {
            List<GetWheelRewardsDetailsResult> result = await sender.Send(new GetWheelRewardsDetailsQuery());
            return Results.Ok(ApiResponse<List<GetWheelRewardsDetailsResult>>.Success(result));
        })
            .RequireAuthorization("Admin")
            .WithTags("Wheel")
            .WithSummary("Get Wheel Rewards Details")
            .Produces<ApiResponse<List<GetWheelRewardsDetailsResult>>>();

    }
}
