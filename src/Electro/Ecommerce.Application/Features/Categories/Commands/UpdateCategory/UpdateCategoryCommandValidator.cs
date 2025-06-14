﻿namespace Ecommerce.Application.Features.Categories.Commands.UpdateCategory;
public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("اسم الفئة مطلوب");
    }
}
