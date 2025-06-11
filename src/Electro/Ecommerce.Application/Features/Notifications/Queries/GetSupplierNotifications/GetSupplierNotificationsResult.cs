namespace Ecommerce.Application.Features.Notifications.Queries.GetSupplierNotifications;

public class GetSupplierNotificationsResult
{
    public object Payload { get; set; }
    public NotificationType NotificationType { get; set; }
    public DateTime Date { get; set; }
}