namespace Ecommerce.Application.Features.Products.Commands.AddProductImages;

public record AddProductImagesCommand : IRequest<List<AddProductImagesResult>>
{
    [System.Text.Json.Serialization.JsonIgnore]
    public Guid ProductId { get; set; }
    public List<IFormFile> Images { get; set; } = [];

}
