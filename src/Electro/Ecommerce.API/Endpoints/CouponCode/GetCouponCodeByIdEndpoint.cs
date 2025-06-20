using Ecommerce.Application.Features.CouponCodes.Queries.GetCouponById;

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
               .WithSummary("Get Coupon Code By Id")
               .Produces<ApiResponse<GetCouponByIdResult>>();
    }
}
