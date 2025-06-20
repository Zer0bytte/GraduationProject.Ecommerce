namespace Ecommerce.Application.Features.Products.Queries.GetCurrentSupplierProducts;

public class GetCurrentSupplierProductsResult
{
    public Guid Id { get; set; }
    public string Category { get; set; } = default!;
    public string Title { get; set; } = default!;
    public decimal Price { get; set; }
    public string[] Images { get; set; } = default!;
    public int BoughtCount { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedOn { get; set; }

}

