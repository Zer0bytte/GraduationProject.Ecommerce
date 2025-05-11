using Ecommerce.Application.Features.Orders.Commands.Admin.UpdateOrderItemStatus;
using Ecommerce.Domain.Entities;

namespace Ecommerce.API.Endpoints.Orders.Admin;

public record UpdateOrderItemStatusRequest
{
    public OrderItemStatus Status { get; set; }
}
public class UpdateOrderItemStatusEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/orders/order-items/{orderItemId}/status", async (Guid orderItemId, UpdateOrderItemStatusRequest request, ISender sender) =>
        {
            UpdateOrderItemStatusResult result = await sender.Send(new UpdateOrderItemStatusCommand
            { OrderItemId = orderItemId, Status = request.Status });

            return Results.Ok(ApiResponse<UpdateOrderItemStatusResult>.Success(result, ArabicResponseMessages.Orders.StatusUpdated));
        })
            .RequireAuthorization("Admin")
            .WithTags("Orders")
            .WithSummary("Update Order Item Status")
            .Produces<ApiResponse<UpdateOrderItemStatusResult>>();
    }
}