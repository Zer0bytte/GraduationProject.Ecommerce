namespace Ecommerce.Application.Features.Cart.Commands.AddToCart;

public class AddToCartCommandHandler(IApplicationDbContext context, IDistributedCache cache, IHttpContextAccessor httpContextAccessor) : IRequestHandler<AddToCartCommand, AddToCartResult>
{
    const int FIRST_IMAGE_INDEX = 0;
    const int INITIAL_QUANTITY = 1;
    CartDto UpdateOrAddCartItem(string cachedCart, CartItemDto newItem)
    {
        CartDto? cart = JsonConvert.DeserializeObject<CartDto>(cachedCart);
        CartItemDto? existingItem = cart.CartItems.FirstOrDefault(i => i.Id == newItem.Id);

        if (existingItem is not null)
        {
            existingItem.Quantity++;
        }
        else
        {
            cart.CartItems.Add(newItem);
        }

        return cart;
    }
    public async Task<AddToCartResult> Handle(AddToCartCommand command, CancellationToken cancellationToken)
    {
        Product product = await context.Products
            .Include(p => p.Category)
            .Include(p => p.Images)
            .SingleOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken)
            ?? throw new NotFoundException("Product", command.ProductId);

        if (product.IsOutOfStock()) throw new InternalServerException($"Product: '{product.Title}' max stock value is: {product.Stock}");

        HttpRequest httpRequest = httpContextAccessor.HttpContext.Request;
        string imageUrl = httpRequest.Scheme + "://" + httpRequest.Host + "/media/" + product.Images[FIRST_IMAGE_INDEX].NameOnServer;

        CartItemDto newCartItem = new CartItemDto
        {
            Id = product.Id,
            Category = product.Category.Name,
            ImageUrl = imageUrl,
            Price = product.Price,
            DiscountedPrice = product.Discount >= 1 ? product.Price * (1 - product.Discount / 100m) : 0,
            DiscountPercentage = product.Discount,
            Quantity = INITIAL_QUANTITY,
            Title = product.Title
        };

        string? cachedCart = await cache.GetStringAsync(command.CartId);
        CartDto cart = string.IsNullOrEmpty(cachedCart)
            ? new CartDto
            {
                Id = command.CartId,
                CartItems = new List<CartItemDto> { newCartItem }
            }
            : UpdateOrAddCartItem(cachedCart, newCartItem);

        await cache.SetStringAsync(command.CartId, JsonConvert.SerializeObject(cart));
        return new AddToCartResult(true);
    }
}

