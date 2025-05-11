using Ecommerce.Application.Features.Orders.Queries.GetOrderDetails;

namespace Ecommerce.API.Endpoints.Orders;

public class GetOrderDetailsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/orders/{id}/details", async (Guid id, ISender sender) =>
        {

            GetOrderDetailsResult result = await sender.Send(new GetOrderDetailsQuery() { OrderId = id });

            return Results.Ok(ApiResponse<GetOrderDetailsResult>.Success(result));

        })
            .RequireAuthorization()
            .WithTags("Orders")
            .WithSummary("Get Order Details")
            .Produces<GetOrderDetailsResult>();
    }
}
