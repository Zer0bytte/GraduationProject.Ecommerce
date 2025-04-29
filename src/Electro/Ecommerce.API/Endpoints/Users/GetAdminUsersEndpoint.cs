using Ecommerce.Application.Features.Users.Queries.GetAdminUsers;

namespace Ecommerce.API.Endpoints.Users;

public class GetAdminUsersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users", async ([AsParameters] GetAdminUsersQuery query, ISender sender) =>
        {
            IEnumerable<GetAdminUsersResult> result = await sender.Send(query);
            return Results.Ok(ApiResponse<IEnumerable<GetAdminUsersResult>>.Success(result));

        })
            .RequireAuthorization("Admin")
            .WithTags("Users")
            .WithSummary("Get Users")
            .Produces<IEnumerable<GetAdminUsersResult>>();
    }
}
