using Ecommerce.Application.Common.Persistance.Cursor;
using Ecommerce.Application.Features.Products.Queries.GetAllProducts;

namespace Ecommerce.API.Endpoints.Products;

public class GetAllProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products", async ([AsParameters] GetAllProductsQuery query, ISender sender) =>
        {
            CursorResult<GetAllProductsResult> result = await sender.Send(query);

            return Results.Ok(ApiResponse<CursorResult<GetAllProductsResult>>.Success(result));

        })
            .WithTags("Products")
            .WithSummary("Get Products")
            .Produces<CursorResult<GetCurrentSupplierProductsResult>>();
    }
}
