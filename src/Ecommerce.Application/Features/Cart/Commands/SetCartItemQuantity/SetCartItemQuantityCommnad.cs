namespace Ecommerce.Application.Features.Cart.Commands.SetCartItemQuantity;

public class SetCartItemQuantityCommnad : IRequest<SetCartItemQuantityResult>
{
    public string CartId { get; set; } = default!;
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
