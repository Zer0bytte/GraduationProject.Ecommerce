namespace Ecommerce.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Brand).NotEmpty().WithMessage("Brand is required");
        RuleFor(p => p.CategoryId).NotNull().WithMessage("CategoryId is required");
        RuleFor(p => p.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price is required");
        RuleFor(p => p.DiscountPercentage).GreaterThanOrEqualTo(0).WithMessage("Discount should be more than '0' ");
        RuleFor(p => p.Stock).GreaterThan(0).WithMessage("Stock is required");
        RuleFor(p => p.SKU).NotEmpty().WithMessage("SKU is required");
        RuleFor(p => p.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(p => p.Tags).Matches("^([a-zA-Z]+,)*[a-zA-Z]+$").WithMessage("Please enter tags in this format tag1,tag2,tag3....");
    }
}
