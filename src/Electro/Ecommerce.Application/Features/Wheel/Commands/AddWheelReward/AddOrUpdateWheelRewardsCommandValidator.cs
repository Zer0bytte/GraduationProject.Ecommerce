using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Wheel.Commands.AddWheelReward;
public class AddOrUpdateWheelRewardsCommandValidator : AbstractValidator<AddOrUpdateWheelRewardsCommand>
{
    public AddOrUpdateWheelRewardsCommandValidator()
    {
        RuleFor(x => x.WheelRewards)
            .NotNull().WithMessage("قائمة الجوائز مطلوبة.")
            .Must(list => list.Count == 8)
            .WithMessage("يجب إرسال 8 جوائز بالضبط.");

        RuleFor(x => x.WheelRewards)
            .Must(list => Math.Abs(list.Sum(r => r.Probability) - 1.0) < 0.0001)
            .WithMessage("مجموع احتمالات الجوائز يجب أن يساوي 1.0.");
    }
}
