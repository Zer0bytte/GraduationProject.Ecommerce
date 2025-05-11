using Ecommerce.Application.Features.Orders.Queries.Supplier.GetSupplierOrderItems;
using Ecommerce.Application.Features.Suppliers.Queries.GetSupplierById;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.API.Endpoints.Orders.Supplier;

public class GetSupplierOrderItemsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/suppliers/order-items", async ([AsParameters] GetSupplierOrderItemsQuery query, ISender sender) =>
        {
            PagedResult<GetSupplierOrderItemsResult> result = await sender.Send(query);
            return Results.Ok(ApiResponse<PagedResult<GetSupplierOrderItemsResult>>.Success(result));
        })
            .RequireAuthorization("Supplier")
            .WithTags("Suppliers")
            .WithSummary("Get Supplier Orders")
            .Produces<PagedResult<GetSupplierOrderItemsResult>>();

    }
}
