using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.CouponCodes.Commands.UpdateCouponCode;
public class UpdateCouponCodeCommandValidator : AbstractValidator<UpdateCouponCodeCommand>
{
    public UpdateCouponCodeCommandValidator()
    {
        RuleFor(c => c.Code).NotEmpty().WithMessage("رمز الكوبون مطلوب!");
        RuleFor(c => c.DiscountPercentage).GreaterThanOrEqualTo(1).WithMessage("يجب أن تكون نسبة الخصم 1% على الأقل");
        RuleFor(c => c.MaximumDiscountValue).GreaterThanOrEqualTo(1).WithMessage("يجب أن تكون قيمة الخصم القصوى 1 على الأقل");
        RuleFor(c => c.ExpirationDate).GreaterThan(DateTime.Now).WithMessage("يجب أن يكون تاريخ الانتهاء في المستقبل");
    }
}
