namespace Ecommerce.Application.Features.Orders.Commands.User.CreateOrder;

public record CreateOrderResult
{
    public bool IsSuccess { get; set; }
    public string PaymentUrl { get; set; } = default!;
}
