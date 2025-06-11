using Ecommerce.Application.Features.Notifications.Queries.GetSupplierNotifications;

namespace Ecommerce.API.Endpoints.Notifications;

public class GetSupplierNotificationsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/notifications", async (ISender sender) =>
        {
            List<GetSupplierNotificationsResult> result = await sender.Send(new GetSupplierNotificationsQuery());

            return Results.Ok(ApiResponse<List<GetSupplierNotificationsResult>>.Success(result));

        })
            .RequireAuthorization("Supplier")
            .WithTags("Notifications")
            .WithSummary("Get Notifications")
            .Produces<ApiResponse<List<GetSupplierNotificationsResult>>>();
    }
}
