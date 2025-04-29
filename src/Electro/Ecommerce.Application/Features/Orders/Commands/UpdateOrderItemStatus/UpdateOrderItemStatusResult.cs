namespace Ecommerce.Application.Features.Orders.Commands.UpdateOrderItemStatus;

public record UpdateOrderItemStatusResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = default!;
} 