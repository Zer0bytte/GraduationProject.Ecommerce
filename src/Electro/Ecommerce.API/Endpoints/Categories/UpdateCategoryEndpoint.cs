using Ecommerce.Application.Features.Categories.Commands.UpdateCategory;

namespace Ecommerce.API.Endpoints.Categories;

public class UpdateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/categories/{id}", async (UpdateCategoryCommand command, Guid id, ISender sender) =>
        {
            UpdateCategoryResult result = await sender.Send(command);
            return Results.Ok(ApiResponse<UpdateCategoryResult>.Success(result, "Category updated successfully."));

        })
            .RequireAuthorization("Admin")
            .WithTags("Categories")
            .WithSummary("Update Category")
            .Produces<UpdateCategoryResult>();
    }
}
