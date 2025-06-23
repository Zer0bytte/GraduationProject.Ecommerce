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
        RuleForEach(x => x.WheelRewards).SetValidator(new WheelRewardsDtoValidator());

        RuleFor(x => x.WheelRewards)
            .NotNull().WithMessage("قائمة الجوائز مطلوبة.")
            .Must(list => list.Count == 8)
            .WithMessage("يجب إرسال 8 جوائز بالضبط.");

        RuleFor(x => x.WheelRewards)
            .Must(list => Math.Abs(list.Sum(r => r.Probability) - 1.0) < 0.0001)
            .WithMessage("مجموع احتمالات الجوائز يجب أن يساوي 1.0.");


    }
}
public class WheelRewardsDtoValidator : AbstractValidator<WheelRewardsDto>
{
    public WheelRewardsDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("اسم المكافأة مطلوب.");

        RuleFor(x => x.Probability)
            .InclusiveBetween(0, 1).WithMessage("يجب أن تكون نسبة الاحتمالية بين 0 و 1.");

        When(x => x.IsExtraChance, () =>
        {
            RuleFor(x => x.ExtraRetries)
                .NotNull().WithMessage("عدد المحاولات الإضافية مطلوب عندما تكون المكافأة فرصة إضافية.")
                .GreaterThan(0).WithMessage("يجب أن يكون عدد المحاولات الإضافية أكبر من 0.");
        });

        When(x => !x.IsExtraChance, () =>
        {
            RuleFor(x => x.ExtraRetries)
                .Null().WithMessage("يجب أن يكون عدد المحاولات الإضافية فارغًا إذا لم تكن المكافأة فرصة إضافية.");
        });
    }
}