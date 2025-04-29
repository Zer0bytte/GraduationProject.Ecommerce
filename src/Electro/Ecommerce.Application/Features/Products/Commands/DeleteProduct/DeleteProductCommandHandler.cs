namespace Ecommerce.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        Product? product = await context.Products.FindAsync(command.ProductId);
        if (product is null)
            throw new NotFoundException("Product", command.ProductId);

        product.MarkAsDeleted();
        await context.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult
        {
            IsSuccess = true
        };
    }
}
