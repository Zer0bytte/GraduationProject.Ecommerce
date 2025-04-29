namespace Ecommerce.Application.Features.Orders.Queries.GetSupplierOrderItems;

public class GetSupplierOrderItemsResult
{
    public Guid OrderItemId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public OrderItemStatus Status { get; set; }
    public DateTime OrderDate { get; set; }
    public string BuyerEmail { get; set; } = default!;
    public string BuyerName { get; set; } = default!;
    public string ShippingAddress { get; set; } = default!;
} 