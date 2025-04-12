namespace Ecommerce.Application.Features.CouponCodes.ValidateCoupon;

public class ValidateCouponCommand : IRequest<ValidateCouponResult>
{
    public decimal TotalPrice { get; set; }
    public string Code { get; set; } = default!;
}
