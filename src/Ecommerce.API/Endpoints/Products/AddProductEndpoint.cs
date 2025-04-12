using Ecommerce.Application.Features.Products.Commands.AddProduct;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Endpoints.Products;

public class AddProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/products/", async ([FromForm] AddProductCommand request, ISender sender) =>
        {
            AddProductResult result = await sender.Send(request);
            return Results.Ok(ApiResponse<AddProductResult>.Success(result, "Product created succesfully."));

        })
            .WithTags("Products")
            .WithSummary("Create Product")
            .RequireAuthorization("Supplier")
            .DisableAntiforgery();
    }
}
