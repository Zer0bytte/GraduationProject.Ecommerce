namespace Ecommerce.Application.Features.CouponCodes.Commands.ValidateCoupon;

public class ValidateCouponCommand : IRequest<ValidateCouponResult>
{
    public decimal TotalPrice { get; set; }
    public string Code { get; set; } = default!;
}
