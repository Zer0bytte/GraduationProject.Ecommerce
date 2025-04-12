using Ecommerce.Application.Features.Products.Queries.GetProductByd;

namespace Ecommerce.API.Endpoints.Products;

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app.MapGet("/api/products/{id}", async (Guid id, ISender sender) =>
        {
            GetProductByIdResult result = await sender.Send(new GetProductByIdQuery
            {
                Id = id
            });
            return Results.Ok(ApiResponse<GetProductByIdResult>.Success(result));

        })
            .WithTags("Products")
            .WithSummary("Get Product By Id")
            .Produces<GetProductByIdResult>();
    }
}
