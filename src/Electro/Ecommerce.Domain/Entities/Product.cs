using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;

public class Product : BaseEntity
{
    public string Title { get; set; } = default!;
    public decimal Price { get; set; }
    public int Discount { get; set; }
    public int Stock { get; set; }
    public string SKU { get; set; } = default!;
    public string Tags { get; set; } = default!;
    public string Brand { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Category Category { get; set; } = default!;
    public Guid CategoryId { get; set; }
    public List<ProductImage> Images { get; set; } = [];
    public List<ProductReview> Reviews { get; set; } = [];
    public List<ProductOption> Options { get; set; } = [];
    public List<OrderItem> OrderItems { get; set; } = [];
    public SupplierProfile? Supplier { get; set; }
    public Guid? SupplierId { get; set; }



    public bool IsOutOfStock()
    {
        return Stock <= 0;
    }
}


