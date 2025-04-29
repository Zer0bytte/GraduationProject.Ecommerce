using Ecommerce.Application.Features.Orders.Commands.CreateOrder;

namespace Ecommerce.API.Endpoints.Orders;

public class CreateOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/orders/create-order", async (CreateOrderCommand command, ISender sender) =>
        {
            CreateOrderResult result = await sender.Send(command);


            return Results.Ok(ApiResponse<CreateOrderResult>.Success(result, "Order created successfully."));

        })
            .RequireAuthorization("User")
            .WithTags("Orders")
            .WithSummary("Create Order")
            .Produces<CreateOrderResult>();
    }
}
