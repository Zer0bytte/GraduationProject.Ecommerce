namespace Ecommerce.Application.Features.Cart.Commands.SetCartItemQuantity;

public class SetCartItemQuantityCommnadHandler(IApplicationDbContext context, IDistributedCache cache)
    : IRequestHandler<SetCartItemQuantityCommnad, SetCartItemQuantityResult>
{
    public async Task<SetCartItemQuantityResult> Handle(SetCartItemQuantityCommnad command, CancellationToken cancellationToken)
    {
        Product product = await context.Products
            .Include(p => p.Category)
            .Include(p => p.Images)
            .SingleOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken) ?? throw new NotFoundException("Product", command.ProductId);

        if (command.Quantity > product.Stock) throw new InternalServerException($"Product: '{product.Title}' max stock value is: {product.Stock}");

        string? cachedCart = await cache.GetStringAsync(command.CartId);

        if (string.IsNullOrEmpty(cachedCart))
        {
            throw new NotFoundException("Cart", command.CartId);
        }

        CartDto? cart = JsonConvert.DeserializeObject<CartDto>(cachedCart);

        CartItemDto? cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == command.ProductId);

        if (cartItem is null) throw new NotFoundException("Product", command.ProductId);

        if (command.Quantity == 0)
            cart.CartItems.Remove(cartItem);
        else
            cartItem.Quantity = command.Quantity;

        await cache.SetStringAsync(command.CartId, JsonConvert.SerializeObject(cart));

        return new SetCartItemQuantityResult
        {
            IsSuccess = true
        };


    }
}
