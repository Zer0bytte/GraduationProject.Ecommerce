using Ecommerce.Application.Features.CouponCodes;

namespace Ecommerce.API.Endpoints.CouponCode;

public class AddCouponCodeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/coupons/", async (AddCouponCodeCommand command, ISender sender) =>
        {
            AddCouponCodeResult result = await sender.Send(command);

            return Results.Ok(ApiResponse<AddCouponCodeResult>.Success(result, ArabicResponseMessages.CouponCodes.Created));

        })
            .RequireAuthorization("Admin")
            .WithTags("Coupon Codes")
            .WithSummary("Add Coupon Code")
            .Produces<AddCouponCodeResult>();
    }
}
