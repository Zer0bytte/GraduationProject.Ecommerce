namespace Ecommerce.Application.Features.Cart.Commands.AddToCart;

public class AddToCartCommandHandler(IApplicationDbContext context, IDistributedCache cache, HostingConfig hostingConfig) : IRequestHandler<AddToCartCommand, AddToCartResult>
{
    const int FIRST_IMAGE_INDEX = 0;
    const int INITIAL_QUANTITY = 1;
    CartDto UpdateOrAddCartItem(CartDto cachedCart, CartItemDto newItem, Product product)
    {
        CartItemDto? existingItem = cachedCart.CartItems.FirstOrDefault(i => i.Id == newItem.Id);

        if (existingItem is not null)
        {
            existingItem.Quantity++;
            if(existingItem.Quantity > product.Stock)
                throw new InternalServerException($"Product: '{product.Title}' max value is: {product.Stock}");
        }
        else
        {
            cachedCart.CartItems.Add(newItem);
        }

        return cachedCart;
    }
    public async Task<AddToCartResult> Handle(AddToCartCommand command, CancellationToken cancellationToken)
    {
        Product product = await context.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Include(p => p.Images)
            .SingleOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken)
            ?? throw new NotFoundException("Product", command.ProductId);

        if (product.IsOutOfStock()) throw new InternalServerException($"Product: '{product.Title}' max stock value is: {product.Stock}");

        string imageUrl = hostingConfig.HostName + "/media/" + product.Images[FIRST_IMAGE_INDEX].NameOnServer;

        CartItemDto newCartItem = new CartItemDto
        {
            Id = product.Id,
            Category = product.Category.Name,
            ImageUrl = imageUrl,
            Price = product.Price,
            DiscountedPrice =  product.Price * (1 - product.Discount / 100m),
            DiscountPercentage = product.Discount,
            Quantity = INITIAL_QUANTITY,
            Title = product.Title
        };


        string? cachedCart = await cache.GetStringAsync(command.CartId);

        CartDto cart = new();

        if (string.IsNullOrEmpty(cachedCart))
        {
            cart = new CartDto
            {
                Id = command.CartId,
                CartItems = new List<CartItemDto> { newCartItem },
            };
        }
        else
        {
            cart = JsonConvert.DeserializeObject<CartDto>(cachedCart);
            UpdateOrAddCartItem(cart, newCartItem,product);
        }

        await cache.SetStringAsync(command.CartId, JsonConvert.SerializeObject(cart));
        return new AddToCartResult(true);
    }
}