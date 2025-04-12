using Ecommerce.Domain.Enums;
using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;

public class Order : BaseEntity
{
    public string BuyerEmail { get; set; } = default!;
    public decimal SubTotal { get; set; }
    public string? PaymentIntentId { get; set; } = default!;
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.None;
    public Address Address { get; set; } = default!;
    public Guid AddressId { get; set; } = default!;
    public IReadOnlyList<OrderItem> OrderItems { get; set; } = [];
    public PaymentMethod PaymentMethod { get; set; }
    public DeliveryMethod DeliveryMethod { get; set; } = default!;
    public Guid DeliveryMethodId { get; set; } = default!;

    public Guid UserId { get; set; }
    public AppUser User { get; set; }
}
public enum PaymentStatus
{
    None,
    Pending,
    Paid,
    Failed
}