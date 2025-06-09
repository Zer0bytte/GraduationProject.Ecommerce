using Ecommerce.Application.Features.Users.Queries.GetAdminUsers;

namespace Ecommerce.API.Endpoints.Users;

public class GetUsersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users", async ([AsParameters] GetUsersQuery query, ISender sender) =>
        {
            PagedResult<GetUsersResult> result = await sender.Send(query);
            return Results.Ok(ApiResponse<PagedResult<GetUsersResult>>.Success(result));

        })
            .RequireAuthorization("Admin")
            .WithTags("Users")
            .WithSummary("Get Users")
            .Produces< ApiResponse<PagedResult<GetUsersResult>>>();
    }
}
