using Ecommerce.Application.Features.Identity.Authentication.Queries.GetInfo;

namespace Ecommerce.API.Endpoints.Identity.Authentication;

public class GetInfoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/auth/me", async (ISender sender) =>
        {
            GetInfoResult result = await sender.Send(new GetInfoQuery());

            return Results.Ok(ApiResponse<GetInfoResult>.Success(result));


        })
            .RequireAuthorization()
            .WithTags("Authentication")
            .WithSummary("Info")
            .Produces<GetInfoResult>();
    }

}
