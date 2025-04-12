namespace Ecommerce.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        Product? product = await context.Products.FindAsync(command.Id);
        if (product is null) throw new NotFoundException("Product", command.Id);

        bool categoryExist = await context.Categories.AnyAsync(cat => cat.Id == command.CategoryId);
        if (!categoryExist) throw new NotFoundException("Category", command.CategoryId);

        product.Title = command.Title;
        product.Tags = command.Tags;
        product.SKU = command.SKU;
        product.Stock = command.Stock;
        product.Price = command.Price;
        product.Brand = command.Brand;
        product.CategoryId = command.CategoryId;
        product.Description = command.Description;
        product.Discount = command.DiscountPercentage;
        product.ModifiedOn = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult
        {
            IsSuccess = true
        };
    }
}
