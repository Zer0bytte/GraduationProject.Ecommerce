using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;

public class TransactionRefernce : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public string TransactionRefId { get; set; } = default!;
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public DateTime? PaidOn { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
}
