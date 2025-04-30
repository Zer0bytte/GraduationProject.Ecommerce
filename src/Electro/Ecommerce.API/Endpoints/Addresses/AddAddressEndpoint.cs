using Ecommerce.Application.Features.Address.Commands.AddAddress;

namespace Ecommerce.API.Endpoints.Addresses;

public class AddAddressEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/addresses", async (AddAddressCommand request, ISender sender) =>
        {
            AddAddressResult result = await sender.Send(request);

            return Results.Ok(ApiResponse<AddAddressResult>.Success(result, ArabicResponseMessages.Addresses.Created));
        })
            .RequireAuthorization("User")
            .WithTags("Addresses")
            .WithSummary("Add Address")
            .Produces<AddAddressResult>();
    }
}
