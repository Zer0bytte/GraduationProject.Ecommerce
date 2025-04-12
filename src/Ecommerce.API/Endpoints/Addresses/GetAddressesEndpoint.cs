using Ecommerce.Application.Features.Address.Queries.GetAddresses;

namespace Ecommerce.API.Endpoints.Addresses;

public class GetAddressesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/addresses", async (ISender sender) =>
        {
            List<GetAddressesResult> result = await sender.Send(new GetAddressesQuery());
            return Results.Ok(ApiResponse<List<GetAddressesResult>>.Success(result));

        })
            .RequireAuthorization("User")
            .WithTags("Addresses")
            .WithSummary("Get Addresses")
            .Produces<List<GetAddressesResult>>();
    }
}
