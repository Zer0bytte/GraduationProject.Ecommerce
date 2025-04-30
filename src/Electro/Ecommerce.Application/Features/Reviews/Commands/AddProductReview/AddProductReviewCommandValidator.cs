namespace Ecommerce.Application.Features.Reviews.Commands.AddProductReview;

public class AddProductReviewCommandValidator : AbstractValidator<AddProductReviewCommand>
{
    public AddProductReviewCommandValidator()
    {
        RuleFor(rev => rev.ReviewText).NotEmpty().WithMessage("نص المراجعة مطلوب");
        RuleFor(rev => rev.Stars).InclusiveBetween(1, 5).WithMessage("يجب أن تكون النجوم بين 1-5");
        RuleFor(x => x.Image)
            .Must(BeValidImage)
            .When(x => x.Image != null).WithMessage("صورة غير صالحة");

    }

    private bool BeValidImage(IFormFile file)
    {
        if (file == null) return false;

        string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return allowedExtensions.Contains(extension);
    }
}
