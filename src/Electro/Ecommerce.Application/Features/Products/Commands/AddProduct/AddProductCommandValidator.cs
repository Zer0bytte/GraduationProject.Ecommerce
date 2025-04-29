using BuildingBlocks.MediaSecurity;

namespace Ecommerce.Application.Features.Products.Commands.AddProduct;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    private readonly MediaValidator mediaValidator;

    public AddProductCommandValidator(MediaValidator mediaValidator)
    {
        RuleFor(x => x.Images)
            .NotNull()
            .WithMessage("Images collection cannot be null")
            .NotEmpty()
            .WithMessage("At least one image is required");

        RuleFor(x => x.Images)
             .Must(images => images.Count <= 5)
             .When(x => x.Images != null)
             .WithMessage("Maximum 5 images allowed");

        RuleForEach(x => x.Images)
            .Must(BeValidImage)
            .When(x => x.Images != null)
            .WithMessage("Invalid image file format")
            .Must(file => file.Length > 0)
            .When(x => x.Images != null)
            .WithMessage("Image file cannot be empty")
            .Must(file => file.Length <= 5 * 1024 * 1024) // 5MB max size
            .When(x => x.Images != null)
            .WithMessage("Image file size must not exceed 5MB");

        RuleFor(p => p.Brand).NotEmpty().WithMessage("Brand is required");
        RuleFor(p => p.CategoryId).NotNull().WithMessage("CategoryId is required");
        RuleFor(p => p.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price is required");
        RuleFor(p => p.DiscountPercentage).GreaterThanOrEqualTo(0).WithMessage("Discount should be more than '0' ");
        RuleFor(p => p.Stock).GreaterThan(0).WithMessage("Stock is required");
        RuleFor(p => p.SKU).NotEmpty().WithMessage("SKU is required");
        RuleFor(p => p.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(p => p.Tags)
         .Matches("^([\\u0600-\\u06FFa-zA-Z0-9]+,)*[\\u0600-\\u06FFa-zA-Z0-9]+$")
         .WithMessage("Please enter tags in this format: tag1,tag2,tag3...");

        this.mediaValidator = mediaValidator;
    }

    private bool BeValidImage(IFormFile file)
    {
        return mediaValidator.IsMediaValid(file);
        //if (file == null) return false;

        //string[] allowedExtensions = [".jpg", ".jpeg", ".png", ".gif",".avif"];
        //string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        //return allowedExtensions.Contains(extension);
    }
}
