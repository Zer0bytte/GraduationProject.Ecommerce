namespace Ecommerce.Application.Features.Cart.Commands.AddToCart;

public class AddToCartCommand : IRequest<AddToCartResult>
{
    public Guid ProductId { get; set; }
    public string CartId { get; set; } = default!;
}

