using Ecommerce.Application.Features.Orders.Commands.User.CreateOrder;

namespace Ecommerce.API.Endpoints.Orders.User;

public class CreateOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/orders/create-order", async (CreateOrderCommand command, ISender sender) =>
        {
            CreateOrderResult result = await sender.Send(command);


            return Results.Ok(ApiResponse<CreateOrderResult>.Success(result, ArabicResponseMessages.Orders.Created));

        })
            .RequireAuthorization("User")
            .WithTags("Orders")
            .WithSummary("Create Order")
            .Produces<CreateOrderResult>();
    }
}
