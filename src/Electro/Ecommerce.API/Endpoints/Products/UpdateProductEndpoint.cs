using Ecommerce.Application.Features.Products.Commands.UpdateProduct;

namespace Ecommerce.API.Endpoints.Products;

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/products/{id}", async (UpdateProductCommand request, Guid id, ISender sender) =>
        {
            request.Id = id;
            UpdateProductResult result = await sender.Send(request);
            return Results.Ok(ApiResponse<UpdateProductResult>.Success(result, "Product updated succesfully."));

        })
            .WithTags("Products")
            .WithSummary("Update Product")
            .RequireAuthorization("Supplier");
    }
}
