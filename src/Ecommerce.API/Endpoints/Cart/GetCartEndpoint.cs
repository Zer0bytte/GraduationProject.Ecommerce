using Ecommerce.Application.Features.Cart.Queries.GetCart;

namespace Ecommerce.API.Endpoints.Cart;

public class GetCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/cart/{id}", async (string id, ISender sender) =>
        {
            GetCartResult result = await sender.Send(new GetCartQuery()
            {
                Id = id
            });

            return Results.Ok(ApiResponse<GetCartResult>.Success(result));

        })
            .WithTags("Cart")
            .WithSummary("Get Cart")
            .Produces<GetCartResult>();
    }
}
