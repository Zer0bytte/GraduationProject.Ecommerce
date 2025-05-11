namespace Ecommerce.Application.Features.Orders.Commands.Admin.UpdateOrderItemStatus;

public class UpdateOrderItemStatusCommand : IRequest<UpdateOrderItemStatusResult>
{
    public Guid OrderItemId { get; set; }
    public OrderItemStatus Status { get; set; }
}