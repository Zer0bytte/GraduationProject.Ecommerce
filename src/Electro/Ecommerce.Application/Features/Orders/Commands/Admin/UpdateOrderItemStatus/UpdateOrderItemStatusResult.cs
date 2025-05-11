namespace Ecommerce.Application.Features.Orders.Commands.Admin.UpdateOrderItemStatus;

public record UpdateOrderItemStatusResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = default!;
}