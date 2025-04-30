using Ecommerce.Application.Features.Cart.Commands.SetCartItemQuantity;

namespace Ecommerce.API.Endpoints.Cart;

public class SetCartItemQuantityEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/cart/update-cart", async (SetCartItemQuantityCommnad request, ISender sender) =>
        {
            SetCartItemQuantityResult result = await sender.Send(request);
            return Results.Ok(ApiResponse<SetCartItemQuantityResult>.Success(result, ArabicResponseMessages.Cart.UpdatedCart));

        })
            .WithTags("Cart")
            .WithSummary("Update Cart")
            .Produces<SetCartItemQuantityResult>();
    }
}
