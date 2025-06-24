
using Ecommerce.Application.Features.Authentication.Commands.ChangeProfileDetails;

namespace Ecommerce.API.Endpoints.Authentication;

public class ChangeProfileDetailsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/auth/change-details", async (ChangeProfileDetailsCommand command, ISender sender) =>
        {
            ChangeProfileDetailsResult result = await sender.Send(command);
            return Results.Ok(ApiResponse<ChangeProfileDetailsResult>.Success(result, ArabicResponseMessages.Authentication.ChangeProfileDetailsSuccess));

        })
           .RequireAuthorization("User")
           .WithTags("Authentication")
           .WithSummary("Change Details")
           .Produces<ApiResponse<ChangeProfileDetailsResult>>();
    }
}
