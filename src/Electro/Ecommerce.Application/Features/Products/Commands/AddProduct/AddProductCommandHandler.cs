using Ecommerce.Application.Common.Interfaces;
using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;

namespace Ecommerce.Application.Features.Products.Commands.AddProduct;

public class AddProductCommandHandler(IApplicationDbContext context, DirectoryConfiguration directoryConfiguration, ICurrentUser currentUser)
    : IRequestHandler<AddProductCommand, AddProductResult>
{
    public async Task<AddProductResult> Handle(AddProductCommand command, CancellationToken cancellationToken)
    {
        if (!(currentUser.IsSupplier && currentUser.IsVerifiedSupplier))
        {
            throw new ApplicationException("من فضلك انتظر لحين تأكيد حسابك");
        }
        Product product = new Product
        {
            Id = Guid.Parse(NewId.Next().ToString()),
            Brand = command.Brand,
            Title = command.Title,
            CategoryId = command.CategoryId,
            Description = command.Description,
            Price = command.Price,
            Discount = command.DiscountPercentage,
            Stock = command.Stock,
            SKU = command.SKU,
            Tags = command.Tags,
            SupplierId = currentUser.SupplierId

        };
        foreach (IFormFile image in command.Images)
        {
            string fileName = Guid.NewGuid().ToString() + ".webp";
            string filePath = Path.Combine(directoryConfiguration.MediaDirectory, fileName);
            using (FileStream webPFileStream = new FileStream(filePath, FileMode.Create))
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
