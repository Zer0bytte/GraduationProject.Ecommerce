namespace Ecommerce.Application.Features.Orders.Queries.GetOrderDetails;

public record GetOrderDetailsResult
{
    public Guid OrderId { get; set; }
    public List<OrderDetailItems> OrderItems { get; set; } = [];
    public decimal SubTotal { get; set; }
    public decimal ShippingPrice { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.None;
    public OrderDetailsAddress ShippingAddress { get; set; } = default!;
    public PaymentMethod PaymentMethod { get; set; }
}
