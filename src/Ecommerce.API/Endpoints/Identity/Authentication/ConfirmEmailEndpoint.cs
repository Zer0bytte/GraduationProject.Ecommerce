using Ecommerce.Application.Features.Identity.Authentication.Commands.ConfirmEmail;

namespace Ecommerce.API.Endpoints.Identity.Authentication;

public class ConfirmEmailEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/confirm-email", async (ConfirmEmailCommand command, ISender sender) =>
        {
            ConfirmEmailResult result = await sender.Send(command);
            return Results.Ok(result);
        })
            .WithTags("Authentication")
            .WithSummary("Confirm Email")
            .Produces<ConfirmEmailResult>();
    }
}
