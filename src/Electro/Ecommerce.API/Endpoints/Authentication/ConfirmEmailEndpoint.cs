using Ecommerce.Application.Features.Authentication.Commands.ChangePassword;
using Ecommerce.Application.Features.Authentication.Commands.ConfirmEmail;

namespace Ecommerce.API.Endpoints.Authentication;

public class ConfirmEmailEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/confirm-email", async (ConfirmEmailCommand command, ISender sender) =>
        {
            ConfirmEmailResult result = await sender.Send(command);
            return Results.Ok(ApiResponse<ConfirmEmailResult>.Success(result, ArabicResponseMessages.Authentication.EmailConfirmedSuccess));

        })
            .WithTags("Authentication")
            .WithSummary("Confirm Email")
            .Produces<ApiResponse<ConfirmEmailResult>>();
    }
}
