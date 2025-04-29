namespace Ecommerce.Application.Features.Orders.Commands.UpdateOrderItemStatus;

public class UpdateOrderItemStatusCommandHandler(IApplicationDbContext context, ICurrentUser currentUser) 
    : IRequestHandler<UpdateOrderItemStatusCommand, UpdateOrderItemStatusResult>
{
    public async Task<UpdateOrderItemStatusResult> Handle(UpdateOrderItemStatusCommand command, CancellationToken cancellationToken)
    {
        // Get the order item with its supplier information
        OrderItem? orderItem = await context.OrderItems
            .Include(oi => oi.Supplier)
            .FirstOrDefaultAsync(oi => oi.Id == command.OrderItemId, cancellationToken);

        if (orderItem == null)
            throw new NotFoundException("OrderItem", command.OrderItemId);

        // Verify that the current user is the supplier of this order item
        if (orderItem.SupplierId != currentUser.SupplierId)
            throw new UnauthorizedAccessException("You are not authorized to update this order item");

        // Update the status
        orderItem.Status = command.Status;

        // If all items in the order are delivered, update the order status to delivered
        if (command.Status == OrderItemStatus.Delivered)
        {
            bool allItemsDelivered = await context.OrderItems
                .Where(oi => oi.OrderId == orderItem.OrderId)
                .AllAsync(oi => oi.Status == OrderItemStatus.Delivered, cancellationToken);

            if (allItemsDelivered)
            {
                Order order = await context.Orders.FindAsync(orderItem.OrderId);
                if (order != null)
                {
                    order.Status = OrderStatus.Delivered;
                }
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