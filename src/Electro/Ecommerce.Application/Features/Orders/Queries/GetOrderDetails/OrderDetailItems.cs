namespace Ecommerce.Application.Features.Orders.Queries.GetOrderDetails;

public record OrderDetailItems
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Guid? SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public OrderItemStatus Status { get; set; }
}