using Ecommerce.Application.Features.Address.Commands.DeleteAddress;

namespace Ecommerce.API.Endpoints.Addresses;

public class DeleteAddressEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/addresses/{id}", async (Guid id, ISender sender) =>
        {
            DeleteAddressResult result = await sender.Send(new DeleteAddressCommand(id));

            return Results.Ok(ApiResponse<DeleteAddressResult>.Success(result, ArabicResponseMessages.Addresses.Deleted));
        })
            .RequireAuthorization("User")
            .WithTags("Addresses")
            .WithSummary("Delete Address")
            .Produces<DeleteAddressResult>();
    }

}
