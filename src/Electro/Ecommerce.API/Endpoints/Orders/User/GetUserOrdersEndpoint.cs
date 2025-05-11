using Ecommerce.Application.Features.Orders.Queries.User.GetUserOrders;

namespace Ecommerce.API.Endpoints.Orders.User;

public class GetUserOrdersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/orders/", async ([AsParameters] GetUserOrdersQuery query, ISender sender) =>
        {
            PagedResult<GetUserOrdersResult> result = await sender.Send(query);

            return Results.Ok(ApiResponse<PagedResult<GetUserOrdersResult>>.Success(result));

        })
            .RequireAuthorization("User")
            .WithTags("Orders")
            .WithSummary("Get Orders")
            .Produces<PagedResult<GetUserOrdersResult>>(); ;
    }
}
