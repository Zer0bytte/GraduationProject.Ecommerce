namespace Ecommerce.Application.Features.Orders.Queries.GetUserOrders;

public class GetUserOrdersResult
{
    public Guid OrderId { get; set; }
    public decimal SubTotal { get; set; }
    public decimal ShippingPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}
