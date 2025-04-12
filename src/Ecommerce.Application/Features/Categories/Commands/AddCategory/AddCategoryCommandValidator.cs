namespace Ecommerce.Application.Features.Categories.Commands.AddCategory;

public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
{

    public AddCategoryCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Category name is required");
    }
}
