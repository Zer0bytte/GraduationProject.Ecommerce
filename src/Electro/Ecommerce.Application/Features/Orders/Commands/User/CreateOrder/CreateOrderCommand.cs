namespace Ecommerce.Application.Features.Orders.Commands.User.CreateOrder;

public class CreateOrderCommand : IRequest<CreateOrderResult>
{
    public string CartId { get; set; } = default!;
    public string? CouponCode { get; set; }
    public Guid Address { get; set; } = default!;
    public PaymentMethod PaymentMethod { get; set; }

}
