using Ecommerce.Application.Features.Authentication.Commands.RegisterUser;

namespace Ecommerce.API.Endpoints.Authentication;

public class RegisterUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/register", async (RegisterUserCommand command, ISender sender) =>
        {
            RegisterUserResult result = await sender.Send(command);
            if (result.Errors.Count > 0)
                return Results.BadRequest(result);


            return Results.Ok(ApiResponse<RegisterUserResult>.Success(result, ArabicResponseMessages.Authentication.RegisterSuccess));

        })
            .WithTags("Authentication")
            .WithSummary("Register")
            .Produces<RegisterUserResult>();
    }
}
