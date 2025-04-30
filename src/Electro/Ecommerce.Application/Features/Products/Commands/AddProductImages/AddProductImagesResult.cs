namespace Ecommerce.Application.Features.Products.Commands.AddProductImages;

public record AddProductImagesResult
{
    public Guid ImageId { get; set; }
    public string ImageUrl { get; set; } = default!;
}


