
using Ecommerce.Application.Features.CouponCodes.Commands.UpdateCouponCode;
using Ecommerce.Application.Features.CouponCodes.Queries.GetCouponCodes;

namespace Ecommerce.API.Endpoints.CouponCode;

public record UpdateCouponCodeRequest
{
    public string Code { get; set; } = default!;
    public string? Description { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal MaximumDiscountValue { get; set; }
    public DateTime ExpirationDate { get; set; }
}
public class UpdateCouponCodeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/coupons/{id}", async (UpdateCouponCodeRequest request, Guid id, ISender sender) =>
       {
           UpdateCouponCodeResult result = await sender.Send(new UpdateCouponCodeCommand

           {
               Id = id,
               Code = request.Code,
               Description = request.Description,
               DiscountPercentage = request.DiscountPercentage,
               ExpirationDate = request.ExpirationDate,
               MaximumDiscountValue = request.MaximumDiscountValue,
           });

           return Results.Ok(ApiResponse<UpdateCouponCodeResult>.Success(result));

       })
               .RequireAuthorization("Admin")
               .WithTags("Coupon Codes")
               .WithSummary("Update Coupon Code")
               .Produces<ApiResponse<UpdateCouponCodeResult>>();
    }
}
