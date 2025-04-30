
using Ecommerce.Application.Features.Authentication.Commands.LoginWithGoogle;
using Ecommerce.Application.Features.Authentication.Commands.LoginWithRefreshToken;

namespace Ecommerce.API.Endpoints.Authentication;

public class LoginWithGoogleEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/google-login", async (LoginWithGoogleCommand request, ISender sender) =>
        {
            LoginWithGoogleResult result = await sender.Send(request);
            return Results.Ok(ApiResponse<LoginWithGoogleResult>.Success(result));
        })
            .WithTags("Authentication")
            .WithSummary("Login With Google")
            .Produces<LoginWithGoogleResult>();



    }
}
