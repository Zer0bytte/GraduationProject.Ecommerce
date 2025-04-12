using Ecommerce.Application.Features.Categories.Commands.DeleteCategory;

namespace Ecommerce.API.Endpoints.Categories;

public class DeleteCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/categories/{id}", async (Guid id, ISender sender) =>
        {
            DeleteCategoryResult result = await sender.Send(new DeleteCategoryCommand() { CategoryId = id });
            return Results.Ok(ApiResponse<DeleteCategoryResult>.Success(result, "Category deleted successfully."));
        })
            .WithTags("Categories")
            .WithSummary("Delete Category").
            Produces<DeleteCategoryResult>();
    }
}
