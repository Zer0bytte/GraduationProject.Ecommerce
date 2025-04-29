namespace Ecommerce.Application.Features.CouponCodes;

public class AddCouponCodeCommandValidator : AbstractValidator<AddCouponCodeCommand>
{
    public AddCouponCodeCommandValidator()
    {
        RuleFor(c => c.Code).NotEmpty().WithMessage("Coupon code is required!");
        RuleFor(c => c.DiscountPercentage).GreaterThanOrEqualTo(1).WithMessage("Discount percentage should be 1% at least");
        RuleFor(c => c.MaximumDiscountValue).GreaterThanOrEqualTo(1).WithMessage("Maximum discount value should be 1 at least");
        RuleFor(c => c.ExpirationDate).GreaterThan(DateTime.Now).WithMessage("Expiration date must be in the future");

    }
}
