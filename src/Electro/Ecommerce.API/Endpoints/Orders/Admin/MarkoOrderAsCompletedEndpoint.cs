using Ecommerce.Application.Features.Orders.Commands.User.MarkOrderAsCompleted;
using Ecommerce.Application.Features.Orders.Queries.Admin.GetAllOrders;

namespace Ecommerce.API.Endpoints.Orders.Admin;

public class MarkoOrderAsCompletedEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/admin/orders/{id}/complete", async (Guid id, ISender sender) =>
        {

            MarkOrderAsCompletedResult result = await sender.Send(new MarkOrderAsCompletedCommand
            {
                OrderId = id
            });

            return Results.Ok(ApiResponse<MarkOrderAsCompletedResult>.Success(result));

        })
            .RequireAuthorization("Admin")
            .WithTags("Admin")
            .WithSummary("Complete Order")
            .Produces<ApiResponse<MarkOrderAsCompletedResult>>();
    }
}
