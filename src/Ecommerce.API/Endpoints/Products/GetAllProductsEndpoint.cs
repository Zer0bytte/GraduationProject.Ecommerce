using Ecommerce.Application.Features.Products.Queries.GetAllProducts;

namespace Ecommerce.API.Endpoints.Products;

public class GetAllProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products", async ([AsParameters] GetAllProductsQuery query, ISender sender) =>
        {
            PagedResult<GetAllProductsResult> result = await sender.Send(query);

            return Results.Ok(ApiResponse<PagedResult<GetAllProductsResult>>.Success(result));

        })
            .WithTags("Products")
            .WithSummary("Get Products")
            .Produces<PagedResult<GetAllProductsResult>>();
    }
}
