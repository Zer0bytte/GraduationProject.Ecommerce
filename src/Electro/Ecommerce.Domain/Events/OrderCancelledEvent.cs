namespace Ecommerce.Domain.Events;
public class OrderCancelledEvent
{
    public Guid OrderId { get; set; }
}