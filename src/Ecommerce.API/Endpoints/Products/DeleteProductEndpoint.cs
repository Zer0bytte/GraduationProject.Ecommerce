using Ecommerce.Application.Features.Products.Commands.DeleteProduct;

namespace Ecommerce.API.Endpoints.Products;

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/products/{id}", async (Guid id, ISender sender) =>
        {
            DeleteProductResult result = await sender.Send(new DeleteProductCommand() { ProductId = id });
            return Results.Ok(ApiResponse<DeleteProductResult>.Success(result, "Product deleted succesfully."));

        })
            .RequireAuthorization("Admin")
            .WithTags("Products")
            .WithSummary("Delete Product")
            .Produces<DeleteProductResult>();
    }
}
