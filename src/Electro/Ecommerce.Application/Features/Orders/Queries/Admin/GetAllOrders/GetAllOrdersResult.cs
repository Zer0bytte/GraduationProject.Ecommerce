namespace Ecommerce.Application.Features.Orders.Queries.Admin.GetAllOrders;

public class GetAllOrdersResult
{
    public Guid OrderId { get; set; }
    public string BuyerEmail { get; set; } = default!;
    public decimal SubTotal { get; set; }
    public decimal ShippingPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}
