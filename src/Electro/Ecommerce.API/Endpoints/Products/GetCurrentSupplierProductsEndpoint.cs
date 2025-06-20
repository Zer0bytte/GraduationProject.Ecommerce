using Ecommerce.Application.Common.Persistance.Cursor;
using Ecommerce.Application.Features.Products.Queries.GetCurrentSupplierProducts;

namespace Ecommerce.API.Endpoints.Products;

public class GetCurrentSupplierProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/supplier-products", async ([AsParameters] GetCurrentSupplierProductsQuery query, ISender sender) =>
        {
            CursorResult<GetCurrentSupplierProductsResult> result = await sender.Send(query);

            return Results.Ok(ApiResponse<CursorResult<GetCurrentSupplierProductsResult>>.Success(result));

        })
            .RequireAuthorization("Supplier")
            .WithTags("Products")
            .WithSummary("Get Supplier Products")
            .Produces<CursorResult<GetCurrentSupplierProductsResult>>();
    }
}
