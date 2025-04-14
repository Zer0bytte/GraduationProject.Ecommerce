using Microsoft.Extensions.Hosting;

namespace Ecommerce.Application.Features.Products.Commands.AddProduct;

public class AddProductCommandHandler(IApplicationDbContext context, IHostEnvironment hostingEnvironment, ICurrentUser ICurrentUser)
    : IRequestHandler<AddProductCommand, AddProductResult>
{
    public async Task<AddProductResult> Handle(AddProductCommand command, CancellationToken cancellationToken)
    {
        string uploads = Path.Combine(hostingEnvironment.ContentRootPath, "uploads");
        Product product = new Product
        {
            Id = Guid.CreateVersion7(),
            Brand = command.Brand,
            Title = command.Title,
            CategoryId = command.CategoryId,
            Description = command.Description,
            Price = command.Price,
            Discount = command.DiscountPercentage,
            Stock = command.Stock,
            SKU = command.SKU,
            Tags = command.Tags,
            SupplierId = ICurrentUser.SupplierId

        };
        foreach (IFormFile image in command.Images)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            string filePath = Path.Combine(uploads, fileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            product.Images.Add(new ProductImage
            {
                NameOnServer = fileName,
            });
        }
        if (command.ProductOptions is not null)
        {
            foreach (AddProductOption option in command.ProductOptions)
            {
                product.Options.Add(new ProductOption
                {
                    OptionGroupName = option.OptionGroupName,
                    OptionName = option.OptionName,
                    OptionPrice = option.OptionPrice,
                });
            }
        }

        await context.Products.AddAsync(product);
        await context.SaveChangesAsync(cancellationToken);
        return new AddProductResult() { IsSuccess = true };

    }
}
