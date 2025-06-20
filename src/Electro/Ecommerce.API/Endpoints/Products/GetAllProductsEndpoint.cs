using Ecommerce.API.ResponseStructure;
using Ecommerce.Application.Common.Persistance.Cursor;
using Ecommerce.Application.Features.Products.Queries.GetAllProducts;
using Ecommerce.Application.Features.Products.Queries.GetMostSoldProducts;
using System.Collections.Generic;

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


public class GetMostSoldProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products/most-sold", async (ISender sender) =>
        {
            List<GetMostSoldProductsResult> result = await sender.Send(new GetMostSoldProductsQuery());

            return Results.Ok(ApiResponse<List<GetMostSoldProductsResult>>.Success(result));

        })
            .WithTags("Products")
            .WithSummary("Get Most Sold")
            .Produces<ApiResponse<List<GetMostSoldProductsResult>>>();
    }
}
