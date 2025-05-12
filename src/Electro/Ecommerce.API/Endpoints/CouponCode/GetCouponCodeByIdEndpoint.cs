
using Ecommerce.Application.Features.CouponCodes.Queries.GetById;
using Ecommerce.Application.Features.CouponCodes.Queries.GetCouponById;
using Ecommerce.Application.Features.CouponCodes.Queries.GetCouponCodes;

namespace Ecommerce.API.Endpoints.CouponCode;

public class GetCouponCodeByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/coupons/{id}", async (Guid id, ISender sender) =>
        {
            GetCouponByIdResult result = await sender.Send(new GetCouponByIdQuery
            {
                Id = id
            });

            return Results.Ok(ApiResponse<GetCouponByIdResult>.Success(result));

        })
               .RequireAuthorization("Admin")
               .WithTags("Coupon Codes")
               .WithSummary("Get Coupon Codes By Id")
               .Produces<ApiResponse<GetCouponByIdResult>>();
    }
}
