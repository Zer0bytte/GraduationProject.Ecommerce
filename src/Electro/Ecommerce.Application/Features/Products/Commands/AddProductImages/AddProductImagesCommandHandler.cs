using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;

namespace Ecommerce.Application.Features.Products.Commands.AddProductImages;

public class AddProductImagesCommandHandler(IApplicationDbContext context, DirectoryConfiguration directoryConfiguration) : IRequestHandler<AddProductImagesCommand, AddProductImagesResult>
{
    public async Task<AddProductImagesResult> Handle(AddProductImagesCommand command, CancellationToken cancellationToken)
    {
        bool product = await context.Products.AnyAsync(prd => prd.Id == command.ProductId);
        if (!product) throw new NotFoundException("Product", command.ProductId);

        int productImagesCount = await context.ProductImages.CountAsync(prd => prd.ProductId == command.ProductId);

        if (productImagesCount >= 5 || 5 - productImagesCount < command.Images.Count)
            throw new InternalServerException("You cant upload more than 5 images per product!");

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
            context.ProductImages.Add(new ProductImage
            {
                NameOnServer = fileName,
                ProductId = command.ProductId
            });
        }

        await context.SaveChangesAsync(cancellationToken);

        return new AddProductImagesResult
        {
            IsSuccess = true
        };
    }
}
