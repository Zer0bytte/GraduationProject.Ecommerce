using Ecommerce.Application.Features.DeliveryMethods.Commands.AddDeliveryMethod;

namespace Ecommerce.API.Endpoints.DeliveryMethods;

public class AddDeliveryMethodEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/dms/", async (AddDeliveryMethodCommand request, ISender sender) =>
        {
            AddDeliveryMethodResult result = await sender.Send(request);
            return Results.Ok(ApiResponse<AddDeliveryMethodResult>.Success(result, "Delivery method created succesfully."));

        })
            .WithTags("Delivery Methods")
            .WithSummary("Add Delivery Method")
            .Produces<AddDeliveryMethodResult>();

    }
}
