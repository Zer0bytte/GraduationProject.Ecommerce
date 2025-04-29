namespace Ecommerce.Application.Features.Cart.Queries.GetCart;

public record GetCartQuery : IRequest<GetCartResult>
{
    public string Id { get; set; } = default!;
}

public record GetCartResult
{
    public CartDto Cart { get; set; } = default!;
}
