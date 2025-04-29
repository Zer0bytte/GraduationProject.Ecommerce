
using Ecommerce.Application.Features.Authentication.Commands.LoginWithRefreshToken;
using Ecommerce.Application.Features.Authentication.Commands.RegisterUser;

namespace Ecommerce.API.Endpoints.Authentication;

public class LoginWithRefreshTokenEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/refresh-token", async (LoginWithRefreshTokenCommand request, ISender sender) =>
        {
            LoginWithRefreshTokenResult result = await sender.Send(request);
            return Results.Ok(ApiResponse<LoginWithRefreshTokenResult>.Success(result));
        })
            .WithTags("Authentication")
            .WithSummary("Refresh Token")
            .Produces<LoginWithRefreshTokenResult>();



    }
}
