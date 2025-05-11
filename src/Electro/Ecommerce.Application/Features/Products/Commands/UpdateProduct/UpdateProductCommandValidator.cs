namespace Ecommerce.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
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
    }
}
