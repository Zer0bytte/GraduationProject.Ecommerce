using Ecommerce.Domain.Enums;
using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;
public class SupplierNotification : BaseEntity
{
    public NotificationType NotificationType { get; set; }
    public string NotificationPayload { get; set; } = default!;
    public Guid SupplierId { get; set; }
    public DateTime? SeenOn { get; set; }

}
