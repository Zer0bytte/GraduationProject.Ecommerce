namespace Ecommerce.Application.Features.Products.Commands.DeleteProductImage;

public class DeleteProductImageCommandHandler(IApplicationDbContext context, ICurrentUser currentUser) : IRequestHandler<DeleteProductImageCommand, DeleteProductImageResult>
{
    public async Task<DeleteProductImageResult> Handle(DeleteProductImageCommand command, CancellationToken cancellationToken)
    {
        ProductImage? productImage = await context.ProductImages.FirstOrDefaultAsync(img => img.Id == command.ImageId && img.Product.SupplierId == currentUser.SupplierId);
        if (productImage is null) throw new NotFoundException("ProductImage", command.ImageId);

        productImage.MarkAsDeleted();

        await context.SaveChangesAsync(cancellationToken);

        return new DeleteProductImageResult
        {
            IsSuccess = true
        };
    }
}
