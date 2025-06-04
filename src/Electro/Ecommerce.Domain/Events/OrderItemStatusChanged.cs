using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Events;

public class OrderItemStatusChangedEvent
{
    public OrderItemStatus Status { get; set; }
    public string Email { get; set; } = default!;
    public string CustomerName { get; set; } = default!;
    public string ProductName { get; set; } = default!;

}