
using Ecommerce.Application.Features.CouponCodes.Commands.DeactivateCouponCode;

namespace Ecommerce.API.Endpoints.CouponCode;

public class DeactiveCouponCodeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/coupons/{id}/deactivate", async (Guid id, ISender sender) =>
        {
            DeactivateCouponCodeResult result = await sender.Send(new DeactivateCouponCodeCommand
            {
                Id = id,
            });

            return Results.Ok(ApiResponse<DeactivateCouponCodeResult>.Success(result, ArabicResponseMessages.CouponCodes.Deactivated));

        })
          .RequireAuthorization("Admin")
          .WithTags("Coupon Codes")
          .WithSummary("Deactivate Coupon Code")
          .Produces<ApiResponse<DeactivateCouponCodeResult>>();
    }
}



