using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;

namespace Ecommerce.Application.Features.Categories.Commands.AddCategory;

public class AddCategoryCommandHandler(IApplicationDbContext context, DirectoryConfiguration directoryConfiguration) : IRequestHandler<AddCategoryCommand, AddCategoryResult>
{
    public async Task<AddCategoryResult> Handle(AddCategoryCommand command, CancellationToken cancellationToken)
    {
        bool categoryExist = context.Categories.Any(x => x.Name == command.Name);
        if (categoryExist) throw new DuplicateNamesException(command.Name);
        string fileName = Guid.NewGuid().ToString() + ".webp";
        string filePath = Path.Combine(directoryConfiguration.MediaDirectory, fileName);

        using (var webPFileStream = new FileStream(filePath, FileMode.Create))
        {
            using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
            {

                imageFactory.Load(command.Image.OpenReadStream())
                            .Format(new WebPFormat())
                            .Quality(75)
                            .Save(webPFileStream);
            }
        }

        Category category = new Category
        {
            Name = command.Name,
            ImageNameOnServer = fileName
        };

        context.Categories.Add(category);

        await context.SaveChangesAsync(cancellationToken);

        return new AddCategoryResult
        {
            IsSuccess = true
        };
    }
}
