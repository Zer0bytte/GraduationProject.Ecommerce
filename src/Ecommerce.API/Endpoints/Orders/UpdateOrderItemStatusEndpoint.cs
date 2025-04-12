using Ecommerce.Application.Features.Orders.Commands.UpdateOrderItemStatus;

namespace Ecommerce.API.Endpoints.Orders;

public class UpdateOrderItemStatusEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/orders/order-items/{orderItemId}/status", async (Guid orderItemId, UpdateOrderItemStatusCommand command, ISender sender) =>
        {
            command.OrderItemId = orderItemId;
            UpdateOrderItemStatusResult result = await sender.Send(command);

            return Results.Ok(ApiResponse<UpdateOrderItemStatusResult>.Success(result, "Order item status updated successfully."));
        })
            .RequireAuthorization("Supplier")
            .WithTags("Orders")
            .WithSummary("Update Order Item Status")
            .Produces<UpdateOrderItemStatusResult>();
    }
} 