using Ecommerce.Application.Features.Categories.Queries.GetCategories;

namespace Ecommerce.API.Endpoints.Categories;

public class GetCategoriesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/categories", async ([AsParameters] GetCategoriesQuery query, ISender sender) =>
        {
            PagedResult<GetCategoriesResult> result = await sender.Send(query);

            return Results.Ok(ApiResponse<PagedResult<GetCategoriesResult>>.Success(result));
        })
            .WithTags("Categories")
            .WithSummary("Get Categories")
            .Produces<PagedResult<GetCategoriesResult>>();

    }
}
