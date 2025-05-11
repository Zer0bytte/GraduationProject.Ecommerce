using Ecommerce.Application.Features.Orders.Queries.Admin.GetAllOrders;

namespace Ecommerce.API.Endpoints.Orders.Admin;

public class GetAllOrdersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/admin/orders/", async ([AsParameters] GetAllOrdersQuery query, ISender sender) =>
        {

            PagedResult<GetAllOrdersResult> result = await sender.Send(query);

            return Results.Ok(ApiResponse<PagedResult<GetAllOrdersResult>>.Success(result));

        })
            .RequireAuthorization("Admin")
            .WithTags("Admin")
            .WithSummary("Orders")
            .Produces<PagedResult<GetAllOrdersResult>>();
    }
}
