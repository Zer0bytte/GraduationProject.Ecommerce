using Ecommerce.Application.Features.Users.Commands.DeleteAdmin;

namespace Ecommerce.API.Endpoints.Users;

public class DeleteAdminUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/users/{id}", async (Guid id, ISender sender) =>
        {
            DeleteAdminResult result = await sender.Send(new DeleteAdminCommand { Id = id });
            return Results.Ok(ApiResponse<DeleteAdminResult>.Success(result, "Admin deleted successfully."));

        })
            .RequireAuthorization("Admin")
            .WithTags("Users")
            .WithSummary("Delete Admin User")
            .Produces<DeleteAdminResult>();
    }
}
