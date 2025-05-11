using Ecommerce.Application.Features.Orders.Commands.User.CancelOrderItem;
using Ecommerce.Application.Features.Orders.Commands.User.CreateOrder;

namespace Ecommerce.API.Endpoints.Orders.User;

public class CancelOrderItemEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/orders/cancel-item/{id}", async (Guid id, ISender sender) =>
        {
            CancelOrderItemResult result = await sender.Send(new CancelOrderItemCommand
            {
                OrderItemId = id
            });


            return Results.Ok(ApiResponse<CancelOrderItemResult>.Success(result, ArabicResponseMessages.Orders.Cancelled));

        })
            .RequireAuthorization("User")
            .WithTags("Orders")
            .WithSummary("Cancel Order Item")
            .Produces<ApiResponse<CancelOrderItemResult>>();
    }
}
