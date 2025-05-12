using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;

namespace Ecommerce.Application.Features.Products.Commands.AddProductImages;

public class AddProductImagesCommandHandler(IApplicationDbContext context, DirectoryConfiguration directoryConfiguration, ICurrentUser currentUser, HostingConfig hostingConfig)
    : IRequestHandler<AddProductImagesCommand, List<AddProductImagesResult>>
{
    public async Task<List<AddProductImagesResult>> Handle(AddProductImagesCommand command, CancellationToken cancellationToken)
    {
        
        bool product = await context.Products.AnyAsync(prd => prd.Id == command.ProductId && prd.SupplierId == currentUser.SupplierId);
        if (!product) throw new NotFoundException("Product", command.ProductId);

        int productImagesCount = await context.ProductImages.CountAsync(prd => prd.ProductId == command.ProductId);

        if (productImagesCount >= 5 || 5 - productImagesCount < command.Images.Count)
            throw new InternalServerException("يمكنك اضافة 5 صور فقط بحد اقصي");

        List<AddProductImagesResult> results = new List<AddProductImagesResult>();

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

            ProductImage productImage = new ProductImage
            {
                Id = Guid.NewGuid(),
                NameOnServer = fileName,
                ProductId = command.ProductId
            };
            context.ProductImages.Add(productImage);

            results.Add(new AddProductImagesResult
            {
                ImageId = productImage.Id,
                ImageUrl = hostingConfig.HostName + "/media/" + productImage.NameOnServer
            });
        }
        await context.SaveChangesAsync(cancellationToken);
        return results;
    }
}
