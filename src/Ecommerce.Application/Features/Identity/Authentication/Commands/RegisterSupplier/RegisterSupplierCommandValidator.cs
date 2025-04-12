namespace Ecommerce.Application.Features.Identity.Authentication.Commands.RegisterSupplier;

public class RegisterSupplierCommandValidator : AbstractValidator<RegisterSupplierCommand>
{

    public RegisterSupplierCommandValidator()
    {
        RuleFor(x => x.FullName)
                    .NotEmpty().WithMessage("Full name is required.")
                    .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Invalid phone number format.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        RuleFor(x => x.BusinessName)
            .NotEmpty().WithMessage("Business name is required.");

        RuleFor(x => x.StoreName)
            .NotEmpty().WithMessage("Store name is required.");

        RuleFor(x => x.TaxNumber)
            .NotEmpty().WithMessage("Tax number is required.")
            .Matches(@"^\d{10,15}$").WithMessage("Tax number must be numeric and 10-15 digits long.");

        RuleFor(x => x.NationalId)
            .NotEmpty().WithMessage("National ID is required.")
            .Matches(@"^\d{14}$").WithMessage("National ID must be exactly 14 digits.");

        RuleFor(x => x.NationalIdFront)
            .NotNull().WithMessage("National ID front image is required.")
            .Must(BeAValidImage).WithMessage("National ID front must be a valid image.");

        RuleFor(x => x.NationalIdBack)
            .NotNull().WithMessage("National ID back image is required.")
            .Must(BeAValidImage).WithMessage("National ID back must be a valid image.");

        RuleFor(x => x.TaxCard)
            .NotNull().WithMessage("Tax card image is required.")
            .Must(BeAValidImage).WithMessage("Tax card must be a valid image.");
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
