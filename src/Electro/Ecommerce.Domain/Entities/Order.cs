using Ecommerce.Domain.Enums;
using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;

public class Order : BaseEntity
{
    public string BuyerEmail { get; set; } = default!;
    public decimal SubTotal { get; set; }
    public decimal ShippingPrice { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.None;
    public Address Address { get; set; } = default!;
    public Guid AddressId { get; set; } = default!;
    public ICollection<OrderItem> OrderItems { get; set; } = [];
    public PaymentMethod PaymentMethod { get; set; }

    public Guid UserId { get; set; }
    public AppUser User { get; set; }


    public void UpdateOrderStatusBasedOnItems()
    {
        if (OrderItems == null || !OrderItems.Any())
            return;

        // Get only non-cancelled items
        var activeItems = OrderItems
            .Where(i => i.Status != OrderItemStatus.Cancelled)
            .ToList();

        if (!activeItems.Any())
        {
            // All items are cancelled
            Status = OrderStatus.Cancelled;
            return;
        }

        // Get the most common status among active items
        var mostCommonStatus = activeItems
            .GroupBy(i => i.Status)
            .OrderByDescending(g => g.Count())
            .First().Key;

        // Map OrderItemStatus to OrderStatus
        Status = mostCommonStatus switch
        {
            OrderItemStatus.Pending => OrderStatus.Pending,
            OrderItemStatus.Confirmed => OrderStatus.Confirmed,
            OrderItemStatus.Shipped => OrderStatus.Shipped,
            OrderItemStatus.Delivered => OrderStatus.Delivered,
            _ => OrderStatus.Pending
        };

        Console.WriteLine(Status);
    }
}
public enum PaymentStatus
{
    None,
    Pending,
    Paid,
    Failed
}