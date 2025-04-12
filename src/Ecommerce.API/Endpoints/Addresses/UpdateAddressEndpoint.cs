using Ecommerce.Application.Features.Address.Commands.UpdateAddress;

namespace Ecommerce.API.Endpoints.Addresses;

public class UpdateAddressEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/addresses/{id}", async (UpdateAddressCommand request, Guid id, ISender sender) =>
        {
            request.Id = id;
            UpdateAddressResult result = await sender.Send(request);

            return Results.Ok(ApiResponse<UpdateAddressResult>.Success(result, "Address updated successfully."));
        })
            .RequireAuthorization("User")
            .WithTags("Addresses")
            .WithSummary("Update Address")
            .Produces<UpdateAddressResult>();
    }

}
