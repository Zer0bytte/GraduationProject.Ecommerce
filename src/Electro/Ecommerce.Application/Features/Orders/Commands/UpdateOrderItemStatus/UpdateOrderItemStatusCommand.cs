namespace Ecommerce.Application.Features.Orders.Commands.UpdateOrderItemStatus;

public class UpdateOrderItemStatusCommand : IRequest<UpdateOrderItemStatusResult>
{
    public Guid OrderItemId { get; set; }
    public OrderItemStatus Status { get; set; }
} 