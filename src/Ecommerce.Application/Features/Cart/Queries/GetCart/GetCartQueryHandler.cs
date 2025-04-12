namespace Ecommerce.Application.Features.Cart.Queries.GetCart;

public class GetCartQueryHandler(IDistributedCache cache) : IRequestHandler<GetCartQuery, GetCartResult>
{
    public async Task<GetCartResult> Handle(GetCartQuery query, CancellationToken cancellationToken)
    {
        string? cartJson = await cache.GetStringAsync(query.Id);
        if (string.IsNullOrEmpty(cartJson))
            throw new NotFoundException("Cart", query.Id);

        CartDto? cart = JsonConvert.DeserializeObject<CartDto>(cartJson);

        return new GetCartResult
        {
            Cart = cart!
        };
    }
}
