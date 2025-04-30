using Ecommerce.Application.Features.ProductOptions.Queries.NewFolder.GetProductOptions;
using Ecommerce.Application.Features.Products.Commands.AddProduct;

namespace Ecommerce.API.Endpoints.ProductOptions;

public class GetProductOptionsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/product-options", async (ISender sender) =>
        {
            List<GetProductOptionsResult> result = await sender.Send(new GetProductOptionsQuery());
            return Results.Ok(ApiResponse<List<GetProductOptionsResult>>.Success(result));

        })
            .WithTags("Product Options")
            .WithSummary("Get Product Options");
    }

}
