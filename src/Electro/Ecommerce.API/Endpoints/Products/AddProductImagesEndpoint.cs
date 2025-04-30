using Ecommerce.Application.Features.Products.Commands.AddProductImages;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Endpoints.Products;

public class AddProductImagesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/products/{id}/images", async ([FromForm] AddProductImagesCommand request, Guid id, ISender sender) =>
        {
            request.ProductId = id;

            List<AddProductImagesResult> result = await sender.Send(request);

            return Results.Ok(ApiResponse<List<AddProductImagesResult>>.Success(result, ArabicResponseMessages.Products.ImageAdded));

        })
            .RequireAuthorization("Supplier")
            .WithTags("Products")
            .WithSummary("Add Product Image")
            .DisableAntiforgery();

    }
}
