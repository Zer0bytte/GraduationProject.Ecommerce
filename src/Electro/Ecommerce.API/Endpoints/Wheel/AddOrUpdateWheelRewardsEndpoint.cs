using Ecommerce.Application.Features.Wheel.Commands.AddWheelReward;

namespace Ecommerce.API.Endpoints.Wheel;

public class AddOrUpdateWheelRewardsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/wheel", async (AddOrUpdateWheelRewardsCommand request, ISender sender) =>
        {
            AddOrUpdateWheelRewardsResult result = await sender.Send(request);
            return Results.Ok(ApiResponse<AddOrUpdateWheelRewardsResult>.Success(result, "تم اضافة عناصر عجلة الحظ بنجاح"));
        })
            .RequireAuthorization("Admin")
            .WithTags("Wheel")
            .WithSummary("Add Wheel Rewards");

    }
}
