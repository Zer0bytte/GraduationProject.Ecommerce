namespace Ecommerce.Application.Features.Address.Commands.UpdateAddress;

public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("الاسم الأول مطلوب");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("الاسم الأخير مطلوب");
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("رقم الهاتف مطلوب");
        RuleFor(x => x.City).NotEmpty().WithMessage("المدينة مطلوبة");
        RuleFor(x => x.Street).NotEmpty().WithMessage("الشارع مطلوب");
    }
}
