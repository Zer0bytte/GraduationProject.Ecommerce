using Ecommerce.Application.Features.Users.Commands.RestoreUser;

namespace Ecommerce.API.Endpoints.Users;

public class ReActivateUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/users/{id}/reactivate", async (Guid id, ISender sender) =>
        {
            ReactivateUserResult result = await sender.Send(new ReactivateUserCommand { UserId = id });
            return Results.Ok(ApiResponse<ReactivateUserResult>.Success(result, ArabicResponseMessages.AdminUsers.Reactivated));

        })
            .RequireAuthorization("Admin")
            .WithTags("Users")
            .WithSummary("Reactivate User")
            .Produces<ApiResponse<ReactivateUserResult>>();
    }
}