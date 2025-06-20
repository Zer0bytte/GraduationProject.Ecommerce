namespace Ecommerce.Application.Features.Products.Queries.GetMostSoldProducts;

public record GetMostSoldProductsResult
{

    public Guid Id { get; set; }
    public Guid? SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public string Category { get; set; } = default!;
    public string Title { get; set; } = default!;
    public decimal Price { get; set; }
    public int DiscountPercentage { get; set; }
    public decimal DiscountedPrice { get; set; }
    public string Description { get; set; } = default!;
    public string[] Images { get; set; } = default!;
    public DateTime CreatedOn { get; set; }


}