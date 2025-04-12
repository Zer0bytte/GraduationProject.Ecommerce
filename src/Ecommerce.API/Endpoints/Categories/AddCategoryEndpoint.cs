using Ecommerce.Application.Features.Categories.Commands.AddCategory;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Endpoints.Categories;

public class AddCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/categories", async ([FromForm] AddCategoryCommand command, ISender sender) =>
        {
            AddCategoryResult result = await sender.Send(command);
            return Results.Ok(ApiResponse<AddCategoryResult>.Success(result, "Category created successfully."));

        })
            .DisableAntiforgery()
            .RequireAuthorization("Admin")
            .WithTags("Categories")
            .WithSummary("Add Category")
            .Produces<AddCategoryResult>();
    }
}
