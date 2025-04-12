namespace Ecommerce.Application.Features.Products.Commands.DeleteProductImage;

public record DeleteProductImageCommand : IRequest<DeleteProductImageResult>
{
    public Guid ImageId { get; set; }
}
