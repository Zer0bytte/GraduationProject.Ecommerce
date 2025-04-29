namespace Ecommerce.Application.Features.Products.Commands.DeleteProduct;

public record DeleteProductCommand : IRequest<DeleteProductResult>
{
    public Guid ProductId { get; set; }
}
