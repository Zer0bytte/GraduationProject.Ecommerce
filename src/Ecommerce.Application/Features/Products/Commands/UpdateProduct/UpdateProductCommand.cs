namespace Ecommerce.Application.Features.Products.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest<UpdateProductResult>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public decimal Price { get; set; }
    public int DiscountPercentage { get; set; }
    public int Stock { get; set; }
    public string SKU { get; set; } = default!;
    public string Tags { get; set; } = default!;
    public string Brand { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid CategoryId { get; set; }

}
