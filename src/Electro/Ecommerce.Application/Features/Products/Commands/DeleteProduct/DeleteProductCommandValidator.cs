namespace Ecommerce.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{

    public DeleteProductCommandValidator()
    {
        RuleFor(p => p.ProductId).NotNull().WithMessage("معرف المنتج مطلوب");
    }
}
