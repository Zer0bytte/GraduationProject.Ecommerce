using Ecommerce.Application.Common.Configs;
using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using Microsoft.Extensions.Hosting;

namespace Ecommerce.Application.Features.Products.Commands.AddProduct;

public class AddProductCommandHandler(IApplicationDbContext context, DirectoryConfiguration directoryConfiguration, ICurrentUser ICurrentUser)
    : IRequestHandler<AddProductCommand, AddProductResult>
{
    public async Task<AddProductResult> Handle(AddProductCommand command, CancellationToken cancellationToken)
    {
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
            string fileName = Guid.NewGuid().ToString() + ".webp";
            string filePath = Path.Combine(directoryConfiguration.MediaDirectory, fileName);
            using (var webPFileStream = new FileStream(filePath, FileMode.Create))
            {
                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                {

                    imageFactory.Load(image.OpenReadStream())
                                .Format(new WebPFormat())
                                .Quality(75)
                                .Save(webPFileStream);
                }
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
