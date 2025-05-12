namespace Ecommerce.Application.Features.CouponCodes.Commands.ValidateCoupon;

public record ValidateCouponResult
{
    public decimal NewDiscountedPrice { get; set; }
    public decimal DiscountValue { get; set; }
}
