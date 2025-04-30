namespace Ecommerce.Application.Features.CouponCodes.ValidateCoupon;

public class ValidateCouponCommandValidator : AbstractValidator<ValidateCouponCommand>
{
    public ValidateCouponCommandValidator()
    {
        RuleFor(c => c.Code).NotEmpty().WithMessage("رمز الكوبون مطلوب!");
        RuleFor(c => c.TotalPrice).GreaterThan(1).WithMessage("يجب أن يكون السعر الإجمالي أكثر من 1");
    }
}
