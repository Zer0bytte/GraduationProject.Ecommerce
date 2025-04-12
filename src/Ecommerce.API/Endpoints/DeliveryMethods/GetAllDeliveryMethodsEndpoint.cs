using Ecommerce.Application.Features.DeliveryMethods.Queries.GetDeliveryMethods;

namespace Ecommerce.API.Endpoints.DeliveryMethods;

public class GetAllDeliveryMethodsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/dms/", async (ISender sender) =>
        {
            List<GetAllDeliveryMethodsResult> result = await sender.Send(new GetAllDeliveryMethodsQuery());

            return Results.Ok(ApiResponse<List<GetAllDeliveryMethodsResult>>.Success(result));

        })
            .WithTags("Delivery Methods")
            .WithSummary("Get Delivery Methods")
            .Produces<List<GetAllDeliveryMethodsResult>>();

    }
}
