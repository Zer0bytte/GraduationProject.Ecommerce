namespace Ecommerce.Application.Features.CouponCodes.ValidateCoupon;

public class ValidateCouponCommandValidator : AbstractValidator<ValidateCouponCommand>
{
    public ValidateCouponCommandValidator()
    {
        RuleFor(c => c.Code).NotEmpty().WithMessage("Coupon code is required!");
        RuleFor(c => c.TotalPrice).GreaterThan(1).WithMessage("Total price should be above 1");
    }
}
