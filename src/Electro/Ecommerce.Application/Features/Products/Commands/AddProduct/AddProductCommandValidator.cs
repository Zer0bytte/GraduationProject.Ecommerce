using BuildingBlocks.MediaSecurity;

namespace Ecommerce.Application.Features.Products.Commands.AddProduct;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    private readonly MediaValidator mediaValidator;

    public AddProductCommandValidator(MediaValidator mediaValidator)
    {
        RuleFor(x => x.Images)
            .NotNull()
            .WithMessage("مجموعة الصور لا يمكن أن تكون فارغة")
            .NotEmpty()
            .WithMessage("مطلوب صورة واحدة على الأقل");

        RuleFor(x => x.Images)
             .Must(images => images.Count <= 5)
             .When(x => x.Images != null)
             .WithMessage("الحد الأقصى 5 صور مسموح به");

        RuleForEach(x => x.Images)
            .Must(BeValidImage)
            .When(x => x.Images != null)
            .WithMessage("تنسيق ملف الصورة غير صالح")
            .Must(file => file.Length > 0)
            .When(x => x.Images != null)
            .WithMessage("ملف الصورة لا يمكن أن يكون فارغاً")
            .Must(file => file.Length <= 5 * 1024 * 1024) // 5MB max size
            .When(x => x.Images != null)
            .WithMessage("يجب ألا يتجاوز حجم ملف الصورة 5 ميجابايت");

        RuleFor(p => p.Brand).NotEmpty().WithMessage("العلامة التجارية مطلوبة");
        RuleFor(p => p.CategoryId).NotNull().WithMessage("معرف الفئة مطلوب");
        RuleFor(p => p.Description).NotEmpty().WithMessage("الوصف مطلوب");
        RuleFor(p => p.Price).GreaterThan(0).WithMessage("السعر مطلوب");
        RuleFor(p => p.DiscountPercentage).GreaterThanOrEqualTo(0).WithMessage("يجب أن يكون الخصم أكثر من '0'");
        RuleFor(p => p.Stock).GreaterThan(0).WithMessage("المخزون مطلوب");
        RuleFor(p => p.SKU).NotEmpty().WithMessage("رمز SKU مطلوب");
        RuleFor(p => p.Title).NotEmpty().WithMessage("العنوان مطلوب");
        RuleFor(p => p.Tags)
         .Matches("^([\\u0600-\\u06FFa-zA-Z0-9]+,)*[\\u0600-\\u06FFa-zA-Z0-9]+$")
         .WithMessage("الرجاء إدخال الوسوم بهذا الشكل: tag1,tag2,tag3...");

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
