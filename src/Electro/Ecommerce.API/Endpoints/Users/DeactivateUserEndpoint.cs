using Ecommerce.Application.Features.Users.Commands.DeactivateUser;

namespace Ecommerce.API.Endpoints.Users;

public class DeactivateUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/users/{id}/deactivate", async (Guid id, ISender sender) =>
        {
            DeactivateUserResult result = await sender.Send(new DeactivateUserCommand { Id = id });
            return Results.Ok(ApiResponse<DeactivateUserResult>.Success(result, ArabicResponseMessages.AdminUsers.Deactivated));

        })
            .RequireAuthorization("Admin")
            .WithTags("Users")
            .WithSummary("Deactivate User")
            .Produces<ApiResponse<DeactivateUserResult>>();
    }
}
