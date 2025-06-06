namespace Ecommerce.Application.Features.Orders.Commands.User.MarkOrderAsCompleted;
public class MarkOrderAsCompletedCommandHandler(IApplicationDbContext context) : IRequestHandler<MarkOrderAsCompletedCommand, MarkOrderAsCompletedResult>
{
    public async Task<MarkOrderAsCompletedResult> Handle(MarkOrderAsCompletedCommand command, CancellationToken cancellationToken)
    {
        Order? order = await context.Orders.FindAsync(command.OrderId);
        if (order is null) throw new NotFoundException("هذا الطلب غير موجود");
        order.Status = OrderStatus.Completed;

        await context.SaveChangesAsync(cancellationToken);

        return new MarkOrderAsCompletedResult
        {
            IsSuccess = true
        };
    }
}