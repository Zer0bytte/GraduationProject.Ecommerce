using Ecommerce.Application.Features.Cart.Commands.AddToCart;

namespace Ecommerce.API.Endpoints.Cart;

public class AddToCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/cart/add-to-cart", async (AddToCartCommand request, ISender sender) =>
        {
            AddToCartResult result = await sender.Send(request);
            return Results.Ok(ApiResponse<AddToCartResult>.Success(result, "Product added to cart."));

        })
            .RequireRateLimiting("fixed")
            .WithTags("Cart")
            .WithSummary("Add To Cart")
            .Produces<AddToCartResult>();
    }
}
