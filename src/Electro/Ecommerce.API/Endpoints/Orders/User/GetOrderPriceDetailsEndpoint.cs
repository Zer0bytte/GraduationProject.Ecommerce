using Ecommerce.Application.Features.Orders.Queries.GetOrderDetails;
using Ecommerce.Application.Features.Orders.Queries.User.GetOrderPriceDetails;

namespace Ecommerce.API.Endpoints.Orders.User;

public class GetOrderPriceDetailsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/cart/price-details", async ([AsParameters] GetOrderPriceDetailsQuery query, ISender sender) =>
        {

            GetOrderPriceDetailsResponse result = await sender.Send(query);

            return Results.Ok(ApiResponse<GetOrderPriceDetailsResponse>.Success(result));

        })
                   .RequireAuthorization("User")
                   .WithTags("Cart")
                   .WithSummary("Get Order Price Details")
                   .Produces<GetOrderPriceDetailsResponse>();
    }
}
