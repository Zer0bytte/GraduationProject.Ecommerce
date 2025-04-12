using Ecommerce.Application.Features.Users.Commands.SetPassword;

namespace Ecommerce.API.Endpoints.Users;

public class SetPasswordEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/users/set-password", async (SetPasswordCommand command, ISender sender) =>
        {
            SetPasswordResult result = await sender.Send(command);
            return Results.Ok(ApiResponse<SetPasswordResult>.Success(result));
        })
            .WithTags("Users")
            .WithSummary("Set Password")
            .Produces<SetPasswordResult>();
    }
}
