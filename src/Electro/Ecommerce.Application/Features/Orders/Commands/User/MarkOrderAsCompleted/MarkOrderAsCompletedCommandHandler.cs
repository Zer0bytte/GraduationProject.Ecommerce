namespace Ecommerce.Application.Features.Orders.Commands.User.MarkOrderAsCompleted;
public class MarkOrderAsCompletedCommandHandler(IApplicationDbContext context) : IRequestHandler<MarkOrderAsCompletedCommand, MarkOrderAsCompletedResult>
{
    public async Task<MarkOrderAsCompletedResult> Handle(MarkOrderAsCompletedCommand command, CancellationToken cancellationToken)
    {
        Order? order = await context.Orders.FindAsync(command.OrderId);
        if (order is null) throw new NotFoundException("هذا الطلب غير موجود");

        if (order.Status == OrderStatus.Completed || order.Status == OrderStatus.Cancelled)
            throw new ApplicationException("لا يمكنك تغيير حالة هذا الطلب");

        if (context.OrderItems.Any(oi => oi.OrderId == order.Id
        && new[] { OrderItemStatus.Pending, OrderItemStatus.Confirmed, OrderItemStatus.Shipped }
        .Contains(oi.Status)))
            throw new ApplicationException("لا يمكنك تغيير حالة هذا الطلب");

        order.Status = OrderStatus.Completed;

        await context.SaveChangesAsync(cancellationToken);

        return new MarkOrderAsCompletedResult
        {
            IsSuccess = true
        };
    }
}