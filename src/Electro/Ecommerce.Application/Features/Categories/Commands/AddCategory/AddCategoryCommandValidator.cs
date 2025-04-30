using BuildingBlocks.MediaSecurity;

namespace Ecommerce.Application.Features.Categories.Commands.AddCategory;

public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
{
    private readonly MediaValidator mediaValidator;

    public AddCategoryCommandValidator(MediaValidator mediaValidator)
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("اسم الفئة مطلوب");
        RuleFor(c => c.Image).Must(BeValidImage);
        this.mediaValidator = mediaValidator;
    }
    private bool BeValidImage(IFormFile file)
    {
        return mediaValidator.IsMediaValid(file);
    }
}
