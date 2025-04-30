using Ecommerce.Application.Features.Products.Commands.AddProduct;

namespace Ecommerce.Application.Features.Products.Commands.AddProductImages;

public class AddProductImagesCommandValidator:AbstractValidator<AddProductImagesCommand>
{
    public AddProductImagesCommandValidator()
    {
        RuleFor(x => x.Images)
            .NotNull()
            .WithMessage("مجموعة الصور لا يمكن أن تكون فارغة")
            .NotEmpty()
            .WithMessage("مطلوب صورة واحدة على الأقل");

    }
}
