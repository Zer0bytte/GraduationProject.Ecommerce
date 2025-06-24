using Ecommerce.Application.Features.Authentication.Commands.ValidateEmail;

namespace Ecommerce.API.Endpoints.Authentication;

public class ValidateRegisterdDataEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/validate-data", async (ValidateEmailAndPhoneCommand request, ISender sender) =>
        {
            ValidateEmailAndPhoneResult result = await sender.Send(request);

            return Results.Ok(ApiResponse<ValidateEmailAndPhoneResult>.Success(result));


        })
            .RequireRateLimiting("fixed")
            .WithTags("Authentication")
            .WithSummary("Validate Data")
            .Produces<ValidateEmailAndPhoneResult>();
    }
}
