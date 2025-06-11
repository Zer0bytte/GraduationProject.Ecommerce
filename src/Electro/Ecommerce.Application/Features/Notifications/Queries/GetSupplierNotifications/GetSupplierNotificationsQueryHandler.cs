namespace Ecommerce.Application.Features.Notifications.Queries.GetSupplierNotifications;
public class GetSupplierNotificationsQueryHandler(IApplicationDbContext context, ICurrentUser currentUser) 
    : IRequestHandler<GetSupplierNotificationsQuery, List<GetSupplierNotificationsResult>>
{
    public async Task<List<GetSupplierNotificationsResult>> Handle(GetSupplierNotificationsQuery request, CancellationToken cancellationToken)
    {
        var notifications = await context.SupplierNotifications
            .Where(s => s.SupplierId == currentUser.SupplierId && !s.SeenOn.HasValue)
            .Select(n => new GetSupplierNotificationsResult
            {
                NotificationType = n.NotificationType,
                Date = n.CreatedOn,
                Payload = MapPayloadToTargetType(n)
            }).OrderByDescending(n=>n.Date).Take(10).ToListAsync(cancellationToken);

        return notifications;
    }

    private static object MapPayloadToTargetType(SupplierNotification n)
    {
        switch (n.NotificationType)
        {
            case NotificationType.Order:
                return JsonConvert.DeserializeObject<OrderNotificationDto>(n.NotificationPayload);
            case NotificationType.Message:
                return null;
            default:
                return null;
        }
    }
}