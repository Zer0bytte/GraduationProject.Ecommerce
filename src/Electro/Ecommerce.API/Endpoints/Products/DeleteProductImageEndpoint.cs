using Ecommerce.Application.Features.Products.Commands.DeleteProductImage;

namespace Ecommerce.API.Endpoints.Products;

public class DeleteProductImageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/products/images/{id}", async (Guid id, ISender sender) =>
        {
            DeleteProductImageResult result = await sender.Send(new DeleteProductImageCommand { ImageId = id });

            return Results.Ok(ApiResponse<DeleteProductImageResult>.Success(result));

        })
            .RequireAuthorization("Admin")
            .WithTags("Products")
            .WithSummary("Delete Product Image")
            .Produces<DeleteProductImageResult>();
    }
}
