namespace Ecommerce.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsResult
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public decimal Price { get; set; }
    public int DiscountPercentage { get; set; }
    public decimal DiscountedPrice { get; set; }
    public string Description { get; set; } = default!;
    public string[] Images { get; set; } = default!;
}
