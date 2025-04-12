using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }

}