using Ecommerce.Application.Features.Authentication.Commands.RegisterUser;
using Ecommerce.Application.Features.Authentication.Queries.Login;

namespace Ecommerce.API.Endpoints.Authentication;

public class LoginEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/login", async (LoginQuery request, ISender sender) =>
        {
            LoginResult result = await sender.Send(request);

            return Results.Ok(ApiResponse<LoginResult>.Success(result, ArabicResponseMessages.Authentication.LoginSuccess));


        }).WithTags("Authentication")
            .WithSummary("Login")
            .Produces<LoginResult>(); ;
    }

}
