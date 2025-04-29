namespace Ecommerce.Application.Features.Products.Commands.DeleteProductImage;

public class DeleteProductImageCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteProductImageCommand, DeleteProductImageResult>
{
    public async Task<DeleteProductImageResult> Handle(DeleteProductImageCommand command, CancellationToken cancellationToken)
    {
        ProductImage? productImage = await context.ProductImages.FindAsync(command.ImageId);
        if (productImage is null) throw new NotFoundException("ProductImage", command.ImageId);

        productImage.MarkAsDeleted();

        await context.SaveChangesAsync(cancellationToken);

        return new DeleteProductImageResult
        {
            IsSuccess = true
        };
    }
}
