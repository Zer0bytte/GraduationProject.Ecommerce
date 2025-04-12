using Ecommerce.Application.Features.Users.Commands.AddAdminUser;

namespace Ecommerce.API.Endpoints.Users;

public class AddAdminUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/users", async (AddAdminCommand command, ISender sender) =>
        {
            AddAdminResult result = await sender.Send(command);

            return Results.Ok(ApiResponse<AddAdminResult>.Success(result, "Account created and the reset password url was sent."));

        })
            .WithTags("Users")
            .WithSummary("Add Admin User")
            .Produces<AddAdminResult>();
    }
}
