using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Rules;
using MassTransit;
using MassTransit.Transports;
using Microsoft.Extensions.Hosting;
using TagLib.Ape;

namespace Ecommerce.Application.Features.Orders.Commands.Admin.UpdateOrderItemStatus;

public class UpdateOrderItemStatusCommandHandler(IApplicationDbContext context, IBus bus)
    : IRequestHandler<UpdateOrderItemStatusCommand, UpdateOrderItemStatusResult>
{
    public async Task<UpdateOrderItemStatusResult> Handle(UpdateOrderItemStatusCommand command, CancellationToken cancellationToken)
    {
        OrderItem? orderItem = await context.OrderItems
            .Include(oi => oi.Order).ThenInclude(order => order.User)
            .FirstOrDefaultAsync(oi => oi.Id == command.OrderItemId, cancellationToken);

        if (orderItem == null)
            throw new NotFoundException("OrderItem", command.OrderItemId);

        if (!OrderItemStatusTransitions.IsValidTransition(orderItem.Status, command.Status))
            throw new InvalidOperationException($"áÇ íãßä ÊÛííÑ ÇáÍÇáÉ ãä {orderItem.Status} Åáì {command.Status}");

        orderItem.Status = command.Status;

        var statusEvent = new OrderItemStatusChangedEvent
        {
            CustomerName = orderItem.Order.User.FullName,
            Email = orderItem.Order.User.Email,
            ProductName = orderItem.ProductName,
        };
        switch (command.Status)
        {
            case OrderItemStatus.Confirmed:
                statusEvent.Status = OrderItemStatus.Confirmed;
                break;
            case OrderItemStatus.Shipped:
                statusEvent.Status = OrderItemStatus.Shipped;
                break;
            case OrderItemStatus.Delivered:
                statusEvent.Status = OrderItemStatus.Delivered;
                break;
            case OrderItemStatus.Cancelled:
                statusEvent.Status = OrderItemStatus.Cancelled;

                break;
            default:
                break;
        }


        if (command.Status == OrderItemStatus.Delivered)
        {
            if (!context.OrderItems.Any(oi => oi.OrderId == orderItem.OrderId
               && new[] { OrderItemStatus.Pending, OrderItemStatus.Confirmed, OrderItemStatus.Shipped }
               .Contains(oi.Status)))
            {
                orderItem.Order.Status = OrderStatus.Completed;
            }



            var supplierProfile = context.SupplierProfiles.Find(orderItem.SupplierId);
            if (supplierProfile != null)
            {
                supplierProfile.Balance += orderItem.Price * orderItem.Quantity;
                supplierProfile.BalanceTransactions.Add(new SupplierBalanceTransaction
                {
                    TransactionType = TransactionType.Revenue,
                    Amount = orderItem.Price * orderItem.Quantity,
                    Reason = $"Revenue from order: {orderItem.Id}, and item: {orderItem.ProductName}"
                });
            }
        }

        await context.SaveChangesAsync(cancellationToken);
        await bus.Publish(statusEvent);

        return new UpdateOrderItemStatusResult
        {
            IsSuccess = true,
            Message = "Order item status updated successfully"
        };
    }
}