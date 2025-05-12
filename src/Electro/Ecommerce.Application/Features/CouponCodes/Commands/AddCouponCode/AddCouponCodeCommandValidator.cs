namespace Ecommerce.Application.Features.CouponCodes.Commands.AddCouponCode;

public class AddCouponCodeCommandValidator : AbstractValidator<AddCouponCodeCommand>
{
    public AddCouponCodeCommandValidator()
    {
        RuleFor(c => c.Code).NotEmpty().WithMessage("رمز الكوبون مطلوب!");
        RuleFor(c => c.DiscountPercentage).GreaterThanOrEqualTo(1).WithMessage("يجب أن تكون نسبة الخصم 1% على الأقل");
        RuleFor(c => c.MaximumDiscountValue).GreaterThanOrEqualTo(1).WithMessage("يجب أن تكون قيمة الخصم القصوى 1 على الأقل");
        RuleFor(c => c.ExpirationDate).GreaterThan(DateTime.Now).WithMessage("يجب أن يكون تاريخ الانتهاء في المستقبل");

    }
}
