
using Ecommerce.Application.Features.CouponCodes.Commands.AddCouponCode;
using Ecommerce.Application.Features.CouponCodes.Queries.GetCouponCodes;

namespace Ecommerce.API.Endpoints.CouponCode;

public class GetCouponCodesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/coupons", async ([AsParameters] GetCouponCodesQuery query, ISender sender) =>
        {
            PagedResult<GetCouponCodesResult> result = await sender.Send(query);

            return Results.Ok(ApiResponse<PagedResult<GetCouponCodesResult>>.Success(result));

        })
               .RequireAuthorization("Admin")
               .WithTags("Coupon Codes")
               .WithSummary("Get Coupon Codes")
               .Produces<ApiResponse<PagedResult<GetCouponCodesResult>>>();
    }
}
