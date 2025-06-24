using Ecommerce.Application.Features.CouponCodes.Commands.ValidateCoupon;

namespace Ecommerce.API.Endpoints.CouponCode;

public class ValidateCouponCodeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/coupons/validate", async (ValidateCouponCommand command, ISender sender) =>
        {
            ValidateCouponResult result = await sender.Send(command);

            return Results.Ok(ApiResponse<ValidateCouponResult>.Success(result, ArabicResponseMessages.CouponCodes.Valid));

        })
            .RequireRateLimiting("fixed")
            .WithTags("Coupon Codes")
            .WithSummary("Validate Coupon Code")
            .Produces<ValidateCouponResult>();
    }
}
