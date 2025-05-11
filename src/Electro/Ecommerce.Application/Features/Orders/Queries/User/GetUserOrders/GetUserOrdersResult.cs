namespace Ecommerce.Application.Features.Orders.Queries.User.GetUserOrders;

public class GetUserOrdersResult
{
    public Guid OrderId { get; set; }
    public decimal SubTotal { get; set; }
    public decimal ShippingPrice { get; set; }
    public DateTime OrderDate { get; set; }
    //public OrderStatus Status { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public ICollection<OrderItemResult> OrderItems { get; set; }
    public string ShipTo { get; set; } = default!;

}


public record OrderItemResult
{
    public Guid OrderItemId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public OrderItemStatus Status { get; set; }
}