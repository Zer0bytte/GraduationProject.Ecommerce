namespace Ecommerce.Application.Features.Authentication.Commands.RegisterSupplier;

public class RegisterSupplierCommandValidator : AbstractValidator<RegisterSupplierCommand>
{

    public RegisterSupplierCommandValidator()
    {
        RuleFor(x => x.FullName)
                    .NotEmpty().WithMessage("الاسم الكامل مطلوب")
                    .MaximumLength(100).WithMessage("يجب ألا يتجاوز الاسم الكامل 100 حرف");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("رقم الهاتف مطلوب")
            .Matches(@"^\+?[0-9]{10,15}$").WithMessage("تنسيق رقم الهاتف غير صالح");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("البريد الإلكتروني مطلوب")
            .EmailAddress().WithMessage("بريد إلكتروني غير صالح");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("كلمة المرور مطلوبة")
            .MinimumLength(6).WithMessage("يجب أن تكون كلمة المرور 6 أحرف على الأقل");

        RuleFor(x => x.BusinessName)
            .NotEmpty().WithMessage("اسم العمل مطلوب");

        RuleFor(x => x.StoreName)
            .NotEmpty().WithMessage("اسم المتجر مطلوب");

        RuleFor(x => x.TaxNumber)
            .NotEmpty().WithMessage("الرقم الضريبي مطلوب")
            .Matches(@"^\d{10,15}$").WithMessage("يجب أن يكون الرقم الضريبي رقماً ويتراوح بين 10 و 15 رقماً");

        RuleFor(x => x.NationalId)
            .NotEmpty().WithMessage("الرقم القومي مطلوب")
            .Matches(@"^\d{14}$").WithMessage("يجب أن يكون الرقم القومي 14 رقماً بالضبط");

        RuleFor(x => x.NationalIdFront)
            .NotNull().WithMessage("صورة الرقم القومي الأمامية مطلوبة")
            .Must(BeAValidImage).WithMessage("يجب أن تكون صورة الرقم القومي الأمامية صورة صالحة");

        RuleFor(x => x.NationalIdBack)
            .NotNull().WithMessage("صورة الرقم القومي الخلفية مطلوبة")
            .Must(BeAValidImage).WithMessage("يجب أن تكون صورة الرقم القومي الخلفية صورة صالحة");

        RuleFor(x => x.TaxCard)
            .NotNull().WithMessage("صورة البطاقة الضريبية مطلوبة")
            .Must(BeAValidImage).WithMessage("يجب أن تكون صورة البطاقة الضريبية صورة صالحة");
    }


    private bool BeAValidImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return false;

        string[] permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
        string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return permittedExtensions.Contains(extension);
    }
}
