
using Ecommerce.Application.Features.CouponCodes.Commands.DeactivateCouponCode;
using Ecommerce.Application.Features.CouponCodes.Commands.ReactivateCouponCode;

namespace Ecommerce.API.Endpoints.CouponCode;

public class ReactiveCouponCodeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/coupons/{id}/reactivate", async (Guid id, ISender sender) =>
        {
            ReactivateCouponCodeResult result = await sender.Send(new ReactivateCouponCodeCommand
            {
                Id = id,
            });

            return Results.Ok(ApiResponse<ReactivateCouponCodeResult>.Success(result, ArabicResponseMessages.CouponCodes.Reactivated));

        })
          .RequireAuthorization("Admin")
          .WithTags("Coupon Codes")
          .WithSummary("Deactivate Coupon Code")
          .Produces<ApiResponse<ReactivateCouponCodeResult>>();
    }
}



