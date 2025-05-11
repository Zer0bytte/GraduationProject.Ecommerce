using Ecommerce.Domain.Rules;
using Microsoft.Extensions.Hosting;
using TagLib.Ape;

namespace Ecommerce.Application.Features.Orders.Commands.Admin.UpdateOrderItemStatus;

public class UpdateOrderItemStatusCommandHandler(IApplicationDbContext context)
    : IRequestHandler<UpdateOrderItemStatusCommand, UpdateOrderItemStatusResult>
{
    public async Task<UpdateOrderItemStatusResult> Handle(UpdateOrderItemStatusCommand command, CancellationToken cancellationToken)
    {
        OrderItem? orderItem = await context.OrderItems
            .Include(oi => oi.Order)
            .FirstOrDefaultAsync(oi => oi.Id == command.OrderItemId, cancellationToken);

        if (orderItem == null)
            throw new NotFoundException("OrderItem", command.OrderItemId);

        if (!OrderItemStatusTransitions.IsValidTransition(orderItem.Status, command.Status))
            throw new InvalidOperationException($"áÇ íãßä ÊÛííÑ ÇáÍÇáÉ ãä {orderItem.Status} Åáì {command.Status}");

        orderItem.Status = command.Status;

        if (command.Status == OrderItemStatus.Delivered)
        {
            bool allItemsDelivered = await context.OrderItems
                .Where(oi => oi.OrderId == orderItem.OrderId && oi.Status != OrderItemStatus.Cancelled)
                .AllAsync(oi => oi.Status == OrderItemStatus.Delivered);

            if (allItemsDelivered)
            {
                orderItem.Order.Status = OrderStatus.Delivered;

            }
        }

        await context.SaveChangesAsync(cancellationToken);

        return new UpdateOrderItemStatusResult
        {
            IsSuccess = true,
            Message = "Order item status updated successfully"
        };
    }
}