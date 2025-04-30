namespace Ecommerce.Application.Features.Authentication.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("الاسم الكامل مطلوب")
            .MaximumLength(100).WithMessage("يجب ألا يتجاوز الاسم الكامل 100 حرف");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("رقم الهاتف مطلوب")
            .Matches(@"^(\+?\d{10,15})$").WithMessage("يجب أن يكون رقم الهاتف بين 10 و 15 رقماً ويمكن أن يبدأ بـ '+'");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("البريد الإلكتروني مطلوب")
            .EmailAddress().WithMessage("يجب إدخال بريد إلكتروني صالح");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("كلمة المرور مطلوبة")
            .MinimumLength(6).WithMessage("يجب أن تكون كلمة المرور 6 أحرف على الأقل")
            .Matches(@"[A-Z]").WithMessage("يجب أن تحتوي كلمة المرور على حرف كبير واحد على الأقل")
            .Matches(@"[a-z]").WithMessage("يجب أن تحتوي كلمة المرور على حرف صغير واحد على الأقل")
            .Matches(@"\d").WithMessage("يجب أن تحتوي كلمة المرور على رقم واحد على الأقل")
            .Matches(@"[^\w\d\s]").WithMessage("يجب أن تحتوي كلمة المرور على رمز خاص واحد على الأقل");
    }
}
