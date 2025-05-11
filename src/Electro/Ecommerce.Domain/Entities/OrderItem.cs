using Ecommerce.Domain.Shared;
using System.ComponentModel.DataAnnotations;

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
    public Guid? SupplierId { get; set; }
    public SupplierProfile? Supplier { get; set; }
    public OrderItemStatus Status { get; set; } = OrderItemStatus.Pending;

    public void Cancel()
    {
        Status = OrderItemStatus.Cancelled;
    }
}

public enum OrderItemStatus
{
    Pending,
    Confirmed,
    Shipped,
    Delivered,
    Cancelled
}