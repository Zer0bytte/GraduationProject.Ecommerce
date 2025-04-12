namespace Ecommerce.Application.Features.Reviews.Commands.AddProductReview;

public class AddProductReviewCommandValidator : AbstractValidator<AddProductReviewCommand>
{
    public AddProductReviewCommandValidator()
    {
        RuleFor(rev => rev.ReviewText).NotEmpty().WithMessage("Review text is required");
        RuleFor(rev => rev.Stars).InclusiveBetween(1, 5).WithMessage("Stars must be between 1-5");
        RuleFor(x => x.Image)
            .Must(BeValidImage)
            .When(x => x.Image != null).WithMessage("Invalid image.");

    }

    private bool BeValidImage(IFormFile file)
    {
        if (file == null) return false;

        string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return allowedExtensions.Contains(extension);
    }
}
