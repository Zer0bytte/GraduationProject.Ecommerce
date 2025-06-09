using Ecommerce.Application.Common.Interfaces;
using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;

namespace Ecommerce.Application.Features.Products.Commands.AddProduct;

public class AddProductCommandHandler(IApplicationDbContext context, DirectoryConfiguration directoryConfiguration, ICurrentUser currentUser)
    : IRequestHandler<AddProductCommand, AddProductResult>
{
    public async Task<AddProductResult> Handle(AddProductCommand command, CancellationToken cancellationToken)
    {
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
            Tags = string.Join(",", command.Tags),
            SupplierId = currentUser.SupplierId,
            IsAuction = command.IsAuction,
            MinumumBidPrice = command.MinimumBidPrice,
            AuctionExpirationDate = command.AuctionExpirationDate
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
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync(cancellationToken);
        return new AddProductResult() { IsSuccess = true };

    }
}
